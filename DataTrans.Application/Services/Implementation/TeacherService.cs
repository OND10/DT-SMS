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
    public class TeacherService  : ITeacherService
    {
        private readonly ITeacherRepository _repo;

        public TeacherService(ITeacherRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<IEnumerable<TeacherDto>>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            var result = list.Select(t => new TeacherDto { Id = t.Id, Name = t.FullName });
            return await Result<IEnumerable<TeacherDto>>.SuccessAsync(result, "All teachers retrieved", true);
        }

        public async Task<Result<TeacherDto>> GetByIdAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null)
                return await Result<TeacherDto>.FaildAsync(false, "Teacher not found");

            return await Result<TeacherDto>.SuccessAsync(new TeacherDto { Id = t.Id, Name = t.FullName }, "Teacher found", true);
        }

        public async Task<Result<string>> AddAsync(TeacherDto dto)
        {
            var entity = new Teacher { FullName = dto.Name };
            await _repo.AddAsync(entity);
            return await Result<string>.SuccessAsync("Teacher added successfully", true);
        }

        public async Task<Result<string>> UpdateAsync(TeacherDto dto)
        {
            var teacher = await _repo.GetByIdAsync(dto.Id);
            if (teacher == null)
                return await Result<string>.FaildAsync(false, "Teacher not found");

            teacher.FullName = dto.Name;
            await _repo.UpdateAsync(teacher);
            return await Result<string>.SuccessAsync("Teacher updated successfully", true);
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return await Result<string>.SuccessAsync("Teacher deleted successfully", true);
        }


    }
}
