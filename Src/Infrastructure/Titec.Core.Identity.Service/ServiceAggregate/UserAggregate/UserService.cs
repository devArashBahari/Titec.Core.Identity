using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titec.Core.Identity.Application.ViewModel.IdentityAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Application.ServiceContract;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.Service.Validations;
using Titec.Framework.Repository;
using Titec.Core.Identity.Application.Exceptions;
using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace Titec.Core.Identity.Service.ServiceAggregate.UserAggregate
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _Userrepository;
        private readonly IRoleRepository _IRoleRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IUserCustomerService _UserCustomerService;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository UserRepository, IUnitOfWork unitOfWork, IUserCustomerService UserCustomerService, IUserRoleRepository UserRoleRepository, IRoleRepository IRoleRepository)
        {
            _Userrepository = UserRepository;
            _unitOfWork = unitOfWork;
            _UserCustomerService = UserCustomerService;
            _UserRoleRepository = UserRoleRepository;
            _IRoleRepository = IRoleRepository;
        }

        public async Task AddCustomersToUser(AddCustomersToUserCommandViewModel model)
        {
            await _UserCustomerService.AddCustomersToUser(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddRoleToUser(RoleUserAddCommandModel model)
        {
            if (!await _Userrepository.IsUserExistsById(model.userId))
            {

                throw new EntityNotFoundException("کاربری یافت نشد");
            }
            if (!await _IRoleRepository.RolsExist(model.roleIds))
            {

                throw new EntityNotFoundException("این نقش ها موجود نیست");
            }
            if (await _UserRoleRepository.UserHaseRols(model.userId, model.roleIds))
            {

                throw new DuplicateException("این نقش ها تکراری می باشد");
            }

            await _UserRoleRepository.AddRolesToUser(model);
            await _unitOfWork.SaveAsync();

        }

        public async Task<EditUserBaseViewModel> EditUser(EditUserCommandModel model, int userId)
        {
            var user = await _Userrepository.GetUserById(userId);

            if (user == null)
            {
                throw new EntityNotFoundException("User not found");
            }
            var compairUserIdByUserName = await _Userrepository.FindUserIdByUserName(model.UserName);
            var compairUserIdByEmail = await _Userrepository.FindUserIdByEmail(model.Email);
            var compairUserIdByMobileNumber = await _Userrepository.FindUserIdByMobileNumber(model.MobileNo);
            if (user.UserName == model.UserName && user.Id == userId && user.Email == model.Email && user.MobileNo == model.MobileNo)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.IsDeleted = false;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MobileNo = model.MobileNo;
                user.LastEditDate = DateTime.Now;
                var result = await _Userrepository.EditUser(user);
                await _unitOfWork.SaveAsync();
                return (EditUserBaseViewModel)result;
            }
            if (await _Userrepository.IsUserExistsByUsername(model.UserName) && compairUserIdByUserName != user.Id)
            {
                throw new DuplicateException("نام کاربری تکراری می باشد");
            }
            if (await _Userrepository.IsUserExistsByEmail(model.Email) && compairUserIdByEmail != user.Id)
            {
                throw new DuplicateException("ایمیل تکراری است");
            }
            if (!model.MobileNo.CheckMobileNo())
            {
                throw new MobileNumberInvalidExeption("موبایل معتبر وارد کنید");
            }
            if (await _Userrepository.IsUserExistsByMobileNo(model.MobileNo) && compairUserIdByMobileNumber != user.Id)
            {
                throw new DuplicateException("شماره موبایل تکراری است");
            }
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.IsDeleted = false;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.MobileNo = model.MobileNo;
            user.LastEditDate = DateTime.Now;
            var res = await _Userrepository.EditUser(user);
            await _unitOfWork.SaveAsync();
            return (EditUserBaseViewModel)res;

        }

        public async Task<List<GetUsersBaseViewModel>> FilterSortPagination(GetUserAddCommandModel model)
        {
            if (model.SearchText != null)
            {
                await _Userrepository.FilterAll(model.SearchText);
            }

            if (model.Orderfiled != null)
            {
                await _Userrepository.Sort(model.Orderfiled, model.sortDirection);

            }

            return await _Userrepository.GetStudents(model.page, model.pageSize);
        }

        public async Task GenerateOTP(GenerateOtpAddCommandModel model)
        {
             await _Userrepository.GenerateOTP(model);
            await _unitOfWork.SaveAsync();
            return ;
        }

        public async Task<List<UserForRoleBaseViewModel>> GetAllUsers()
        {
            return await _Userrepository.GetAllUsers();
        }

        public async Task<List<object>> GetRowLevelAccessList()
        {
           return await _Userrepository.GetRowLevelAccessList();
        }

        public async Task<UserForRoleBaseViewModel> GetUserByEmail(string email)
        {
            return await _Userrepository.GetUserByEmail(email);
        }

        public async Task<IEnumerable<UsersWithRolsBaseViewModel>> GetUsersWithRols()
        {
            return await _Userrepository.GetUsersWithRols();
        }

        public async Task<bool> IsUserExistsByEmail(string email)
        {
            return await _Userrepository.IsUserExistsByEmail(email);
        }

        public Task<bool> IsUserExistsById(int userId)
        {
            return _Userrepository.IsUserExistsById(userId);
        }

        public async Task<bool> IsUserExistsByUsername(string username)
        {
            return await _Userrepository.IsUserExistsByUsername(username);
        }

        public async Task<TokenResponseViewModel> LoginUser(LoginAddCommandModel login)
        {
            return await _Userrepository.LoginUser(login);
        }

        public async Task<TokenResponseViewModel> LoginWithOtp(LoginWithOtpAddCommandModel model)
        {
            return await _Userrepository.LoginWithOtp(model);
        }

        public async Task<RegisterBaseViewModel> RegisterUser(RegisterAddCommandModel model)
        {
            if (!model.MobileNo.CheckMobileNo())
            {
                throw new MobileNumberInvalidExeption("موبایل معتبر وارد کنید");
            }
            if (!model.Password.CheckPassword())
            {
                throw new InvalidPasswordExeption("رمز عبور باید حداقل 8 کاراکتر باشد و حداقل شامل 1 کاراکتر ویژه باشد");
            }
            if (await _Userrepository.IsUserExistsByEmail(model.Email))
            {
                throw new DuplicateException("Email Exist");
            }
            if (await _Userrepository.IsUserExistsByUsername(model.UserName))
            {
                throw new DuplicateException("UserName Exist");
            }
            if (await _Userrepository.IsUserExistsByMobileNo(model.MobileNo))
            {
                throw new DuplicateException("شماره موبایل تکراری است");
            }
            var userEntity = (UserEntity)model;
            var Register = await _Userrepository.RegisterUser(userEntity);
            await _unitOfWork.SaveAsync();
            return (RegisterBaseViewModel)Register;
        }

        public async Task UpdateCustomersToUser(AddCustomersToUserCommandViewModel model)
        {
            await _UserCustomerService.AddCustomersToUser(model);
            await _unitOfWork.SaveAsync();
        }

        public Task<UserForRoleBaseViewModel> UpdateUser(EditUserRoleAddCommandModelViewModel roleToUser)
        {

            var Update = _Userrepository.UpdateUser(roleToUser);
            _unitOfWork.SaveAsync();
            return Update;
        }
    }
}
