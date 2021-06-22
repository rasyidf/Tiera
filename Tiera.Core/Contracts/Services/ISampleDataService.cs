using System.Collections.Generic;
using System.Threading.Tasks;

using Tiera.Core.Models;

namespace Tiera.Core.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();

        Task<IEnumerable<SampleOrder>> GetGridDataAsync();
    }
}
