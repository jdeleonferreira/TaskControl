using System;
using System.Collections.Generic;

namespace TaskControl.Entities
{
    public partial class TaskMember
    {
        public Guid TaskId { get; set; }
        public int UserId { get; set; }
        public int TaskRoleId { get; set; }

        public virtual Task Task { get; set; } = null!;
        public virtual TaskRole TaskRole { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
