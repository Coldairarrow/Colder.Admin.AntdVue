using AutoMapper;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public class HomeBusiness : BaseBusiness<Base_User>, IHomeBusiness, ITransientDependency
    {
        readonly IOperator _operator;
        readonly IMapper _mapper;
        public HomeBusiness(IMyRepository repository, IOperator @operator, IMapper mapper)
            : base(repository)
        {
            _operator = @operator;
            _mapper = mapper;
        }

        [Transaction]
        public async Task<string> SubmitLoginAsync(string userName, string password)
        {
            if (userName.IsNullOrEmpty() || password.IsNullOrEmpty())
                throw new BusException("账号或密码不能为空！");
            password = password.ToMD5String();
            var theUser = await GetIQueryable().Where(x => x.UserName == userName && x.Password == password).FirstOrDefaultAsync();

            if (theUser.IsNullOrEmpty())
                throw new BusException("账号或密码不正确！");

            //生成token,有效期一天
            JWTPayload jWTPayload = new JWTPayload
            {
                UserId = theUser.Id,
                Expire = DateTime.Now.AddDays(1)
            };
            string token = JWTHelper.GetToken(jWTPayload.ToJson(), JWTHelper.JWTSecret);

            return token;
        }

        public async Task ChangePwdAsync(string oldPwd, string newPwd)
        {
            var theUser = _operator.Property;
            if (theUser.Password != oldPwd?.ToMD5String())
                throw new BusException("原密码错误!");

            theUser.Password = newPwd.ToMD5String();
            await UpdateAsync(_mapper.Map<Base_User>(theUser));
        }
    }
}
