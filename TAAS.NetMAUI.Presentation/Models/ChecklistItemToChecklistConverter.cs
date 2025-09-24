using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Presentation.Models {
    public static class ChecklistDtoToChecklistItemConverter {
        public static List<ChecklistItem> Convert( IEnumerable<ChecklistDto> checklists ) {

            List<ChecklistItem> lstResult = new List<ChecklistItem>();
            foreach ( ChecklistDto checklist in checklists ) {
                lstResult.Add( new ChecklistItem() {
                    Id = checklist.Id,
                    Institution = checklist.AuditProgram?.Institution,
                    KeyRequirement = checklist.AuditProgram?.KeyRequirement,
                    SpecificFunction = checklist.AuditProgram?.SpecificFunction,
                    Comment = checklist.Comment,
                    SamplingRowNumber = checklist.SamplingRowNumber,
                    ChecklistTemplate = checklist.ChecklistTemplate,
                    Turkish = checklist.Turkish,
                    ReviewedAuditor = checklist.ReviewedAuditor,
                    ChecklistAuditors = checklist.ChecklistAuditors,
                } );
            }
            return lstResult;
        }
    }
}
