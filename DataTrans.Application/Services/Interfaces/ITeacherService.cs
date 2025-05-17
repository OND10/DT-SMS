using DataTrans.Application.Common.Handler;
using DataTrans.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<Result<IEnumerable<TeacherDto>>> GetAllAsync();
        Task<Result<TeacherDto>> GetByIdAsync(int id);
        Task<Result<string>> AddAsync(TeacherDto dto);
        Task<Result<string>> UpdateAsync(TeacherDto dto);
        Task<Result<string>> DeleteAsync(int id);
    }
}
