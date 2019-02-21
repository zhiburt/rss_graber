using System.Collections.Generic;
using System.Threading.Tasks;
using RSSRepository.Models;

namespace cli.Service
{
    public interface IRSSFounder
    {
        Task<IEnumerable<RSS>> GetRSSFromChanel(RSSChanel res);
    }
}