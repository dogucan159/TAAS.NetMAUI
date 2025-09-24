using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Presentation.Converters {
    public class StrategyPlanPeriodToStringConverter : IValueConverter {
        public object? Convert( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            var strategyPlanPeriod = value as StrategyPlanPeriodDto;
            if ( strategyPlanPeriod != null ) {
                return $"{strategyPlanPeriod.Code}".Trim();
            }
            return string.Empty;
        }

        public object? ConvertBack( object? value, Type targetType, object? parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
