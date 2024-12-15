using AutoMapper;
using EndpointParametersSolution.DTOs;
using EndpointParametersSolution.Models;
using EndpointParametersSolution.Repositories;

namespace EndpointParametersSolution.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<UserOutputDTO> GetAllUsers()
        {
            List<User> users = _userRepository.GetAllUsers().OrderBy(u => u.userName).ToList();
            List<UserOutputDTO> userOutputDTOs = _mapper.Map<List<UserOutputDTO>>(users);
            if (userOutputDTOs == null || userOutputDTOs.Count == 0)
            {
                throw new InvalidOperationException("No users found");
            }
            return userOutputDTOs;
        }

        public User GetUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Invalid email");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Invalid password");
            }
            var user = _userRepository.GetUser(email, password);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return user;
        }

        public UserOutputDTO AddUser(UserInputDTO userInputDTO)
        {
            var users = _userRepository.GetAllUsers().ToList();
            if (users.Any(u => u.userEmail == userInputDTO.userEmail))
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }
            var addedUser = _userRepository.AddUser(_mapper.Map<User>(userInputDTO));
            return _mapper.Map<UserOutputDTO>(addedUser);
        }

        public UserOutputDTO UpdateUser(UserInputDTO userInputDTO, int id)
        {
            var userToCheck = _userRepository.GetAllUsers().FirstOrDefault(u => u.userEmail.ToLower() == userInputDTO.userEmail.ToLower());
            var existingUser = _userRepository.GetUserById(id);
            if (userToCheck != null && userToCheck.userId != existingUser.userId)
            {
                throw new InvalidOperationException("A user with this email already exists");
            }

            var userToUpdate = _mapper.Map<User>(userInputDTO);
            userToUpdate.userId = existingUser.userId;

            var updatedUser = _userRepository.UpdateUser(userToUpdate);

            var updatedUserDTO = _mapper.Map<UserOutputDTO>(updatedUser);

            return updatedUserDTO;
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            _userRepository.DeleteUser(user);
        }
    }
}
