using EndpointParametersSolution.Models;

namespace EndpointParametersSolution.Repositories
{
    public interface IUserRepository
    {
        User AddUser(User user);
        void DeleteUser(User user);
        IEnumerable<User> GetAllUsers();
        User GetUser(string email, string password);
        User UpdateUser(User user);
        User GetUserById(int id);
    }
}