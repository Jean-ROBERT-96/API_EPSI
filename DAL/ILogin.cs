using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ILogin
    {
        public Task<bool> Login(User user);
        public Task<bool> Register(User user);
    }
}
