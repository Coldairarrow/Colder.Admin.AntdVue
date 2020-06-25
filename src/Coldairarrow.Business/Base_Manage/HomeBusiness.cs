using AutoMapper;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.IBusiness;
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
        public HomeBusiness(IDbAccessor db, IOperator @operator, IMapper mapper)
            : base(db)
        {
            _operator = @operator;
            _mapper = mapper;
        }

        public async Task<string> SubmitLoginAsync(LoginInputDTO input)
        {
            input.password = input.password.ToMD5String();
            var theUser = await GetIQueryable()
                .Where(x => x.UserName == input.userName && x.Password == input.password)
                .FirstOrDefaultAsync();

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

        public async Task ChangePwdAsync(ChangePwdInputDTO input)
        {
            var theUser = _operator.Property;
            if (theUser.Password != input.oldPwd?.ToMD5String())
                throw new BusException("原密码错误!");

            theUser.Password = input.newPwd.ToMD5String();
            await UpdateAsync(_mapper.Map<Base_User>(theUser));
        }
    }
}
