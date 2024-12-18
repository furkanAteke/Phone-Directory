using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserDetail : BaseEntity
    {
        public int UserId { get; set; }
        public string? Numara2 { get; set; }
        public string? ImageUrl { get; set; }
        public User User { get; set; }
    }
}
