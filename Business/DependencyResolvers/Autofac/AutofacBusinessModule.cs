using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.RuleForBusiness;
using Castle.DynamicProxy;
using Core.Security.Jwt;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
  public  class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfProductDal>().As<IProductDal>();
            builder.RegisterType<ProductManager>().As<IProductService>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<BusinessRules>().As<IBusinessRules>();

            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptionSelector()
                }).SingleInstance();
        }
    }
}
