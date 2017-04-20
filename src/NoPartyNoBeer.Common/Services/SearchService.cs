using RedDog.Search;
using RedDog.Search.Http;
using RedDog.Search.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoBeerNoParty.Common.Services
{
    public class SearchService : ISearchService
    {
        private readonly string _key;
        private readonly string _connection;
        private readonly ApiConnection _apiClient;
        private IndexManagementClient _indexMgr;
        
        public SearchService(string connection, string key)
        {
            _connection = connection;
            _key = key;
            _apiClient = ApiConnection.Create(connection, key);
        }

        public async Task<IApiResponse<Index>> CreateIndexAsync(Index index)
        {
            _indexMgr = new IndexManagementClient(_apiClient);
            return await _indexMgr.CreateIndexAsync(index);
        }

        public async Task<IApiResponse<IEnumerable<IndexOperationResult>>> PopulateIndexAsync(string indexName, IndexOperation[] data)
        {
            _indexMgr = new IndexManagementClient(_apiClient);
            return await _indexMgr.PopulateAsync(indexName, data);
        }
    }
}
