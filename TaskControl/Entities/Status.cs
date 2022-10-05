using System;
using System.Collections.Generic;

namespace TaskControl.Entities
{
    public partial class Status
    {
        public Status()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
