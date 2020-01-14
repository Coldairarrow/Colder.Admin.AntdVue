namespace Coldairarrow.Util
{
    public class SuccessResult<T> : AjaxResult<T>
    {
        public SuccessResult(T data = default, string msg = "操作成功!", int total = 0)
        {
            Success = true;
            Data = data;
            Msg = msg;
            Total = total;
        }
    }
}