using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExpressionSerialization;

namespace Life.Utilities
{
    [Serializable]
    public class StoredQuery : ISerializable
    {
        private Expression _expression;

        public Expression Expression
        {
            get { return _expression; }
        }

        public StoredQuery(SerializationInfo info, StreamingContext context)
        {
            var queryStr = (string) info.GetValue("expression", typeof (string));
            var query = XElement.Parse(queryStr);
            var resolver = new TypeResolver(App.Plugins, new [] { typeof(Expression) });
            var serializer = new ExpressionSerializer(resolver)
            {
                Converters = { new XmlConverter(null, resolver) }
            };

            _expression = serializer.Deserialize(query);
        }

        public StoredQuery(Expression expression)
        {
            _expression = expression;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var resolver = new TypeResolver(App.Plugins, new[] { typeof(Expression) });
            var serializer = new ExpressionSerializer(resolver)
                {
                    Converters = {new XmlConverter(null, resolver)}
                };

            var rootEl = serializer.Serialize(_expression);
            info.AddValue("expression", rootEl.ToString());
        }
    }

    public class XmlConverter : CustomExpressionXmlConverter
	{
		private DataContext dc;
		private TypeResolver resolver;
		public IQueryable QueryKind { get; private set; }

        public XmlConverter(DataContext dc, TypeResolver resolver)
		{
			this.dc = dc;
			this.resolver = resolver;
		}

		public override bool TryDeserialize(XElement expressionXml, out Expression e)
		{
			if (expressionXml.Name.LocalName == "Table")
			{
				var type = resolver.GetType(expressionXml.Attribute("Type").Value);
				var table = dc.GetTable(type);
				// REturning a random IQueryable of the right kind so that we can re-create the IQueryable
				// instance at the end of this method...
				QueryKind = table;
				e = Expression.Constant(table);
				return true;	
			}
			e = null;
			return false;
		}

		public override bool TrySerialize(Expression expression, out XElement x)
		{
			if (typeof(IQueryService).IsAssignableFrom(expression.Type))
			{
				x = new XElement("Table",
					new XAttribute("Type", expression.Type.GetGenericArguments()[0].FullName));
				return true;
			}
			x = null;
			return false;
		}
	}
}
