using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
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
using System.Xml.Linq;

namespace Calender {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            using var conn = new SQLiteConnection(@"URI=file:userdat.db");
            conn.Open();
            using var cmd = new SQLiteCommand(conn);

            //create(cmd);
            ReadData(cmd);
            conn.Close();
        }

        public void ReadData(SQLiteCommand cmd) {
            SQLiteDataReader sqlite_datareader;
            cmd.CommandText = "SELECT * FROM User";

            sqlite_datareader = cmd.ExecuteReader();
            while (sqlite_datareader.Read()) {
                // Correctly read each column by its data type
                int id = sqlite_datareader.GetInt32(0);
                string name = sqlite_datareader.GetString(1);
                string password = sqlite_datareader.GetString(2);

                UserLabel.Content = $"ID: {id}, Name: {name}, Password: {password}";
            }
        }


        public void create(SQLiteCommand cmd) {
            string Createsql = "CREATE TABLE User (ID INTEGER PRIMARY KEY, Name VARCHAR(30), Password VARCHAR(60))";
            cmd.CommandText = Createsql;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User (name, password) VALUES('MyUser ', 'abc123'); ";
            cmd.ExecuteNonQuery();
        }
    }
}
