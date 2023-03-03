using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.Core.Entity;
using ULDeneme.ViewModel.VocabularyViewModels;

namespace ULDeneme.BLL.Abstract
{
    public interface IBaseBLL<TEntity>
        where TEntity : BaseEntity
    {
        ResultService<List<TEntity>> GetAllActive();
    }
}
