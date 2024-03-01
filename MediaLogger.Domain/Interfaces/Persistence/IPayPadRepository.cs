using MediaLogger.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Persistence
{
    public interface IPayPadRepository
    {
        Task<IEnumerable<PayPad>> GetAllAsync();
        Task<string?> GetPaypadPasswordAsync(int id);
    }
}
