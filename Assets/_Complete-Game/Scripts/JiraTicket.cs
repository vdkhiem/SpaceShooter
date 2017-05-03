using System;

namespace _2ToTango
{
    [Serializable]
    public class JiraTicket
    {
        public string Key { get; set; }
        public string Priority { get; set; }
        public string Reporter { get; set; }
        public string Assignee { get; set; }
        public string Epic { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
