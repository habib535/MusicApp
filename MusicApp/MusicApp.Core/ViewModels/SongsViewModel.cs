using MusicApp.Models;
using MusicApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModels
{
    public class SongsViewModel : BaseViewModel
    {
        public ObservableCollection<Song> Songs { get; set; }

        public SongsViewModel()
        {
            Title = "Songs";
            Songs = new ObservableCollection<Song>();
        }

        public ICommand PerformSearch => new Command<string>(async (string searchTerm) =>
        {
            await ExecuteAsyncCode(async () =>
            {
                Songs.Clear();
                SearchServiceClient = new YoutubeSearchServiceClient();
                List<Song> items = await SearchServiceClient.Search(searchTerm);
                foreach (var item in items)
                {
                    Songs.Add(item);
                }
            });
        });
    }
}