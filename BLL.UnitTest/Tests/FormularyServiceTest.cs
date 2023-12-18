using AutoMapper;
using BLL.UnitTest.Mock;
using Library.BLL.Handlers.Formulary;
using Library.BLL.Requests.Formulary;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Library.Mapping.DAL.Profiles;

namespace BLL.UnitTest;

public class FormularyServiceTest
{
    private readonly IMapper _mapper;
    private IRepository<ReaderFormulary> _repository;
    private IRepository<Book> _booksRepository;

    public FormularyServiceTest()
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
        _repository = mockFabric.GetFormularyRepository().Object;
        _booksRepository = mockFabric.GetBookRepository().Object;
    }
    
    [Test]
    public async Task CreateFormulary_Formulary_Success()
    {
        var request = new CreateFormularyRequest()
        {
            Name = "ReaderName3",
            Surname = "ReaderSurname3",
        };
        var requestHandler = new CreateFormularyRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(_repository.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public async Task GetAllFormulary_Empty_Success()
    {
        var request = new GetAllFormularyRequest();
        var requestHandler = new GetAllFormularyRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Count, Is.EqualTo(2));
    }
    
    [Test]
    public async Task GetFormularyById_Id_Success()
    {
        var request = new GetFormularyByIdRequest()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
        };
        var requestHandler = new GetFormularyByIdRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo("ReaderName1"));
    }
    
    [Test]
    public async Task UpdateFormulary_Formulary_Success()
    {
        var request = new UpdateFormularyRequest()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
            Name = "ReaderName5",
            Surname = "ReaderSurname5"
        };
        var requestHandler = new UpdateFormularyRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo("ReaderName5"));
        Assert.That(_repository.FirstOrDefault(f => f.Name == "ReaderName5"), Is.Not.Null);
    }
    
    [Test]
    public async Task DeleteFormulary_Id_Success()
    {
        var request = new DeleteFormularyRequest()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
        };
        var requestHandler = new DeleteFormularyRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(_repository.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public async Task TakeBookByReader_BookAndReader_Success()
    {
        var request = new TakeBookByReaderRequest()
        {
            ReaderId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
            BookIds = new List<Guid>(){ Guid.Parse("00000000-0000-0000-0000-000000000000")}
        };
        var requestHandler = new TakeBookByReaderRequestHandler( _repository, _booksRepository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);

        var reader = _repository.SingleOrDefault(f => f.Id == request.ReaderId);
        
        Assert.That(reader!.TakenBooks.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task ReturnBookByReader_BookAndReader_Success()
    {
        var request = new ReturnBookByReaderRequest()
        {
            ReaderId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
            BookIds = new List<Guid>(){ Guid.Parse("00000000-0000-0000-0000-000000000000")}
        };
        var requestHandler = new ReturnBookByReaderRequestHandler(_repository, _booksRepository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);

        var reader = _repository.SingleOrDefault(f => f.Id == request.ReaderId);

        Assert.That(reader!.TakenBooks.Count, Is.EqualTo(0));
    }
}