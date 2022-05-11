using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMovies.DataAccess;
using MyMovies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Services
{
    public class MovieService : IMovieService
    {

        private readonly MovieDbContext _dbContext;
        public MovieService(MovieDbContext dbContext)
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


        public async Task<Movie> AddAsync(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentException("Entity cannot be null.");
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

        public async Task<Movie> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id is not correct.");
            }

            try
            {
                var movies = await GetAllAsync();
                var movie = movies.Find(movie => movie.Id == id);

                return movie;
            }
            catch (Exception e)
            {
                throw new Exception($"Entity cannot be retrieved. {e.Message}", e);
            }
        }

        public async Task<Movie> UpdateAsync(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentException("Entity cannot be null");
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

        public async Task<int> DeleteAsync(int id)
        {

            if (id <= 0)
            {
                throw new ArgumentException($"Id is not correct.");
            }
            try
            {
                var movies = await GetAllAsync();
                _dbContext.Remove(movies.Find(prp => prp.Id == id));
                await _dbContext.SaveChangesAsync();

                return id;
            }
            catch (Exception e)
            {
                throw new Exception($"Entity cannot be deleted. {e.Message}", e);
            }
        }
    }
}
