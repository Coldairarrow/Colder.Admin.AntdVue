namespace Coldairarrow.Util
{
    public class PageInput<T> : PageInput where T : new()
    {
        public T Search { get; set; } = new T();
    }
}
