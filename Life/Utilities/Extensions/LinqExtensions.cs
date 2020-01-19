using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Life.Utilities.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static string AttributeValue(this XElement el, XName name)
        {
            if (el == null)
                return null;
            if (el.Attribute(name) == null)
                return null;
            return el.Attribute(name).Value;
        }

        public static IEnumerable<TSource> Replace<TSource>(this IEnumerable<TSource> source, TSource search,
                                                            TSource replace)
        {
            foreach (var item in source)
            {
                if (item.Equals(search))
                    yield return replace;
                else
                    yield return item;
            }
        }
    }
}
