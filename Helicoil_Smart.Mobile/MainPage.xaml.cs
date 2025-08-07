using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using System.Text.Json;
using System.Text;

namespace Helicoil_Smart.Mobile
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnRegistrarClicked(object sender, EventArgs e)
        {
            var nombre = nombreEntry.Text;
            var cedula = cedulaEntry.Text;
            var fechaHora = DateTime.Now;

            var ubicacion = await Geolocation.GetLastKnownLocationAsync();

            if (ubicacion == null)
            {
                ubicacion = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            if (ubicacion == null)
            {
                await DisplayAlert("Error", "No se pudo obtener la ubicación", "OK");
                return;
            }

            string datos = $"{nombre},{cedula},{fechaHora},{ubicacion.Latitude},{ubicacion.Longitude}";

            // Guardar en archivo CSV (modo Excel)
            var path = Path.Combine(FileSystem.AppDataDirectory, "registros.csv");
            File.AppendAllText(path, datos + Environment.NewLine);

            resultadoLabel.Text = "Datos registrados correctamente.";
        }
    }

}
