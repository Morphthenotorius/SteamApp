using Business.Abstract;
using Business.Abstract.Auth;
using Business.Abstract.Payment;
using Business.Concrete;
using Business.Concrete.Auth;
using Business.Concrete.Payment;
using Core.Entities.User;
using Core.Utilites.Security.Abstract;
using Core.Utilites.Security.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Helpers.ServiceRegistration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IGameDAL,EfGameDAL>();
            services.AddScoped<IGameService,GameManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EfCategoryDAL>();
            services.AddScoped<ILibraryDAL,EfLibraryDAL>();
            services.AddScoped<ILibraryService, LibraryManager>();
            services.AddScoped<IReviewDAL,EfReviewDAL>();
            services.AddScoped<IReviewService,ReviewManager>();
            services.AddScoped<ICompanyDAL,EfCompanyDAL>();
            services.AddScoped<ICompanyService, CompanyManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IStripeService, StripeManager>();
            services.AddDataProtection();
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();
            
                return services;
        }
    }
}

