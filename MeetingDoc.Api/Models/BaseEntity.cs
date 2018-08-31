using System;

namespace MeetingDoc.Api.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}