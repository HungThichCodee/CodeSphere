namespace CodeSphere.Extensions
{
    public static class EnumerableExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(this IEnumerable<T> source)
        {
            return await source.ToListAsync(CancellationToken.None);
        }

        public static async Task<List<T>> ToListAsync<T>(this IEnumerable<T> source, CancellationToken cancellationToken)
        {
            // Thực chất chỉ chạy đồng bộ và bọc trong Task.Run
            return await Task.Run(() => source.ToList(), cancellationToken);
        }
    }
}
