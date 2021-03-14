using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieServiceBase = Grpc.Services.MovieService.MovieServiceBase;

namespace MovieGrpcService
{
    public class MovieService : MovieServiceBase
    {
        private readonly ILogger<MovieService> _logger;
        public MovieService(ILogger<MovieService> logger)
        {
            _logger = logger;
        }

        public override Task<MovieResponse> GetMovies(Empty request, ServerCallContext context)
        {
            var moviesResponse = new MovieResponse();
            moviesResponse.Movies.Add(new Movie { Title = "Title_0", Year = 2000, ImageUrl = "http://wwww.aaa.com/images/0", Genre = Genre.Action });
            moviesResponse.Movies.Add(new Movie { Title = "Title_1", Year = 2001, ImageUrl = "http://wwww.aaa.com/images/1", Genre = Genre.Comedy });
            moviesResponse.Movies.Add(new Movie { Title = "Title_2", Year = 2002, ImageUrl = "http://wwww.aaa.com/images/2", Genre = Genre.Drama });
            moviesResponse.Movies.Add(new Movie { Title = "Title_3", Year = 2003, ImageUrl = "http://wwww.aaa.com/images/3", Genre = Genre.Scifi });
            moviesResponse.Movies.Add(new Movie { Title = "Title_4", Year = 2004, ImageUrl = "http://wwww.aaa.com/images/4", Genre = Genre.Thriller });
            return Task.FromResult(moviesResponse);
        }

        public override async Task GetMoviesStream(Empty request, IServerStreamWriter<Movie> responseStream, ServerCallContext context)
        {
            var i = 0;
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(
                    new Movie
                    {
                        Title = $"Title_{i}",
                        Year = 2000 + i,
                        ImageUrl = $"http://wwww.aaa.com/images/{i}",
                        Genre = (Genre)(i % 6)
                    });

                await Task.Delay(1000);
                i++;
            }
        }
    }
}
