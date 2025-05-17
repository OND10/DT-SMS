using DataTrans.Application.Common.Handler;
using DataTrans.Application.Dtos;
using DataTrans.Application.Services.Interfaces;
using DataTrans.Domain.Entities;
using DataTrans.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Services.Implementation
{
    public class SubjectService: ISubjectService
    {
        private readonly ISubjectRepository _repo;

        public SubjectService(ISubjectRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<IEnumerable<SubjectDto>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var result = list.Select(s => new SubjectDto { Id = s.Id, Name = s.Name });
            return await Result<IEnumerable<SubjectDto>>.SuccessAsync(result, "All subjects retrieved", true);
        }

        public async Task<Result<SubjectDto>> GetByIdAsync(int id)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s == null)
                return await Result<SubjectDto>.FaildAsync(false, "Subject not found");

            return await Result<SubjectDto>.SuccessAsync(new SubjectDto { Id = s.Id, Name = s.Name }, "Subject found", true);
        }

        public async Task<Result<string>> AddAsync(SubjectDto dto)
        {
            var entity = new Subject { Name = dto.Name };
            await _repo.AddAsync(entity);
            return await Result<string>.SuccessAsync("Subject added successfully", true);
        }

        public async Task<Result<string>> UpdateAsync(SubjectDto dto)
        {
            var subject = await _repo.GetByIdAsync(dto.Id);
            if (subject == null)
                return await Result<string>.FaildAsync(false, "Subject not found");

            subject.Name = dto.Name;
            await _repo.UpdateAsync(subject);
            return await Result<string>.SuccessAsync("Subject updated successfully", true);
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return await Result<string>.SuccessAsync("Subject deleted successfully", true);
        }
    }
}
