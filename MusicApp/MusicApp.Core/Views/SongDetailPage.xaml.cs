using MusicApp.Models;
using MusicApp.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MusicApp.Views
{
    [DesignTimeVisible(false)]
    public partial class SongDetailPage : ContentPage
    {
        SongDetailViewModel viewModel;

        public SongDetailPage(SongDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadPlayLists.Execute(null);
        }

        public SongDetailPage()
        {
            InitializeComponent();

            viewModel = new SongDetailViewModel(new Song());
            BindingContext = viewModel;
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddSongToPlayList");
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}