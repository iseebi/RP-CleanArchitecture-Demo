using System.Collections.Generic;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Models;
using MasterDetail.Scenes;
using Microsoft.Practices.Unity;
using Reactive.Bindings;
using System;
using Reactive.Bindings.Extensions;

namespace MasterDetail.ViewModels
{
    public class BrowseViewModel : IViewModel
    {
        [Dependency]
        public IWordsScene Scene { get; set; }

        [Dependency]
        public INavigationService NavigationService { get; set; }

        public ReactiveProperty<List<Item>> Items { get; private set; }
        public ReactiveCommand NavigateToNew { get; } = new ReactiveCommand();
        public ReactiveCommand<Item> NavigateToDetail { get; } = new ReactiveCommand<Item>();

        [InjectionMethod]
        public void Initialize()
        {
            Items = Scene.Items.ObserveOnUIDispatcher().ToReactiveProperty();
            NavigateToNew.Subscribe(_ => OnNavigateToNew());
            NavigateToDetail.Subscribe(OnNavigateToDetail);
        }

        private void OnNavigateToNew()
        {
            NavigationService.NavigateTo(nameof(NewItemViewModel).Replace("ViewModel", ""));
        }

        private void OnNavigateToDetail(Item item)
        {
            NavigationService.NavigateTo(nameof(DetailViewModel).Replace("ViewModel", ""), Items.Value.IndexOf(item));
        }
    }
}