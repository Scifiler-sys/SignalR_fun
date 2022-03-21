namespace Backend.Models
{
    /// <summary>
    /// Deprecated
    /// </summary>
    public class Chat
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }

        public Chat()
        {
            Data = new List<int>();
            Label = "";
        }
    }
}