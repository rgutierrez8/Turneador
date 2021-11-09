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

                using (var connection = new SQLite.SQLiteConnection(App.Route))
                {
                    connection.CreateTable<Turn>();
                    connection.CreateTable<User>();
                    connection.Insert(newUser);
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