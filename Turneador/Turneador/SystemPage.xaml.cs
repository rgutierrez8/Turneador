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
            List<Turn> turn;
            base.OnAppearing();

            using(var connection = new SQLite.SQLiteConnection(App.Route))
            {
                connection.CreateTable<Turn>();
                connection.CreateTable<User>();
                turn = connection.Table<Turn>().Where(user => user.UserId == User.Id).ToList();

                if(turn.Count == 0)
                {
                    infoLabel.Text = "No hay turnos asociados a este usuario";
                }
                else
                {
                    turnListView.ItemsSource = turn;
                }
            }
        }
    }
}