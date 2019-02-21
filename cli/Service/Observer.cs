using cli.Repository;
using RSSRepository.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace cli.Service
{
    public struct Info{
        public int Checked;
        public int Updated;
    }

    public class Observer
    {
        private readonly ChanelRepository chanelRepository;
        private readonly cli.Repository.RSSRepository rssRepository;
        private readonly IRSSFounder founder = new RSSFounder();

        public Observer(ChanelRepository chanelRepository, cli.Repository.RSSRepository rssRepository)
        {
            this.chanelRepository = chanelRepository;
            this.rssRepository = rssRepository;
        }

        public async Task<Info> Update()
        {
            var info = new Info();
            var chanels = chanelRepository.GetAll();
            foreach(var chan in chanels)
            {
                var rss = await founder.GetRSSFromChanel(chan);
                var i = await CreateAllRSSAsync(rss);
                info.Updated += i.Updated;    
                info.Checked += i.Checked;
            }

            return info;
        }

        private async Task<Info> CreateAllRSSAsync(IEnumerable<RSS> rs)
        {
            Info i = new Info();
            foreach (var r in rs)
            {
                var success = await rssRepository.Create(r);
                if (success)
                {
                    i.Updated++;
                }

                i.Checked++;
            }

            return i;
        }
    }
}
