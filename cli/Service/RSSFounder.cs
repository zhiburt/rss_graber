using RSSRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace cli.Service
{
    public class RSSFounder : IRSSFounder
    {
        public async Task<IEnumerable<RSS>> GetRSSFromChanel(RSSChanel res)
        {
            string rss = await GetRSSByURL(res.URL);
            return ParseRSS(rss);
        }

        private async Task<string> GetRSSByURL(string url)
        {
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/rss+xml"));
            var res = await new HttpClient().SendAsync(req);
            return await res.Content.ReadAsStringAsync();
        }

        private IEnumerable<RSS> ParseRSS(string RSS)
        {
            RSS = DoRemovespace(RSS);
            XDocument xdoc = XDocument.Parse(RSS, LoadOptions.PreserveWhitespace);
            var items = from item in xdoc.Descendants().Elements("item")
                        select item;

            var resp = new List<RSS>();
            foreach(var item in items)
            {
                var rss = new RSS
                {
                    Title = item.Element("title").Value,
                    Description = item.Element("description").Value,
                    LinkToOriginal = item.Element("link").Value,
                    Date = DateTime.Parse(item.Element("pubDate").Value)
                };

                resp.Add(rss);
            }

            return resp;
        }

        private static string DoRemovespace(string strFile)
        {

            strFile = strFile.Replace("\n", "");
            strFile = strFile.Replace("\r", "");
            Regex regex = new Regex(@">\s*<");
            return regex.Replace(strFile, "><"); ;
        }
    }
}
