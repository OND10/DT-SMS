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
    internal class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;
        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            // Here you can choose to return as a related object. 
            // You can achieve that using Include however if you want related objects 
            // for readonly you can add AsNoTracking as implemented in code.
            return await _context.Teachers.Include(t => t.TeacherSubjects).AsNoTracking().ToListAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _context.Teachers
                .Include(t => t.TeacherSubjects)
                .ThenInclude(ts => ts.Subject)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }
    }
}
