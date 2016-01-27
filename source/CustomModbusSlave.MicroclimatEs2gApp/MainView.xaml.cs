using System.Windows;
using AlienJust.Adaptation.WindowsPresentation;

namespace CustomModbusSlave.MicroclimatEs2gApp
{
	/// <summary>
	/// Interaction logic for MainView.xaml
	/// </summary>
	public partial class MainView : Window
	{
		public MainView()
		{
			InitializeComponent();
			DataContext = new MainViewModel(new WpfUiNotifier(Dispatcher), new WpfWindowSystem());
		}
	}
}
