﻿//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////

using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly DbConn _db;
        public UserRepository(DbConn dbConn)
        {
            _db = dbConn;
        }
        public async Task<BaseResponse> Create(User User)
        {
            var check_user =  await _db.Users.FirstOrDefaultAsync(m => m.UserName == User.UserName) ; 

            if ( check_user == null )
            {
                var res = _db.Users.Add(User);
                await _db.SaveChangesAsync();
                return new BaseResponse { IsSuccess = true , Message = "تم اضافة المستخدم بنجاح" };
            }
            else
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "لم تتم الاضافة المستخدم موجود بالفعل",
                };
            }
        }

        public Task<BaseResponse> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll() => _db.Users.ToList();

        public async Task<User> GetById(Guid id)
        {
            User user = await _db.Users.FindAsync(id);
            if ( user != null )
                return user;

            return null; 
        }

        public async Task<User> GetByName(string Name)
        {
            User user = await _db.Users.FirstOrDefaultAsync(m => m.UserName == Name);

            return user; 
        }

        public async Task<int> TotalUsers()
        {
            var userscount = await _db.Users.CountAsync();

            return userscount ; 
        }

        public async Task<BaseResponse> Update(User User)
        {
            var DbUser = await  _db.Users.FindAsync(User.ID);
            if (DbUser != null)
            {
                User.Create_At = DbUser.Create_At;
                User.Created_by = DbUser.Created_by;

                _db.Entry(DbUser).CurrentValues.SetValues(User);
                await _db.SaveChangesAsync(true);
                return new BaseResponse { IsSuccess = true, Message = "تم تحديث بيانات المستخدم " };

            }
            else
            {
                return new BaseResponse() { IsSuccess = false, Message = "الرجاء  التحقق من العملية " };
            }
        }
    }
}
