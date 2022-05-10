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
        RutaController ruta = new RutaController();
        private bool ruote = false;
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
            Console.WriteLine(e.SignalTime.Date); 
            
        }
        

        private void btnRuta_Click(object sender, EventArgs e)
        {
            using (var fd = new FolderBrowserDialog())
            {
                
                    System.Diagnostics.Process.Start(lblRuta.Text);
                
            }
        }
        private void path() 
        {
            RutaController rc = new RutaController();
            ruote = ruta.rutaPrincipal();
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
        }

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
