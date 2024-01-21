namespace d04.Model;
using System.Text.Json;

public static class JsonLoader
{
    public static List<BookReview> LoadBookReviews(string jsonPath)
    {
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            var root = JsonSerializer.Deserialize<Root>(jsonContent);

            if (root?.results != null)
            {
                return root.results.Select(result => new BookReview
                {
                    Title = result.book_details.FirstOrDefault()?.title ?? "",
                    Author = result.book_details.FirstOrDefault()?.author ?? "",
                    SummaryShort = result.book_details.FirstOrDefault()?.description ?? "",
                    Rank = result.rank,
                    ListName = result.list_name,
                    Url = result.amazon_product_url
                }).ToList();
            }

            return new List<BookReview>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading JSON data: {ex.Message}");
            return new List<BookReview>();
        }
    }

    public static List<MovieReview> LoadMovieReviews(string jsonPath)
    {
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            var movieRoot = JsonSerializer.Deserialize<MovieRoot>(jsonContent);

            if (movieRoot?.results != null)
            {
                return movieRoot.results.Select(result => new MovieReview
                {
                    Title = result.title,
                    MpaaRating = result.mpaa_rating,
                    IsCriticsPick = result.critics_pick,
                    SummaryShort = result.summary_short,
                    Url = result.link.url
                }).ToList();
            }

            return new List<MovieReview>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading movie JSON data: {ex.Message}");
            return new List<MovieReview>();
        }
    }


    public class Isbn
    {
        public string isbn10 { get; set; }
        public string isbn13 { get; set; }
    }

    public class BookDetail
    {
        public string title { get; set; }
        public string description { get; set; }
        public string contributor { get; set; }
        public string author { get; set; }
        public string contributor_note { get; set; }
        public string price { get; set; }
        public string age_group { get; set; }
        public string publisher { get; set; }
        public string primary_isbn13 { get; set; }
        public string primary_isbn10 { get; set; }
    }

    public class Review
    {
        public string book_review_link { get; set; }
        public string first_chapter_link { get; set; }
        public string sunday_review_link { get; set; }
        public string article_chapter_link { get; set; }
    }

    public class Result
    {
        public string list_name { get; set; }
        public string display_name { get; set; }
        public string bestsellers_date { get; set; }
        public string published_date { get; set; }
        public int rank { get; set; }
        public int rank_last_week { get; set; }
        public int weeks_on_list { get; set; }
        public int asterisk { get; set; }
        public int dagger { get; set; }
        public string amazon_product_url { get; set; }
        public List<Isbn> isbns { get; set; }
        public List<BookDetail> book_details { get; set; }
        public List<Review> reviews { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public int num_results { get; set; }
        public string last_modified { get; set; }
        public List<Result> results { get; set; }
    }

    public class Link
    {
        public string type { get; set; }
        public string url { get; set; }
        public string suggested_link_text { get; set; }
    }

    public class MovieResult
    {
        public string title { get; set; }
        public string mpaa_rating { get; set; }
        public int critics_pick { get; set; }
        public string summary_short { get; set; }
        public Link link { get; set; }
    }

    public class MovieRoot
    {
        public List<MovieResult> results { get; set; }
    }
}