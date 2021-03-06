﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
    /// <summary>
    /// Interaction logic for CollectionEditor.xaml
    /// </summary>
    public partial class CollectionEditor : UserControl, ITypeEditor
    {
        private PropertyItem _item;

        public CollectionEditor()
        {
            InitializeComponent();
        }

        protected virtual void Button_Click(object sender, RoutedEventArgs e)
        {
            var editor = new CollectionControlDialog(_item.PropertyType);
            _item.SetBinding(PropertyItem.ValueProperty, new Binding("ItemsSource")
            {
                Source = editor,
                Mode = BindingMode.TwoWay
            });
            editor.ShowDialog();
        }

        public virtual FrameworkElement ResolveEditor(PropertyItem propertyItem)
        {
            _item = propertyItem;
            return this;
        }
    }
}
