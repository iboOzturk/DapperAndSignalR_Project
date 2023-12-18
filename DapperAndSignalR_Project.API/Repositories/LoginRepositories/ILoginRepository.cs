using DapperAndSignalR_Project.API.DTOs.LoginDtos;

namespace DapperAndSignalR_Project.API.Repositories.LoginRepositories
{
	public interface ILoginRepository
	{
		Task<UserDto> AuthenticateUser(string userName, string password);
	}
}
