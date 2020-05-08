using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace AdonisUI.Demo.MarkupExtensions
{
    // Taken from https://stackoverflow.com/a/28647821
    /// <summary>
    /// Provides resource resolution by binding to a resource key.
    /// </summary>
    class ResourceBinding : MarkupExtension
    {
        public static object GetResourceBindingKeyHelper(DependencyObject obj)
        {
            return (object)obj.GetValue(ResourceBindingKeyHelperProperty);
        }

        public static void SetResourceBindingKeyHelper(DependencyObject obj, object value)
        {
            obj.SetValue(ResourceBindingKeyHelperProperty, value);
        }

        public static readonly DependencyProperty ResourceBindingKeyHelperProperty = 
            DependencyProperty.RegisterAttached("ResourceBindingKeyHelper", typeof(object), typeof(ResourceBinding), new PropertyMetadata(null, ResourceKeyChanged));

        static void ResourceKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as FrameworkElement;
            var newVal = e.NewValue as Tuple<object, DependencyProperty>;

            if (target == null || newVal == null)
                return;

            var dp = newVal.Item2;

            if (newVal.Item1 == null)
            {
                target.SetValue(dp, dp.GetMetadata(target).DefaultValue);
                return;
            }

            target.SetResourceReference(dp, newVal.Item1);
        }

        public ResourceBinding()
        {
        }

        public ResourceBinding(string path)
        {
            Path = new PropertyPath(path);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var provideValueTargetService = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (provideValueTargetService == null)
                return null;

            if (provideValueTargetService.TargetObject != null &&
                provideValueTargetService.TargetObject.GetType().FullName == "System.Windows.SharedDp")
                return this;

            var targetObject = provideValueTargetService.TargetObject as FrameworkElement;
            var targetProperty = provideValueTargetService.TargetProperty as DependencyProperty;
            if (targetObject == null || targetProperty == null)
                return null;

            var binding = new Binding
            {
                Path = Path,
                XPath = XPath,
                Mode = Mode,
                UpdateSourceTrigger = UpdateSourceTrigger,
                Converter = Converter,
                ConverterParameter = ConverterParameter,
                ConverterCulture = ConverterCulture
            };

            if (RelativeSource != null)
                binding.RelativeSource = RelativeSource;

            if (ElementName != null)
                binding.ElementName = ElementName;

            if (Source != null)
                binding.Source = Source;

            binding.FallbackValue = FallbackValue;

            var multiBinding = new MultiBinding
            {
                Converter = HelperConverter.Current,
                ConverterParameter = targetProperty
            };

            multiBinding.Bindings.Add(binding);

            multiBinding.NotifyOnSourceUpdated = true;

            targetObject.SetBinding(ResourceBindingKeyHelperProperty, multiBinding);

            return null;
        }


        /// <summary> The source path (for CLR bindings).</summary>
        public object Source
        {
            get;
            set;
        }

        /// <summary> The source path (for CLR bindings).</summary>
        public PropertyPath Path
        {
            get;
            set;
        }

        /// <summary> The XPath path (for XML bindings).</summary>
        [DefaultValue(null)]
        public string XPath
        {
            get;
            set;
        }

        /// <summary> Binding mode </summary>
        [DefaultValue(BindingMode.Default)]
        public BindingMode Mode
        {
            get;
            set;
        }

        /// <summary> Update type </summary>
        [DefaultValue(UpdateSourceTrigger.Default)]
        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get;
            set;
        }

        /// <summary> The Converter to apply </summary>
        [DefaultValue(null)]
        public IValueConverter Converter
        {
            get;
            set;
        }

        /// <summary>
        /// The parameter to pass to converter.
        /// </summary>
        /// <value></value>
        [DefaultValue(null)]
        public object ConverterParameter
        {
            get;
            set;
        }

        /// <summary> Culture in which to evaluate the converter </summary>
        [DefaultValue(null)]
        [TypeConverter(typeof(System.Windows.CultureInfoIetfLanguageTagConverter))]
        public CultureInfo ConverterCulture
        {
            get;
            set;
        }

        /// <summary>
        /// Description of the object to use as the source, relative to the target element.
        /// </summary>
        [DefaultValue(null)]
        public RelativeSource RelativeSource
        {
            get;
            set;
        }

        /// <summary> Name of the element to use as the source </summary>
        [DefaultValue(null)]
        public string ElementName
        {
            get;
            set;
        }

        /// <summary> Value to use when source cannot provide a value </summary>
        /// <remarks>
        ///     Initialized to DependencyProperty.UnsetValue; if FallbackValue is not set, BindingExpression
        ///     will return target property's default when Binding cannot get a real value.
        /// </remarks>
        public object FallbackValue
        {
            get;
            set;
        }

        private class HelperConverter : IMultiValueConverter
        {
            public static readonly HelperConverter Current = new HelperConverter();

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                return Tuple.Create(values[0], (DependencyProperty)parameter);
            }
            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
