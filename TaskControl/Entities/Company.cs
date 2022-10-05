using System;
using System.Collections.Generic;

namespace TaskControl.Entities
{
    public partial class Company
    {
        public Company()
        {
            Users = new HashSet<User>();
            Workspaces = new HashSet<Workspace>();
        }

        public int Id { get; set; }
        public string BusinessName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Workspace> Workspaces { get; set; }
    }
}
