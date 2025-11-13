using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Converters
{
    public class BoolToColorMultiConverter : IMultiValueConverter {
        public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture ) {
            var current = values[0];
            var selected = values[1];
            return Equals( current, selected ) ? Microsoft.Maui.Graphics.Color.FromArgb( "#919191" ) : Microsoft.Maui.Graphics.Color.FromArgb( "#FFFFFF" );
        }

        public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
