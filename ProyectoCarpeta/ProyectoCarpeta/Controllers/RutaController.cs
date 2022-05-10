using Newtonsoft.Json;
using ProyectoCarpeta.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCarpeta.Controllers
{
    public class RutaController
    {
        string path = Directory.GetCurrentDirectory() + "\\ruta.json";

        //valida si exite el archivo
        public bool rutaPrincipal()
        {

            bool result = File.Exists(path);
            if (result == true)
            {
            
                return true;

            }
            else
            {
             
                return false;

            }
        }
        //valida si existe carpetas
        public void ValidarCarpetas(bool excel, bool otros, string ruta) 
        {
            bool validaExcel = File.Exists(ruta+"\\excel");
            bool validaOtros = File.Exists(ruta + "\\otros");

            excel = validaExcel;
            otros = validaOtros;
        }
        //crea la ruta
        public void CrearRuta(string ruta)
        {
            RutaModel dejarRuta = new RutaModel();
            dejarRuta.Ruta = ruta;
            string output = JsonConvert.SerializeObject(dejarRuta);
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(output);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        //retorna la ruta
        public string obtenerRuta() 
        {
            RutaModel rm = new RutaModel();
            string json = "";
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    json = s;
                }
            }
          rm = JsonConvert.DeserializeObject<RutaModel>(json);
            return rm.Ruta;
        }

        //EditarRuta
        public void EditarRuta(string ruta)
        {
            RutaModel dejarRuta = new RutaModel();
            dejarRuta.Ruta = ruta;
            string output = JsonConvert.SerializeObject(dejarRuta);
            try
            {
                File.Delete(path);
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(output);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        //crearCapetas
        public void CrearCarpetas(string ruta, string carpeta) 
        {
       Directory.CreateDirectory(ruta+"\\"+ carpeta);
        }

    }
}
