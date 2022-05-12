using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace viteMonLogement
{
    public class CL_Bdd
    {
        //Variables
        public static DataTable DT = new DataTable();
        public static DataGridView DGV = new DataGridView();

        /// <summary>
        /// Lire et écrire dans la BDD
        /// </summary>
        /// <param name="type">Lecture pour lire sinon, n'importe quoi</param>
        /// <param name="sql">La requête SQL</param>
        /// <param name="erreur">Le message d'erreur</param>
        /// <param name="dgv">Le dgv à remplir (peut être vide)</param>
        public static bool Connexion_Bdd(string type, string sql, string erreur, DataGridView dgv = null)
        {

            //Essayer
            try
            {

                //Connexion à la BDD
                String host_name = "";
                String database = "";
                String user_name = "";
                String password = "";

                string connStr = "server=" + host_name + ";uid=" + user_name + ";pwd=" + password + ";database=" + database;

                MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder
                {
                    { "database", "vtm_logement" },
                    { "server", "localhost" },
                    { "User Id", "root" },
                    { "Password", "root" }
                };
                //localhost str Connexion
                MySqlConnection connection = new MySqlConnection(connBuilder.ConnectionString);

                //online str connexion
                //MySqlConnection connection = new MySqlConnection(connStr);

                MySqlCommand cmd = connection.CreateCommand();

                //Ouverture de la base
                connection.Open();

                //Ecriture
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();

                //Si lecture
                if (type == "Lecture")
                {
                    DT = new DataTable();
                    DT.Load(reader);
                    if (dgv != null)
                    {
                        dgv.DataSource = null;
                        dgv.Columns.Clear();
                        dgv.Rows.Clear();
                        dgv.Refresh();
                        dgv.DataSource = DT;

                    }
                }

                //Fermeture de la base
                connection.Close();

                //Return
                return true;
            }
            catch (Exception ex)
            {
                //Message d'erreur
                //CL_Divers.Message_erreur(erreur);
                MessageBox.Show(ex.ToString());
                //Return
                return false;
            }
        }

        /// <summary>
        /// Récupérer la première valeur du résultat de la requête
        /// </summary>
        /// <param name="strSql">La requête SQL</param>
        /// <param name="erreur">Le message d'erreurL</param>
        /// <returns>La première valeur du résultat de la requête. Si aucun retourne "0"</returns>
        public static string Recup_first_val_bdd(string strSql, string erreur)
        {
            //Charge BDD
            Connexion_Bdd("Lecture", strSql, erreur);
            if (DT.Rows.Count == 0 || DT.Rows[0].ItemArray[0].ToString() == "")
            { return "0"; }
            else
            { return DT.Rows[0].ItemArray[0].ToString(); }
        }

    }
}
