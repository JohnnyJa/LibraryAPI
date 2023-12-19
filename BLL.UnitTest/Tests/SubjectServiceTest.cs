using AutoMapper;
using BLL.UnitTest.Mock;
using Library.BLL.Handlers.Subject;
using Library.BLL.Handlers.Subjects;
using Library.BLL.Requests.Author;
using Library.BLL.Requests.Subject;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Library.Mapping.DAL.Profiles;

namespace BLL.UnitTest;

public class SubjectServiceTest
{
    private readonly IMapper _mapper;
    private IRepository<Subject> _repository;
    public SubjectServiceTest()
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
        _repository = mockFabric.GetSubjectRepository().Object;
    }
    
    [Test]
    public async Task CreateSubject_Subject_Success()
    {
        var request = new CreateSubjectRequest()
        {
            Name = "Name3"
        };
        var requestHandler = new CreateSubjectRequestHandler(_mapper, _repository);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(_repository.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public async Task GetSubjectById_Id_Subject()
    {
        var request = new GetSubjectByIdRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000")
        };
        var requestHandler = new GetSubjectByIdRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo("Subject1"));
    }
    
    [Test]
    public async Task UpdateSubject_Subject_Success()
    {
        var request = new UpdateSubjectRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000"),
            Name = "Name3",
        };
        var requestHandler = new UpdateSubjectRequestHandler(_repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(_repository.FirstOrDefault(a => a.Id == request.Id)!.Name, Is.EqualTo("Name3"));
    }
    
    [Test]
    public async Task DeleteSubject_Id_Success()
    {
        var request = new DeleteSubjectRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000")
        };
        var requestHandler = new DeleteSubjectRequestHandler(_repository);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(_repository.Count(), Is.EqualTo(1));
    }
}