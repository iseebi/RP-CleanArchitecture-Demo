using System.Reactive.Linq;
using MasterDetail.Models;
using MasterDetail.Scenes;
using MasterDetail.Views;
using Microsoft.Practices.Unity;
using Reactive.Bindings;
using System;

namespace MasterDetail.ViewModels
{
    public class DetailViewModel : IViewModel, IViewLifecycleWatching
    {
        [Dependency]
        public IWordsScene Scene { get; set; }

        public ReactiveProperty<Item> Item { get; } = new ReactiveProperty<Item>();
        public ReadOnlyReactiveProperty<string> Title { get; }
        public ReadOnlyReactiveProperty<string> Description { get; }

        public DetailViewModel()
        {
            Title = Item.Select(i => i?.Title).ToReadOnlyReactiveProperty();
            Description = Item.Select(i => i?.Description).ToReadOnlyReactiveProperty();
        }

        public void OnLoad(object parameter)
        {
            var index = (int)parameter;
            if (index < 0 || index >= Scene.Items.Value.Count) { return; }

            Item.Value = Scene.Items.Value[index];
        }

        public void OnWillAppear()
        {
        }

        public void OnDidAppear()
        {
        }

        public void OnWillDisappear()
        {
        }

        public void OnDidDisappear()
        {
        }
    }
}