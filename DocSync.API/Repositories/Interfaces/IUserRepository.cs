using DocSync.API.Models;

namespace DocSync.API.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task<UserDetails> GetByUserNameAndPassword(string userName, string password);
        Task<int> CreateAsync(UserDetails userDetails);

    }
}
