using System;
using System.Collections.Generic;

namespace TaskControl.Entities
{
    public partial class Workspace
    {
        public Workspace()
        {
            Tasks = new HashSet<Task>();
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
