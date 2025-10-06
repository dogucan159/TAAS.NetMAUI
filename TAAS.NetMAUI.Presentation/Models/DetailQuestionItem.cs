using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAAS.NetMAUI.Presentation.Models {
    public class DetailQuestionItem {
        public long Id { get; set; }
        public string Comment { get; set; }

        public string CommentTr { get; set; }

        public string Answer { get; set; }
        public string Explanation { get; set; }

        public string FileNames { get; set; }
        public bool HasExplanationFormatted { get; set; }

        public bool IsTouched { get; set; } = false;
    }
}
