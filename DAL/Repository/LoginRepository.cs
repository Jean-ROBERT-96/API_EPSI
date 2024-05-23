using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class LoginRepository : ILogin
    {
        private readonly DataContext _context;

        public LoginRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task<bool> Login(User user)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Mail == user.Mail && x.Password == user.Password);
            if (result == null)
                return false;

            return true;
        }

        public async Task<bool> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
