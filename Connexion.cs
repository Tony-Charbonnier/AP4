using System;
using System.Data;
using System.Windows.Forms;

namespace viteMonLogement
{

    public partial class Connexion : Form
    {




        public Connexion()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string user1 = PRI_PN_LOG_TXT_USER.Text;
            string mdpUser = PRI_PN_LOG_TXT_PASSWORD.Text;
            string sql1 = "SELECT * FROM utilisateur WHERE login='" + user1 + "' AND mdp='" + mdpUser + "'";
            string co = CL_Bdd.Recup_first_val_bdd(sql1, "not Working");

            if (co == "0")
            {
                MessageBox.Show("Login ou Mot de passe incorect");
            }
            else
            {
                CL_Bdd.Connexion_Bdd("lecture", sql1, "nop");
                DataTable userTb = CL_Bdd.DT;
                var date1 = new DateTime(2000, 06, 12);
                if (userTb.Rows[0].ItemArray[11].ToString().Length != 0)
                {
                    date1 = DateTime.Parse(userTb.Rows[0].ItemArray[11].ToString());
                }


                string login = userTb.Rows[0].ItemArray[1].ToString();
                int idType = int.Parse(userTb.Rows[0].ItemArray[10].ToString());
                int id = int.Parse(userTb.Rows[0].ItemArray[0].ToString());
                string nom = userTb.Rows[0].ItemArray[4].ToString();
                string prenom = userTb.Rows[0].ItemArray[5].ToString();
                string mail = userTb.Rows[0].ItemArray[3].ToString();
                string adresse = userTb.Rows[0].ItemArray[6].ToString();
                string CP = userTb.Rows[0].ItemArray[7].ToString();
                string ville = userTb.Rows[0].ItemArray[8].ToString();
                string Telephone = userTb.Rows[0].ItemArray[10].ToString();
                String RIB = userTb.Rows[0].ItemArray[12].ToString();
                String Banque = userTb.Rows[0].ItemArray[13].ToString();
                int Rev = int.Parse(userTb.Rows[0].ItemArray[14].ToString());

                CL_user.userReg1 = new CL_user(login, idType, id, date1, nom, prenom, mail, adresse, CP, ville, Telephone);
                CL_Dossier.Dossier1 = new CL_Dossier(id, RIB, Rev, nom, prenom, mail, adresse, CP, ville, Banque, Telephone, date1);

                if (CL_user.userReg1.getIdType() == 1)
                {
                    new userType1().Show();
                }
                else if (CL_user.userReg1.getIdType() == 2)
                {
                    new userType2().Show();
                }
                else if (CL_user.userReg1.getIdType() == 3)
                {
                    new userType3().Show();
                }





                this.Hide();

            }
            PRI_PN_LOG_TXT_USER.Clear();
            PRI_PN_LOG_TXT_PASSWORD.Clear();
            PRI_PN_LOG_TXT_USER.Focus();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            PRI_PN_LOG_TXT_USER.Clear();
            PRI_PN_LOG_TXT_PASSWORD.Clear();
            PRI_PN_LOG_TXT_USER.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PRI_PN_LOG_TXT_PASSWORD.PasswordChar = '*';
            pictureBox4.BringToFront();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PRI_PN_LOG_TXT_PASSWORD.PasswordChar = '\0';
            pictureBox5.BringToFront();
        }
    }
}
