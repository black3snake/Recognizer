using HandyControl.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Recognizer.ViewModel
{
	public class Logics : IWork
	{

		private MainWindow window;
		byte[] bytes = new byte[10];
		//FileInfo FileInfo;
		string FileName;
		
		static Dictionary<string, string> SigStrings = new Dictionary<string, string>()
		{
			{ "PDF", "25-50-44-46-2D" },
			{ "DOC", ""},
			{ "XLS", ""},
			{ "TXT", ""}
		};

		public Logics(MainWindow window)
		{
			this.window = window;
		}

		public Logics(string filename)
		{
			FileName = filename;
		}

		/*public Logics(string fileName)
		{
			//FileInfo = file;
			FileName = fileName;
		}*/

		public void RecognMethod()
		{
			var FileInfo = new FileInfo(FileName);

			using (var stream = FileInfo.OpenRead())
			{
				int count = stream.Read(bytes, 0, bytes.Length);
			}
			string hex = BitConverter.ToString(bytes);

			foreach (var itemK in SigStrings)
			{
				if (hex.Contains(itemK.Value))
				{
					//Application.Current.MainWindow.Co   SigName.Text = "dfd";
					window.SigName.Text = itemK.Key;
					window.SigNameTag.Text = "Сигнатура распознана";
					break;
				}


			}

		}

		//private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		//{
		//	OpenFileDialog fileDialog = new OpenFileDialog();
		//	fileDialog.Title = "Укажите файл который нужно распознать";
		//	fileDialog.InitialDirectory = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Desktop\";
		//	byte[] bytes = new byte[10];

		//	if (fileDialog.ShowDialog() == true)
		//	{
		//		var fileInfo = new FileInfo(fileDialog.FileName);

		//		//Logics logics = new Logics(fileDialog.FileName);

		//		//logics.RecognMethod();


		//		using (var stream = fileInfo.OpenRead())
		//		{
		//			int count = stream.Read(bytes, 0, bytes.Length);
		//		}
		//		string hex = BitConverter.ToString(bytes);

		//		foreach (var itemK in SigStrings)
		//		{
		//			if (hex.Contains(itemK.Value))
		//			{
		//				SigName.Text = itemK.Key;
		//				SigNameTag.Text = "Сигнатура распознана";
		//				break;
		//			}


		//		}

		//	}
		//}


	}
}
