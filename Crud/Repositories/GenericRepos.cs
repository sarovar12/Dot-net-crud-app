using Crud.Data;
using Crud.DTOs.InformationDTOs;
using CrudApp.Models;
using Microsoft.EntityFrameworkCore;
namespace Crud.Repositories
{
    public class GenericRepos : IGenericRepos
    {
        private readonly CrudContext _context;
        public GenericRepos(CrudContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAll<T>() where T : class
        {
            return await this._context.Set<T>().ToListAsync();
        }
        public async Task<T> GetOne<T>(int id) where T : class
        {
            if (_context.Information == null)
            {
                return null;
            }
            var info = await this._context.Set<T>().FindAsync(id);
            if (info == null)
            {
                return null;
            }
            return info;
        }
        public async Task<T> Add<T>(T tObj) where T : class
        {
            _context.Set<T>().Add(tObj);
            await this._context.SaveChangesAsync();
            if (tObj == null)
            {
                throw new Exception();
            }
            return tObj;
        }
        public async Task<T> Update<T>(int id, T tObj) where T : class
        {
            
            _context.Set<T>().Update(tObj);
            await _context.SaveChangesAsync();
            return tObj;
        }
        public async Task Delete<T>(int id) where T : class
        {
            var tObj = await _context.Set<T>().FindAsync(id);
            if (tObj == null)
            {
                throw new Exception();
            }
            _context.Set<T>().Remove(tObj);
            await _context.SaveChangesAsync();
            return;
        }
    }
}