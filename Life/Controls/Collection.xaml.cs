/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Life.Controls
{
    /// <summary>
    /// Interaction logic for CollectionEditor.xaml
    /// </summary>
    public partial class Collection
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof (IList),
            typeof (Collection),
            new PropertyMetadata(null));

        private readonly PropertyItem _item;

        public IList Value
        {
            get { return (IList) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private Type Type { get; set; }

        public Collection(Type type, PropertyItem item)
        {
            InitializeComponent();
            Type = type;
            _item = item;

            if (_item.Value != null && _item.PropertyType != Type && _item.PropertyType != null)
            {
                var converter = GetType()
                    .GetMethods(BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance)
                    .First(x => x.Name == "ConvertList");
                converter = converter.MakeGenericMethod(_item.PropertyType, Type);

                var result = converter.Invoke(this, new [] { _item.Value });
                Value = (IList)result;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var editor = new CollectionControlDialog(Type)
                {
                    ItemsSource = new ObservableCollection<object>(Value.OfType<object>())
                };
            SetBinding(ValueProperty, new Binding("ItemsSource")
                {
                    Source = editor,
                    Mode = BindingMode.TwoWay
                });
            /*_item.SetBinding(PropertyItem.ValueProperty, new Binding("Value")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                });*/
            editor.ShowDialog();

            // convert items to array of defined type
            if (Value != null && _item.PropertyType != Type && _item.PropertyType != null)
            {
                var converter = GetType()
                    .GetMethods(BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance)
                    .First(x => x.Name == "ConvertList");
                converter = converter.MakeGenericMethod(Type, _item.PropertyType);

                var result = converter.Invoke(this, new object[] {Value});
                _item.Value = result;
            }
        }

        private Type GetListType(Type listType)
        {
            var iListOfT = listType.GetInterfaces().FirstOrDefault(
                (i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IList<>));

            var newType = (iListOfT != null)
                              ? iListOfT.GetGenericArguments()[0]
                              : null;
            return newType;
        }

        private TTo ConvertList<TFrom, TTo>(IList values)
            where TFrom : IList
            where TTo : IList
        {
            IList list;

            if (!typeof (TTo).IsArray)
            {
                var constructor = typeof (TTo).GetConstructor(Type.EmptyTypes);
                list = (IList) constructor.Invoke(null);
            }
            else
                list = new ArrayList();

            var oldType = GetListType(typeof (TFrom));
            var newType = GetListType(typeof (TTo));

            if (newType == null)
                return default(TTo);

            var converter = TypeDescriptor.GetConverter(oldType);
            if (converter.GetType() == typeof (TypeConverter))
                converter = TypeDescriptor.GetConverter(newType);
            foreach (var item in values)
            {
                var converted = converter.ConvertFrom(item);
                list.Add(converted);
            }

            if (!typeof (TTo).IsArray)
                return (TTo) list;

            var cast = typeof (Enumerable)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .First(x => x.Name == "Cast");
            cast = cast.MakeGenericMethod(newType);
            var result = (IEnumerable)cast.Invoke(null, new object[] {list});
            var toArray = typeof (Enumerable)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .First(x => x.Name == "ToArray");
            toArray = toArray.MakeGenericMethod(newType);
            return (TTo) toArray.Invoke(null, new object[] {result});
        }
    }
}
