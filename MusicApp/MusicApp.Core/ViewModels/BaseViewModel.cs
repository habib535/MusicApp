using MusicApp.Models;
using MusicApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MusicApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public YoutubeSearchServiceClient SearchServiceClient { get; set; }
        public YoutubePlayListServiceClient PlayListServiceClient { get; set; }

        public async Task<List<PlayList>> FetchMyPlayLists()
        {
            PlayListServiceClient = new YoutubePlayListServiceClient();
            return await PlayListServiceClient.MyPlayLists();
        }

        public async Task<List<Song>> FetchSongsFromPlayList(PlayList playList)
        {
            PlayListServiceClient = new YoutubePlayListServiceClient();
            return await PlayListServiceClient.FetchSongsFromPlayList(playList);
        }

        public async Task ExecuteAsyncCode(Func<Task> action)
        {
            try
            {
                if (IsLoading)
                    return;

                IsLoading = true;
                await action();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
