using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;

namespace Life.Controls
{
    /// <summary>
    /// Interaction logic for Messages.xaml
    /// </summary>
    public partial class Messages
    {

        public Messages()
        {
            InitializeComponent();
            Expression<Func<IQueryable<MessageRequest>, IQueryable<FromResult>>> from =
                messages => messages.GroupBy(x => x.From, (s, enumerable) => new FromResult { From = s, Count = enumerable.Count() });
            Life.Triggers.Application.Search(From, from);
        }

        private void From_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var from = ((FromResult)From.SelectedItem).From;
            Expression<Func<IQueryable<MessageRequest>, IQueryable<MessageResult>>> results =
                messages => messages.Where(x => x.From.Contains(from)).Select(x => new MessageResult
                    {
                        From = x.From,
                        Message = x.Message,
                        Time = x.Time
                    });
            Life.Triggers.Application.Search(Results, results);
        }
    }
}
