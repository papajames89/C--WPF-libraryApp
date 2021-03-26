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
using System.Data.SqlClient;
using System.IO;

namespace projektBiblioteczka
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			fillComboSTART();
			fillText();
			fillImage();
			textbox1.Text = "Witam w mojej Biblioteczce, zapraszam do wyboru książki.";
			Obraz.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Okladki/biblioteka.jpg", UriKind.Absolute));

		}
		public void fillCombo()
		{
			SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bazadanych.mdf;Integrated Security=True");
			combobox1.Items.Clear();
			try
			{
				con.Open();
				string Query = "Select * from tbl_Ksiazki";
				SqlCommand createCommand = new SqlCommand(Query, con);
				SqlDataReader dr = createCommand.ExecuteReader();
				while (dr.Read())
				{
					string Tytul = dr.GetString(1);
					combobox1.Items.Add(Tytul);
				}
				con.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void fillComboSTART()
		{
			SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bazadanych.mdf;Integrated Security=True");
			try
			{
				con.Open();
				string Query = "Select * from tbl_Ksiazki";
				SqlCommand createCommand = new SqlCommand(Query, con);
				SqlDataReader dr = createCommand.ExecuteReader();
				while (dr.Read())
				{
					string Tytul = dr.GetString(1);
					combobox1.Items.Add(Tytul);
				}
				con.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void fillText()
		{
			SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bazadanych.mdf;Integrated Security=True");

			try
			{
				con.Open();
				string Query = "Select * from tbl_Ksiazki where Tytul='" + combobox1.Text + "'";
				SqlCommand createCommand = new SqlCommand(Query, con);
				SqlDataReader dr = createCommand.ExecuteReader();
				while (dr.Read())
				{
					string Opis = dr.GetString(2);
					textbox1.Text = Opis;
				}
				con.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public void fillImage()
		{
			SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bazadanych.mdf;Integrated Security=True");

			try
			{
				con.Open();
				string Query = "Select * from tbl_Ksiazki where Tytul='" + combobox1.Text + "'";
				SqlCommand createCommand = new SqlCommand(Query, con);
				SqlDataReader dr = createCommand.ExecuteReader();
				while (dr.Read())
				{
					string Zdjecie = dr.GetString(3);
					string sciezka = "Okladki/";
					Obraz.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + sciezka + Zdjecie , UriKind.Absolute));
				}
				con.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void wyczysc()
		{
			textbox1.Text = "Witam w mojej Biblioteczce, zapraszam do wyboru książki.";
			Obraz.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Okladki/biblioteka.jpg", UriKind.Absolute));
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bazadanych.mdf;Integrated Security=True");
			try
			{
				con.Open();
				string Query = "DELETE FROM tbl_Ksiazki WHERE Tytul='"+combobox1.Text+"'";
				SqlCommand createCommand = new SqlCommand(Query, con);
				createCommand.ExecuteNonQuery();
				MessageBox.Show("Pozycja została usunięta");
				con.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			fillCombo();
			wyczysc();
		}

		
		private void Button_Click_1(object sender, RoutedEventArgs e)

		{
			SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bazadanych.mdf;Integrated Security=True");
			try
			{	
				con.Open();
				string Query = "INSERT INTO tbl_Ksiazki (Tytul, Opis, Zdjecie) VALUES ('" + combobox1.Text + "', '" + textbox1.Text + "', '" + combobox1.Text + ".jpg')";
				SqlCommand createCommand = new SqlCommand(Query, con);
				createCommand.ExecuteNonQuery();
				MessageBox.Show("Pozycja została dodana");
				con.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			fillCombo();
		}


		private void combobox1_DropDownClosed(object sender, EventArgs e)
		{
			fillText();
			fillImage();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			// Configure open file dialog box
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.DefaultExt = ".jpg"; // Default file extension
			dlg.Filter = "JPEG Files (.jpg)|*.jpg"; // Filter files by extension

			// Show open file dialog box
			Nullable<bool> result = dlg.ShowDialog();

			// Process open file dialog box results
			if (result == true)
			{
				if (dlg.CheckFileExists)
				{
					System.IO.File.Copy(dlg.FileName, @"Okladki/"+combobox1.Text+".jpg");
				}
				Obraz.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Okladki/"+combobox1.Text+".jpg", UriKind.Absolute));
			}
		}
	}
}
