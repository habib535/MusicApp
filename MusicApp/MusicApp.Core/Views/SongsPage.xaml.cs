using MusicApp.Models;
using MusicApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MusicApp.Views
{
    [DesignTimeVisible(false)]
    public partial class SongsPage : ContentPage
    {
        SongsViewModel viewModel;

        public SongsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SongsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Song;
            if (item == null)
                return;

            await Navigation.PushAsync(new SongDetailPage(new SongDetailViewModel(item)));

            // Manually deselect item.
            SongsListView.SelectedItem = null;
        }
    }
}