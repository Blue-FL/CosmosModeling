namespace FullModel.Data
{
	public class DataConstants
	{
		public static string USER_CONTAINER = "User";
		public static string USER_INSTITUTION_CONTAINER = "UserInstitution";
		public static string FINANCIAL_ACCOUNT_CONTAINER = "FinancialAccount";
		public static string INSTITUTION_CONTAINER = "Institution";
		public static string CATEGORY_CONTAINER = "TransactionCategory";
	}

	public class PartitionConstants
	{
		public static string PARTITION_KEY = "/PartitionKey";
	}
}
