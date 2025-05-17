using DataTrans.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Entities
{
    public class Subject: ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? DeletedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
