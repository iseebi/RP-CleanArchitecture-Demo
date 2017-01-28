using MasterDetail.Models;
using MasterDetail.Scenes;
using Microsoft.Practices.Unity;
using Reactive.Bindings;
using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace MasterDetail.ViewModels
{
    public class NewItemViewModel : IViewModel
    {
        [Dependency]
        public INavigationService NavigationService { get; set; }

        [Dependency]
        public IWordsScene Scene { get; set; }

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> Description { get; } = new ReactiveProperty<string>();

        public ReactiveCommand CommitChange { get; } = new ReactiveCommand();

        public NewItemViewModel()
        {
            CommitChange.Subscribe(_ => OnCommitChange());
        }

        private async void OnCommitChange()
        {
            await Scene.AddItemAsync(new Item
            {
                Title = Title.Value,
                Description = Description.Value
            });
            NavigationService.GoBack();
        }
    }
}