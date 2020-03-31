using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IHomeBusiness
    {
        Task<string> SubmitLoginAsync(string userName, string password);
        Task ChangePwdAsync(string oldPwd, string newPwd);
    }
}
