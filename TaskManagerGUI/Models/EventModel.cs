namespace TaskManagerGUI.Models
{
    public class EventModel
    {
        public string Index { get; set; }
        public DateTime Time { get; set; }
        public string EntryType { get; set; }
        public string Source { get; set; }
        public string InstanceId { get; set; }
        public string Message { get; set; }
    }
}
