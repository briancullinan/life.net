﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Life;
using log4net;
using Activity = Life.Utilities.Activity;
using Trigger = Life.Utilities.Trigger;

namespace Messages.Activities
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
                        .Replace<Life.Controls.MessageRequest, TrillianMessage, string>(message => message.From, message => message.From)
                        .Replace<Life.Controls.MessageRequest, TrillianMessage, string>(message => message.Message, message => message.Message)
                        .Replace<Life.Controls.MessageRequest, TrillianMessage, DateTime>(message => message.Time, message => message.Id)
                        .AsExpression();

                    var compiled = expression.Compile();
                    var result = compiled.DynamicInvoke(data.TrillianMessages);
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
