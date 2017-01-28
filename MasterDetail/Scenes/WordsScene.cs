using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDetail.DataStores;
using MasterDetail.Models;
using MasterDetail.Repositories;
using Microsoft.Practices.Unity;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace MasterDetail.Scenes
{
    public interface IWordsScene
    {
        ReadOnlyReactiveProperty<List<Item>> Items { get; }
        Task AddItemAsync(Item item);
    }

    public class WordsScene : IWordsScene
    {
        [Dependency]
        public IWordsRepository Repository { get; set; }

        public ReadOnlyReactiveProperty<List<Item>> Items { get; private set; }

        [InjectionMethod]
        public void Initialize()
        {
            Items = Repository.Items.ToReadOnlyReactiveProperty();
        }

        public Task AddItemAsync(Item item)
        {
            return Repository.AddItemAsync(item);
        }
    }
}