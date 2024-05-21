namespace WhenAllApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main Thread id :"+Thread.CurrentThread.ManagedThreadId);

            List<string> list = new List<string>() {
             "https://qafqazinfo.az/",
             "https://oxu.az/?v=1",
             "https://www.google.com/",
             "https://www.trendyol.com/"

            };
            List<Task<Content>> contentTasks = new List<Task<Content>>();
            list.ToList().ForEach(x =>
            {
                contentTasks.Add(getSiteContentAsync(x));
            });

            var contentData = await Task.WhenAll(contentTasks.ToArray());

            foreach (var item in contentData.ToList())
            {
                Console.WriteLine($"Operation Thread Id :{item.ThreadId} Url: {item.Url}  Length: {item.Len}");
            }
        }
        public async static Task<Content> getSiteContentAsync(string url)
        {
            var response = await new HttpClient().GetStringAsync(url);

            var content = new Content()
            {
                Len = response.Length,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                Url = url
            };
            return content;

        }
    }
}