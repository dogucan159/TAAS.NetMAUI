using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;
using TAAS.NetMAUI.Presentation.Models;

namespace TAAS.NetMAUI.Presentation.Converters {
    public class AuditNameToStringConverter : IValueConverter {
        public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            var auditAssignment = value as AuditAssignmentDto;

            if ( auditAssignment == null )
                return "";

            var components = new List<string>
            {
                auditAssignment.MainTask.Code,
                auditAssignment.TaskType.Code,
                auditAssignment.Task.Code
            };

            if ( auditAssignment.StrategyPlanPeriod != null )
                components.Add( auditAssignment.StrategyPlanPeriod.Code );

            if ( auditAssignment.AuditPeriod != null )
                components.Add( auditAssignment.AuditPeriod.Code );

            return string.Join( " - ", components );
        }

        public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
