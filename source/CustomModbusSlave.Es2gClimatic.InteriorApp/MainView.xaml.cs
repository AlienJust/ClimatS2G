using AlienJust.Adaptation.WindowsPresentation;
using CustomModbusSlave.MicroclimatEs2gApp;
using MahApps.Metro.Controls;

namespace CustomModbusSlave.Es2gClimatic.InteriorApp
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
