using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turneador.Data;
using Turneador.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turneador
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewUserPage : ContentPage
    {
        public NewUserPage()
        {
            InitializeComponent();
        }

        public void Button_click(object sender, EventArgs args)
        {
            if(newPassEntry.Text == newPassRepeatEntry.Text)
            {
                User newUser = new User()
                {
                    Name = newNameEntry.Text,
                    LastName = newLastNameEntry.Text,
                    Dni = int.Parse(newDniEntry.Text),
                    Email = newEmailEntry.Text,
                    Telephone = newPhoneEntry.Text,
                    Username = newUserEntry.Text,
                    Password = newPassEntry.Text
                };

                //==================================== SECUENCIA SQLITE ==============================================
                //using (var connection = new SQLite.SQLiteConnection(App.Route))
                //{
                //    connection.CreateTable<Turn>();
                //    connection.CreateTable<User>();
                //    connection.Insert(newUser);
                //}
                //====================================================================================================

                MasterConection conn = new MasterConection();
                if (conn.Connection())
                {
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
                    command.CommandText = "insert into User (name, lastname, dni, telephone, email, username, password) values ('" + newUser.Name + "' , '" + newUser.LastName + "', '" + newUser.Dni + "', '"
                                            + newUser.Telephone + "','" + newUser.Email + "', '" + newUser.Username + "','" + newUser.Password + "')";
                    command.ExecuteNonQuery();
                    con2.Close();
                }

                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Error", "Las contraseñas no coinciden...", "Rehacer");
            }
        }
    }
}