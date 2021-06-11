using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MCopy.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		#region [ Fields ]

		private MainWindow View;
		private string sourcePath = "Z:\\Dev";
		private string filter = "*.cs;*.dll";
		private List<string> Filters;

		#endregion

		#region [ Properties ]

		public string SourcePath
		{
			get => sourcePath;
			set
			{
				if (sourcePath != value)
				{
					sourcePath = value;
					NotifyPropertyChanged("SourcePath");
				}
			}
		}
		public string Filter
		{
			get => filter;
			set
			{
				if (filter != value)
				{
					filter = value;
					NotifyPropertyChanged("Filter");
				}
			}
		}
		ObservableCollection<FileInfo> CopyFiles { get; set; } = new ObservableCollection<FileInfo>();

		#endregion

		#region [ Constructor ]

		public MainViewModel(MainWindow mainWindow)
		{
			View = mainWindow;
		}

		#endregion

		#region [ Notification ]
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion


		public async Task ScanFolders()
		{
			TreeViewItem root = new TreeViewItem()
			{
				Header = SourcePath,
				IsExpanded = true,
			};
			View.FoldersTreeView.Items.Add(root);
			Filters = new List<string>(Filter.Split(';'));
			await CollectFoldersAndFiles(SourcePath, root);
		}

		public async Task CollectFoldersAndFiles(string path, TreeViewItem parent)
		{
			IEnumerable<string> folders = Directory.GetDirectories(path);
			foreach (string folder in folders)
			{
				TreeViewItem branch = new TreeViewItem()
				{
					Header = folder.Replace($"{path}\\", ""),
					IsExpanded = true,
				};
				parent.Items.Add(branch);
				await CollectFoldersAndFiles(folder, branch);
			}
		}

	}
}
