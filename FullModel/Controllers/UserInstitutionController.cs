using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FullModel.Data;
using FullModel.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullModel.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UserInstitutionController : ControllerBase
	{
		private readonly FinancialAggregationContext _dbContext;

		public UserInstitutionController(FinancialAggregationContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet("{user}/all")]
		public async Task<IActionResult> GetUserInstitutions([FromRoute] Guid user)
		{
			var stopwatch = Stopwatch.StartNew();
			var userInstitutions = await _dbContext.UserInstitutions.Where(ui => ui.PartitionKey == user).ToListAsync();
			stopwatch.Stop();

			return Ok(new EntityResponse<UserInstitution>
			{
				ElapsedTimeMs = stopwatch.ElapsedMilliseconds,
				Entities = userInstitutions
			});
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserInstitutionById([FromRoute] Guid id)
		{
			var stopwatch = Stopwatch.StartNew();
			var userInstitutions = await _dbContext.UserInstitutions.Where(ui => ui.Id == id).ToListAsync();
			stopwatch.Stop();

			return Ok(new EntityResponse<UserInstitution>
			{
				ElapsedTimeMs = stopwatch.ElapsedMilliseconds,
				Entities = userInstitutions
			});
		}
	}
}