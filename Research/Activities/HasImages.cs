using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Research.Activities
{
    public partial class Classify
    {
        private void HasImages(IEnumerable<IWebElement> results)
        {
            IWebElement hasImages;
            if ((hasImages = results.FirstOrDefault(x => x.Text.StartsWith("Images for"))) != null)
            {
                // download some images and save them for later
                GoToAndWaitForElement(hasImages.GetAttribute("href") + "&tbs=isz:l", "resultStats");
            }
        }
    }
}
