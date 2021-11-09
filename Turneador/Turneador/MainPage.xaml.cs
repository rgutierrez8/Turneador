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

            List<User> userSelected;
           
            using(var connection = new SQLite.SQLiteConnection(App.Route))
            {
                //connection.DropTable<User>();
                //connection.DropTable<Turn>();
                //return;
                connection.CreateTable<Turn>();
                connection.CreateTable<User>();
                users = connection.Table<User>().ToList();

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
                    userSelected = (from uS in users where uS.Username == userEntry.Text select uS).ToList();
                    if(userSelected.Count == 0)
                    {
                        DisplayAlert("Error", "Usuario inválido", "Continuar");
                        btnSingIn.IsEnabled = true;
                    }
                    else
                    {
                        string passSelected = userSelected[0].Password;
                        if (passSelected == passEntry.Text)
                        {
                            var loggedUser = new User { 
                                Name = userSelected[0].Name,
                                LastName = userSelected[0].LastName,
                                Dni = userSelected[0].Dni,
                                Email = userSelected[0].Email,
                                Telephone = userSelected[0].Telephone
                            };
                            Navigation.PushAsync(new SystemPage(loggedUser));
                        }
                        else
                        {
                            DisplayAlert("Error", "Contraseña incorrecta!", "Continuar");
                            btnSingIn.IsEnabled = true;
                        }
                    }
                }
            }
            
        }
    }
}
