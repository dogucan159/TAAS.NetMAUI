using TAAS.NetMAUI.Core.Entities;

namespace TAAS.NetMAUI.Infrastructure.Interfaces {
    public interface ITaasFileRepository {

        Task<List<TaasFile>> GetAllTaasFilesByChecklistId( long checklistId, bool trackChanges );
        Task<TaasFile?> GetOneTaasFileById( long id, bool trackChanges );
        Task<TaasFile?> GetOneTaasFileByApiId( long apiId, bool trackChanges );
        void CreateOneTaasFile( TaasFile taasFile );
        void UpdateOneTaasFile( TaasFile taasFile );
    }
}
