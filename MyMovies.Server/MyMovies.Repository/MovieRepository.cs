using Microsoft.EntityFrameworkCore;
using MyMovies.DataAccess;
using MyMovies.Models.Entities;
using MyMovies.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Repositories.Repositories
{
    public class MovieRepository : IMovieRepository
    { 
        private readonly MovieDbContext _dbContext;
        public MovieRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Movie> GetAll()
        {
            try
            {
                return _dbContext.Set<Movie>();
            }
            catch (Exception e)
            {
                throw new Exception($"Entities cannot be retrieved. {e.Message}", e);
            }
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            try
            {
                return await GetAll().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Entities cannot be retrieved. {e.Message}", e);
            }
        }


        public async Task<Movie> AddAsync(Movie movie)
        {
            if (movie == null)
            {
                throw new Exception("Entity cannot be null.");
            }

            try
            {
                await _dbContext.AddAsync(movie);
                await _dbContext.SaveChangesAsync();

                return movie;
            }
            catch (Exception e)
            {
                throw new Exception($"Entity cannot be saved. {e.Message}", e);
            }
        }

        public async Task<Movie> UpdateAsync(Movie movie)
        {
            if (movie == null)
            {
                throw new Exception("Entity cannot be null");
            }

            try
            {
                _dbContext.Update(movie);
                await _dbContext.SaveChangesAsync();

                return movie;
            }
            catch (Exception e)
            {
                throw new Exception($"Entity cannot be updated. {e.Message}", e);
            }
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
