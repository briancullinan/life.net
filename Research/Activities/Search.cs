using System;
using System.Linq;
using Life;
using Life.Triggers;
using TimelineLibrary;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Research.Activities
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
            var query = data.Researches.Where(
                x =>
                x.Address.Contains(searchTerm) || x.InterestingHtml.Contains(searchTerm) ||
                x.Title.Contains(searchTerm));
            using (var command = data.GetCommand(query))
            {
                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    foreach (var result in data.Translate<Research>(reader))
                    {
                        OnResulted(new TimelineEvent
                            {
                                Description = result.Title,
                                StartDate = DateTime.UtcNow
                            }, queue);
                    }
                }
            }
        }
    }
}
