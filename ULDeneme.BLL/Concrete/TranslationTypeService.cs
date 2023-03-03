using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.BLL.Abstract;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.DAL.Abstract;
using ULDeneme.Model.Entities;
using ULDeneme.Model.Enums;
using ULDeneme.ViewModel.UserViewModels;
using ULDeneme.ViewModel.TranslationTypeViewModels;
using Microsoft.EntityFrameworkCore;
using ULDeneme.DAL.Concrete.Context;
using ULDeneme.DAL;

namespace ULDeneme.BLL.Concrete
{
    public class TranslationTypeService : ITranslationTypeBLL
    {
        private readonly ITranslationTypeDAL _translationTypeDAL;
        private readonly IUserDAL _userDAL;
        private readonly ULDenemeDbContext _context;

        public TranslationTypeService(ITranslationTypeDAL translationTypeDAL, IUserDAL userDAL, ULDenemeDbContext context)
        {
            _translationTypeDAL = translationTypeDAL;
            _userDAL = userDAL;
            _context = context;
        }

        public ResultService<TranslationType> GetTypeById(int id)
        {
            ResultService<TranslationType> result = new ResultService<TranslationType>();
            try
            {
                result.Data = _translationTypeDAL.Get(x => x.ID == id);
            }
            catch (Exception ex)
            {
                result.AddError("Error", ex.Message);
            }
            return result;
        }

        public ResultService<TranslationTypeCreateVM> Insert(TranslationTypeCreateVM translationType)
        {
            ResultService<TranslationTypeCreateVM> result = new ResultService<TranslationTypeCreateVM>();

            var knownLang = translationType.KnownLang.ToLower();
            var unknownLang = translationType.UnknownLang.ToLower();
            //var userID = translationType.UserID;

            var existingTranslationType = _translationTypeDAL.Get(x => x.KnownLang.ToLower() == knownLang && x.UnknownLang.ToLower() == unknownLang);
            if (existingTranslationType != null && existingTranslationType.IsActive == true)
            {
                result.AddError("Error", "Translation Type already exists");
                return result;
            }
            else if (existingTranslationType != null && existingTranslationType.IsActive == false)
            {
                existingTranslationType.IsActive = true;
                _translationTypeDAL.Update(existingTranslationType);
                _context.SaveChanges();
                result.Data = translationType;
                return result;
            }
            else
            {
                try
                {
                    var user = _userDAL.Get(x => x.ID == translationType.UserID);

                    TranslationType newType = new TranslationType
                    {
                        KnownLang = translationType.KnownLang,
                        UnknownLang = translationType.UnknownLang,
                        UserRole = translationType.UserRole,
                        UserID = translationType.UserID,
                    };
                    newType.Name = newType.UnknownLang.ToUpper() + "-" + newType.KnownLang.ToUpper() + " Dictionary";
                    _translationTypeDAL.Add(newType);
                    result.Data = translationType;
                }
                catch (Exception ex)
                {
                    result.AddError("Error", ex.Message);
                }
            }
            return result;
        }

        public ResultService<bool> Delete(int id)
        {
            ResultService<bool> result = new ResultService<bool>();
            try
            {
                var deleteType = _translationTypeDAL.Get(x => x.ID == id);
                deleteType.IsActive = false;
                deleteType.ModifiedDate= DateTime.Now;
                _translationTypeDAL.Update(deleteType);
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

        public ResultService<List<TranslationType>> GetTypesByUserID(int userID)
        {
            ResultService<List<TranslationType>> result = new ResultService<List<TranslationType>>();
            try
            {
                result.Data = _translationTypeDAL.GetList(x => x.UserID == userID && x.IsActive == true);
                if (result.Data.Count == 0)
                {
                    result.AddError("GetTypesByUserID", "There are no translation types for this user");
                }
            }
            catch (Exception ex)
            {
                result.AddError("GetTypesByUserID", ex.Message);
            }
            return result;
        }

        public ResultService<TranslationTypeEditVM> Update(TranslationTypeEditVM translationTypeEditVM)
        {
            ResultService<TranslationTypeEditVM> result = new ResultService<TranslationTypeEditVM>();

            try
            {
                TranslationType translationType = _translationTypeDAL.Get(x => x.ID == translationTypeEditVM.ID);
                if (translationType == null)
                {
                    result.AddError("", "TranslationTypeNotFound");
                    return result;
                }
                translationType.KnownLang = translationTypeEditVM.KnownLang;
                translationType.UnknownLang = translationTypeEditVM.UnknownLang;
                translationType.ModifiedDate= translationTypeEditVM.ModifiedDate;
                translationType.Name = translationTypeEditVM.UnknownLang + "-" + translationTypeEditVM.KnownLang + " Dictionary";
                _translationTypeDAL.Update(translationType);
                int resultSave = _context.SaveChanges();
                if (resultSave > 0)
                {
                    result.Data = translationTypeEditVM;
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

        public ResultService<List<TranslationType>> GetAllActive()
        {
            ResultService<List<TranslationType>> result = new ResultService<List<TranslationType>>();

            try
            {
                result.Data = _translationTypeDAL.GetList(x=>x.IsActive ==true);
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
    }
}