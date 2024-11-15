using DocSync.API.DTOs;

namespace DocSync.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginUser(UserDto userDto);
        Task<ResponseDto> RegisterUser(UserDetailsDto userDto);
    }
}
