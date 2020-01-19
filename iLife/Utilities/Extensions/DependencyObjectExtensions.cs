using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace iLife.Utilities.Extensions
{
    /// <summary>
    /// Contains methods for use with dependency objects
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            //get parent item
            var parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            var parent = parentObject as T;

            return parent ??
                //use recursion to proceed with next level
                   TryFindParent<T>(parentObject);
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            var contentElement = child as ContentElement;
            if (contentElement != null)
            {
                var parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                var fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            var frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                var parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        /// <summary>
        /// Find the ancestor of the specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="that"></param>
        /// <returns></returns>
        public static T FindAncestor<T>(this DependencyObject that)
            where T : DependencyObject
        {
            for (var curItem = that.GetParentObject();
                 !ReferenceEquals(curItem, null);
                 curItem = curItem.GetParentObject())
            {
                if (curItem is T)
                    return curItem as T;
            }
            return null;
        }

        /// <summary>
        /// Find the child of the specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="that"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static T FindChild<T>(this DependencyObject that, string elementName = null)
            where T : FrameworkElement
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(that);

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(that, i);
                var frameworkElement = child as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (string.IsNullOrEmpty(elementName) && frameworkElement is T)
                        return frameworkElement as T;

                    if (elementName == frameworkElement.Name && frameworkElement is T)
                        return frameworkElement as T;

                    if ((frameworkElement = frameworkElement.FindChild<T>(elementName)) != null)
                        return frameworkElement as T;
                }
            }

            return null;
        }

        /// <summary>
        /// List all the ancestors of an element.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject item)
        {
            if (ReferenceEquals(item, null)) yield break;
            for (var curItem = item.GetParentObject();
                 !ReferenceEquals(curItem, null);
                 curItem = curItem.GetParentObject())
            {
                yield return curItem;
            }
        }
    }
}