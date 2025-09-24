using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Presentation.Models {
    public class ChecklistDetailGroupItem {
        public ChecklistDetailDto Master { get; set; }
        public ObservableCollection<DetailQuestionItem> Details { get; set; }
    }
}
