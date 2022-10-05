using System;
using System.Collections.Generic;

namespace TaskControl.Entities
{
    public partial class TaskRole
    {
        public TaskRole()
        {
            TaskMembers = new HashSet<TaskMember>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<TaskMember> TaskMembers { get; set; }
    }
}
