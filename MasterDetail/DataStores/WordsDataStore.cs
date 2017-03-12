using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDetail.Models;
using Reactive.Bindings;
using Realms;

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

        public WordsDataStore()
        {
            Reload();
        }

        private void Reload()
        {
            using (var realm = Realm.GetInstance())
            {
                Items.Value = realm.All<RealmModels.Item>()
                    .ToList() // 一度 List<T> にしないと Select できない (Realm がサポートしていない)
                    .Select(v => v.ToItem())
                    .ToList();
            }
        }

        public async Task AddItemAsync(Item item)
        {
            using (var realm = Realm.GetInstance())
            {
                await realm.WriteAsync(r => r.Add(new RealmModels.Item
                    {
                        Title = item.Title,
                        Description = item.Description

                    })
                );
                Reload();
            }
        }
    }
}