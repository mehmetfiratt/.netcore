using System;
using System.Linq;
using System.Security.Authentication;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static System.String;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (IsNullOrEmpty(authorizationHeader))
            {
                throw new UnauthorizedAccessException(Messages.UnAuthorizationDenied);
            }

            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            if (_roles.Any(role => roleClaims.Contains(role)))
            {
                return;
            }


            throw new AuthenticationException(Messages.AuthorizationDenied);
        }
    }
}
