using DataTrans.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Interfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetAllAsync();
        Task<Grade> GetByIdAsync(int id);
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(int id);
    }
}
