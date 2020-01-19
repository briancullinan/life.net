using System.Activities.Presentation.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Life.Controls;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace Life.Utilities
{
    public class FileInfoEditor : TypeEditor<FileInfo>
    {
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = FileInfo.ValueProperty;
        }
    }

    public class DirectoryInfoEditor : TypeEditor<DirectoryInfo>
    {
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = DirectoryInfo.ValueProperty;
        }
    }

    public class CollectionEditor<T> : ITypeEditor
    {
        public FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            var editor = new Collection(typeof(T), propertyItem);

            return editor;
        }
    }

    public class ExpressionEditor : TypeEditor<ExpressionTextBox>
    {
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = ExpressionTextBox.ExpressionProperty;
        }
    }
}
