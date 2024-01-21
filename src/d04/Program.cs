using d04.Model;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../.."))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var bookReviewsPath = configuration["FilePaths:BookReviews"];
        var movieReviewsPath = configuration["FilePaths:MovieReviews"];

        var bookReviews = JsonLoader.LoadBookReviews(bookReviewsPath);
        var movieReviews = JsonLoader.LoadMovieReviews(movieReviewsPath);

        Console.WriteLine("Book Reviews:");
        PrintReviews(bookReviews);

        Console.WriteLine("\nMovie Reviews:");
        PrintReviews(movieReviews);
        
        if (args.Length > 0 && args[0].ToLower() == "best")
        {
            var bestBook = MediaSearcher.FindBest(bookReviews, b => -b.Rank);
            var bestMovie = MediaSearcher.FindBest(movieReviews, m => m.IsCriticsPick != 0 ? 1 : 0);

            DisplayBestItems(bestBook, bestMovie);
        }
        else
        {
            Console.Write("\nInput search text: ");
            string searchTerm = Console.ReadLine() ?? "";

            var bookSearchResults = bookReviews.Search(searchTerm);
            var movieSearchResults = movieReviews.Search(searchTerm);

            Console.WriteLine($"\nItems found: {bookSearchResults.Length + movieSearchResults.Length}\n");

            PrintSearchResults(bookSearchResults, "Book");
            PrintSearchResults(movieSearchResults, "Movie");
        }
    }
    
    static void PrintReviews<T>(IEnumerable<T> reviews) where T : ISearchable
    {
        foreach (var review in reviews.OrderBy(r => r.Title))
        {
            Console.WriteLine(review.ToString());
        }
    }
    
    static void PrintSearchResults<T>(T[] searchResults, string mediaType)
        where T : ISearchable
    {
        Console.WriteLine($"{mediaType} search result [{searchResults.Length}]:");

        foreach (var item in searchResults)
        {
            Console.WriteLine($"- {item}");
        }

        Console.WriteLine();
    }

    static void DisplayBestItems(BookReview? bestBook, MovieReview? bestMovie)
    {
        Console.WriteLine("Best in books:");
        Console.WriteLine($"- {bestBook}");

        Console.WriteLine("\nBest in movie reviews:");
        Console.WriteLine($"- {bestMovie}");
    }
}