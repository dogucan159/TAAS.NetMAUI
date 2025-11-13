using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Converters {
    public class BoolToColorConverter : IValueConverter {
        public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            return value != null && ( bool )value ? Microsoft.Maui.Graphics.Color.FromArgb( "#919191" ) : Microsoft.Maui.Graphics.Color.FromArgb( "#FFFFFF" );
        }

        public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
