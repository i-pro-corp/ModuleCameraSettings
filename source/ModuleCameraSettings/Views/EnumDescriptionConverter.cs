using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace ModuleCameraSettings.Views
{
    /// <summary>
    /// Get description from DescriptionAttribute in enum definition.
    /// </summary>
    public class EnumDescriptionConverter : IValueConverter
    {
        public static readonly EnumDescriptionConverter Shared = new EnumDescriptionConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueText = value.ToString();
            if (value is Enum)
            {
                var fieldInfo = value.GetType().GetField(valueText);
                var attributes = fieldInfo?.GetCustomAttributes<DescriptionAttribute>(false);
                return attributes?.FirstOrDefault()?.Description ?? valueText;
            }
            return valueText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
