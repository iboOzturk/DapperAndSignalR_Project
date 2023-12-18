using Dapper;
using DapperAndSignalR_Project.API.DTOs.LoginDtos;
using DapperAndSignalR_Project.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace DapperAndSignalR_Project.API.Repositories.LoginRepositories
{
	public class LoginRepository : ILoginRepository
	{
		private readonly DapperContext _dapperContext;

		public LoginRepository(DapperContext dapperContext)
		{
			_dapperContext = dapperContext;
		}
		public static string ComputeHash(string input)
		{
			using (var sha256 = SHA256.Create())
			{
				var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
				var hashString= BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
				Console.WriteLine(hashString);
				return hashString;
			}
		}

		public async Task<UserDto> AuthenticateUser(string userName, string password)
		{	
			
			var query = "SELECT * FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash;";
			var parametres = new DynamicParameters();
			parametres.Add("@Username", userName);
			parametres.Add("@PasswordHash", ComputeHash(password));
			using (var connection=_dapperContext.CreateConnection())
			{				
				var user = await connection.QueryFirstOrDefaultAsync<UserDto>(query, parametres);
				user.PasswordHash = "";

				return user;				
				
			}	

		}
	}
}
