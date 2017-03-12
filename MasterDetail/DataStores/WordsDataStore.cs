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
            StartupAsync();
        }

        private async Task StartupAsync()
        {
            await LoginAsync();
            Reload();
        }

        private async Task LoginAsync()
        {
            var credentials = Credentials.UsernamePassword(AppCredentials.UserName, AppCredentials.Password, false);
            User = await User.LoginAsync(credentials, new Uri(AppCredentials.AuthUrl));
            SyncConfiguration = new SyncConfiguration(User, new Uri(AppCredentials.DatabaseUrl));

            var realm = Realm.GetInstance(SyncConfiguration);
            realm.All<RealmModels.Item>().AsRealmCollection().CollectionChanged += (sender, e) => Reload();
        }

        private void Reload()
        {
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
            using (var realm = Realm.GetInstance(SyncConfiguration))
            {
                await realm.WriteAsync(r => r.Add(new RealmModels.Item
                    {
                        Title = item.Title,
                        Description = item.Description
                    })
                );
            }
        }
    }
}