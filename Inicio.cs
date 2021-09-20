using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanadaAir
{
    public partial class Inicio : Form
    {
        private SQLiteConnection sqLite;
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

        }

        private void registrar_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new Registrar());
        }

        private void historial_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new Historial());
        }

        private void acciones_Click(object sender, EventArgs e)
        {
            abrirFormEnPanel(new Acciones());
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Form formActivado = null;

        private void abrirFormEnPanel(Form formHijo)
        {
            if (formActivado != null)
            {
                formActivado.Close();
            }
            formActivado = formHijo;
            formHijo.TopLevel = false;
            formHijo.Dock = DockStyle.Fill;
            Pfmrs.Controls.Add(formHijo);
            Pfmrs.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show();
        }
    }
}
