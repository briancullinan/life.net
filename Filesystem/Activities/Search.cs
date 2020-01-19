﻿using System.Drawing;
using System.Linq;
using Life;
using TimelineLibrary;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Files.Activities
{

    public class Search : Activity
    {
        public Search(Life.Activity activity)
            : base(activity)
        {
        }

        public override void Execute(object context, ActivityQueue queue, Trigger trigger)
        {
            var searchTerm = context.ToString();
            var data = new DatalayerDataContext();
            var query = data.Filesystems.Where(
                x =>
                x.Filepath.Contains(searchTerm) || x.Computer.Contains(searchTerm) ||
                x.Username.Contains(searchTerm));
            using (var command = data.GetCommand(query))
            {
                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    foreach (var result in data.Translate<Filesystem>(reader))
                    {
                        OnResulted(new TimelineEvent
                            {
                                Description = result.Filepath,
                                //EventImage = Icon.ExtractAssociatedIcon(result.Filepath),
                                StartDate = result.Time
                            }, queue);
                    }
                }
            }

        }
    }
}
