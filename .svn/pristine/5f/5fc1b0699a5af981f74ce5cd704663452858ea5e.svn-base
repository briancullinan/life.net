using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Life.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsConstructed(this object obj)
        {
            var trace = new StackTrace();
            var frames = trace.GetFrames();
            
            return frames
                .Select(t => t.GetMethod())
                .All(method => !method.IsConstructor || !obj.GetType().IsAssignableFrom(method.ReflectedType));
        }

        public static IEnumerable<Type> GetDeclaringTypes(this Type type)
        {
            var types = new List<Type>();
            while (type.DeclaringType != null)
            {
                types.Add(type.DeclaringType);
                type = type.DeclaringType;
            }
            return types;
        }

        public static IEnumerable<Type> GetDeclaringTypes(this MethodInfo method)
        {
            return new List<Type> {method.DeclaringType}.Union(method.DeclaringType.GetDeclaringTypes());
        }

        public static T FindParent<T>(this DependencyObject child)

            where T : DependencyObject
        {

            var parent = VisualTreeHelper.GetParent(child) as T;

            return parent ?? FindParent<T>(parent);
        }
    }
}
