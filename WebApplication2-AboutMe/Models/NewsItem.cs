namespace WebApplication2_AboutMe.Models;

public class NewsItem
{
	public int Id { get; set; }	
	public string Title { get; set; }
	public string? Image { get; set; }
	[System.Text.Json.Serialization.JsonIgnore]
	public string ImageUrl => "/uploads/images/news/" + Image;
	public DateTime CreatedAt { get; set; }
	public string ShortDescription { 
		get 
		{
			if (string.IsNullOrEmpty(FullDescription))
			{
				return "";
			}
			if(FullDescription.Length>100)
			{
                var temp = FullDescription.Substring(0, 97) + "...";
				return temp;
            }
			return FullDescription.Substring(0, Math.Min(FullDescription.Length, 100));
		} 
	}
	public string FullDescription { get; set; }
}
