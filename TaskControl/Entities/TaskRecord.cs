using System;
using System.Collections.Generic;

namespace TaskControl.Entities
{
    public partial class TaskRecord
    {
        public Guid TaskId { get; set; }
        public string Action { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
