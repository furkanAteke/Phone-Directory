using Core.Abstracts.Services;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService<User>
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;

        }
        public async Task<User> AddAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Eklenecek kullanıcı boş olamaz.");
            }
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"ID {id} ile eşleşen bir kullanıcı bulunamadı.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Detail(int id)
        {
            var user = await _context.Users
                                     .Include(u => u.UserDetail) 
                                     .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"ID {id} ile eşleşen bir kullanıcı bulunamadı.");
            }

            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.UserDetail).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users
                         .Include(u => u.UserDetail)
                         .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"ID {id} ile eşleşen bir kullanıcı bulunamadı.");
            }

            return user;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var user = await _context.Users.Include(u => u.UserDetail).FirstOrDefaultAsync(u => u.Id == entity.Id);
            if (user == null)
            {
                throw new KeyNotFoundException($"ID {entity.Id} ile eşleşen bir kullanıcı bulunamadı.");
            }

            user.Ad = entity.Ad;
            user.Soyad = entity.Soyad;
            user.Numara = entity.Numara;

            if (user.UserDetail != null)
            {
                user.UserDetail.ImageUrl = entity.UserDetail.ImageUrl;
                user.UserDetail.Numara2 = entity.UserDetail.Numara2;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
