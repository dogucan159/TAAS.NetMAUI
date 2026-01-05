using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Shared;

namespace TAAS.NetMAUI.Presentation.Converters {
    public class ChecklistStatusToStatusLongConverter : IValueConverter {
        public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            var status = value != null ? value.ToString() : "";
            string statusLong = "";
            if ( status == ChecklistStatusConst.INITIAL ) {
                statusLong = "Initial";
            }
            else if ( status == ChecklistStatusConst.FINALIZED ) {
                statusLong = "Finalized";
            }
            else if ( status == ChecklistStatusConst.APPROVED ) {
                statusLong = "Approved";
            }
            return statusLong;

        }

        public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
