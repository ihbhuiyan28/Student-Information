using Microsoft.EntityFrameworkCore;
using StudentInformation.Models;

namespace StudentInformation.Data
{
    public class StudentInformationContext : DbContext
    {
        public StudentInformationContext(DbContextOptions<StudentInformationContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
    }
}
