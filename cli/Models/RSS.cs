using System;
using System.Collections.Generic;
using System.Text;

namespace RSSRepository.Models
{
    public class RSS
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkToOriginal { get; set; }
        public DateTime Date { get; set; }
    }
}
