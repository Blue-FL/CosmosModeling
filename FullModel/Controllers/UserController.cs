using FullModel.Data;
using FullModel.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FullModel.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserPartitionRepository _userPartitionRepository;

		public UserController(UserPartitionRepository userPartitionRepository)
		{
			_userPartitionRepository = userPartitionRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var stopwatch = Stopwatch.StartNew();
			var users = await _userPartitionRepository.GetItemsAsync<User>("SELECT * FROM c WHERE c.EntityType = 'User'");
			stopwatch.Stop();

			return Ok(new
			{
				ElapsedTime = stopwatch.ElapsedMilliseconds,
				Users = users.Item1,
			});
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUser(Guid userId)
		{
			var stopwatch = Stopwatch.StartNew();
			var user = await _userPartitionRepository.GetItemAsync<User>(userId.ToString(), userId);
			stopwatch.Stop();

			return Ok(new
			{
				ElapsedTime = stopwatch.ElapsedMilliseconds,
				User = user
			});
		}
	}
}