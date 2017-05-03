using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace _2ToTango
{
    [Serializable]
    public class JiraTicketCollection
    {
        public List<JiraTicket> Tickets { get; set; }

        public JiraTicketCollection()
        {
            Tickets = new List<JiraTicket>();
        }

        internal void SaveAs(string filename)
        {
            var serializer = new XmlSerializer(typeof(JiraTicketCollection));

            // Create a new file stream to write the serialized object to a file
            using (var fs = new StreamWriter(filename))
            {
                serializer.Serialize(fs, this);
                fs.Close();
            }
        }
    }
}
