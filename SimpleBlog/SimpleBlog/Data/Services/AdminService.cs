using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Models;
using SimpleBlog.Data.Services.Interfaces;

namespace SimpleBlog.Data.Services
{
    public class AdminService : IAdminService
    {
        private readonly BlogContext _context;

        public AdminService(BlogContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminAsync()
            => await Task.FromResult(_context.Admins.Include(x => x.Posts).SingleOrDefault());
    }
}
