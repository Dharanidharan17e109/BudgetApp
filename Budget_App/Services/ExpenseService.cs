using Budget_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;

namespace Budget_App.Services
{
    public class ExpenseService:IExpenseService
    {
        private Container _container;
        public ExpenseService(CosmosClient cosmosClient,string databaseName,string containerName)
        {
            _container=cosmosClient.GetContainer(databaseName,containerName);   
        }

        public async Task AddAsync(Expense expense)
        {
            await _container.CreateItemAsync(expense, new PartitionKey(expense.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Expense>(id, new PartitionKey(id));
        }

        public async Task<Expense> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Expense>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<Expense>> GetMultipleAsync(string queryStr)
        {
            var query = _container.GetItemQueryIterator<Expense>(new QueryDefinition(queryStr));
            var results = new List<Expense>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, Expense expense)
        {
            await _container.UpsertItemAsync(expense, new PartitionKey(id));
        }
    }
}
