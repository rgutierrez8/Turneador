using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turneador.Models;
using Xamarin.Forms;

namespace Turneador
{
    public partial class MainPage : ContentPage
    {
        List<User> users;
        public MainPage()
        {
            InitializeComponent();
            users = new List<User>();
        }

        public void ButtonNew_click(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewUserPage());
        }

        public void ButtonSingIn_click(object sender, EventArgs args)
        {
            btnSingIn.IsEnabled = false;

            //List<User> userSelected;
           
            //======================================= SECCION SQLITE ===============================================================
            //using(var connection = new SQLite.SQLiteConnection(App.Route))
            //{
            //    //connection.DropTable<User>();
            //    //connection.DropTable<Turn>();
            //    //return;
            //    connection.CreateTable<Turn>();
            //    connection.CreateTable<User>();
            //    users = connection.Table<User>().ToList();

            //    if (String.IsNullOrEmpty(userEntry.Text))
            //    {
            //        DisplayAlert("Error", "Ingrese el usuario", "OK!");
            //        btnSingIn.IsEnabled = true;
            //    }
            //    else if (String.IsNullOrEmpty(passEntry.Text))
            //    {
            //        DisplayAlert("Error", "Ingrese la contraseña", "OK!");
            //        btnSingIn.IsEnabled = true;
            //    }
            //    else
            //    {
            //        userSelected = (from uS in users where uS.Username == userEntry.Text select uS).ToList();
            //        if(userSelected.Count == 0)
            //        {
            //            DisplayAlert("Error", "Usuario inválido", "Continuar");
            //            btnSingIn.IsEnabled = true;
            //        }
            //        else
            //        {
            //            string passSelected = userSelected[0].Password;
            //            if (passSelected == passEntry.Text)
            //            {
            //                var loggedUser = new User { 
            //                    Id = userSelected[0].Id,
            //                    Name = userSelected[0].Name,
            //                    LastName = userSelected[0].LastName,
            //                    Dni = userSelected[0].Dni,
            //                    Email = userSelected[0].Email,
            //                    Telephone = userSelected[0].Telephone,
            //                    Username = userSelected[0].Username,
            //                    Password = userSelected[0].Password
            //                };
            //                Navigation.PushAsync(new SystemPage(loggedUser));
            //            }
            //            else
            //            {
            //                DisplayAlert("Error", "Contraseña incorrecta!", "Continuar");
            //                btnSingIn.IsEnabled = true;
            //            }
            //        }
            //    }
            //}
            //==============================================================================================================

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

            if (String.IsNullOrEmpty(userEntry.Text))
            {
                DisplayAlert("Error", "Ingrese el usuario", "OK!");
                btnSingIn.IsEnabled = true;
            }
            else if (String.IsNullOrEmpty(passEntry.Text))
            {
                DisplayAlert("Error", "Ingrese la contraseña", "OK!");
                btnSingIn.IsEnabled = true;
            }
            else
            {
                command.CommandText = "SELECT * FROM User WHERE Username = '" + userEntry.Text + "'";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string passSelected = reader["Password"].ToString();
                    if (passSelected == passEntry.Text)
                    {
                        var loggedUser = new User
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Dni = Convert.ToInt32(reader["Dni"].ToString()),
                            Email = reader["Email"].ToString(),
                            Telephone = reader["Telephone"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                        };
                        Navigation.PushAsync(new SystemPage(loggedUser));
                    }
                    else
                    {
                        DisplayAlert("Error", "Contraseña incorrecta!", "Continuar");
                        btnSingIn.IsEnabled = true;
                    }
                }
                else
                {
                    DisplayAlert("Error", "Usuario inválido", "Continuar");
                    btnSingIn.IsEnabled = true;
                }
            }

            con2.Close();
        }
    }
}
