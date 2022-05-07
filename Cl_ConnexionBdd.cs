using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Data;

namespace viteMonLogement
{
    public class Cl_ConnexionBdd 
    {
            //Variables
            private static OleDbConnection _laConnexion;
            public static DataSet DTS = new DataSet();
            public static DataGridView DGV = new DataGridView();

            /// <summary>
            /// Verifie si la base est disponible
            /// </summary>
            /// <returns>true or false</returns>
            public static bool Verif_Bdd()
            {
            //Variables

            string cheminAccdb =""; //CL_Divers.mesVariables["Bdd"];
            string cheminLAccdb ="" ;//CL_Divers.mesVariables["Bdd"].Replace(".accdb", ".laccdb");

                //Verif si la BDD est ouverte
                if (File.Exists(cheminLAccdb))
                {
                    try
                    {
                        File.Delete(cheminLAccdb);
                    }

                    catch (Exception ex)
                    {
                        // Supression impossible
                        //CL_Divers.Message_erreur("La base de donnée est ouverte, veuillez la fermer !" + Environment.NewLine + "Voici le message d'erreur :" + Environment.NewLine + ex);
                        return false;
                    }
                }

                //Verif si la connexion est possible
                OleDbConnection connexionBdd = new OleDbConnection();
                connexionBdd.ConnectionString = "Provider=MySQLProv;Data Source=vtm_logement;User Id=root;Password=root;";
                try
                {
                    _laConnexion = connexionBdd;
                    connexionBdd.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    // Connexion impossible
                    //CL_Divers.Message_erreur("La connexion à la base de donnée est impossible !" + Environment.NewLine + "Voici le message d'erreur :" + Environment.NewLine + ex);
                    connexionBdd.Close();
                    return false;
                }


            }

            /// <summary>
            /// Lire dans la BDD
            /// </summary>
            /// <param name="strSQL">La requête SQL</param>
            /// <param name="dgv">Le DGV dans lequel les données doivent être affichées</param>
            /// <returns></returns>
            public static bool Charger_bdd(string strSQL, DataGridView dgv = null)
            {
                //Initialise
                DTS.Tables.Clear();
                OleDbCommand cmdSelect = new OleDbCommand();

                //Test la connexion à la BDD
                if (Verif_Bdd() == false)
                {
                    return false;
                }

                //Importe les valeurs
                cmdSelect.Connection = _laConnexion;
                cmdSelect.CommandText = strSQL;
                OleDbDataAdapter adatp = new OleDbDataAdapter(cmdSelect);
                adatp.Fill(DTS, "TableKami");

                //Charge le DGV
                if (dgv != null)
                {
                    dgv.DataSource = null;
                    dgv.Columns.Clear();
                    dgv.Rows.Clear();
                    dgv.Refresh();
                    dgv.DataSource = DTS.Tables["TableKami"];
                }
                _laConnexion.Close();
                return true;
            }

            /// <summary>
            /// Ecrire dans la BDD
            /// </summary>
            /// <param name="strSql">La requête SQL</param>
            /// <param name="message">Le message à afficher en cas d'erreur d'accès à la base</param>
            /// <returns></returns>
            public static bool Ecrire_bdd(string strSql, string message)
            {
                if (Verif_Bdd() == false)
                {
                    //CL_Divers.Message_erreur(message);
                    return false;
                }
                else
                {
                    OleDbCommand cmd_ajout = new OleDbCommand();
                    cmd_ajout.CommandType = System.Data.CommandType.Text;
                    cmd_ajout.Connection = _laConnexion;
                    cmd_ajout.CommandText = strSql;
                    _laConnexion.Open();
                    cmd_ajout.ExecuteNonQuery();
                    _laConnexion.Close();
                    return true;
                }
            }

            /// <summary>
            /// Récupérer la première valeur du résultat de la requête
            /// </summary>
            /// <param name="strSql">La requête SQL</param>
            /// <returns>La première valeur du résultat de la requête. Si aucun retourne "0"</returns>
            public static string Recup_first_val_bdd(string strSql)
            {
                //Charge BDD
                Charger_bdd(strSql);
                if (DTS.Tables[0].Rows.Count == 0 || DTS.Tables[0].Rows[0].ItemArray[0].ToString() == "")
                { return "0"; }
                else
                { return DTS.Tables[0].Rows[0].ItemArray[0].ToString(); }
            }

        }
    }

}
}
