namespace TAAS.NetMAUI.Core.Entities {
    public class Auditor : BaseEntity {

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String IdentificationNumber { get; set; }
        public String Password { get; set; }
        public String? AccessToken { get; set; }

        public ICollection<AuditAssignmentAuditor> AuditAssignmentAuditors { get; set; }
        public ICollection<AuditAssignmentTemporaryAuditor> AuditAssignmentTemporaryAuditors { get; set; }
        public ICollection<ChecklistAuditor> ChecklistAuditors { get; set; }

    }
}
