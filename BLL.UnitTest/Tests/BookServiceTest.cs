using AutoMapper;
using BLL.UnitTest.Mock;
using Library.BLL.Commands.Books;
using Library.BLL.Handlers.Books;
using Library.BLL.Requests.Book;
using Library.DAL.Entities;
using Library.DAL.Repository.IRepository;
using Library.Mapping.DAL.Profiles;

namespace BLL.UnitTest;

public class BookServiceTest
{
    private readonly IMapper _mapper;
    private IRepository<Book> _repository;
    public BookServiceTest()
    {
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new BookProfile());
        }).CreateMapper();
    }
    
    [SetUp]
    public void Setup()
    {
        var mockFabric = new MockFabric();
        _repository = mockFabric.GetBookRepository().Object;
    }

    [Test]
    public async Task CreateBook_Book_Success()
    {
        var request = new CreateBookRequest()
        {
            Name = "Name3",
            ISBN = "ISBN3",
            NumberOfCopies = 3,
            Author = new Author()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Name = "Name",
                Surname = "Surname"
            },
            Subject = new Subject()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Name = "Subject1",
            }
        };
        var requestHandler = new CreateBookRequestHandler( _repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(_repository.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public async Task GetBookById_Id_Book()
    {
        var request = new GetBookByIdRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000")
        };
        var requestHandler = new GetBookByIdRequestHandler(_repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo("Book1"));
    }
    
    [Test]
    public async Task UpdateBook_Book_Success()
    {
        var request = new UpdateBookRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000"),
            Name = "Name3",
            ISBN = "ISBN3",
            NumberOfCopies = 3,
            Author = new Author()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Name = "Name",
                Surname = "Surname"
            },
            Subject = new Subject()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Name = "Subject1",
            }
        };
        var requestHandler = new UpdateBookRequestHandler(_repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(_repository.FirstOrDefault(a => a.Id == request.Id)!.Name, Is.EqualTo("Name3"));
    }
    
    [Test]
    public async Task DeleteBook_Id_Success()
    {
        var request = new DeleteBookRequest()
        {
            Id = new Guid("00000000-0000-0000-0000-000000000000")
        };
        var requestHandler = new DeleteBookRequestHandler(_repository);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(_repository.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public async Task GetAllBook_Id_Success()
    {
        var request = new GetAllBooksRequest();
        var requestHandler = new GetAllBooksRequestHandler(_repository, _mapper);
        var result = await requestHandler.Handle(request, CancellationToken.None);
        Assert.IsFalse(result.IsError);
        Assert.That(_repository.Count(), Is.EqualTo(2));
    }
    
    // [Test]
    // public async Task SearchBookByName_Id_Success()
    // {
    //     var request = new SearchBookRequest()
    //     {
    //         Name = "Book1"
    //     };
    //     var requestHandler = new SearchBookByNameRequestHandler(_repository, _mapper);
    //     var result = await requestHandler.Handle(request, CancellationToken.None);
    //     Assert.IsFalse(result.IsError);
    //     Assert.That(_repository.Count(), Is.EqualTo(2));
    // }
}