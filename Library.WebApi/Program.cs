using System.Net.NetworkInformation;
using Library.DAL.Contexts;
using Library.DAL.Repository;
using Library.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Library.BLL.Commands.Books;
using Library.Mapping.DAL.Profiles;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateBookRequestHandler>());
builder.Services.AddAutoMapper(typeof(LibraryProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();