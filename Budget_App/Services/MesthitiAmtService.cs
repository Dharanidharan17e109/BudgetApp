using Budget_App.Models;
using Microsoft.Azure.Cosmos;

namespace Budget_App.Services
{
    public class MesthitiAmtService : IMesthiriAmtService
    {
        private Container _container;
        public MesthitiAmtService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(MesthiriAmt mesthiriAmt)
        {
            await _container.CreateItemAsync(mesthiriAmt, new PartitionKey(mesthiriAmt.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<MesthiriAmt>(id, new PartitionKey(id));
        }

        public async Task<MesthiriAmt> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<MesthiriAmt>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<MesthiriAmt>> GetMultipleAsync(string queryStr)
        {
            var query = _container.GetItemQueryIterator<MesthiriAmt>(new QueryDefinition(queryStr));
            var results = new List<MesthiriAmt>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, MesthiriAmt mesthiriAmt)
        {
            await _container.UpsertItemAsync(mesthiriAmt, new PartitionKey(id));
        }
    }
}
