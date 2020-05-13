
using MusicApp.Models;
using MusicApp.Services;
using MusicApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModels
{
    public class SongDetailViewModel : BaseViewModel
    {

        public Song Item { get; set; }
        public ObservableCollection<PlayList> PlayLists { get; set; }
        public PlayList SelectedPlayList { get; set; }
        public SongDetailViewModel(Song item = null)
        {
            Title = item?.Title;
            Item = item;
            PlayLists = new ObservableCollection<PlayList>();

            MessagingCenter.Subscribe<SongDetailPage>(this, "AddSongToPlayList", async (obj) =>
            {
                await ExecuteAsyncCode(async () =>
                {
                    if (Validated())
                    {
                        PlayListServiceClient = new YoutubePlayListServiceClient();
                        await PlayListServiceClient.AddSongToPlayList(SelectedPlayList, Item);
                        Status = "Song added to playlist";
                        HasStatus = true;
                        StatusColor = "Green";
                    }
                });
            });
        }

        private bool Validated()
        {
            if (SelectedPlayList == null)
            {
                Status = "Please select playlist from dropdown";
                HasStatus = true;
                StatusColor = "Red";
                return false;
            }
            return true;
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

        #region status info
        private string status;
        private string statusColor;
        private bool hasStatus;

        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public string StatusColor
        {
            get => statusColor;
            set
            {
                statusColor = value;
                OnPropertyChanged(nameof(StatusColor));
            }
        }
        public bool HasStatus
        {
            get => hasStatus;
            set
            {
                hasStatus = value;
                OnPropertyChanged(nameof(HasStatus));
            }
        }
        #endregion
    }
}
