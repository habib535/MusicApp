using MusicApp.Models;
using MusicApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModels
{
    public class PlayListsViewModel : BaseViewModel
    {
        public CSVHelper CSVHelper { get; set; }
        public ObservableCollection<PlayList> PlayLists { get; set; }
        public PlayListsViewModel()
        {
            Title = "Play Lists";
            PlayLists = new ObservableCollection<PlayList>();
            CSVHelper = new CSVHelper();
        }

        public ICommand LoadPlayLists => new Command(async () =>
        {
            await ExecuteAsyncCode(async () =>
            {
                PlayLists.Clear();
                List<PlayList> items = await FetchMyPlayLists();
                foreach (var item in items)
                {
                    PlayLists.Add(item);
                }
            });
        });

        public ICommand ExportCsvCommand => new Command(async () =>
        {
            await ExecuteAsyncCode(async () =>
            {
                List<PlayList> playLists = await FetchMyPlayLists();
                foreach (var playList in playLists)
                {
                    playList.Songs = await FetchSongsFromPlayList(playList);
                }
                CSVHelper.Export(playLists);
            });
        });


    }
}