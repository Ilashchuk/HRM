﻿using HRM.Data;
using HRM.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM.Services.RequestsServices
{
    public class RequestsControlService : IRequestsControlService
    {
        private readonly HRMContext _context;
        public RequestsControlService(HRMContext context)
        {
            _context = context;
        }
        public async Task<List<Request>> GetRequestListForCurrentUserAsync(User? currentUser)
        {
            var HR = await _context.RoleTypes.FirstOrDefaultAsync(r => r.Name == "HR");
            if (HR != null && currentUser != null)
            {
                List<Request> requests = await GetListAsync();
                if (currentUser.RoleType == HR)
                    return requests;
                return requests.Where(u => u.UserId == currentUser.Id).ToList();
            }
            return new List<Request>();
        }
        public async Task<List<Request>> GetListAsync() => await _context.Requests
            .Include(u => u.User)
            .Include(u => u.RequestType)
            .Include(u => u.Status).ToListAsync();
        public async Task<Request?> GetByIdAsync(int? id) => await _context.Requests
            .Include(u => u.User)
            .Include(u => u.RequestType)
            .Include(u => u.Status).FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(Request t)
        {
            _context.Requests.Add(t);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Request t)
        {
            _context.Requests.Remove(t);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Request t)
        {
            _context.Requests.Update(t);
            await _context.SaveChangesAsync();
        }
        public bool NotEmpty()
        {
            return (_context.Requests?.Any()).GetValueOrDefault();
        }
        public bool Exists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}