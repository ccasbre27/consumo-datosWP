using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVVM.Common
{
    public class ServiceModel
    {
        public event EventHandler<FlickCamerasEventArgs> GetCamerasCompleted;
        // método asincrono que devuelve lista de cámaras
        public async void getCameras()
        {
            string uri = "https://api.flickr.com/services/rest/?method=flickr.cameras.getBrands&api_key=c9ea2337723bf09a027e0258c59c505f&format=rest";

            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(uri); // awwait es para que espere que termine

             //LINQ para XML

             // xdocument permite interpretar una cadena
             var doc = XDocument.Parse(result);

             // en doc ya está el documento en xml

             // realizamos una consulta para cada uno de los elementos que sea descendiente del elemento brand
             var query = from c in doc.Descendants("brand")
             select new Camera() { Name = c.Attribute("name").Value }; // creamos una nueva cámara y la instanciamos con el nombre de la cámara que está en el archivo de xml

             // al ejecutar el método ToList regresa una lista de tipo cámara en este caso es así porque se instanciaron elemento de tipo cámara
             var results = query.ToList();

             if (GetCamerasCompleted != null)
             {
                GetCamerasCompleted(this, new FlickCamerasEventArgs(results));
             }
            
        }
    }
}
