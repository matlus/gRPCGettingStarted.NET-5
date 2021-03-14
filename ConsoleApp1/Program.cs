using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Services;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
            var movieServiceClient = new MovieService.MovieServiceClient(grpcChannel);
            var movieResponse = await movieServiceClient.GetMoviesAsync(new Empty());

            foreach (var movie in movieResponse.Movies)
            {
                PrintMovie(movie);
            }

            Console.WriteLine("______________________________");

            var asynServerStreamingCall = movieServiceClient.GetMoviesStream(new Empty());
            await foreach (var movie in asynServerStreamingCall.ResponseStream.ReadAllAsync())
            {
                PrintMovie(movie);
            }

            Console.ReadKey();
        }

        private static void PrintMovie(Movie movie) => Console.WriteLine($"Title: {movie.Title}\tYear: {movie.Year}\t Image Url: {movie.ImageUrl}\tGenre: {movie.Genre}");
    }
}
