using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MasterDetail.Models;
using Reactive.Bindings;
using Realms;
using Realms.Sync;

namespace MasterDetail.DataStores
{
    public interface IWordsDataStore
    {
        ReactiveProperty<List<Item>> Items { get; }
        Task AddItemAsync(Item item);
    }

    public class WordsDataStore : IWordsDataStore
    {
        public ReactiveProperty<List<Item>> Items { get; } = new ReactiveProperty<List<Item>>(new List<Item>());

        private User User { get; set; }
        private SyncConfiguration SyncConfiguration { get; set; }

        public WordsDataStore()
        {
            ReloadAsync();
        }

        private async Task LoginAsync()
        {
            if (User == null)
            {
                var credentials = Credentials.UsernamePassword(AppCredentials.UserName, AppCredentials.Password, false);
                User = await User.LoginAsync(credentials, new Uri(AppCredentials.AuthUrl));
                Debug.WriteLine(User.Identity);
                SyncConfiguration = new SyncConfiguration(User, new Uri(AppCredentials.DatabaseUrl));
            }
        }

        private async Task ReloadAsync()
        {
            await LoginAsync();
            using (var realm = Realm.GetInstance(SyncConfiguration))
            {
                Items.Value = realm.All<RealmModels.Item>()
                    .ToList() // 一度 List<T> にしないと Select できない (Realm がサポートしていない)
                    .Select(v => v.ToItem())
                    .ToList();
            }
        }

        public async Task AddItemAsync(Item item)
        {
            await LoginAsync();
            using (var realm = Realm.GetInstance(SyncConfiguration))
            {
                await realm.WriteAsync(r => r.Add(new RealmModels.Item
                    {
                        Title = item.Title,
                        Description = item.Description
                    })
                );
                await ReloadAsync();
            }
        }
    }
}