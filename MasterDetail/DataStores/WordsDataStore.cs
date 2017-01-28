using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDetail.Models;
using Reactive.Bindings;

namespace MasterDetail.DataStores
{
    public interface IWordsDataStore
    {
        ReactiveProperty<List<Item>> Items { get; }
        Task AddItemAsync(Item item);
    }

    public class WordsDataStore : IWordsDataStore
    {
        public ReactiveProperty<List<Item>> Items { get; } = new ReactiveProperty<List<Item>>(new List<Item>
        {
            new Item { Title = "Sharp", Description = "C# is very sharp!"}
        });

        public Task AddItemAsync(Item item)
        {
            return Task.Run(() =>
            {
                var items = new List<Item>(Items.Value);
                items.Add(item);
                Items.Value = items;
            });
        }
    }
}