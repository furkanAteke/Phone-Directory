using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Numara { get; set; }
        public UserDetail UserDetail { get; set; }
    }
}
