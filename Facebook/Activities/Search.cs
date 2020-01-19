﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Life;
using log4net;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Facebook.Activities
{
    public class Search : Activity
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Search));

        public Search(Life.Activity activity)
            : base(activity)
        {
        }

        public override void Execute(object context, ActivityQueue queue, Trigger trigger)
        {
            IQueryable query;
            var data = new DatalayerDataContext();
            var expression = context as LambdaExpression;
            if (expression != null)
            {
                try
                {
                    expression = expression.AsExpressable()
                        .Replace<Life.Controls.MessageRequest, FacebookMessage, string>(message => message.From, msg => msg.FacebookContact.Name)
                        .Replace<Life.Controls.MessageRequest, FacebookMessage, string>(message => message.Message, msg => msg.Message)
                        .Replace<Life.Controls.MessageRequest, FacebookMessage, DateTime>(message => message.Time, msg => msg.TimeCreated)
                        .AsExpression();
                    
                    var compiled = expression.Compile();
                    var result = compiled.DynamicInvoke(data.FacebookMessages);
                    query = result as IQueryable;
                    if (query == null)
                        return;
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Could not convert expression to known types: {0}", context), ex);
                    return;
                }
            }
            else
                throw new NotImplementedException();

            using (var command = data.GetCommand(query))
            {
                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var returnType = query.GetType().GetGenericArguments().First();
                    foreach (var result in data.Translate(returnType, reader))
                    {
                        var entity = result;
                        OnResulted(entity, queue);
                    }
                }
            }
        }
    }
}