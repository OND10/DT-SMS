using DataTrans.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Interfaces
{
    public interface IClassRepository
    {
        Task<IEnumerable<Classe>> GetAllAsync();
        Task<Classe> GetByIdAsync(int id);
        Task AddAsync(Classe classe);
        Task UpdateAsync(Classe classe);
        Task DeleteAsync(int id);
    }
}
