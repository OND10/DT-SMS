using DataTrans.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Interfaces
{
    public interface ITeacherSubjectRepository
    {
        Task AssignSubjectToTeacherAsync(int teacherId, int subjectId);
        Task RemoveSubjectFromTeacherAsync(int teacherId, int subjectId);
        Task<IEnumerable<Subject>> GetSubjectsByTeacherAsync(int teacherId);
    }
}
