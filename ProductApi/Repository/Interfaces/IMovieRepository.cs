
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Models;

namespace ProductApi.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetByID(int id);
        Task<List<MovieDisplay>> GetActiveAllMovies();
        Task<Category> GetCategory(int id);
        Task<List<Category>> GetAllCategory();
    }
}