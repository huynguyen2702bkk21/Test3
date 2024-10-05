using Microsoft.EntityFrameworkCore;
using WebAPISchoolTest.Domain.SeedWork;

namespace WebAPISchoolTest.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class> GetClassByIdAsync(int id)
        {
            return await _context.Classes.FindAsync(id);
        }

        public async Task AddClassAsync(Class classObj)
        {
            _context.Classes.Add(classObj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClassAsync(Class classObj)
        {
            _context.Entry(classObj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(int id)
        {
            var classObj = await _context.Classes.FindAsync(id);
            if (classObj == null)
            {
                throw new ArgumentException("Class not found.");
            }

            _context.Classes.Remove(classObj);
            await _context.SaveChangesAsync();
        }

        public IUnitOfWork UnitOfWork => _context; // Triển khai IUnitOfWork
    }
}
