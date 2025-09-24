using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Models {
    public class TaasFileItem : INotifyPropertyChanged {
        public long Id { get; set; }
        public required string Name { get; set; }
        public String Size { get; set; }
        public required string Extension { get; set; }

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
