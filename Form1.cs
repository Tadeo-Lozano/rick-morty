using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RickAndMorty
{
    public partial class Form1 : Form
    {
        static HttpClient client = new HttpClient();
        static string urlPersonajes = "character";
        static string urlEpisodios = "episode";
        static string urlLocacion = "location";

        public class Personaje
        {
            public int id { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public string type { get; set; }
            public string species { get; set; }
            public string gender { get; set; }
            public string status { get; set; }
        }

        public class Locacion
        {
            public int id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string dimension { get; set; }
            public string url { get; set; }
        }

        public class Episodio
        {
            public int id { get; set; }
            public string name { get; set; }
            public string air_date { get; set; }
            public string episode { get; set; }
            public string url { get; set; }
            public string created { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbOpcion.SelectedItem.ToString()=="Personaje")
            {
                //lblResumen.Text = "1";
                Personaje respuesta = ObtenPersonaje(txtID.Text).GetAwaiter().GetResult();
                pctImagen.ImageLocation = respuesta.image;
                lblResumen.Text = "Nombre: " + respuesta.name + "\n" + "Tipo: " + respuesta.type + "\n" + "Especie: " + respuesta.species + "\n" + "Genero: " + respuesta.gender + "\n" + "Estatus: " + respuesta.status;
            }
        }

        static async Task<Personaje> ObtenPersonaje(string id)
        {
            Personaje personaje;
            HttpResponseMessage response = await client.GetAsync(urlPersonajes + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                personaje = await response.Content.ReadFromJsonAsync<Personaje>();
                return personaje;
            }
            return null;
        }

        static async Task<Episodio> ObtenEpisodio(string id)
        {
            Episodio episodio;
            HttpResponseMessage response = await client.GetAsync(urlEpisodios + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                episodio = await response.Content.ReadFromJsonAsync<Episodio>();
                return episodio;
            }
            return null;
        }

        static async Task<Locacion> ObtenLocacion(string id)
        {
            Locacion locacion;
            HttpResponseMessage response = await client.GetAsync(urlLocacion + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                locacion = await response.Content.ReadFromJsonAsync<Locacion>();
                return locacion;
            }
            return null;
        }
    }
}