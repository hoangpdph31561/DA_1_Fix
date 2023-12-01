using AutoMapper;
using BaseSolution.Application.DataTransferObjects.Account;
using BaseSolution.Application.DataTransferObjects.Account.request;
using BaseSolution.Application.Interfaces.Login;
using BaseSolution.Application.Interfaces.Services;
using BaseSolution.Application.ValueObjects.Common;
using BaseSolution.Application.ValueObjects.Response;
using BaseSolution.Domain.Entities;
using BaseSolution.Infrastructure.Database.AppDbContext;
using BaseSolution.Infrastructure.Implements.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Implements.Login
{
    public class LoginService : ILoginService
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public LoginService(AppReadOnlyDbContext dbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<ViewLoginInput>> Login(LoginInputRequest request)
        {
            // Bước 1 : lấy ra được User có user Name và password truyền xuống 
            try
            {
                var result = new ViewLoginInput(); // khai báo để còn return 
                var user = _dbContext.Users.FirstOrDefault(x => x.UserName == request.UserName && x.Password == request.Password);
                if(user == null)
                {
                    return RequestResult<ViewLoginInput>.Fail(_localizationService["Login fail"]);
                }
                else
                {
                    // lấy ra Role của User đó
                    result.UserName = user.UserName;
                    result.Password = user.Password;
                    var role = _dbContext.Users.FirstOrDefault(x => x.UserRoleId == request.UserRoleId && x.UserName == user.UserName && x.Password == user.Password);
                    if(role == null)
                    {
                        return RequestResult<ViewLoginInput>.Fail(_localizationService["Login fail"]);
                    }
                    else 
                    {
                        result.UserRoleId = role.UserRoleId;
                        result.RoleCode = _dbContext.UserRoles.Where(x => x.Id == result.UserRoleId).Select(x => x.RoleCode).FirstOrDefault();
                    }
                }
                return RequestResult<ViewLoginInput>.Succeed(result);
            }
            catch (Exception e)
            {
                return RequestResult<ViewLoginInput>.Fail(_localizationService["Login fail"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                    }
                });
            }
        }
    }
}
