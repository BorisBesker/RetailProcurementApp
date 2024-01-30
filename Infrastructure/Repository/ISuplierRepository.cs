using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public interface ISuplierRepository : IRepository<Suplier>
    {
        public bool Exists(int id);
        public bool ExistsWithSameName(string name);
    }
}
