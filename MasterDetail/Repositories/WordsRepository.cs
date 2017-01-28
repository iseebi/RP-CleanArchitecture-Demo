using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDetail.DataStores;
using MasterDetail.Models;
using Microsoft.Practices.Unity;
using Reactive.Bindings;

namespace MasterDetail.Repositories
{
    public interface IWordsRepository
    {
        ReadOnlyReactiveProperty<List<Item>> Items { get; }
        Task AddItemAsync(Item item);
    }

    public class WordsRepository : IWordsRepository
    {
        [Dependency]
        public IWordsDataStore DataStore { get; set; }

        public ReadOnlyReactiveProperty<List<Item>> Items { get; private set; }

        [InjectionMethod]
        public void Initialize()
        {
            Items = DataStore.Items.ToReadOnlyReactiveProperty();
        }

        public Task AddItemAsync(Item item)
        {
            return DataStore.AddItemAsync(item);
        }
    }
}