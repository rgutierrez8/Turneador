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

                        //==================================== SECUENCIA SQLITE ==============================================
                        //using (var connection = new SQLite.SQLiteConnection(App.Route))
                        //{
                        //    connection.CreateTable<Turn>();
                        //    connection.Insert(newTurn);

                        //    DisplayAlert("Ok", "Turno generado con éxito!", "Continuar");
                        //    Navigation.PushAsync(new SystemPage(User));
                        //};
                        //====================================================================================================

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

                        command.CommandText = "INSERT into Turn (SelectedDate, Process, State, UserId) values ('" + dateTurnEntry.Date + " " + hourTurnEntry.Time + "', '" + tramitTurnEntry.SelectedItem + 
                                                "', 0, '" + User.Id + "' )";
                        command.ExecuteNonQuery();
                        Navigation.PushAsync(new SystemPage(User));

                        con2.Close();
                    }
                    else
                    {
                        DisplayAlert("Error", "Debe seleccionar un trámite", "Continuar");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Debe seleccionar una fecha actual o futura", "Continuar");
                }
            }
            else
            {
                DisplayAlert("Error", "Debe seleccionar un horario entre las 8 y 23hs", "Continuar");
            }
        }
    }
}