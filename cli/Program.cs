using cli.Service;
using Microsoft.EntityFrameworkCore;
using RSSRepository.Context;
using RSSRepository.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new Exception("need more params for start");
            }

            var command = args[0].ToLower();
            if (command == "add")
            {
                using (var context = new RSSContext())
                {
                    var repo = new cli.Repository.ChanelRepository(context);
                    repo.Create(new RSSChanel()
                    {
                        URL = args[1]
                    }).Wait();

                    Debug.WriteLine("command 1");
                }
            }
            else if (command == "update")
            {
                using (var context1 = new RSSContext())
                using (var context2 = new RSSContext())
                {
                    var chanelRepo = new cli.Repository.ChanelRepository(context1);
                    var rssRepo = new cli.Repository.RSSRepository(context2);
                    var info = new Observer(chanelRepo, rssRepo).Update().Result;

                    Console.WriteLine($"was added {info.Updated}\nwas checked {info.Checked}");
                    Debug.WriteLine("command 22");
                }
            }
        }
    }
}
