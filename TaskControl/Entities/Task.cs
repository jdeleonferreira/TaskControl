using System;
using System.Collections.Generic;
using TaskControl.Entities;

namespace TaskControl.Entities
{
    public partial class Task
    {
        public Task()
        {
            TaskMembers = new HashSet<TaskMember>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int StatusId { get; set; }
        public int WorkspaceId { get; set; }

        public virtual Status Status { get; set; } = null!;
        public virtual Workspace Workspace { get; set; } = null!;
        public virtual ICollection<TaskMember> TaskMembers { get; set; }
    }

}
