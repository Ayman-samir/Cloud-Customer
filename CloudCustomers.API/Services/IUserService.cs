using CloudCustomers.API.Model;

namespace CloudCustomers.API.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
    }
}
