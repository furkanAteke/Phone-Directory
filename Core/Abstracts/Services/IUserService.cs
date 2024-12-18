using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstracts.Services
{
    public interface IUserService<User> : IBaseService<User>
    {
        Task<User> Detail(int id);
    }
}
