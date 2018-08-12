using System;

namespace MeetingDoc.Api.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}