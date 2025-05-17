using DataTrans.Application.Common.Handler;
using DataTrans.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<Result<IEnumerable<SubjectDto>>> GetAllAsync();
        Task<Result<SubjectDto>> GetByIdAsync(int id);
        Task<Result<string>> AddAsync(SubjectDto dto);
        Task<Result<string>> UpdateAsync(SubjectDto dto);
        Task<Result<string>> DeleteAsync(int id);
    }
}
