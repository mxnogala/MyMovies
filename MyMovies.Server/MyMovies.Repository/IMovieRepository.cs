using MyMovies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Repositories.Interfaces
{
    public interface IMovieRepository 
    {
        IQueryable<Movie> GetAll();
        Task<List<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> AddAsync(Movie movie);
        Task<Movie> UpdateAsync(Movie movie);
        Task<int> DeleteAsync(int id);
    }
}
