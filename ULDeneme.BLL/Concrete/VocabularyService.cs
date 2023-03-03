using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.BLL.Abstract;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.DAL.Abstract;
using ULDeneme.DAL.Concrete.Context;
using ULDeneme.Model.Entities;
using ULDeneme.ViewModel.VocabularyViewModels;

namespace ULDeneme.BLL.Concrete
{
    public class VocabularyService : IVocabularyBLL
    {
        private readonly IVocabularyDAL _vocabularyDAL;
        private readonly ISozlukDAL _sozlukDAL;
        private readonly ULDenemeDbContext _context;

        public VocabularyService(IVocabularyDAL vocabularyDAL, ULDenemeDbContext context)
        {
            _vocabularyDAL = vocabularyDAL;
            _context = context;
        }

        public ResultService<bool> Delete(int id)
        {
            ResultService<bool> result = new ResultService<bool>();
            try
            {
                var deleteType = _vocabularyDAL.Get(x => x.ID == id);
                deleteType.IsActive = false;
                deleteType.ModifiedDate = DateTime.Now;
                _vocabularyDAL.Update(deleteType);
                _context.SaveChanges();
                if (deleteType.IsActive == false)
                {
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;

                }
            }
            catch (Exception ex)
            {
                result.AddError("Error", ex.Message);
            }
            return result;
        }

        public ResultService<List<Vocabulary>> GetAllActive()
        {
            ResultService<List<Vocabulary>> result = new ResultService<List<Vocabulary>>();

            try
            {
                result.Data = _vocabularyDAL.GetList(x => x.IsActive == true);
                result.IsSuccess = true;
                result.Message = "Sozluk retrieved successfully.";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public ResultService<Vocabulary> GetVocByID(int id)
        {
            ResultService<Vocabulary> result = new ResultService<Vocabulary> { };
            try
            {
                result.Data = _vocabularyDAL.Get(x => x.ID == id);
            }
            catch (Exception ex)
            {
                result.AddError("Error", ex.Message);
            }
            return result;
        }

        public ResultService<List<Vocabulary>> GetVocBySozlukID(int sozlukId)
        {
            ResultService<List<Vocabulary>> result = new ResultService<List<Vocabulary>>();
            try
            {
                result.Data = _vocabularyDAL.GetList(x => x.SozlukID == sozlukId && x.IsActive == true);
                result.IsSuccess = true;
                if (result.Data.Count == 0)
                {
                    result.AddError("GetVocBySozlukID", "There are no vocabulary for this Sozluk");
                }
            }
            catch (Exception ex)
            {

                result.AddError("GetVocBySozlukID", ex.Message);
            }
            return result;
        }

        public ResultService<VocabularyCreateVM> Insert(VocabularyCreateVM vocabulary)
        {
            ResultService<VocabularyCreateVM> result = new ResultService<VocabularyCreateVM>();

            try
            {
                Vocabulary newVoc = new Vocabulary
                {
                    UnKVoc = vocabulary.UnKVoc,
                    KVoc = vocabulary.KVoc,
                    SozlukID = vocabulary.SozlukID
                };
                _vocabularyDAL.Add(newVoc);
                result.Data = vocabulary;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.AddError("Error", ex.Message);
            }
            return result;
        }

        public ResultService<VocabularyUpdateVM> Update(VocabularyUpdateVM vocabularyEditVM)
        {
            ResultService<VocabularyUpdateVM> result = new ResultService<VocabularyUpdateVM>();

            try
            {
                Vocabulary vocabulary = _vocabularyDAL.Get(x => x.ID == vocabularyEditVM.ID);
                if (vocabulary == null)
                {
                    result.AddError("", "VocabularyNotFound");
                    return result;
                }
                vocabulary.UnKVoc = vocabularyEditVM.UnKVoc;
                vocabulary.KVoc = vocabularyEditVM.KVoc;
                vocabulary.ModifiedDate = vocabularyEditVM.ModifiedDate;
                _vocabularyDAL.Update(vocabulary);
                int resultSave = _context.SaveChanges();
                if (resultSave > 0)
                {
                    result.Data = vocabularyEditVM;
                }
                else
                {
                    result.AddError("", "TranslationTypeNotSaved");
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message, "birşeyler yanlış oldu");
            }
            return result;
        }
    }
}
