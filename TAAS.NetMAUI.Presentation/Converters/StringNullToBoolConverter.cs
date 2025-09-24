using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Converters {
    public class StringNullToBoolConverter : IValueConverter {
        public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture )
            => value != null && !string.IsNullOrWhiteSpace( value.ToString() );

        public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
