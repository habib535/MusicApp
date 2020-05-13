using MusicApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModels
{
    public class PlayListDetailsDetailViewModel : BaseViewModel
    {
        public PlayList PlayList { get; set; }
        public ObservableCollection<Song> Songs { get; set; }

        public PlayListDetailsDetailViewModel(PlayList playList = null)
        {
            Title = PlayList?.Title;
            PlayList = playList;
            Songs = new ObservableCollection<Song>();
        }

        public ICommand LoadSongs => new Command(async () =>
        {
            await ExecuteAsyncCode(async () =>
            {
                Songs.Clear();
                List<Song> items = await FetchSongsFromPlayList(PlayList);
                foreach (var item in items)
                {
                    Songs.Add(item);
                }
            });
        });


    }
}
