namespace WebAPISchoolTest.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                throw new KeyNotFoundException("Teachers not found");
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<TeacherDetailsList> GetTeacherDetails(int id)
        {
            // Tìm giáo viên theo id
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.TeacherID == id);

            if (teacher == null)
            {
                throw new KeyNotFoundException("Teacher not found");
            }

            // Lấy thông tin lớp mà giáo viên dạy
            var classes = await _context.Classes
                .Where(c => c.TeacherID == teacher.TeacherID)
                .ToListAsync();

            // Lấy danh sách học sinh từ các lớp đó
            var students = await _context.Students
                .Where(s => classes.Select(c => c.ClassID).Contains(s.ClassID))
                .ToListAsync();

            // Tạo đối tượng DTO để trả về thông tin
            var teacherDetails = new TeacherDetailsList
            {
                TeacherID = teacher.TeacherID,
                Name = teacher.Name,
                Age = teacher.Age,
                Classes = classes.Select(c => new ClassList
                {
                    ClassID = c.ClassID,
                    ClassName = c.ClassName,
                    Students = students.Where(s => s.ClassID == c.ClassID).Select(s => new StudentList
                    {
                        StudentID = s.StudentUUID,
                        Name = s.Name,
                        Age = s.Age,
                        GPA = s.GPA
                    }).ToList()
                }).ToList()
            };

            return teacherDetails;
        }


        public async Task<bool> TeacherExistsAsync(int id)
        {
            return await _context.Teachers.AnyAsync(e => e.TeacherID == id);
        }
    }
}
