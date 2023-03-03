using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.BLL.Abstract;
using ULDeneme.DAL.Concrete.DependencyInjection;

namespace ULDeneme.BLL.Concrete.DependencyInjection
{
    public static class EFContextBLL
    {
        public static void AddScopeBLL(this IServiceCollection services)
        {
            services.AddScopeDAL();
            services.AddScoped<IUserBLL, UserService>();
            services.AddScoped<ITranslationTypeBLL,TranslationTypeService>();
            services.AddScoped<ISozlukBLL, SozlukService>();

        }
    }
}
