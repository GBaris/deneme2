using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.Model.Entities;
using ULDeneme.ViewModel.VocabularyViewModels;

namespace ULDeneme.BLL.Abstract
{
    public interface IVocabularyBLL: IBaseBLL<Vocabulary>
    {
        ResultService<Vocabulary> GetVocByID(int id);
        ResultService<List<Vocabulary>> GetVocBySozlukID(int sozlukId);
        ResultService<VocabularyCreateVM> Insert(VocabularyCreateVM vocabulary);
        ResultService<VocabularyUpdateVM> Update(VocabularyUpdateVM vocabularyEditVM);
        ResultService<bool> Delete(int id);
    }
}
