using EntitiesLibrary;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public interface IRepositoryManager
    {
        void Save();
        Task SaveAsync();
    }

    public class RepositoryManager : IRepositoryManager
    {
        private EntitiesContext _entitiesContext;
        public RepositoryManager(EntitiesContext entitiesContext)
        {
            _entitiesContext = entitiesContext;
        }
        public void Save() => _entitiesContext.SaveChanges();
        public async Task SaveAsync() => await _entitiesContext.SaveChangesAsync();
    }
}
