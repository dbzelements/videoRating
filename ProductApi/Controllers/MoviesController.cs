using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository.Interfaces;
using ProductApi.Repository;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : BaseController
    {
        public MoviesController(IConfiguration configuration) : base(configuration) { }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "test1", "test2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // GET api/movies/movie/5
        [HttpGet("movie/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Movie>> GetByIdAsync(int id)
        {
            using (var repo = new MovieRepository())  {

                var response = await repo.GetByID(id);

                var returnObject = new ObjectResult(response);

                return returnObject;
            }
      
        }

        [HttpGet("all")]    
        public async System.Threading.Tasks.Task<ActionResult<List<MovieDisplay>>> GetAllActiveMovies()
        {
            using (var repo = new MovieRepository())
            {

                var response = await repo.GetActiveAllMovies();

                var returnObject = new ObjectResult(response);

                return returnObject;
            }

        }

        [HttpGet("all/{filter}")]
        public async System.Threading.Tasks.Task<ActionResult<List<MovieDisplay>>> GetAllActiveMoviesFiltered(string filter)
        {
            using (var repo = new MovieRepository())
            {

                var response = await repo.GetActiveAllMoviesFiltered(filter);

                var returnObject = new ObjectResult(response);

                return returnObject;
            }

        }

        [HttpGet("rating/{rating}")]
        public async System.Threading.Tasks.Task<ActionResult<List<MovieDisplay>>> GetAllActiveMoviesRating(int rating)
        {
            using (var repo = new MovieRepository())
            {

                var response = await repo.GetActiveAllMoviesRating(rating);

                var returnObject = new ObjectResult(response);

                return returnObject;
            }

        }


        [HttpGet("all/categories")]
        public async System.Threading.Tasks.Task<ActionResult<List<MovieDisplay>>> GetAllCategories()
        {
            using (var repo = new MovieRepository())
            {

                var response = await repo.GetAllCategory();

                var returnObject = new ObjectResult(response);

                return returnObject;
            }

        }

        // POST api/values
        [HttpPost]
        public void Create([FromBody] Movie value)
        {
            using (var repo = new MovieRepository())
            {

                Movie movie = new Movie()
                {
                    Title = value.Title,
                    Category = value.Category,
                    Rating = value.Rating,
                    IsDeleted = false,
                };

                var objId = repo.create(movie);

            }
        }

    

        // PUT api/movies/5
        [HttpPut("{id}")]
        public async void ChangeMovieAsync([FromRoute] int id, [FromBody] Movie movie)
        {

            using (var repo = new MovieRepository())
            {
                Movie newMovie = await repo.GetByID(id);

                newMovie.Title = movie.Title;
                newMovie.Category = movie.Category;
                newMovie.Rating = movie.Rating;
                movie.DeletedDate = System.DateTime.Now;

                var response = repo.UpdateMovie(id, movie);

            }

        }

        [HttpPut("delete/{id}")]
        public async void Remove([FromRoute] int id)
        {

            using (var repo = new MovieRepository())
            {
                Movie movie = await repo.GetByID(id);

                movie.IsDeleted = true;
                movie.DeletedDate = System.DateTime.Now;

                var response = repo.UpdateMovie(id, movie);

            }

        }

    }
}
