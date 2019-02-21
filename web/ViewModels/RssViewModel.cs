using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.ViewModels
{
    public class RSSViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LinkToOriginal { get; set; }
        public DateTime Date { get; set; }
    }
}
