using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAAS.NetMAUI.Core.DTOs;

namespace TAAS.NetMAUI.Presentation.Models {
    public static class ChecklistDetailDtoToDetailQuestionItemConverter {
        public static IEnumerable<DetailQuestionItem> Convert( IEnumerable<ChecklistDetailDto> dtos ) {
            List<DetailQuestionItem> lst = new List<DetailQuestionItem>();

            foreach ( var dto in dtos ) {


                var fileNames = dto.ChecklistDetailTaasFiles?
                    .Where( f => f.Deleted is not true )
                    .Select( f => f.TaasFile?.Name )
                    .Where( n => !string.IsNullOrWhiteSpace( n ) )
                    .ToList();


                lst.Add( new DetailQuestionItem() {
                    Id = dto.Id,
                    Comment = dto.ChecklistTemplateDetail.Comment,
                    CommentTr = dto.ChecklistTemplateDetail.CommentTr,
                    Answer = dto.Answer,
                    Explanation = dto.Explanation,
                    FileNames = ( fileNames is { Count: > 0 } ) ? string.Join( ", ", fileNames ) : "No files attached...",
                    HasExplanationFormatted = !string.IsNullOrEmpty( dto.ExplanationFormatted )
                } );
            }

            return lst;
        }
    }
}
