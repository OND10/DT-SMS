using DataTrans.Domain.Common.Exceptions;
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
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context; 
        public StudentRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student);
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

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();    
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var result = await _context.Students.FirstOrDefaultAsync(t => t.Id == id);
            
            if(result is null)
            {
                throw new ModelNullException($"{result}", "Model is null");
            }

            return result;
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
