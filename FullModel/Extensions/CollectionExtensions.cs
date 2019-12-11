using System.Collections.Generic;
using System.Linq;

namespace FullModel.Extensions
{
	public static class CollectionExtensions
	{
		public static List<List<T>> ChunkBy<T>(this ICollection<T> source, int chunkSize)
		{
			return source
				.Select((x, i) => new { Index = i, Value = x })
				.GroupBy(x => x.Index / chunkSize)
				.Select(x => x.Select(v => v.Value).ToList())
				.ToList();
		}
	}
}
