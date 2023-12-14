using System.Collections.ObjectModel;
namespace Maui_App8
{
    public partial class MainPage : ContentPage
    {
        public class Usuario
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string usuario { get; set; }
            public string contrasena { get; set; }
        }
        ObservableCollection<Usuario> datos = new ObservableCollection<Usuario>();

        public MainPage()
        {
            InitializeComponent();
        }
        public async Task llamadaGetJsonAsync(string url)
        {
            //Creamos una instancia de HttpClient
            var client = new HttpClient();
            //Asignamos la URL
            client.BaseAddress = new Uri(url);
            //Llamada asíncrona al sitio
            var response = await client.GetAsync(client.BaseAddress);
            //Nos aseguramos de recibir una respuesta satisfactoria
            response.EnsureSuccessStatusCode();
            //Convertimos la respuesta a una variable string
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            // titulo.Text = jsonResult;
            //Se deserializa la cadena y se convierte en una instancia de WeatherResult
            var listado =
            Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(jsonResult);
            //Asignamos el nuevo valor de las propiedades
            datos = listado;
            //titulo.Text = listado.Count.ToString();
            //Console.WriteLine("Numero de usuarios:"+listado.Count);
            milista.ItemsSource = null;
            milista.ItemsSource = datos;
            //foreach (Usuario element in listado.)
        }
        private void milista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var myListView = (ListView)sender;
            var myItem = myListView.SelectedItem;
            int index = datos.IndexOf((Usuario)myItem);
            DisplayAlert("Usuario", "Nombre: " + datos[index].nombre + "\nUsuario" +
            datos[index].usuario, "ok");
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                _ = llamadaGetJsonAsync("https://tilinazo.000webhostapp.com/listar.php");
            }
            catch
            {
                DisplayAlert("Mensa", "Error al tratar de conectarse", "ok");
            }
        }
    }
}