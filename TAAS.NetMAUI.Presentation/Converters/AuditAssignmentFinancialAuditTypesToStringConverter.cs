using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Presentation.Converters {
    public class AuditAssignmentFinancialAuditTypesToStringConverter : IValueConverter {
        public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            var auditTypes = value as IEnumerable<AuditAssignmentFinancialAuditTypeDto>;
            if ( auditTypes == null || auditTypes.Count() == 0 ) return "No Audit Types assigned";

            return string.Join( ", ", auditTypes.Select( a => $"{a.AuditType.Code}" ) );
        }

        public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
