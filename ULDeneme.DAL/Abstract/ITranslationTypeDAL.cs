using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.Core;
using ULDeneme.Model.Entities;

namespace ULDeneme.DAL.Abstract
{
    public interface ITranslationTypeDAL : IRepository<TranslationType>
    {
        List<TranslationType> GetList(Expression<Func<TranslationType, bool>> filter = null);
    }
}
