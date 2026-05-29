using Lib_Presentacion_Autos.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Lib_Presentacion_Autos.Implementaciones
{
    public class Comunicaciones : IComunicaciones
    {
        private HttpClient CrearCliente(Dictionary<string, object> datos)
        {
            var client = new HttpClient();
            client.Timeout = new TimeSpan(0, 4, 0);

            if (datos.TryGetValue("X-Usuario", out var usuario) && usuario is string u && !string.IsNullOrEmpty(u))
                client.DefaultRequestHeaders.Add("X-Usuario", u);

            datos.Remove("X-Usuario");
            return client;
        }
        public async Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos)
        {
            var url = datos["Url"].ToString();
            datos.Remove("Url");
            var stringData = datos.ContainsKey("Entidad") ? //pregunta si la entidad existe, si existe la convierte en un json, sino, solo agrega corchetes
                JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json"); // avisa que mandara la entidad convertida en json, en el tiopo de texto que la quiere y le avisa que esta mandando un json

            var httpClient = CrearCliente(datos); //crea al cliente http, Esto es como abrir un navegador invisible desde código. Sirve para hacer peticiones a APIs o páginas web.
            httpClient.Timeout = new TimeSpan(0, 4, 0); //si al intentar acceder se demora 4 minutos, cancela la petición (horas, minutos, segundos)

            var message = await httpClient.GetAsync(url); // ingresa a la url, y pide informacion, como escribir una url en el navegador

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error Comunicacion"); //si algo sucede con la peticion, ingresa un valor de error

            var resp = await message.Content.ReadAsStringAsync(); //convierte la respuesta del servidor en texto
            httpClient.Dispose(); httpClient = null; //destruye el httpClient

            resp = Replace(resp); // llama al metodo de replace que esta abajo, que recibe la respuesta y modifica como se veran los datos 
            return new Dictionary<string, object>() {
                { "Valor", resp }// regresa el resultado de todo aquello
            };
        }


        public async Task<Dictionary<string, object>> EjecutarPost(Dictionary<string, object> datos)
        {
            var url = datos["Url"].ToString();
            datos.Remove("Url");
            var stringData = datos.ContainsKey("Entidad") ?
                JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            var httpClient = CrearCliente(datos);
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            var message = await httpClient.PostAsync(url, body); // como se envia el body al servidor, se necesita recibir en el metodo

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error Comunicacion");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose(); httpClient = null;

            resp = Replace(resp);
            return new Dictionary<string, object>() {
                { "Valor", resp }
            };
        }

        public async Task<Dictionary<string, object>> EjecutarPut(Dictionary<string, object> datos)
        {
            var url = datos["Url"].ToString();
            datos.Remove("Url");
            var stringData = datos.ContainsKey("Entidad") ?
                JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            var httpClient = CrearCliente(datos);
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            var message = await httpClient.PutAsync(url, body); // como se envia el body al servidor, se necesita recibir en el metodo

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error Comunicacion");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose(); httpClient = null;

            resp = Replace(resp);
            return new Dictionary<string, object>() {
                { "Valor", resp }
            };
        }

        public async Task<Dictionary<string, object>> EjecutarDelete(Dictionary<string, object> datos)
        {
            var url = datos["Url"].ToString();
            datos.Remove("Url");
            var stringData = datos.ContainsKey("Entidad") ?
                JsonConvert.SerializeObject(datos["Entidad"]) : "{}";
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            var httpClient = CrearCliente(datos);
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            var request = new HttpRequestMessage //voy a crear una peticion http
            {
                Method = HttpMethod.Delete, // aqui decimos que la petición sera sobre borrar algo
                RequestUri = new Uri(url!), //aca decimos que la petición sera a esa url
                Content = body // y aca metemos el contenido de la entidad dentrro
            };

            var message = await httpClient.SendAsync(request); // aca se envia la petición 

            if (!message.IsSuccessStatusCode)
                throw new Exception("Error Comunicacion");

            var resp = await message.Content.ReadAsStringAsync();
            httpClient.Dispose(); httpClient = null;

            resp = Replace(resp);
            return new Dictionary<string, object>() {
                { "Valor", resp }
            };
        }

        private string Replace(string resp)
        {
            // Si la respuesta viene envuelta en comillas extra de una serialización doble, las quitamos
            if (resp.StartsWith("\"") && resp.EndsWith("\""))
                resp = resp.Substring(1, resp.Length - 2);

            return resp
                .Replace("\\r\\n", "")
                .Replace("\\n", "")
                .Replace("\\r", "");
        }
    }
}
