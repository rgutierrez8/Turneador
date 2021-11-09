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
    public partial class TurnPage : ContentPage
    {
        public User User { get; set; }
        public TurnPage(User user)
        {
            InitializeComponent();

            User = user;

            nameTurnLabel.Text = $"{User.Name} {User.LastName}";
            dniTurnLabel.Text = $"{User.Dni}";
            emailTurnLabel.Text = User.Email;

            tramitTurnEntry.Items.Add("Pagos");
            tramitTurnEntry.Items.Add("Solicitud de información");
            tramitTurnEntry.Items.Add("Presentar documentación");
            tramitTurnEntry.Items.Add("Inscripción");
        }

        public void GetTurn_click(object sender, EventArgs args)
        {
            if(hourTurnEntry.Time.Hours > 8 && hourTurnEntry.Time.Hours < 23)
            {
                if(dateTurnEntry.Date.Year == DateTime.Now.Year)
                {
                    if(tramitTurnEntry.SelectedItem != null)
                    {
                        Turn newTurn = new Turn
                        {
                            SelectedDate = $"{dateTurnEntry.Date} {hourTurnEntry.Time}",
                            Process = $"{tramitTurnEntry.SelectedItem}",
                            UserId = User.Id
                        };

                        using (var connection = new SQLite.SQLiteConnection(App.Route))
                        {
                            connection.CreateTable<Turn>();
                            connection.Insert(newTurn);

                            DisplayAlert("Ok", "Turno generado con éxito!", "Continuar");
                            Navigation.PushAsync(new SystemPage(User));
                        };
                    }
                    else
                    {
                        DisplayAlert("Error", "Debe seleccionar un trámite", "Continuar");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Debe seleccionar una fecha", "Continuar");
                }
            }
            else
            {
                DisplayAlert("Error", "Debe seleccionar un horario", "Continuar");
            }
        }
    }
}