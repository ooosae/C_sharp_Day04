namespace d04.Model;

public class MovieReview : ISearchable
{
    public string Title { get; set; } = "";
    public string MpaaRating { get; set; } = "";
    public int IsCriticsPick { get; set; }
    public string SummaryShort { get; set; } = "";
    public string Url { get; set; } = "";

    public override string ToString()
    {
        return $"{Title} {(IsCriticsPick != 0 ? "[NYT critic’s pick]" : "")}\n{SummaryShort}\n{Url}";
    }
}