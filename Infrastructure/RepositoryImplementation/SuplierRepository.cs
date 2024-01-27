using Infrastructure.Data;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class SuplierRepository : Repository<Suplier>, ISuplierRepository
    {
        public SuplierRepository(RetailProcurementContext context)
            : base(context) { }
    }
}
