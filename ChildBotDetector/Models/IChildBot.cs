namespace ChildBotDetector.Models
{
    public interface IChildBot
    {
        string Name { get; set; }
        string Description { get; set; }
         string AppId { get; set; }
    }
}