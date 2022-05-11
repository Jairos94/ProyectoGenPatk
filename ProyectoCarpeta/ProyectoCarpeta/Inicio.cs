using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Timers;
using System.Windows.Forms;
using System.IO;
using ProyectoCarpeta.Controllers;

namespace ProyectoCarpeta
{
    public partial class Inicio : Form
    {
        
        private System.Timers.Timer aTimer;
        
       
        public Inicio()
        {
            
            InitializeComponent();
            Temporizador();
            path();
        }
        //temporizador
        private void Temporizador() 
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(15000);// ejecución cada minuto
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;// llama al método 
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            
        }

        //Evento Temporizador
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            RutaController rc = new RutaController();
            rc.consiliarArchivos();

        }
        
        //Lo lleva a la ruta
        private void btnRuta_Click(object sender, EventArgs e)
        {
            using (var fd = new FolderBrowserDialog())
            {
                
                    System.Diagnostics.Process.Start(lblRuta.Text);
                
            }
        }
        //valida si existen archivos
        private void path() 
        {
            bool ruote = false;
            bool exel = false;
            bool otros = false;
            RutaController ruta = new RutaController();
            RutaController rc = new RutaController();
            ruote = ruta.rutaPrincipal();
            //verifica la ruta
            if (!ruote)
            {
                string rutaDefinida = "";
                using (var fd = new FolderBrowserDialog())
                {
                    if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath))
                    {
                        rutaDefinida = fd.SelectedPath;
                    }
                }
                rc.CrearRuta(rutaDefinida);
            }
            lblRuta.Text = rc.obtenerRuta();

            ruta.ValidarCarpetas(exel,otros, lblRuta.Text);

            if (!exel) 
            {
                rc.CrearCarpetas(lblRuta.Text, "excel");
            }
            if (!otros) 
            {
                rc.CrearCarpetas(lblRuta.Text, "otros");
            }
        }

        //Cambia la ruta 
        private void btnCambiarRuta_Click(object sender, EventArgs e)
        {
            RutaController rc = new RutaController();
            using (var fd = new FolderBrowserDialog())
            {
                if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath))
                {
                    string ruta = fd.SelectedPath; ;
                    lblRuta.Text = ruta;
                    rc.EditarRuta(ruta);
                }
            }
        }
    }
}
