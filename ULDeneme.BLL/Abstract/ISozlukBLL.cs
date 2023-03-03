using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.Model.Entities;
using ULDeneme.ViewModel.SozlukViewModels;

namespace ULDeneme.BLL.Abstract
{
    public interface ISozlukBLL : IBaseBLL<Sozluk>
    {
        ResultService<List<Sozluk>> GetSozluksByUserId(int userid);
        ResultService<List<Sozluk>> GetSozluksByThemesbyUserIDbyTranslationType(int themeID, int translationTypeID, int userID);
        ResultService<List<Sozluk>> GetSozluksByTranslationTypeID(int translationTypeID, int userID);
        ResultService<List<Sozluk>> GetSozluksByThemes(int themeID, int userID);
        ResultService<List<Sozluk>> GetSozluksByThemesByTranslationTypeByAdmin(int themeID, int translationTypeID);
        ResultService<List<Sozluk>> GetSozlukByThemesByAdmin(int themeID);
        ResultService<Sozluk> GetSozlukById(int id);
        ResultService<SozlukCreateVM> Insert(SozlukCreateVM sozluk);
        ResultService<bool> Delete(int id);
        ResultService<SozlukEditVM> Update (SozlukEditVM sozlukEditVM);
    }
}
