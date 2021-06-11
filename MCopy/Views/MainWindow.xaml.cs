using MCopy.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCopy
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainViewModel VM;

		public MainWindow()
		{
			InitializeComponent();

			VM = new MainViewModel(this);
			DataContext = VM;
		}

		private async void ScanButton_Click(object sender, RoutedEventArgs e)
		{
			await VM.ScanFolders();
		}
	}
}
