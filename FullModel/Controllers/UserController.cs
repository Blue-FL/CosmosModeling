using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullModel.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullModel.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
		private readonly FinancialAggregationContext _dbContext;

		public UserController(FinancialAggregationContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _dbContext.Users.ToListAsync();
			return Ok(users);
		}
    }
}