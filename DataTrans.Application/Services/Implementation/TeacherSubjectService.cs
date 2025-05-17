using DataTrans.Application.Common.Handler;
using DataTrans.Application.Dtos;
using DataTrans.Application.Services.Interfaces;
using DataTrans.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Services.Implementation
{
    public class TeacherSubjectService : ITeacherSubjectService
    {
        private readonly ITeacherSubjectRepository _repo;

        public TeacherSubjectService(ITeacherSubjectRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<string>> AssignSubjectAsync(TeacherSubjectDto request)
        {
            await _repo.AssignSubjectToTeacherAsync(request.TeacherId, request.SubjectId);
            return await Result<string>.SuccessAsync("Subject assigned to teacher", true);
        }

        public async Task<Result<string>> RemoveSubjectAsync(int teacherId, int subjectId)
        {
            await _repo.RemoveSubjectFromTeacherAsync(teacherId, subjectId);
            return await Result<string>.SuccessAsync("Subject removed from teacher", true);
        }

        public async Task<Result<IEnumerable<SubjectDto>>> GetSubjectsByTeacherAsync(int teacherId)
        {
            var subjects = await _repo.GetSubjectsByTeacherAsync(teacherId);
            var result = subjects.Select(s => new SubjectDto { Id = s.Id, Name = s.Name});
            return await Result<IEnumerable<SubjectDto>>.SuccessAsync(result, "Subjects for teacher retrieved", true);
        }
    }
}
