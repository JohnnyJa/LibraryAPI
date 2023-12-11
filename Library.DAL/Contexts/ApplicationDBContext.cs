using Library.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Contexts;

public class ApplicationDBContext :IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options){ }

    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<ReaderFormulary> ReaderFormularies { get; set; }
}