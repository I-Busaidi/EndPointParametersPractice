using EndpointParametersSolution.Models;

namespace EndpointParametersSolution.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUser(string email, string password)
        {
            return _context.Users.Where(u => u.userEmail.ToLower() == email.ToLower() & u.userPassword == password).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
