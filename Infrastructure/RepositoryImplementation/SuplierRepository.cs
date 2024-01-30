using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repository
{
    public class SuplierRepository : Repository<Suplier>, ISuplierRepository
    {
        public SuplierRepository(RetailProcurementContext context)
            : base(context) { }

        public bool Exists(int id)
        {
            return Context.Set<Suplier>().Any(x => x.Id == id);
        }

        public bool ExistsWithSameName(string name)
        {
            return Context.Set<Suplier>().Any(x => x.Name.Trim().ToUpper() == name.Trim().Trim());
        }
    }
}
