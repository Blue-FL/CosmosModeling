using FullModel.Configuration;
using FullModel.Data;
using FullModel.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FullModel.Services
{
	public class StartupService : IHostedService
	{
		ILogger<StartupService> _logger;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly TestConfiguration _testConfiguration;
		private readonly UserPartitionRepository _userPartitionRepository;
		private readonly UserInstitutionPartitionRepository _userInstitutionPartitionRepository;
		private readonly FinancialAccountPartitionRepository _financialAccountPartitionRepository;

		public StartupService(
			ILogger<StartupService> logger,
			IServiceScopeFactory serviceScopeFactory,
			TestConfiguration testConfiguration,
			UserPartitionRepository userPartitionRepository,
			UserInstitutionPartitionRepository userInstitutionPartitionRepository,
			FinancialAccountPartitionRepository financialAccountPartitionRepository)
		{
			_logger = logger;
			_serviceScopeFactory = serviceScopeFactory;
			_testConfiguration = testConfiguration;
			_userPartitionRepository = userPartitionRepository;
			_userInstitutionPartitionRepository = userInstitutionPartitionRepository;
			_financialAccountPartitionRepository = financialAccountPartitionRepository;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			if (_testConfiguration.CreateTestData)
			{
				for (int i = 0; i < _testConfiguration.UserCount; i++)
				{
					await GenerateTestData();
				}
			}

			if (_testConfiguration.AddToExistingData)
			{
				await AddToExistingUserData();
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		private async Task GenerateTestData()
		{
			var overallStopwatch = Stopwatch.StartNew();
			var userId = Guid.NewGuid();
			var now = DateTime.UtcNow;
			var random = new Random();

			var userStopwatch = Stopwatch.StartNew();
			var user = new User
			{
				Id = userId.ToString(),
				CreatedDateTimeUTC = now,
				FiservUser = new FiservUser
				{
					CashEdgeId = "123456",
					CreatedDateTimeUTC = now,
					ModifiedDateTimeUTC = now,
					Password = Guid.NewGuid().ToString(),
					Username = Guid.NewGuid().ToString()
				},
				PartitionKey = userId.ToString(),
			};

			await _userPartitionRepository.AddItemAsync(user);
			userStopwatch.Stop();
			_logger.LogInformation("Added user in {userElapsed}ms", userStopwatch.ElapsedMilliseconds);

			
			var numberOfUserInstitutions = random.Next(1, 5);
			for (int i = 0; i < numberOfUserInstitutions; i++)
			{
				var userInstitutionStopwatch = Stopwatch.StartNew();
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
					Id = userInstitutionId.ToString(),
					LoginParameterHash = "hash",
					ModifiedDateTimeUTC = now,
					RunId = 1234567890,
					PartitionKey = userId.ToString()
				};

				await _userPartitionRepository.AddItemAsync(userInstitution);
				userInstitutionStopwatch.Stop();
				_logger.LogInformation("Added user institutions in {elapsed}ms", userInstitutionStopwatch.ElapsedMilliseconds);

				var numberOfFInancialAccounts = random.Next(1, 8);
				for (int j = 0; j < numberOfFInancialAccounts; j++)
				{
					var financialAccountStopwatch = Stopwatch.StartNew();
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
						Id = financialAccountId.ToString(),
						IsActive = true,
						Name = "Credit Card",
						Number = "123",
						PartitionKey = userInstitutionId.ToString()
					};

					await _userInstitutionPartitionRepository.AddItemAsync(financialAccount1);
					financialAccountStopwatch.Stop();
					_logger.LogInformation("Add financial account in {elapsed}ms", financialAccountStopwatch.ElapsedMilliseconds);

					var balanceStopwatch = Stopwatch.StartNew();
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
							PartitionKey = financialAccountId.ToString(),
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
							PartitionKey = financialAccountId.ToString(),
							Type = BalType.Current
						}
					};
					balanceStopwatch.Stop();

					await _financialAccountPartitionRepository.AddItemsAsync(balances).ContinueWith(act =>
					{
						_logger.LogInformation("Added 2 balances in {elapsed}ms", balanceStopwatch.ElapsedMilliseconds);
					});
					var currentBalances = balances.Select(b => new CurrentBalance
					{
						Amount = b.Amount,
						Type = b.Type
					})
					.ToList();

					userInstitution.FinancialAccounts.Add(new FinancialAccountSlim
					{
						AcountNumber = financialAccount1.Number,
						CreatedDateTimeUTC = now,
						CurrentBalances = currentBalances
					});

					await _userInstitutionPartitionRepository.UpdateItemAsync(userInstitution);

					var numberOfTransactions = random.Next(1, 8000);
					var transactions = new List<Transaction>();
					var transactionStopwatch = Stopwatch.StartNew();
					var userTransactions = new List<UserTransaction>();
					var userTransactionStopwatch = Stopwatch.StartNew();
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
							PartitionKey = financialAccountId.ToString(),
							Memo = "memo",
							ModifiedDateTimeUTC = now,
							PostedDate = now,
							Subcategory = "Test",
							Type = TransactionType.DEBIT
						};

						var doc = JsonSerializer.Serialize(transaction);

						transactions.Add(transaction);

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
							PartitionKey = userId.ToString(),
							PostedDate = transaction.PostedDate,
							Subcategory = transaction.Subcategory,

							Type = transaction.Type,
						};

						userTransactions.Add(userTransaction);
					}

					await _userPartitionRepository.AddItemsAsync(userTransactions).ContinueWith(act =>
					{
						userTransactionStopwatch.Stop();
						_logger.LogInformation("Added {count} user transactions in {elapsed}ms", numberOfTransactions, userTransactionStopwatch.ElapsedMilliseconds);
					});

					await _financialAccountPartitionRepository.AddItemsAsync(transactions).ContinueWith(act =>
					{
						userTransactionStopwatch.Stop();
						_logger.LogInformation("Add {count} transactions in {elapsed}ms", numberOfTransactions, transactionStopwatch.ElapsedMilliseconds);
					});
				}
			}
			overallStopwatch.Stop();

			_logger.LogInformation("Completed add user {userId} in {overall}ms", userId, overallStopwatch.ElapsedMilliseconds);
		}

		private async Task AddToExistingUserData()
		{
			//var userId = (await dbContext.Users.FirstAsync()).PartitionKey;
			//var random = new Random();
			//var now = DateTime.Now;
			//var userInstitution = await dbContext.UserInstitutions.FirstOrDefaultAsync(fa => fa.PartitionKey == userId);

			//var numberOfTransactions = random.Next(1, 15);
			//for (int i = 0; i < numberOfTransactions; i++)
			//{
			//	var transaction = new Transaction
			//	{
			//		AggregatorTransactionId = 1,
			//		Amount = new Amount
			//		{
			//			Currency = CurCodeType.USD,
			//			Value = 3.50m
			//		},
			//		Category = "Test",
			//		CreatedDateTimeUTC = now,
			//		FinancialAccountId = userInstitution.FinancialAccounts.First().FinancialAccountId,
			//		Memo = "memo",
			//		ModifiedDateTimeUTC = now,
			//		PartitionKey = userInstitution.FinancialAccounts.First().FinancialAccountId,
			//		PostedDate = now,
			//		Subcategory = "Test",
			//		Type = TransactionType.DEBIT
			//	};

			//	await dbContext.Transactions.AddAsync(transaction);
			//	await dbContext.SaveChangesAsync();

			//	var userTransaction = new UserTransaction
			//	{
			//		AggregatorTransactionId = transaction.AggregatorTransactionId,
			//		Amount = transaction.Amount,
			//		Category = transaction.Category,
			//		CreatedDateTimeUTC = transaction.CreatedDateTimeUTC,
			//		FinancialAccountId = userInstitution.FinancialAccounts.First().FinancialAccountId,
			//		Id = transaction.Id,
			//		Memo = transaction.Memo,
			//		ModifiedDateTimeUTC = transaction.ModifiedDateTimeUTC,
			//		PartitionKey = userId,
			//		PostedDate = transaction.PostedDate,
			//		Subcategory = transaction.Subcategory,

			//		Type = transaction.Type,
			//	};

			//	await dbContext.UserTransactions.AddAsync(userTransaction);
			//	await dbContext.SaveChangesAsync();
			//}
		}
	}
}
