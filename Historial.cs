using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanadaAir
{
    public partial class Historial : Form
    {
        private SQLiteConnection sqLite;
        public Historial()
        {
            InitializeComponent();
            sqLite = new SQLiteConnection("Data Source= sistemaAeropuerto.db");
        }


        private void Historial_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            // You can use this function only to execute the DB statements
            DataTable table = selectQuery("SELECT p.pasaporte, p.p_nombre, p.p_apellido, ap.fecha, ap.hora, a.nombreActividad from ActividadXPersona ap INNER JOIN Persona p on p.pasaporte=ap.pasaporte INNER JOIN Actividad a on a.idActividad=ap.idActividad");
            // I added this for testing
            // Please consider removing this dataGridView from your design too.
            dgHistorial.DataSource = table;
        }

        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqLite.Open();  //Initiate connection to the db
                cmd = sqLite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
                MessageBox.Show(ex.ToString(), "SQL Error");
            }
            sqLite.Close();
            return dt;
        }

    }
}
