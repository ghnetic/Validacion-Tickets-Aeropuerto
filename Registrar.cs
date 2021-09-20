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
    public partial class Registrar : Form
    {
        private SQLiteConnection sqLite;
        private int idPais;
        private int idRegion1;
        private int idRegion2;
        private int idRegion3;
        private int idRegion4;
        private string idPersona = "";
        private List<int> idPaises = new List<int>();
        private List<int> idRegiones1 = new List<int>();
        private List<int> idRegiones2 = new List<int>();
        private List<int> idRegiones3 = new List<int>();
        private List<int> idRegiones4 = new List<int>();
        SQLiteDataAdapter ad;
        SQLiteDataReader dr;
        public Registrar()
        {
            InitializeComponent();

            sqLite = new SQLiteConnection("Data Source= sistemaAeropuerto.db");
            cargarPaises();
            cargarRegion2();
            cargarRegion3();
            cargarRegion4();
        }

        private void Registrar_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            DataTable table = selectQuery("SELECT pasaporte, codigo, p_nombre, p_apellido, rol FROM Persona");
            dgListadoPersonas.DataSource = table;

            cCodigo.Text = GenerarCodigo();
        }

        public DataTable selectQuery(string query)
        {
            
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqLite.Open();
                cmd = sqLite.CreateCommand();
                cmd.CommandText = query;
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.ToString(), "No se puede mostrar");
            }
            sqLite.Close();
            return dt;
        }


        private void cargarPaises()
        {
            sqLite.Open();
            string querys = "SELECT nombrePais,idPais FROM Pais";
            var cmd = new SQLiteCommand(querys, sqLite);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbPais.Items.Add(dr["nombrePais"].ToString());
                idPaises.Add(Int32.Parse(dr["idPais"].ToString()));
            }
            sqLite.Close();
        }

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            idPais = idPaises[cbPais.SelectedIndex];
        }

        private void cargarRegion2()
        {
            sqLite.Open();
            string querys = "SELECT nombrePais,idPais FROM Pais";
            var cmd = new SQLiteCommand(querys, sqLite);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbRegion2.Items.Add(dr["nombrePais"].ToString());
                idRegiones2.Add(Int32.Parse(dr["idPais"].ToString()));
            }
            sqLite.Close();
        }

        private void cbRegion2_SelectedIndexChanged(object sender, EventArgs e)
        {
            idRegion2 = idRegiones2[cbRegion2.SelectedIndex];
        }


        private void cargarRegion3()
        {
            sqLite.Open();
            string querys = "SELECT nombrePais,idPais FROM Pais";
            var cmd = new SQLiteCommand(querys, sqLite);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbRegion3.Items.Add(dr["nombrePais"].ToString());
                idRegiones3.Add(Int32.Parse(dr["idPais"].ToString()));
            }
            sqLite.Close();
        }

        private void cbRegion3_SelectedIndexChanged(object sender, EventArgs e)
        {
            idRegion3 = idRegiones3[cbRegion3.SelectedIndex];
        }


        private void cargarRegion4()
        {
            sqLite.Open();
            string querys = "SELECT nombrePais,idPais FROM Pais";
            var cmd = new SQLiteCommand(querys, sqLite);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbRegion4.Items.Add(dr["nombrePais"].ToString());
                idRegiones4.Add(Int32.Parse(dr["idPais"].ToString()));
            }
            sqLite.Close();
        }

        private void cbRegion4_SelectedIndexChanged(object sender, EventArgs e)
        {
            idRegion4 = idRegiones4[cbRegion4.SelectedIndex];
        }

        private void crearPersona_Click(object sender, EventArgs e)
        {
            try
            {
                crearPersonas();
                limpiarForm();
            }
            catch
            {
                MessageBox.Show("NO SE PUEDE CREAR PERSONA");
            }
        }

        private void limpiarForm()
        {
            cP_Nombre.Text = "";
            cS_Nombre.Text = "";
            cP_Apellido.Text = "";
            cS_Apellido.Text = "";
            cbPais.Text = "";
            cbRol.Text = "";
            cPasaporte.Text = "";
            cTel2.Text = "";
            cTel3.Text = "";
            cTel4.Text = "";
            cbRegion2.Text = "";
            cbRegion3.Text = "";
            cbRegion4.Text = "";
        }


        private void crearPersonas()
        {
            try
            {
                sqLite.Open();
                var cmd = new SQLiteCommand(sqLite);
                cmd.CommandText= "INSERT INTO PERSONA(pasaporte, codigo,idPais,p_nombre,s_nombre,p_apellido,s_apellido,rol) VALUES(@pasaporte, @codigo,@idPais,@p_nombre,@s_nombre,@p_apellido,@s_apellido,@rol)";
                string PASAPORTE = cPasaporte.Text;
                string CODIGO = cCodigo.Text;
                string P_NOMBRE = cP_Nombre.Text;
                string S_NOMBRE = cS_Nombre.Text;
                string P_APELLIDO = cP_Apellido.Text;
                string S_APELLIDO = cS_Apellido.Text;
                string ROL = cbRol.Text;

                cmd.Parameters.AddWithValue("@pasaporte", PASAPORTE);
                cmd.Parameters.AddWithValue("@codigo", CODIGO);
                cmd.Parameters.AddWithValue("@idPais", idPais);
                cmd.Parameters.AddWithValue("@p_nombre", P_NOMBRE);
                cmd.Parameters.AddWithValue("@s_nombre", S_NOMBRE);
                cmd.Parameters.AddWithValue("@p_apellido", P_APELLIDO);
                cmd.Parameters.AddWithValue("@s_apellido", S_APELLIDO);
                cmd.Parameters.AddWithValue("@rol", ROL);
                
                cmd.ExecuteNonQuery();
                idPersona = cPasaporte.Text;

                insertarTelefono(cTel2.Text, idRegion2);
                insertarTelefono(cTel3.Text, idRegion3);
                insertarTelefono(cTel4.Text, idRegion4);

            }
            catch { MessageBox.Show("Error al Crear Persona"); }
            sqLite.Close();
        }


        private void insertarTelefono(string telefono, int region)
        {
            Console.WriteLine("ESTE ES EL ID DE LA PERSONAAAA: "+idPersona);
            Console.WriteLine("Este es el telefono: " + telefono);
            Console.WriteLine("Este es el id del pais: " + region);
            try
            {
                sqLite.Open();
                var cmd = new SQLiteCommand(sqLite);
                cmd.CommandText = "INSERT INTO Telefono(pasaporte,telefono,idPais) VALUES(@pasaporte, @telefono, @idPais)";
                cmd.Parameters.AddWithValue("@pasaporte", idPersona);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@idPais", region);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Nada wey ):");
            }
            sqLite.Close();
        }

        public string GenerarCodigo()
        {
            string codigo = string.Empty;
            int Total = 0;

            sqLite.Open();
            string querys = "SELECT Count(*) as TotalPersonas from Persona";
            var cmd = new SQLiteCommand(querys, sqLite);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Total = Convert.ToInt32(dr["TotalPersonas"]) + 1;
            }
            dr.Close();

            if (Total < 10)
            {
                codigo = "00000" + Total;
            }else if(Total < 100)
            {
                codigo = "0000" + Total;
            }else if (Total < 1000)
            {
                codigo = "000" + Total;
            }else if(Total < 10000)
            {
                codigo = "00" + Total;
            }else if (Total < 100000)
            {
                codigo = "0" + Total;
            }
            sqLite.Close();

            return codigo;
        }
    }
}
