﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WindowsNative;
using log4net;

namespace Facebook
{
    public static class Utilities
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Utilities));

        private static readonly string[] Permissions = new[]
            {
                "email",
                "user_about_me",
                "user_actions.books",
                "user_actions.music",
                "user_actions.news",
                "user_actions.video",
                "user_activities",
                "user_birthday",
                "user_education_history",
                "user_events",
                "user_games_activity",
                "user_groups",
                "user_hometown",
                "user_interests",
                "user_likes",
                "user_location",
                "user_notes",
                "user_photos",
                "user_questions",
                "user_relationship_details",
                "user_relationships",
                "user_religion_politics",
                "user_status",
                "user_subscriptions",
                "user_videos",
                "user_website",
                "user_work_history",
                "friends_about_me",
                "friends_actions.books",
                "friends_actions.music",
                "friends_actions.news",
                "friends_actions.video",
                "friends_activities",
                "friends_birthday",
                "friends_education_history",
                "friends_events",
                "friends_games_activity",
                "friends_groups",
                "friends_hometown",
                "friends_interests",
                "friends_likes",
                "friends_location",
                "friends_notes",
                "friends_photos",
                "friends_questions",
                "friends_relationship_details",
                "friends_relationships",
                "friends_religion_politics",
                "friends_status",
                "friends_subscriptions",
                "friends_videos",
                "friends_website",
                "friends_work_history",
                "export_stream",
                "friends_online_presence",
                "read_friendlists",
                "read_mailbox",
                "read_requests",
                "read_stream",
                "user_online_presence",
            };

        private static string _token;
        private static DateTime _expires;

        public static string GetAuthToken(string credentials, string profile)
        {
            var client = new WebClient();

            var graphData = client.DownloadData(
                "https://graph.facebook.com/search?q=Graph%20API%20Explorer&type=application&method=GET&format=json");

            var graph = Encoding.UTF8.GetString(graphData);

            var graphId = JObject.Parse(graph)["data"][0]["id"];

            var graphUrl = string.Format("https://graph.facebook.com/oauth/authorize?type=user_agent" +
                                 "&client_id={0}" +
                                 "&scope={1}" +
                                 "&redirect_uri=https%3A%2F%2Fdevelopers.facebook.com%2Ftools%2Fexplorer%2Fcallback",
                                 graphId,
                                 string.Join(",", Permissions));

            // log in to facebook
            var fp = new FirefoxProfile(profile, true);
            fp.SetPreference("javascript.enabled", false);
            var driver = new FirefoxDriver(fp);
            driver.Navigate().GoToUrl(graphUrl);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(x => x.FindElement(By.Id("loginform")));

            string username, password;
            if (!GetLogin(credentials, out username, out password))
                return null;

            driver.FindElementsById("email").First().SendKeys(username);
            driver.FindElementsById("pass").First().SendKeys(password);
            driver.FindElementsById("loginbutton").First().Submit();

            Thread.Sleep(500);

            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                var elem = wait.Until(x => x.FindElement(By.Name("__CONFIRM__")));
                new Actions(driver).MoveToElement(elem).Click(elem).Perform();

                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Log.Debug("No required screen.", ex);
            }

            /*try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                var elem = wait.Until(x => x.FindElement(By.Name("__CONFIRM__")));
                new Actions(driver).MoveToElement(elem).Click(elem).Perform();

                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Log.Debug("No permission screen.", ex);
            }
            */

            var addressToken = driver.Url;
            var query = HttpUtility.ParseQueryString(addressToken.Substring(addressToken.IndexOf('#') + 1));
            if (query["access_token"] == null)
            {
                addressToken = HttpUtility.HtmlDecode(driver.PageSource);
                query = HttpUtility.ParseQueryString(addressToken.Substring(addressToken.IndexOf('#') + 1));
            }

            driver.Close();

            return (_token = query["access_token"]);
        }

        private static bool GetLogin(string startsWith, out string username, out string password)
        {
            try
            {
                // get the login information out of the credential store
                int count;
                IntPtr pCredentials;
                var ret = AdvApi32.CredEnumerate(null, 0, out count, out pCredentials);
                if (!ret)
                    throw new Win32Exception();
                var credentials = new IntPtr[count];
                // convert pointer to array to array of pointers
                for (var n = 0; n < count; n++)
                {
                    credentials[n] = Marshal.ReadIntPtr(pCredentials,
                                                        n * Marshal.SizeOf(typeof(IntPtr)));
                }

                var login = credentials
                    // convert each pointer to a structure
                    .Select(ptr => (WinCred.Credential)Marshal.PtrToStructure(ptr, typeof(WinCred.Credential)))
                    // select the structure with the matching credential name
                    .FirstOrDefault(cred => cred.targetName.StartsWith(startsWith));

                // get the password
                var currentPass = Marshal.PtrToStringUni(login.credentialBlob, (int)login.credentialBlobSize / 2);
                var lastError = Marshal.GetLastWin32Error();
                if (lastError != 0)
                    throw new Win32Exception(lastError);

                username = login.userName;
                password = currentPass;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("There was an error finding the credentials for Facebook.", ex);
                username = null;
                password = null;
                return false;
            }
        }

        public static T TryGetValue<T>(this JToken json, object key)
        {
            if (json == null)
                return default(T);
            if (!json.HasValues)
                return default(T);
            return json.Value<T>(key);
        }
    }
}