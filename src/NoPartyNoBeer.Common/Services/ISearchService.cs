using RedDog.Search.Http;
using RedDog.Search.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Common.Services
{
    interface ISearchService
    {
        Task<IApiResponse<Index>> CreateIndexAsync(Index index);

        Task<IApiResponse<IEnumerable<IndexOperationResult>>> PopulateIndexAsync(string indexName, IndexOperation[] data);

    }
}
