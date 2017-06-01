using System;
using System.Reflection;
using System.Collections;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Amezquita.ControlTiempos.Mobile.Infrastructure
{
    public static class TypeExtensions
    {
        public static object GetPropertyValue(this object item, string propertyName)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            Type itemType = item.GetType();
            PropertyInfo itemProperty = itemType.GetRuntimeProperty(propertyName);

            if (itemProperty == null)
                throw new InvalidOperationException($"The property: {propertyName} doesn't exists.");

            return itemProperty.GetValue(item);
        }
    }

    public class BindablePicker : Picker
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(BindablePicker), default(IList), BindingMode.Default, null, OnItemsSourceChanged);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(BindablePicker), default(object), BindingMode.OneWayToSource, null, OnSelectedItemChanged);
        public static readonly BindableProperty DataValueFieldProperty = BindableProperty.Create(nameof(DataValueField), typeof(string), typeof(BindablePicker), default(string));
        public static readonly BindableProperty DataTextFieldProperty = BindableProperty.Create(nameof(DataTextField), typeof(string), typeof(BindablePicker), default(string));

        public BindablePicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex != -1)
                SelectedItem = ItemsSource[SelectedIndex];
            else
                SelectedItem = null;
        }

        public IList ItemsSource
        {
            get { return GetValue(ItemsSourceProperty) as IList; }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty) as object; }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string DataValueField
        {
            get { return GetValue(DataValueFieldProperty) as string; }
            set { SetValue(DataValueFieldProperty, value); }
        }

        public string DataTextField
        {
            get { return GetValue(DataTextFieldProperty) as string; }
            set { SetValue(DataTextFieldProperty, value); }
        }

        public static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as BindablePicker;

            if (picker == null)
                return;

            picker.Items.Clear();

            if (newvalue == null)
                return;

            var list = (IList)newvalue;
            var dataValueField = bindable.GetValue(DataTextFieldProperty) as string;

            foreach (var item in list)
            {
                var itemValue = item.GetPropertyValue(dataValueField);
                if (itemValue != null)
                    picker.Items.Add(itemValue.ToString());
            }
        }

        public static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as BindablePicker;

            if (picker == null)
                return;

            var itemsSource = bindable.GetValue(ItemsSourceProperty) as IList;
            var dataValueField = bindable.GetValue(DataValueFieldProperty) as string;

            if (newvalue != null && itemsSource != null && itemsSource.Count > 0)
            {
                for (var index = 0; index < itemsSource.Count; index++)
                {
                    var item = itemsSource[index];
                    var itemValue = item.GetPropertyValue(dataValueField);
                    var newvalueValue = newvalue.GetPropertyValue(dataValueField);

                    if (itemValue.Equals(newvalueValue))
                    {
                        picker.SelectedIndex = index;
                        break;
                    }
                }
            }
            else
            {
                picker.SelectedIndex = -1;
            }
        }
    }
}