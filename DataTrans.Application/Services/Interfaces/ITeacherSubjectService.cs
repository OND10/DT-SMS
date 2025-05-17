using DataTrans.Application.Common.Handler;
using DataTrans.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Services.Interfaces
{
    public interface ITeacherSubjectService
    {
        Task<Result<string>> AssignSubjectAsync(TeacherSubjectDto request);
        Task<Result<string>> RemoveSubjectAsync(int teacherId, int subjectId);
        Task<Result<IEnumerable<SubjectDto>>> GetSubjectsByTeacherAsync(int teacherId);
    }
}
