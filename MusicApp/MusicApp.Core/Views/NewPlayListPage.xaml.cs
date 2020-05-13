using MusicApp.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MusicApp.Views
{
    [DesignTimeVisible(false)]
    public partial class NewPlayListPage : ContentPage
    {
        NewPlayListViewModel viewModel;
        public NewPlayListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NewPlayListViewModel();
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "CreatePlayList");
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}