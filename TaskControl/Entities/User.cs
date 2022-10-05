using System;
using System.Collections.Generic;

namespace TaskControl.Entities
{
    public partial class User
    {
        public User()
        {
            TaskMembers = new HashSet<TaskMember>();
        }

        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<TaskMember> TaskMembers { get; set; }
    }
}
