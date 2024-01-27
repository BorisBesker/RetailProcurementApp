using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntitiy> where TEntitiy : class
    {
        TEntitiy Get(object id);
        IEnumerable<TEntitiy> GetAll();
        void Add(TEntitiy entity);
        void Update(TEntitiy entity);
        void Remove(TEntitiy entity);
    }
}
