using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.Model.Entities;
using ULDeneme.ViewModel.TranslationTypeViewModels;

namespace ULDeneme.BLL.Abstract
{
    public interface ITranslationTypeBLL : IBaseBLL<TranslationType>
    {
        ResultService<TranslationType> GetTypeById(int id);
        ResultService<List<TranslationType>> GetTypesByUserID(int userID);
        ResultService<TranslationTypeCreateVM> Insert(TranslationTypeCreateVM translationType);
        ResultService<bool> Delete(int id);
        ResultService<TranslationTypeEditVM> Update(TranslationTypeEditVM translationTypeEditVM);
    }
}

