using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Services;
using Moq;
    

namespace MovieShop.UnitTests;

[TestClass]
public class MovieServiceUnitTest
{
    private MovieService _sut;
    private static List<Movie> _movies;
    private Mock<IMovieRepository> _mockMovieRepository;
    
    [TestInitialize]
    // [OneTimeSetUp] in NUnit
    public void OneTimeSetup()
    {
        // Create a mock repository, set up the actual class
        _mockMovieRepository = new Mock<IMovieRepository>();
        
        // SUT System Under Test: MovieService => GetTop30GrossingMovies
        // Create new instance, we need to pass the class that implement IMovieRepository, which is _mockMovieRepository
        _sut = new MovieService(_mockMovieRepository.Object);
        
        // Set up the actual method, tell mock Repository whenever someone calls GetTop30GrossingMovies(), return _movies 
        _mockMovieRepository.Setup(m => m.GetTop30GrossingMovies()).ReturnsAsync(_movies);
    }
     
    [ClassInitialize]
    // Call before each and every method runs so that we can get our fake data from this method
    public static void SetUp(TestContext context)
    { 
        // Mock data
        _movies = new List<Movie>
        {
            new Movie {Id =1, Title = "Avengers: Infinity War", Budget = 1200000},
            new Movie {Id = 2, Title = "Avatar", Budget = 1200000 },
            new Movie() { Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000}
        };
    }

    [TestMethod]
    // Method name should be self-descriptive
    public async Task TestListOfHighestGrossingMoviesFromFakeData()
    {
        // AAA
        // Arrange: The process of creating mock object, data, methods, etc
        // Act: Calling the actual method we are testing
        var movies = await _sut.GetTop30GrossingMovies();
        // Check the actual output with expected data
        // Arrange, Act and Assert: Steps to follow when do Unit testing
        // Assert: We are checking the expected value with the actual value and compare those two values
        // We can do many Asserts, if one of the Assert fails, the whole unit test case fails
        Assert.IsNotNull(movies);
        Assert.IsInstanceOfType(movies, typeof(List<MovieCardModel>));
        Assert.AreEqual(3, movies.Count());
    }
}
