using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.DAL.Abstract;
using ULDeneme.DAL.Concrete.Context;
using ULDeneme.DAL.Concrete.Repository;

namespace ULDeneme.DAL.Concrete.DependencyInjection
{
    public static class EFContextDAL
    {
        public static void AddScopeDAL(this IServiceCollection services)
        {
            services.AddDbContext<ULDenemeDbContext>();
            services.AddScoped<IUserDAL, UserRepository>();
            services.AddScoped<ITranslationTypeDAL, TranslationTypeRepository>();
            services.AddScoped<ISozlukDAL,SozlukRepository>();
            services.AddScoped<IVocabularyDAL, VocabularyRepository>();
        }
    }
}
