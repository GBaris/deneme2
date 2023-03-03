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
using ULDeneme.ViewModel.SozlukViewModels;

namespace ULDeneme.BLL.Concrete
{
    public class SozlukService : ISozlukBLL
    {
        private readonly ISozlukDAL _sozlukDAL;
        private readonly IUserDAL _userDAL;
        private readonly ULDenemeDbContext _context;

        public SozlukService(ISozlukDAL sozlukDAL, IUserDAL userDAL, ULDenemeDbContext context)
        {
            _sozlukDAL = sozlukDAL;
            _userDAL = userDAL;
            _context = context;
        }

        public ResultService<bool> Delete(int id)
        {
            ResultService<bool> result = new ResultService<bool>();
            try
            {
                var deleteType = _sozlukDAL.Get(x => x.ID == id);
                deleteType.IsActive = false;
                deleteType.ModifiedDate = DateTime.Now;
                _sozlukDAL.Update(deleteType);
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

        public ResultService<List<Sozluk>> GetAllActive()
        {
            ResultService<List<Sozluk>> result = new ResultService<List<Sozluk>>();

            try
            {
                result.Data = _sozlukDAL.GetList(x=>x.IsActive==true);
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

        public ResultService<Sozluk> GetSozlukById(int id)
        {
            ResultService<Sozluk> result = new ResultService<Sozluk> { };
            try
            {
                result.Data = _sozlukDAL.Get(x => x.ID == id);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.AddError("Error", ex.Message);
            }
            return result;
        }

        public ResultService<List<Sozluk>> GetSozlukByThemesByAdmin(int themeID)
        {
            ResultService<List<Sozluk>> result = new ResultService<List<Sozluk>>();
            try
            {
                result.Data = _sozlukDAL.GetList(x => x.Theme == (Theme)themeID && x.IsActive == true && x.UserRole == UserRole.Admin);
                if (result.Data.Count == 0)
                {
                    result.AddError("GetSozluksByThemes", "There are no sozluk for this theme");
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.AddError("GetTypesByUserID", ex.Message);
            }
            return result;
        }

        public ResultService<List<Sozluk>> GetSozluksByThemes(int themeID, int userID)
        {
            ResultService<List<Sozluk>> result = new ResultService<List<Sozluk>>();
            try
            {
                result.Data = _sozlukDAL.GetList(x => x.Theme == (Theme)themeID && x.IsActive == true && x.UserID == userID);
                if (result.Data.Count == 0)
                {
                    result.AddError("GetSozluksByThemes", "There are no sozluk for this theme");
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.AddError("GetTypesByUserID", ex.Message);
            }
            return result;
        }

        public ResultService<List<Sozluk>> GetSozluksByThemesByTranslationTypeByAdmin(int themeID, int translationTypeID)
        {
            ResultService<List<Sozluk>> result = new ResultService<List<Sozluk>>();
            try
            {
                result.Data = _sozlukDAL.GetList(x => x.Theme == (Theme)themeID && x.IsActive == true && x.TranslationTypeID == translationTypeID && x.UserRole == UserRole.Admin);
                if (result.Data.Count == 0)
                {
                    result.AddError("GetSozluksByThemes", "There are no sozluk for this theme");
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.AddError("GetTypesByUserID", ex.Message);
            }
            return result;
        }

        public ResultService<List<Sozluk>> GetSozluksByThemesbyUserIDbyTranslationType(int themeID, int translationTypeID, int userID)
        {
            ResultService<List<Sozluk>> result = new ResultService<List<Sozluk>>();
            try
            {
                result.Data = _sozlukDAL.GetList(x => x.Theme == (Theme)themeID && x.IsActive == true && x.TranslationTypeID == translationTypeID && x.UserID == userID);
                if (result.Data.Count == 0)
                {
                    result.AddError("GetSozluksByThemes", "There are no sozluk for this theme");
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.AddError("GetTypesByUserID", ex.Message);
            }
            return result;
        }

        public ResultService<List<Sozluk>> GetSozluksByTranslationTypeID(int translationTypeID,int userID)
        {
            ResultService<List<Sozluk>> result = new ResultService<List<Sozluk>>();
            try
            {
                result.Data = _sozlukDAL.GetList(x => x.TranslationTypeID == translationTypeID && x.IsActive == true &&x.UserID==userID);
                if (result.Data.Count == 0)
                {
                    result.AddError("GetSozluksByTranslationTypeID", "There are no sozluk for this TranslationTypeId");
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.AddError("GetTypesByUserID", ex.Message);
            }
            return result;
        }

        public ResultService<List<Sozluk>> GetSozluksByUserId(int userid)
        {
            ResultService<List<Sozluk>> result = new ResultService<List<Sozluk>>();
            try
            {
                result.Data = _sozlukDAL.GetList(x=>x.UserID== userid && x.IsActive==true);
                if (result.Data.Count == 0)
                {
                    result.AddError("GetSozluksByUserId", "There are no sozluks for this user");
                }
            }
            catch (Exception ex)
            {
                result.AddError("GetTypesByUserID", ex.Message);
            }
            return result;
        }

        public ResultService<SozlukCreateVM> Insert(SozlukCreateVM sozluk)
        {
            ResultService<SozlukCreateVM> result = new ResultService<SozlukCreateVM>();

            var name = sozluk.Name.ToLower();
            var translationId = sozluk.TranslationTypeID;
            var userId = sozluk.UserID;

            var existingSozluk = _sozlukDAL.Get(x => x.Name.ToLower() == name && x.TranslationTypeID == translationId && x.UserID == userId);
            if (existingSozluk != null && existingSozluk.IsActive == true)
            {
                result.AddError("Ekleme Hatası", "Sozluk already exist");
                return result;
            }
            else if (existingSozluk != null && existingSozluk.IsActive == false)
            {
                existingSozluk.IsActive = true;
                _sozlukDAL.Update(existingSozluk);
                _context.SaveChanges();
                result.Data = sozluk;
                return result;
            }
            else
            {
                try
                {
                    var user = _userDAL.Get(x => x.ID == sozluk.UserID);

                    Sozluk newSozluk = new Sozluk
                    {
                        Name = sozluk.Name,
                        Description = sozluk.Description,
                        UserRole = sozluk.UserRole,
                        UserID = sozluk.UserID,
                        Theme= sozluk.Theme,
                        TranslationTypeID = sozluk.TranslationTypeID
                    };
                    _context.Sozluks.Add(newSozluk);
                    _context.SaveChanges();
                     result.IsSuccess= true;    
                    result.Data = sozluk;
                }
                catch (Exception)
                {

                    result.AddError("Ekleme Hatası", "Sozluk ekelenemedi");
                }
            }
            return result;
        }

        public ResultService<SozlukEditVM> Update(SozlukEditVM sozlukEditVM)
        {
            ResultService<SozlukEditVM> result = new ResultService<SozlukEditVM>();

            try
            {
                Sozluk sozluk = _sozlukDAL.Get(x => x.ID == sozlukEditVM.ID);
                if (sozluk == null)
                {
                    result.AddError("ID hatası", "ID'ye aid sözlük bulunamadı");
                    return result;
                }
                sozluk.Name = sozlukEditVM.Name;
                sozluk.Description = sozlukEditVM.Description;
                sozluk.ModifiedDate = sozlukEditVM.ModifiedDate;
                sozluk.Name = sozlukEditVM.Name;
                sozluk.TranslationTypeID = sozlukEditVM.TranslationTypeID;
                int resultSave = _context.SaveChanges();
                if (resultSave > 0)
                {
                    result.Data = sozlukEditVM;
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
