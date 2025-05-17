using DataTrans.Domain.Entities;
using DataTrans.Domain.Interfaces;
using DataTrans.Infustracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Infustracture.Implementation
{
    public class TeacherSubjectRepository : ITeacherSubjectRepository
    {
        private readonly ApplicationDbContext _context;
        public TeacherSubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AssignSubjectToTeacherAsync(int teacherId, int subjectId)
        {
            var exists = await _context.TeacherSubjects
                .AnyAsync(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId);

            if (!exists)
            {
                _context.TeacherSubjects.Add(new TeacherSubject
                {
                    TeacherId = teacherId,
                    SubjectId = subjectId
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveSubjectFromTeacherAsync(int teacherId, int subjectId)
        {
            var entity = await _context.TeacherSubjects
                .FirstOrDefaultAsync(ts => ts.TeacherId == teacherId && ts.SubjectId == subjectId);

            if (entity != null)
            {
                _context.TeacherSubjects.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId)
        {
            return await _context.TeacherSubjects
                .Where(ts => ts.TeacherId == teacherId)
                .Include(ts => ts.Subject)
                .Select(ts => ts.Subject)
                .ToListAsync();
        }
    }
}
