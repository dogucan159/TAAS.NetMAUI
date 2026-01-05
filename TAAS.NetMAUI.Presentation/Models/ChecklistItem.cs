using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Presentation.Models
{
    public class ChecklistItem : INotifyPropertyChanged {
        public long Id { get; set; }
        public InstitutionDto? Institution { get; set; }
        public KeyRequirementDto? KeyRequirement { get; set; }
        public SpecificFunctionDto? SpecificFunction { get; set; }
        public string? Comment { get; set; }
        public long? SamplingRowNumber { get; set; }
        public required ChecklistTemplateDto ChecklistTemplate { get; set; }
        public bool? Turkish { get; set; }
        public AuditorDto? ReviewedAuditor { get; set; }
        public string? Status { get; set; }

        public ICollection<ChecklistAuditorDto>? ChecklistAuditors { get; set; }

        private bool isSelected;

        public bool IsSelected {
            get => isSelected;
            set {
                if ( isSelected == value ) return;
                isSelected = value;
                OnPropertyChanged();

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged( [CallerMemberName] string propertyName = "" ) {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

    }
}
