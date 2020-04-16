namespace Coldairarrow.Util
{
    /// <summary>
    /// 通用条件分页查询DTO
    /// </summary>
    public class CommonPageInputDTO : PageInputDTO
    {
        public string Condition { get; set; }
        public string Keyword { get; set; }
    }
}
