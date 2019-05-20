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


namespace PapasPapbar.Application
{

    public static class BoardgameRepos
    {
        static string boardgameId = txtboardgameId.Text;
        static string boardgameName;
        static string numberOfPlayers;
        static string audience;
        static string expectedGameTime;
        static string distributor;
        static string gameTag;

        public static SqlCommand cmd;
        public static SqlDataReader reader;

        public static void ResetBoardgame()
        {
            txtboardgameId.Text = "";
            txtBrætspil.Text = "";
            txtAntal.Text = "";
            txtAldersgruppe.Text = "";
            txtSpilletid.Text = "";
            txtDistrubutør.Text = "";
            txtGenre.Text = "";
            txtBrætspil.Focus();
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnInsert.IsEnabled = true;
        }

        public static void GetBoardgame()
        {
            using (SqlConnection con = new SqlConnection(DataBaseController.connectionString))

                try
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("ViewGameLibrary", con);                 
                                  
                    con.Close();
                }
                catch (System.Exception)
                {
                    throw;
                }
        }
      
        public static void InsertBoardgame()
        {
            using (SqlConnection con = new SqlConnection(DataBaseController.connectionString))

                try
                {
                    con.Open();
                    cmd = new SqlCommand("Insert into Game_Library (Boardgame_Name, Player_Count, Audience, Game_Time, Distributor, GameTag) Values (@Boardgame_Name, @Player_Count, @Audience, @Game_Time, @Distributor, @GameTag)", con);
                    cmd.Parameters.AddWithValue("Boardgame_Name", boardgameId.Trim());
                    cmd.Parameters.AddWithValue("Player_Count", txtAntal.Text.Trim());
                    cmd.Parameters.AddWithValue("Audience", txtAldersgruppe.Text.Trim());
                    cmd.Parameters.AddWithValue("Game_Time", txtSpilletid.Text.Trim());
                    cmd.Parameters.AddWithValue("Distributor", txtDistrubutør.Text.Trim());
                    cmd.Parameters.AddWithValue("GameTag", txtGenre.Text.Trim());
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (System.Exception)
                {
                    throw;
                }
        }

        public static void DeleteBoardgame()
        {
            using (SqlConnection con = new SqlConnection(DataBaseController.connectionString))      

                try
                {
                    con.Open();
                    cmd = new SqlCommand("Delete From Game_Library Where Boardgame_Id = @Boardgame_Id", con);
                    cmd.Parameters.AddWithValue("Boardgame_Id", txtId.Text.Trim());
                    cmd.ExecuteNonQuery();
                    con.Close();                   
                   // GetBoardgame();
                }
                catch (System.Exception)
                {
                    throw;
                }
        }

        public static void SearchBoardgame()
        {

            using (SqlConnection con = new SqlConnection(DataBaseController.connectionString))

                try
                {
                    con.Open();
                    cmd = new SqlCommand("Select * From Game_Library Where Boardgame_Name Like '%" + txtSearch.Text.Trim() + "%' OR Player_Count Like '%" + txtSearch.Text.Trim() + "%' OR Game_Time Like '%" + txtSearch.Text.Trim() + "%' OR Distributor Like '%" + txtSearch.Text.Trim() + "%' OR GameTag Like '%" + txtSearch.Text.Trim() + "%' Order By Boardgame_Id Desc", con);
                    reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    con.Close();

                }
                catch (System.Exception)
                {
                    throw;
                }
        }

        public static void UpdateBoardgame()
        {

            using (SqlConnection con = new SqlConnection(DataBaseController.connectionString))

                try
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE Game_Library SET Boardgame_Name = @Boardgame_Name, Player_Count = @Player_Count, Audience = @Audience, Game_Time = @Game_Time, Distributor = @Distributor, GameTag = @GameTag WHERE Boardgame_Id = @Boardgame_Id", con);
                    cmd.Parameters.AddWithValue("Boardgame_Id", txtId.Text.Trim());
                    cmd.Parameters.AddWithValue("Boardgame_Name", txtBrætspil.Text.Trim());
                    cmd.Parameters.AddWithValue("Player_Count", txtAntal.Text.Trim());
                    cmd.Parameters.AddWithValue("Audience", txtAldersgruppe.Text.Trim());
                    cmd.Parameters.AddWithValue("Game_Time", txtSpilletid.Text.Trim());
                    cmd.Parameters.AddWithValue("Distributor", txtDistrubutør.Text.Trim());
                    cmd.Parameters.AddWithValue("GameTag", txtGenre.Text.Trim());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GetBoardgame();
                }
                catch (System.Exception)
                {
                    throw;
                }
        }

        public static void SelectionChangedBoardgame()
        {

            using (SqlConnection con = new SqlConnection(DataBaseController.connectionString))


                try
                {
                    DataGrid dg = (DataGrid)sender;
                    DataRowView rowSelected = dg.SelectedItem as DataRowView;
                    if (rowSelected != null)
                    {
                        txtId.Text = rowSelected[0].ToString();
                        txtBrætspil.Text = rowSelected[1].ToString();
                        txtAntal.Text = rowSelected[2].ToString();
                        txtAldersgruppe.Text = rowSelected[3].ToString();
                        txtSpilletid.Text = rowSelected[4].ToString();
                        txtDistrubutør.Text = rowSelected[5].ToString();
                        txtGenre.Text = rowSelected[6].ToString();
                    }
                    btnUpdate.IsEnabled = true;
                    btnDelete.IsEnabled = true;
                    btnInsert.IsEnabled = false;
                }
                catch (System.Exception)
                {
                    throw;
                }
        }
        
    }
}
