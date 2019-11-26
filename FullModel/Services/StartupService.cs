using FullModel.Configuration;
using FullModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FullModel.Services
{
	public class StartupService : IHostedService
	{
		ILogger<StartupService> _logger;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly TestConfiguration _testConfiguration;

		public StartupService(
			ILogger<StartupService> logger,
			IServiceScopeFactory serviceScopeFactory,
			TestConfiguration testConfiguration)
		{
			_logger = logger;
			_serviceScopeFactory = serviceScopeFactory;
			_testConfiguration = testConfiguration;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceScopeFactory.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<FinancialAggregationContext>();
			await dbContext.Database.EnsureCreatedAsync();
			if (_testConfiguration.CreateTestData)
			{
				for (int i = 0; i < _testConfiguration.UserCount; i++)
				{
					await GenerateTestData(dbContext);
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		private async Task GenerateTestData(FinancialAggregationContext dbContext)
		{
			var userId = Guid.NewGuid();
			var now = DateTime.UtcNow;
			var random = new Random();

			var user = new User
			{
				CreatedDateTimeUTC = now,
				FiservUser = new FiservUser
				{
					CashEdgeId = "123456",
					CreatedDateTimeUTC = now,
					ModifiedDateTimeUTC = now,
					Password = Guid.NewGuid().ToString(),
					Username = Guid.NewGuid().ToString()
				},
				Id = userId,
				ModifiedDateTimeUTC = now,
				PartitionKey = userId,
			};

			_ = await dbContext.Users.AddAsync(user);
			await dbContext.SaveChangesAsync();

			var numberOfUserInstitutions = random.Next(1, 5);

			for (int i = 0; i < numberOfUserInstitutions; i++)
			{
				var userInstitutionId = Guid.NewGuid();
				var userInstitution = new UserInstitution
				{
					Active = true,
					CreatedDateTimeUTC = now,
					CredentialUpdateRequestDateTimeUTC = now,
					FinancialInstitutionLoginAccountId = 123,
					HarvestAddId = "ABC123",
					HasInvalidCredentials = true,
					HasMfa = false,
					Institution = new InstitutionSlim
					{
						Name = "Chase"
					},
					Id = userInstitutionId,
					LoginParameterHash = "hash",
					ModifiedDateTimeUTC = now,
					PartitionKey = userId,
					RunId = 1234567890,
					UserId = userId
				};

				var userInstitutionEntity = await dbContext.UserInstitutions.AddAsync(userInstitution);
				await dbContext.SaveChangesAsync();

				var numberOfFInancialAccounts = random.Next(1, 8);
				for (int j = 0; j < numberOfFInancialAccounts; j++)
				{
					var financialAccountId = Guid.NewGuid();
					var financialAccount1 = new FinancialAccount
					{
						AggregatorAccountId = "1",
						AggregatorLastUpdateAttempt = now,
						AggregatorLastUpdateSuccess = now,
						CreatedDateTimeUTC = now,
						ModifiedDateTimeUTC = now,
						Type = AcctType.CCA,
						ExtendedType = ExtAcctType.CCA,
						Owners = new List<Owner>
						{
							new Owner
							{
								CreatedDateTimeUTC = now,
								ModifiedDateTimeUTC = now,
								Name = "Lewis Hamilton"
							},
							new Owner
							{
								CreatedDateTimeUTC = now,
								ModifiedDateTimeUTC = now,
								Name = "Lando Norris"
							}
						},
						Id = financialAccountId,
						IsActive = true,
						Name = "Credit Card",
						Number = "123",
						PartitionKey = userInstitutionId
					};

					var financialAccountEntity = await dbContext.FinancialAccounts.AddAsync(financialAccount1);
					await dbContext.SaveChangesAsync();

					var balances = new List<Balance>
					{
						new Balance
						{
							Amount = new Amount
							{
								Currency = CurCodeType.USD,
								Value = 3.50m
							},
							CreatedDateTimeUTC = now,
							ModifiedDateTimeUTC = now,
							PartitionKey = financialAccountId,
							Type = BalType.Avail
						},
						new Balance
						{
							Amount = new Amount
							{
								Currency = CurCodeType.USD,
								Value = 3.50m
							},
							CreatedDateTimeUTC = now,
							ModifiedDateTimeUTC = now,
							PartitionKey = financialAccountId,
							Type = BalType.Current
						}
					};

					await dbContext.Balances.AddRangeAsync(balances);
					await dbContext.SaveChangesAsync();
					var currentBalances = balances.Select(b => new CurrentBalance
					{
						Amount = b.Amount,
						Type = b.Type
					})
					.ToList();
					userInstitutionEntity.Entity.FinancialAccounts.Add(new FinancialAccountSlim
					{
						AcountNumber = financialAccountEntity.Entity.Number,
						CreatedDateTimeUTC = now,
						CurrentBalances = currentBalances
					});
					await dbContext.SaveChangesAsync();

					var numberOfTransactions = random.Next(1, 20);
					for (int k = 0; k < numberOfTransactions; k++)
					{
						var transaction = new Transaction
						{
							AggregatorTransactionId = 1,
							Amount = new Amount
							{
								Currency = CurCodeType.USD,
								Value = 3.50m
							},
							Category = "Test",
							CreatedDateTimeUTC = now,
							FinancialAccountId = financialAccountId,
							Memo = "memo",
							ModifiedDateTimeUTC = now,
							PartitionKey = financialAccountId,
							PostedDate = now,
							Subcategory = "Test",
							Type = TransactionType.DEBIT
						};

						await dbContext.Transactions.AddAsync(transaction);
						await dbContext.SaveChangesAsync();

						var userTransaction = new UserTransaction
						{
							AggregatorTransactionId = transaction.AggregatorTransactionId,
							Amount = transaction.Amount,
							Category = transaction.Category,
							CreatedDateTimeUTC = transaction.CreatedDateTimeUTC,
							FinancialAccountId = financialAccountId,
							Id = transaction.Id,
							Memo = transaction.Memo,
							ModifiedDateTimeUTC = transaction.ModifiedDateTimeUTC,
							PartitionKey = userId,
							PostedDate = transaction.PostedDate,
							Subcategory = transaction.Subcategory,
							
							Type = transaction.Type,
						};

						await dbContext.UserTransactions.AddAsync(userTransaction);
						await dbContext.SaveChangesAsync();
					}
				}
			}
		}
	}
}
