using DataTrans.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Entities
{
    public class TeacherSubject : ISoftDelete
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? DeletedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
