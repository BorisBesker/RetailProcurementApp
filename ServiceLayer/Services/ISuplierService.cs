using Infrastructure.Models;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public interface ISuplierService
    {
        Suplier? GetSpecificSuplier(int id);
        IEnumerable<Suplier> GetSupliers();
        ServiceResponse<Suplier> CreateSuplier(Suplier item);
        ServiceResponse<Suplier> UpdateSuplier(int id, Suplier item);
        ServiceResponse<Suplier> DeleteSuplier(int id);
    }
}