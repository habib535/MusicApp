using MusicApp.Services;
using MusicApp.Views;
using Xamarin.Forms;

namespace MusicApp.ViewModels
{
    public class NewPlayListViewModel : BaseViewModel
    {
        public string PlayListTitle { get; set; }
        public string PlayListDescription { get; set; }

        public NewPlayListViewModel()
        {
            Title = "New Play List";
            MessagingCenter.Subscribe<NewPlayListPage>(this, "CreatePlayList", async (obj) =>
            {
                await ExecuteAsyncCode(async () =>
                {
                    if (!string.IsNullOrWhiteSpace(PlayListTitle))
                    {
                        PlayListServiceClient = new YoutubePlayListServiceClient();
                        await PlayListServiceClient.CreatePlayList(PlayListTitle, PlayListDescription);
                        Status = "Playlist created successfully";
                        StatusColor = "Green";
                    }
                    else
                    {
                        Status = "Title is required!";
                        StatusColor = "Red";
                    }
                    HasStatus = true;
                });
            });
        }

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
