using Newtonsoft.Json;
using ProyectoCarpeta.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

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

        public void consiliarArchivos()
        {
            
            DirectoryInfo di = new DirectoryInfo(obtenerRuta());
            FileInfo[] files = di.GetFiles();
            
            //FileInfo[] files = di.GetFiles("*.xlsx");
            string str = "";
            foreach (FileInfo file in files)
            {
                if (file.Name.Contains(".xlsx"))
                {
                    if (File.Exists("consiliado.xlsx"))
                    {

                    }
                    else 
                    {
                        using (FileStream fs = File.Create(obtenerRuta()+ "\\consiliado.xlsx"))
                        {
                            Excel.Application consiliado = default(Excel.Application);
                            // Creamos un objeto WorkBook. Para crear el documento Excel.           
                            Excel.Workbook LibroExcel = default(Excel.Workbook);
                            // Creamos un objeto WorkSheet. Para crear la hoja del documento.
                            Excel.Worksheet HojaExcel = default(Excel.Worksheet);

                            // Iniciamos una instancia a Excel, y Hacemos visibles para ver como se va creando el reporte, 
                            // podemos hacerlo visible al final si se desea.
                            consiliado = new Excel.Application();
                            consiliado.Visible = true;

                            /* Ahora creamos un nuevo documento y seleccionamos la primera hoja del 
            * documento en la cual crearemos nuestro informe. 
            */
                            // Creamos una instancia del Workbooks de excel.            
                            LibroExcel = consiliado.Workbooks.Add();
                            // Creamos una instancia de la primera hoja de trabajo de excel            
                            HojaExcel = LibroExcel.Worksheets[1];
                            HojaExcel.Visible = Excel.XlSheetVisibility.xlSheetVisible;

                            // Hacemos esta hoja la visible en pantalla 
                            // (como seleccionamos la primera esto no es necesario
                            // si seleccionamos una diferente a la primera si lo
                            // necesitariamos).
                            HojaExcel.Activate();

                            // Crear el encabezado de nuestro informe.
                            // La primera línea une las celdas y las convierte un en una sola.            
                            HojaExcel.Range["A1:E1"].Merge();
                            // La segunda línea Asigna el nombre del encabezado.
                            HojaExcel.Range["A1:E1"].Value = "----------------------------------------------";
                            // La tercera línea asigna negrita al titulo.
                            HojaExcel.Range["A1:E1"].Font.Bold = true;
                            // La cuarta línea signa un Size a titulo de 15.
                            HojaExcel.Range["A1:E1"].Font.Size = 15;

                            // Crear el subencabezado de nuestro informe
                            HojaExcel.Range["A2:E2"].Merge();
                            HojaExcel.Range["A2:E2"].Value = "ENCUESTA DE SATISFACCIÓN AL CLIENTE EXTERNO";
                            HojaExcel.Range["A2:E2"].Font.Italic = true;
                            HojaExcel.Range["A2:E2"].Font.Size = 13;

                            Excel.Range objCelda = HojaExcel.Range["A3", Type.Missing];
                            objCelda.Value = "ID";

                            objCelda = HojaExcel.Range["B3", Type.Missing];
                            objCelda.Value = "Preguntas";

                            objCelda = HojaExcel.Range["C3", Type.Missing];
                            objCelda.Value = "Opciones";

                            objCelda = HojaExcel.Range["D3", Type.Missing];
                            objCelda.Value = "Valor de la Respuesta";

                            objCelda = HojaExcel.Range["E3", Type.Missing];
                            objCelda.Value = "Numero Votos";

                            objCelda.EntireColumn.NumberFormat = "###,###,###.00";

                            int i = 4;
                            //foreach (DataRow Row in DS.Tables[0].Rows)
                            //{
                            //    // Asignar los valores de los registros a las celdas
                            //    HojaExcel.Cells[i, "A"] = Row.ItemArray[0];
                            //    // ID
                            //    HojaExcel.Cells[i, "B"] = Row.ItemArray[1];
                            //    // Pregunta
                            //    HojaExcel.Cells[i, "C"] = Row.ItemArray[2];
                            //    // Opciones
                            //    HojaExcel.Cells[i, "D"] = Row.ItemArray[3];
                            //    // Valor de la Respuesta
                            //    HojaExcel.Cells[i, "E"] = Row.ItemArray[4];
                            //    // Numero Votos

                            //    // Avanzamos una fila
                            //    i++;
                            //}

                            // Seleccionar todo el bloque desde A1 hasta D #de filas.
                            Excel.Range Rango = HojaExcel.Range["A3:E" + (i - 1).ToString()];

                            // Selecionado todo el rango especificado
                            Rango.Select();

                            // Ajustamos el ancho de las columnas al ancho máximo del
                            // contenido de sus celdas
                            Rango.Columns.AutoFit();

                            // Asignar filtro por columna
                            Rango.AutoFilter(1);

                            // Crear un total general
                            LibroExcel.PrintPreview();

                            
                            byte[] info = new UTF8Encoding(true).GetBytes(consiliado.UserName);
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                }
                else 
                {
                    
                    string otros = obtenerRuta() + "\\otros\\"+file.Name;
                    System.IO.File.Move(file.FullName, otros);


                }
                    
              
                
            }
        }
    }
}
