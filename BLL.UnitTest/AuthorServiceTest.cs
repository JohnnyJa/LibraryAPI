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
    private readonly MockFabric _mockFabric = new MockFabric();
    public AuthorServiceTest()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AuthorProfile());
            cfg.AddProfile(new BookProfile());
        }).CreateMapper();
    }
    
    [SetUp]
    public void Setup()
    {
        _repository = _mockFabric.GetAuthorRepository().Object;
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
}