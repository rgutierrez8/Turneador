using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turneador.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turneador
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SystemPage : ContentPage
    {
        public User  User { get; set; }
        public SystemPage(User user)
        {
            InitializeComponent();
            User = user;

            welcomeLabel.Text = $"Bienvenido {User.Name} {User.LastName}";

        }


        public void Item_click(object sender, EventArgs args)
        {
            Navigation.PushAsync(new TurnPage(User));
        }

        protected override void OnAppearing()
        {
            //List<Turn> turn;
            base.OnAppearing();

            //============================================== SECUENCIA SQLITE ============================================
            //using(var connection = new SQLite.SQLiteConnection(App.Route))
            //{
            //    connection.CreateTable<Turn>();
            //    connection.CreateTable<User>();
            //    turn = connection.Table<Turn>().Where(user => user.UserId == User.Id).ToList();

            //    if(turn.Count == 0)
            //    {
            //        infoLabel.Text = "No hay turnos asociados a este usuario";
            //    }
            //    else
            //    {
            //        turnListView.ItemsSource = turn;
            //    }
            //}
            //=============================================================================================================

            MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();
            Builder.Port = 3306;
            Builder.Server = "sql10.freemysqlhosting.net";
            Builder.Database = "sql10453129";
            Builder.UserID = "sql10453129";
            Builder.Password = "hiqpBFcRrn";
            Builder.AllowUserVariables = true;
            MySqlConnection con2 = new MySqlConnection(Builder.ToString());
            MySqlCommand command = new MySqlCommand();
            command.Connection = con2;
            con2.Open();

            command.CommandText = "SELECT * FROM Turn WHERE UserId = " + User.Id;
            MySqlDataReader reader = command.ExecuteReader();
            List<Turn> turns = new List<Turn>();
            while (reader.Read())
            {
                var turn = new Turn
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    SelectedDate = reader["SelectedDate"].ToString(),
                    Process = reader["Process"].ToString(),
                    State = Convert.ToInt32(reader["State"].ToString()),
                    UserId = User.Id
                };
                turns.Add(turn);
            }

            if (turns.Count == 0)
            {
                infoLabel.Text = "No hay turnos asociados a este usuario";
            }
            else
            {

                foreach(Turn t in turns)
                {
                    if(t.State == 1)
                    {
                        t.LabelState = "TURNO ACEPTADO";
                    }
                    else if(t.State == 2)
                    {
                        t.LabelState = "TURNO RECHAZADO";
                    }
                    else
                    {
                        t.LabelState = "TURNO EN ADMISION";
                    }
                }
                turnListView.ItemsSource = turns;
            }

            con2.Close();
        }
    }
}