using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Recognizer.ViewModel;
//using HandyControl.Controls;

namespace Recognizer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static MainWindow _window;
		static Dictionary<string, string> SigStrings = new Dictionary<string, string>()
		{
			{ "PDF", "25-50-44-46-2D"},
			{ "DOC.XLS", "D0-CF-11-E0-A1-B1-1A-E1"},
			{ "DOCX.XLSX","50-4B-03-04-14-00-06-00"},
			{ "ZIP", "50-4B-03-04-14-00-00-00"},
			{ "RAR5","52-61-72-21-1A-07-01-00"},
			{ "RAR3","52-61-72-21-1A-07-00"},
			{ "LNK", "4C-00-00-00-01-14-02-00" },
			{ "TXT", ""}
		};

		public MainWindow()
		{
			InitializeComponent();
			//_ = new Logics(this);
			_window = this;
		}

		private void StackPanel_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				// Note that you can have more than one file.
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				//var file = files[0];
				RecognMethod(files[0]);
				label1.Content = "Данные обработаны";
			}

		}

		private void StackPanel_DragEnter(object sender, DragEventArgs e)
		{
			if(e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				label1.Content = "Отпустите левую кнопку мыши";
				e.Effects = DragDropEffects.Copy;
			}
		}

		private void StackPanel_DragLeave(object sender, DragEventArgs e)
		{
				label1.Content = "Перетащите файл сюда";

		}

		private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Title = "Укажите файл который нужно распознать";
			fileDialog.InitialDirectory = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Desktop\";


			if (fileDialog.ShowDialog() == true)
			{
				//Logics logics2 = new Logics(fileDialog.FileName);
				//logics2.RecognMethod();

				RecognMethod(fileDialog.FileName);

			}
		}
		public void RecognMethod(string FileName)
		{
			byte[] bytes = new byte[10];
			var FileInfo = new FileInfo(FileName);
			try
			{
				using (var stream = FileInfo.OpenRead())
				{
					int count = stream.Read(bytes, 0, bytes.Length);
				}

			} catch (Exception e) {
				MessageBox.Show(e.Message, "Ошибка");
			}
			string hex = BitConverter.ToString(bytes);

			foreach (var itemK in SigStrings)
			{
				if (hex.Contains(itemK.Value) & !string.IsNullOrEmpty(itemK.Value))
				{
					SigName.FontSize = 90;
					
					if (itemK.Key.Length > 4)
						SigName.FontSize = 50;
					SigName.Text = itemK.Key;
					SigNameTag.Text = "Сигнатура распознана";
					break;
				}
			}
		}

		private void CloseWindow_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
        }

		private void MinWindow_Click(object sender, RoutedEventArgs e)
		{
			if ((bool)CheckTray.IsChecked) 
			{
				this.Hide();
			} 
			else
			{
				Application.Current.MainWindow.WindowState = WindowState.Minimized;
			}
		}

		private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
		{
			this.Show();
		}

		private void Drag(object sender, MouseButtonEventArgs e)
		{
			if (Mouse.LeftButton == MouseButtonState.Pressed)
			{
				MainWindow._window.DragMove();
			}
		}
	}
}
