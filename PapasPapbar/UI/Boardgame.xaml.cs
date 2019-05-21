using PapasPapbar.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace PapasPapbar.UI
{
    /// <summary>
    /// Interaction logic for Boardgame.xaml
    /// </summary>
    public partial class Boardgame : Window
    {
        public Boardgame()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private SqlDataReader reader;



        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            reader = cmd.ExecuteReader();

            dt.Load(reader);
            txtBrætspil.Focus();
            BoardgameRepos.GetBoardgame();
            DataGrid1.Columns[0].Visibility = Visibility.Collapsed;
            DataGrid1.ItemsSource = dt.DefaultView;

        }

        //Get Data for datagrid


        //Nulstil Boardgame
        public void btnReset_Click(object sender, RoutedEventArgs e)
        {
            BoardgameRepos.ResetBoardgame();
        }



        //Indsætfunktion til Boardgame
        public void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BoardgameRepos.InsertBoardgame();
                MessageBox.Show("Record Save Successfully", "Saved", MessageBoxButton.OK);
                BoardgameRepos.GetBoardgame();
                BoardgameRepos.ResetBoardgame();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Slettefunktion til Boardgame
        public void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            BoardgameRepos.DeleteBoardgame();
            MessageBox.Show("Record Deleted Successfully", "Deleted", MessageBoxButton.OK);
            BoardgameRepos.ResetBoardgame();
        }

        //Updatefunktion til Boardgame
        public void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            BoardgameRepos.UpdateBoardgame();
            MessageBox.Show("Record Update Successfully", "Updated", MessageBoxButton.OK);
            BoardgameRepos.ResetBoardgame();
        }

        //Søgefunktion til Boardgame
        public void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            BoardgameRepos.SearchBoardgame();
            DataGrid1.ItemsSource = dt.DefaultView;
            DataGrid1.Columns[0].Visibility = Visibility.Collapsed;
        }

        public void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BoardgameRepos.SelectionChangedBoardgame();
        }

    }
}
