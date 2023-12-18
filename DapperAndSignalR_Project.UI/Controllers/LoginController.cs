using DapperAndSignalR_Project.UI.DTOs.LoginDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DapperAndSignalR_Project.UI.Controllers
{
    public class LoginController : Controller
    {
		private readonly IHttpClientFactory _httpClientFactory;
		string url = "https://localhost:44355/api/Logins";

		public LoginController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}



		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Index(UserDto user)
		{
			
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync(url + $"?userName={user.Username}&password={user.Password}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UserDto>(jsonData);
					
				    HttpContext.Session.SetInt32("UserId", values.UserId);	
					return RedirectToAction("Index", "Home");

				}				
				else
				{
					return RedirectToAction("Index", "Login");
				}

		}
	}
}
