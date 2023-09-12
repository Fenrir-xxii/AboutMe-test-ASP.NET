namespace WebApplication2_AboutMe.Models;

public class Task
{
	public int Id { get; set; }
	public string Title { get; set; }
	public DateTime Date { get; set; }
	public bool IsCompleted { get; set; }
	public virtual User User { get; set; }

}
