using AlienJust.Adaptation.WindowsPresentation;
using MahApps.Metro.Controls;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	/// <summary>
	/// Interaction logic for MainView.xaml
	/// </summary>
	public partial class MainView : MetroWindow
	{
		public MainView()
		{
			InitializeComponent();
			DataContext = new MainViewModel(new WpfUiNotifier(Dispatcher), new WpfWindowSystem());
		}
	}
}
