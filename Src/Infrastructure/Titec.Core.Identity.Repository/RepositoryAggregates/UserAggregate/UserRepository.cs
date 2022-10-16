using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Titec.Core.Identity.Application.ViewModel.IdentityAggregate;
using Titec.Core.Identity.Application.ViewModel.RoleAggregate;
using Titec.Core.Identity.Application.ViewModel.UserAggregate;
using Titec.Core.Identity.Application.RepositoryContract;
using Titec.Core.Identity.Common.PaginationConfig;
using Titec.Core.Identity.Common.Security;
using Titec.Core.Identity.Domain.AccountAggregate;
using Titec.Core.Identity.EF.DataBaseContext;
using Titec.Framework.Application.Identity;
using Titec.Framework.Repository;
using System.Linq;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Titec.Core.Identity.Application.Exceptions;
using Titec.Core.Identity.Service.Validations;
using Microsoft.Extensions.Configuration;

namespace Titec.Core.Identity.Repository.RepositoryAggregates.UserAggregate
{
    public class UserRepository : GenericRepository<UserEntity, int>, IUserRepository
    {

        private IPasswordHelper passwordHelper;
        private IConfiguration Configuration;
        IQueryable<UserEntity> AllUsers;


        //private IQueryable<GetUsersBaseViewModel> Users;
     

        public UserRepository(BaseDbContext dbContext, IPasswordHelper passwordHelper, ITitecIdentity titecIdentity, IConfiguration configuration) : base(dbContext, titecIdentity)
        {

            this.passwordHelper = passwordHelper;
            AllUsers = dbContext.users;
            Configuration = configuration;
        }
        public async Task<List<UserForRoleBaseViewModel>> GetAllUsers()
        {
            var result = await base._dbSet.Where(p => p.IsDeleted == false).Select(p => new UserForRoleBaseViewModel
            {
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                MobileNo = p.MobileNo,
                UserName = p.UserName

            }).ToListAsync();
            return result;
        }

        public async Task<UserForRoleBaseViewModel> GetUserByEmail(string email)
        {
            var Result = await base._dbSet.SingleOrDefaultAsync(s => s.Email == email.ToLower().Trim() && s.IsDeleted == false);

            return (UserForRoleBaseViewModel)Result;
        }

        public async Task<bool> IsUserExistsByEmail(string email)
        {
            var result = await base._dbSet.AnyAsync(s => s.Email == email.ToLower().Trim() && s.IsDeleted == false);
            return result;
        }

        public async Task<bool> IsUserExistsById(int userId)
        {
            return await base._dbSet.AnyAsync(c => c.Id == userId && c.IsDeleted == false);
        }

        public async Task<bool> IsUserExistsByUsername(string username)
        {
            var result = await base._dbSet.AnyAsync(s => s.UserName == username.ToLower().Trim() && s.IsDeleted == false);
            return result;
        }
        public async Task<bool> IsUserExistsByMobileNo(string mobileNo)
        {
            return await base._dbSet.AnyAsync(s => s.MobileNo == mobileNo && s.IsDeleted == false);
        }

        public async Task<TokenResponseViewModel> LoginUser(LoginAddCommandModel login)
        {

            var password = passwordHelper.EncodePasswordMd5(login.Password);

            var user = await base._dbSet
                .SingleOrDefaultAsync(s => s.UserName == login.UserName.ToLower().Trim() && s.Password == password);
            var FullName = user.FirstName + " " + user.LastName;
            var CustomerIds = await base._dbSet.Include(x => x.userCustomers).Where(x => x.Id == user.Id).SelectMany(x => x.userCustomers).Select(x => x.CustumerId).ToListAsync();

            List<string> rolsOfUser = base._dbSet.Include(x => x.UserRoles).ThenInclude(x => x.Role).Where(x => x.Id == user.Id).SelectMany(x => x.UserRoles).Select(x => x.Role.Alias).ToList();
            List<string> AppsofUser = base._dbSet.Include(x => x.userApps).ThenInclude(x => x.App).Where(x => x.Id == user.Id).SelectMany(x => x.userApps).Select(x => x.App.Name).ToList();

            var result = from e in AppsofUser
                         from d in rolsOfUser
                         select new
                         {
                             AppNAme = e,
                             RoleNAme = d
                         };

            var RoleApp = new List<string>();

            foreach (var item in result)
            {
                RoleApp.Add(item.AppNAme + "/" + item.RoleNAme);
            }

            //RoleApp.Add(new { aud = AppsofUser.ToString() });



            if (user == null)
            {
                return null;
            }
            var symmetricSecurityKey = Encoding.UTF8.GetBytes(Configuration["LoginSettings:loginKey"]);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKey), SecurityAlgorithms.HmacSha256);

            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));
            //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



            var cliammm = new List<Claim>();
            cliammm.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            cliammm.Add(new Claim(ClaimTypes.Name, user.UserName));
            cliammm.Add(new Claim(ClaimTypes.UserData, user.rowLevelAccess.ToString()));
            cliammm.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            cliammm.Add(new Claim("FirstName", user.FirstName));
            cliammm.Add(new Claim("LastName", user.LastName));
            cliammm.Add(new Claim("CustomerId", user.CustomerId));
            cliammm.Add(new Claim("CustomerName", user.CustomerName));
            //foreach (var item in CustomerIds)
            //{
            //    cliammm.Add(new Claim(ClaimTypes.Actor, item.ToString()));
            //}



            //cliammm.Add(new Claim(ClaimTypes.GivenName, FullName));
            //cliammm.AddRange(rolsOfUser.Select(CustomerIds => new Claim(ClaimTypes.Country, CustomerIds.ToString())));
            cliammm.AddRange(RoleApp.Select(role => new Claim(ClaimTypes.Role, role)));
            string combinedString = string.Join(",", AppsofUser.ToArray());
            //var tokenOptions = new JwtSecurityToken(

            //    issuer: "http://localhost:5262",
            //    claims: cliammm,
            //    expires: DateTime.Now.AddDays(30),
            //    signingCredentials: signinCredentials,
            //    audience: combinedString
            //);


            var jwt = new JwtSecurityToken(
               issuer: "Titec",
               // (audience): Recipient for which the JWT is intended : Application Name or Area
               audience: combinedString,
               claims: cliammm,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddMinutes(30),  //x min expiry and a client monitor token quality and should request new token with this one expiries
               signingCredentials: signingCredentials);




            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new TokenResponseViewModel()
            {
                Expiration = DateTime.UtcNow.AddMinutes(30).ToString(),
                Token = tokenString
            };
        }


        public async Task<UserEntity> RegisterUser(UserEntity register)
        {

            var user = new UserEntity
            {
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Password = passwordHelper.EncodePasswordMd5(register.Password),
                OtpCode =null,
                CreateDate = DateTime.Now,
                IsDeleted = false,
                MobileNo = register.MobileNo,
                UserName = register.UserName,
                IsActive = register.IsActive,
                CustomerId = register.CustomerId,
                CustomerName = register.CustomerName,
                rowLevelAccess = register.rowLevelAccess
            };

            await base._dbSet.AddAsync(user);

            return user;
        }

        public async Task<UserForRoleBaseViewModel> UpdateUser(EditUserRoleAddCommandModelViewModel roleToUser)
        {
            var usere = await base._dbSet.FirstOrDefaultAsync(x => x.Id == roleToUser.userId && x.IsDeleted == false);
            if (usere == null)
            {
                var massage = new UserForRoleBaseViewModel()
                {
                    Message = "چنین کاربری وجود ندارد"
                };
                return massage;
            }
            usere.UserName = roleToUser.UserName;
            usere.Email = roleToUser.Email;
            usere.MobileNo = roleToUser.MobileNo;
            usere.LastEditDate = DateTime.Now;
            usere.IsDeleted = false;
            usere.LastName = roleToUser.LastName;
            usere.FirstName = roleToUser.FirstName;
            usere.IsActive = roleToUser.IsActive;
            await base.EditAsync(usere);
            return (UserForRoleBaseViewModel)usere;
        }

        public async Task FilterAll(string term)
        {
            AllUsers = AllUsers.Where(user => user.FirstName.Contains(term) && user.IsDeleted == false || user.LastName.Contains(term) && user.IsDeleted == false ||
           user.UserName.Contains(term) && user.IsDeleted == false
            ).AsQueryable();
        }

        public async Task Sort(string filed, string sortDirection)
        {
            var rowLevelNumbers = new List<string>();
            switch (filed)
            {
                case "FirstName" when sortDirection == "desc":
                    AllUsers = AllUsers.OrderByDescending(x => x.FirstName).AsQueryable();

                    //foreach (var item in AllUsers)
                    //{
                    //    rowLevelNumbers.Add(item.rowLevelAccess.ToString());

                    //    foreach (var item1 in rowLevelNumbers)
                    //    {
                    //        var rowlevel =
                    //             (RowLevelAccess)Enum.Parse(typeof(RowLevelAccess), item1);
                    //        item.rowLevelAccess = rowlevel;
                    //    }

                    //}


                    break;
                case "FirstName" when sortDirection == "asc":
                    AllUsers = AllUsers.OrderBy(x => x.FirstName).AsQueryable();
                    break;
                case "LastName" when sortDirection == "desc":
                    AllUsers = AllUsers.OrderByDescending(x => x.LastName).AsQueryable();
                    break;
                case "LastName" when sortDirection == "asc":
                    AllUsers = AllUsers.OrderBy(x => x.LastName).AsQueryable();
                    break;
                case "UserName" when sortDirection == "desc":
                    AllUsers = AllUsers.OrderByDescending(x => x.UserName).AsQueryable();
                    break;
                case "UserName" when sortDirection == "asc":
                    AllUsers = AllUsers.OrderBy(x => x.UserName).AsQueryable();
                    break;
            }
        }

        public async Task<List<GetUsersBaseViewModel>> GetStudents(int page, int pageSize)
        {

            var pagination = await AllUsers.GetPaged(page, pageSize);
            var resultlist = new List<GetUsersBaseViewModel>();
            foreach (var item in pagination.Results)
            {
                resultlist.Add((GetUsersBaseViewModel)item);
            }
            return resultlist;
        }

        public async Task GenerateOTP(GenerateOtpAddCommandModel model)
        {
            try
            {
                var User = await base._dbSet.FirstOrDefaultAsync(x => x.MobileNo == model.MobileNo);
                if (!model.MobileNo.CheckMobileNo())
                {
                    throw new MobileNumberInvalidExeption("موبایل معتبر وارد کنید");
                }
                if (User == null)
                {
                    throw new EntityNotFoundException(" کاربر یافت نشد");
                }
              
                Random rnd = new Random();
                string otpCode = rnd.Next(100000, 999999).ToString();
                User.OtpCodeExpiry = DateTime.Now.AddMinutes(5);
                User.OtpCode = otpCode;
                base._dbSet.Update(User);


                var recptor = new List<string>();
                recptor.Add(model.MobileNo);

                var SendSmsModel = new SendSMSAddViewModel()
                {
                    Receptors = recptor,
                    Message = otpCode
                };

                var json = JsonConvert.SerializeObject(SendSmsModel);
                //var json = JsonConvert.DeserializeObject(SendSmsModel.ToString());
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://195.110.39.70:8086/api/sms/send";
                HttpClient client = new HttpClient();

                var response = await client.PostAsync(url, data);

                var result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                throw new Exception(ex.Message);
            }
        }

        public async Task<TokenResponseViewModel> LoginWithOtp(LoginWithOtpAddCommandModel model)
        {
            var user = await base._dbSet
                .SingleOrDefaultAsync(s => s.OtpCode == model.OtpCode &&s.MobileNo==model.MobileNumber && s.IsDeleted == false && s.Disabled == false);
            if (user == null)
            {
                throw new EntityNotFoundException(" شماره موبایل یا کد تایید اشتباه می باشد");
            }

            if (user.OtpCodeExpiry < DateTime.Now)
            {
                throw new ExpiredCodeException("otp code has expired");
            }
            var FullName = user.FirstName + " " + user.LastName;
            var CustomerIds = await base._dbSet.Include(x => x.userCustomers).Where(x => x.Id == user.Id).SelectMany(x => x.userCustomers).Select(x => x.CustumerId).ToListAsync();

            List<string> rolsOfUser = base._dbSet.Include(x => x.UserRoles).ThenInclude(x => x.Role).Where(x => x.Id == user.Id).SelectMany(x => x.UserRoles).Select(x => x.Role.Alias).ToList();
            List<string> AppsofUser = base._dbSet.Include(x => x.userApps).ThenInclude(x => x.App).Where(x => x.Id == user.Id).SelectMany(x => x.userApps).Select(x => x.App.Name).ToList();

            var result = from e in AppsofUser
                         from d in rolsOfUser
                         select new
                         {
                             AppNAme = e,
                             RoleNAme = d
                         };

            var RoleApp = new List<string>();

            foreach (var item in result)
            {
                RoleApp.Add(item.AppNAme + "/" + item.RoleNAme);
            }

            //RoleApp.Add(new { aud = AppsofUser.ToString() });



            if (user == null)
            {
                return null;
            }
            var symmetricSecurityKey = Encoding.UTF8.GetBytes(Configuration["LoginSettings:loginOTP"]);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKey), SecurityAlgorithms.HmacSha256);

            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));
            //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



            var cliammm = new List<Claim>();
            cliammm.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            cliammm.Add(new Claim(ClaimTypes.Name, user.UserName));
            cliammm.Add(new Claim(ClaimTypes.UserData, user.rowLevelAccess.ToString()));
            cliammm.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            cliammm.Add(new Claim("FirstName", user.FirstName));
            cliammm.Add(new Claim("LastName", user.LastName));
            cliammm.Add(new Claim("CustomerId", user.CustomerId));
            cliammm.Add(new Claim("CustomerName", user.CustomerName));
            //foreach (var item in CustomerIds)
            //{
            //    cliammm.Add(new Claim(ClaimTypes.Actor, item.ToString()));
            //}



            //cliammm.Add(new Claim(ClaimTypes.GivenName, FullName));
            //cliammm.AddRange(rolsOfUser.Select(CustomerIds => new Claim(ClaimTypes.Country, CustomerIds.ToString())));
            cliammm.AddRange(RoleApp.Select(role => new Claim(ClaimTypes.Role, role)));
            string combinedString = string.Join(",", AppsofUser.ToArray());
            //var tokenOptions = new JwtSecurityToken(

            //    issuer: "http://localhost:5262",
            //    claims: cliammm,
            //    expires: DateTime.Now.AddDays(30),
            //    signingCredentials: signinCredentials,
            //    audience: combinedString
            //);


            var jwt = new JwtSecurityToken(
               issuer: "Titec",
               // (audience): Recipient for which the JWT is intended : Application Name or Area
               audience: combinedString,
               claims: cliammm,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddMinutes(30),  //x min expiry and a client monitor token quality and should request new token with this one expiries
               signingCredentials: signingCredentials);




            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new TokenResponseViewModel()
            {
                Expiration = DateTime.UtcNow.AddMinutes(30).ToString(),
                Token = tokenString
            };

        }

        public async Task<UserEntity> GetUserWithUserNameAndEmail(LoginAddCommandModel model)
        {
            var password = passwordHelper.EncodePasswordMd5(model.Password);

            var user = await base._dbSet
                .SingleOrDefaultAsync(s => s.UserName == model.UserName.ToLower().Trim() && s.Password == password);
            return user;
        }

        public async Task<UserEntity> GetUserById(int userId)
        {
            var usere = await base._dbSet.FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == false);
            return usere;
        }

        public async Task<UserEntity> EditUser(UserEntity user)
        {
            await base.EditAsync(user);
            return user;
        }

        public async Task<int> FindUserIdByUserName(string UserName)
        {
            return await base._dbSet.Where(c => c.IsDeleted == false && c.UserName == UserName).Select(c => c.Id).FirstOrDefaultAsync();
        }

        public async Task<int> FindUserIdByEmail(string Email)
        {
            return await base._dbSet.Where(c => c.IsDeleted == false && c.Email == Email).Select(c => c.Id).FirstOrDefaultAsync();
        }

        public async Task<int> FindUserIdByMobileNumber(string MobileNo)
        {
            return await base._dbSet.Where(c => c.IsDeleted == false && c.MobileNo == MobileNo).Select(c => c.Id).FirstOrDefaultAsync();
        }

        public Task<RoleToUserBaseViewModel> AddRoleToUser(RoleToUserAddCommandModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UsersWithRolsBaseViewModel>> GetUsersWithRols()
        {
            var res = await base._dbSet.Include(c => c.UserRoles).ThenInclude(c => c.Role)
              .ToListAsync();
            return res.Select(c => new UsersWithRolsBaseViewModel()
            {
                RoleName = c.UserRoles.Select(c => c.Role.Name),
                UserId = c.Id,
                UserName = c.UserName
            }).ToList();
        }

        public async Task<List<object>> GetRowLevelAccessList()
        {
            string[] names = Enum.GetNames(typeof(RowLevelAccess));
            var result = new List<object>();
            for (int i = 0; i < names.Length; i++)
            {
                result.Add(new { Id = i + 1, Title = names[i] });
            }
            return result;
        }
    }
}
