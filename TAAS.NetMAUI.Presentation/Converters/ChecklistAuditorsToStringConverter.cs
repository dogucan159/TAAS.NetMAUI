using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Presentation.Converters {
    public class ChecklistAuditorsToStringConverter : IValueConverter {
        public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            var auditors = value as IEnumerable<ChecklistAuditorDto>;
            if ( auditors == null || auditors.Count() == 0 ) return "No Auditors assigned";

            return string.Join( ", ", auditors.Select( a => $"{a.Auditor.FirstName} {a.Auditor.LastName}" ) );
        }

        public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
