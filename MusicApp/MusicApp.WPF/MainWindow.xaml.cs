using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace MusicApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();

            Forms.Init();

            LoadApplication(new MusicApp.App());
        }
    }
}
