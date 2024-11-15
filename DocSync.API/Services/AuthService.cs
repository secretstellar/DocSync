using AutoMapper;
using DocSync.API.DTOs;
using DocSync.API.Helpers;
using DocSync.API.Models;
using DocSync.API.Repositories;
using DocSync.API.Repositories.Interfaces;
using DocSync.API.Services.Interfaces;

namespace DocSync.API.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserRepository repository, IMapper mapper, JwtHelper jwtHelper)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }
        public async Task<ResponseDto> LoginUser(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException("Input object must not be empty");

            if (String.IsNullOrEmpty(userDto?.Username))
                throw new ArgumentNullException("Username is required");


            if (String.IsNullOrEmpty(userDto?.Password))
                throw new ArgumentNullException("Password is required");

            var user = await _repository.GetByUserNameAndPassword(userDto.Username, userDto.Password);
            if (user == null)
                return new ResponseDto { IsSuccess = false, Message = "User login failed." };

            //var result = _mapper.Map<UserDetailsDto>(user);

            var token = _jwtHelper.CreateToken(user.Name, user.Role);

            return new ResponseDto
            {
                Data = new { token = token },
                IsSuccess = true,
                Message = "User logged in successfully"
            };
        }

        public async Task<ResponseDto> RegisterUser(UserDetailsDto userDetailsDto)
        {
            if (userDetailsDto == null)
                throw new ArgumentNullException("Input object must not be empty");

            if (String.IsNullOrEmpty(userDetailsDto?.Name))
                throw new ArgumentNullException("Name is required");

            if (String.IsNullOrEmpty(userDetailsDto?.UserName))
                throw new ArgumentNullException("Username is required");

            if (String.IsNullOrEmpty(userDetailsDto?.Password))
                throw new ArgumentNullException("Password is required");

            if (String.IsNullOrEmpty(userDetailsDto?.Email))
                throw new ArgumentNullException("Email is required");

            if (String.IsNullOrEmpty(userDetailsDto?.Role))
                throw new ArgumentNullException("Role is required");

            var userDetails = _mapper.Map<UserDetails>(userDetailsDto);
            userDetails.CreatedBy = userDetailsDto.UserName;
            userDetails.CreatedDate = DateTime.UtcNow;

            var result = await _repository.CreateAsync(userDetails);
            return new ResponseDto
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "User added successfully" : "Error adding User"
            };




        }
    }
}
