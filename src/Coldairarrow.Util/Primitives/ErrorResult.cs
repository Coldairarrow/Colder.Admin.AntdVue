namespace Coldairarrow.Util
{
    public class ErrorResult : AjaxResult
    {
        public ErrorResult(string msg = "操作失败!", int errorCode = 0)
        {
            Msg = msg;
            Success = false;
            ErrorCode = errorCode;
        }
    }
}