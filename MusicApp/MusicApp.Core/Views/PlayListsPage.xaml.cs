using MusicApp.Models;
using MusicApp.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MusicApp.Views
{
    [DesignTimeVisible(false)]
    public partial class PlayListsPage : ContentPage
    {
        PlayListsViewModel viewModel;

        public PlayListsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new PlayListsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadPlayLists.Execute(null);
        }

        async void AddPlayList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewPlayListPage()));
        }

        void ExportCsv_Clicked(object sender, EventArgs e)
        {
            viewModel.ExportCsvCommand.Execute(null);
        }

        async void OnPlayListSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as PlayList;
            if (item == null)
                return;

            await Navigation.PushAsync(new PlayListDetailsPage(new PlayListDetailsDetailViewModel(item)));

            // Manually deselect item.
            PlayListsView.SelectedItem = null;
        }
    }
}