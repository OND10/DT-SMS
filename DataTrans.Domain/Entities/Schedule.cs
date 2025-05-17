using DataTrans.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataTrans.Domain.Entities
{
    public class Schedule : ISoftDelete
    {
        public int Id { get; set; }

        public int ClassId { get; set; }
        public Classe Class { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? DeletedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
