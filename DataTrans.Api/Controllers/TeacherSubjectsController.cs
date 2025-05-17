using DataTrans.Application.Dtos;
using DataTrans.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTrans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSubjectsController : ControllerBase
    {
        private readonly ITeacherSubjectService _teacherSubjectService;

        public TeacherSubjectsController(ITeacherSubjectService teacherSubjectService)
        {
            _teacherSubjectService = teacherSubjectService;
        }

        [HttpPost]
        public async Task<IActionResult> AssignTeacherToSubject([FromBody] TeacherSubjectDto dto)
        {
            var result = await _teacherSubjectService.AssignSubjectAsync(dto);
            return Ok(result);
        }

        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetSubjectsByTeacher(int teacherId)
        {
            var result = await _teacherSubjectService.GetSubjectsByTeacherAsync(teacherId);
            return Ok(result);
        }
    }
}
