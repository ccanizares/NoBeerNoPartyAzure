using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using NoBeerNoParty.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoBeerNoParty.Simulation.Services
{
    public class DocumentDbClient<T>:IDocumentDbClient<T> where T: Item
    {
        private readonly DocumentClient _client;
        private readonly string _dataBase;
        private readonly string _collectionName;
    
        public DocumentDbClient(string endPointUrl, string accessKey, string dataBase)
        {
            _client = new DocumentClient(new Uri(endPointUrl), accessKey);
            _dataBase = dataBase;
            _collectionName = typeof(T).Name;
        }

        private async Task EnsureDbAndCollectionExists()
        {
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _dataBase });
            var dbUrl = UriFactory.CreateDatabaseUri(_dataBase);

            await _client.CreateDocumentCollectionIfNotExistsAsync(dbUrl, new DocumentCollection { Id = _collectionName });
        }


        public async Task<T> AddAsync(T value)
        {
            await EnsureDbAndCollectionExists();

            var collectionUrl = UriFactory.CreateDocumentCollectionUri(_dataBase, _collectionName);

            //TODO: Needed more control over the response status.
            var response = await _client.CreateDocumentAsync(collectionUrl, value);

            return value;
        }

        public Task<T> UpdateAsync(T value) { throw new NotImplementedException(); }
        public Task Remove(string id) { throw new NotImplementedException(); }
        public Task<T> GetByKey(string id) { throw new NotImplementedException(); }
        public Task<T> GetAll(string text) { throw new NotImplementedException(); }

    }
}
