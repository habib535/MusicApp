using MusicApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayListDetailsPage : ContentPage
    {
        PlayListDetailsDetailViewModel viewModel;
        public PlayListDetailsPage()
        {
            InitializeComponent();

            viewModel = new PlayListDetailsDetailViewModel();
            BindingContext = viewModel;
        }

        public PlayListDetailsPage(PlayListDetailsDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadSongs.Execute(null);
        }
    }
}