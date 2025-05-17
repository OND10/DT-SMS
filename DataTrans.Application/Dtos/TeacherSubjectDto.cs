using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Application.Dtos
{
    public class TeacherSubjectDto
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; } = string.Empty;
    }
}
