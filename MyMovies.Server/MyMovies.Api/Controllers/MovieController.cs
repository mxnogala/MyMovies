using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMovies.DataAccess;
using MyMovies.Models.Entities;
using MyMovies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovies.Api.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            try
            {
                var result = await _movieService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("/api/movies/{id}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            try
            {
                var movie = await _movieService.GetByIdAsync(id);

                return Ok(movie);
            }
            catch (Exception e)
            {
                throw new Exception($"Entity cannot be retrieved. {e.Message}", e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            try
            {
                var entity = await _movieService.AddAsync(movie);
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut] 
        public async Task<IActionResult> UpdateMovie([FromBody] Movie movie)
        {
            try
            {
                var entity = await _movieService.UpdateAsync(movie);
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("/api/movie/{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            try
            {
                await _movieService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}
