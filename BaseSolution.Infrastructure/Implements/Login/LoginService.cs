using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using System.Threading;
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
            try
            {
                var result =  _dbContext.Users.AsNoTracking().Where(x => x.UserName == request.UserName && x.Password == request.Password && x.UserRoleId == request.UserRoleId)
                 .ProjectTo<ViewLoginInput>(_mapper.ConfigurationProvider).FirstOrDefault();
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
