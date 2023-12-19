using AutoMapper;
using BLL.UnitTest.Mock;
// using BLL.UnitTest.Mock;
using Library.BLL.Handlers.Authors;
using Library.BLL.Requests.Author;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Library.Mapping.DAL.Profiles;
using MockQueryable.Moq;
using Moq;

namespace BLL.UnitTest;

public class AuthorServiceTest
{
    private readonly IMapper _mapper;
    private IRepository<Author> _repository;
    public AuthorServiceTest()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new LibraryProfile());
        }).CreateMapper();
    }
    
    [SetUp]
    public void Setup()
    {
        var mockFabric = new MockFabric();
        _repository = mockFabric.GetAuthorRepository().Object;
    }

    [Test]
    public async Task CreateAuthor_Author_Success()
    {
        var request = new CreateAuthorRequest()
        {
            Name = "Name3",
            Surname = "Surname3"
        };
        var requestHandler = new CreateAuthorRequestHandler(_mapper, _repository);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(_repository.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public async Task GetAuthorById_Id_Author()
    {
        var request = new GetAuthorByIdRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000")
        };
        var requestHandler = new GetAuthorByIdRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo("Name"));
    }
    
    [Test]
    public async Task UpdateAuthor_Author_Success()
    {
        var request = new UpdateAuthorRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000"),
            Name = "Name3",
            Surname = "Surname3"
        };
        var requestHandler = new UpdateAuthorRequestHandler(_repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(_repository.FirstOrDefault(a => a.Id == request.Id)!.Name, Is.EqualTo("Name3"));
    }
    
    [Test]
    public async Task DeleteAuthor_Id_Success()
    {
        var request = new DeleteAuthorRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000")
        };
        
        var requestHandler = new DeleteAuthorRequestHandler(_repository);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        
        Assert.IsFalse(result.IsError);
        Assert.That(_repository.Count(), Is.EqualTo(1));
    }
}