using DapperAndSignalR_Project.API.Repositories.LoginRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperAndSignalR_Project.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginsController : ControllerBase
	{
		private readonly ILoginRepository _loginRepository;

		public LoginsController(ILoginRepository loginRepository)
		{
			_loginRepository = loginRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Login(string userName, string password)
		{
			var user=await _loginRepository.AuthenticateUser(userName, password);			
			return Ok(user);
		}
	}
}
