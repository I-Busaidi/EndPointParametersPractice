using EndpointParametersSolution.DTOs;
using EndpointParametersSolution.Models;

namespace EndpointParametersSolution.Services
{
    public interface IUserService
    {
        UserOutputDTO AddUser(UserInputDTO userInputDTO);
        void DeleteUser(int id);
        List<UserOutputDTO> GetAllUsers();
        User GetUser(string email, string password);
        UserOutputDTO UpdateUser(UserInputDTO userInputDTO, int id);
    }
}