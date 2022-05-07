using System;
using System.Data;
using System.Windows.Forms;

namespace viteMonLogement
{
    public partial class userType1 : Form
    {
        int indxDT;
        
        public userType1()
        {


            InitializeComponent();
            UserTest.Text = "Bonjour " + CL_user.userReg1.getPrenom() + " " + CL_user.userReg1.getNom() + ". Bienvenue sur ViteMonLogement !";
            int idloca = int.Parse(CL_user.userReg1.getId().ToString());
            DateTime timeNow = DateTime.Now;
            String date = timeNow.Date.ToString("yyyy-MM-dd");
            String sqlLoc = "SELECT * FROM locataire WHERE ID_Locataire=" + idloca + " and (DateFinLoc>='" + date + "' or DateFinLoc is NULL)";
            CL_Bdd.Connexion_Bdd("Lecture", sqlLoc, "Execption Ligne 15 Loca");
            DataTable loca = CL_Bdd.DT;

            if (loca.Rows.Count != 0)
            {
                
                PRN_PN_MAIN_BUTTON_MON_LOGEMENT.BringToFront();
                CL_user.userReg1.setIdLogement(loca.Rows[0].ItemArray[1].ToString());
                PRN_PN_MAIN_BUTTON_LOGEMENTS.Visible = false;


                if (loca.Rows[0].ItemArray[3].ToString() != "")
                {
                    DateTime dt = DateTime.Parse(loca.Rows[0].ItemArray[3].ToString());
                    String dt2 = dt.Date.ToString("yyyy-MM-dd");
                    PRN_LABEL_CompteAReBour.Text = " Vous devez quittez votre logement le :" + dt;
                    PRN_PN_LOC_MON_LOG_B_ANNULE_QUITTE_LOG.BringToFront();
                    PRN_LABEL_CompteAReBour.Visible = true;
                }
            }




        }


        private void button2_Click(object sender, EventArgs e)
        {

            var dgv = PRN_PN_LOC_LOGEMENT_DGV_APPART;
            String sqlAp = "SELECT `id`,`TYPAPPART`,`PRIX_LOC`,`PRIX_CHARG`,`RUE`,`ETAGE`,`DATE_LIBRE`,`Ville`,`CP`,`ASCENSEUR` FROM appartements  WHERE id NOT IN(SELECT id_appartement FROM locataire) ";
            CL_Bdd.Connexion_Bdd("Lecture", sqlAp, "ca marche po", dgv);
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].HeaderText = "Type appartement";
            dgv.Columns[2].HeaderText = "Prix location";
            dgv.Columns[3].HeaderText = "Prix charge";
            dgv.Columns[4].HeaderText = "adresse";
            dgv.Columns[5].HeaderText = "Etage";
            dgv.Columns[6].HeaderText = "Date logement libre";
            dgv.Columns[7].HeaderText = "Ville";
            dgv.Columns[8].HeaderText = "Code postale";
            dgv.Columns[9].HeaderText = "Presence d'un ascenseur";
            PRN_PN_LOC_LOGEMENT.BringToFront();
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CL_user.userReg1.destrucUser();
            CL_Dossier.Dossier1.destrucDossier();
            this.Hide();
            new Connexion().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PRN_PN_LOC_MAIN.BringToFront();

        }

        private void PRN_PN_MAIN_BUTTON_MON_LOGEMENT_Click(object sender, EventArgs e)
        {


            string sql3 = "SELECT `id`,`TYPAPPART`,`PRIX_LOC`,`PRIX_CHARG`,`RUE`,`ETAGE`,`DATE_LIBRE`,`Ville`,`CP`,`ASCENSEUR` FROM appartements  WHERE id=" + CL_user.userReg1.getIdLogement();
            CL_Bdd.Connexion_Bdd("Lecture", sql3, "ca marche po");
            DataTable Appart = CL_Bdd.DT;
            if (Appart.Rows.Count != 0)
            {
                float prixTot = float.Parse(Appart.Rows[0].ItemArray[2].ToString()) + float.Parse(Appart.Rows[0].ItemArray[3].ToString());
                PRN_PN_LOC_MON_LOG.BringToFront();
                PRN_PN_LOC_MON_L_Lab1.Text = "Adresse: " + CL_Bdd.DT.Rows[0].ItemArray[4].ToString() + " " + CL_Bdd.DT.Rows[0].ItemArray[7].ToString() + " " + CL_Bdd.DT.Rows[0].ItemArray[8].ToString();
                PRN_PN_LOC_MON_L_Lab3.Text = "Loyer mensuel (Charges comprise): " + prixTot + "€";
                PRN_PN_LOC_MON_L_Lab2.Text = "Type habitation: " + Appart.Rows[0].ItemArray[1].ToString();

            }
            else
            {

                MessageBox.Show("Erreur dsl frérot");
            }


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PRN_PN_LOC_MAIN.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PRN_PN_LOC_MAIN.BringToFront();
            UserTest.Text = "Bonjour " + CL_user.userReg1.getPrenom() + " " + CL_user.userReg1.getNom() + ". Bienvenue sur ViteMonLogement !";
        }

        private void PRN_PN_LOC_DOSSIER_B_SUB_Click(object sender, EventArgs e)
        {
            String nom = PRN_PN_LOC_DOSSIER_T1.Text;
            String banque = PRN_PN_LOC_DOSSIER_T9.Text;
            String pre = PRN_PN_LOC_DOSSIER_T2.Text;
            String mail = PRN_PN_LOC_DOSSIER_T3.Text;
            String adresse = PRN_PN_LOC_DOSSIER_T4.Text;
            int cp = int.Parse(PRN_PN_LOC_DOSSIER_T5.Text);
            String ville = PRN_PN_LOC_DOSSIER_T6.Text;
            int tel = int.Parse(PRN_PN_LOC_DOSSIER_T7.Text);
            var date1 = PRN_PN_LOC_DOSSIER_DATE1.Value;
            string date = date1.Date.ToString("yyyy-MM-dd");
            String RIB = PRN_PN_LOC_DOSSIER_T8.Text;
            int Revenu = int.Parse(PRN_PN_LOC_DOSSIER_T10.Text);

            string sqlUserUp = "UPDATE `utilisateur` SET`mail`='" + mail + "',`nom`='" + nom + "',`Prenom`='" + pre + "',`adresse`='" + adresse + "',`CP`=" + cp + ",`ville`='" + ville + "',`Telephone`=" + tel + ",`DateNaissance`='" + date + "',`RIB`='" + RIB + "',`Banque`='" + banque + "',`RevenuAnnuel`=" + Revenu + "  WHERE id=" + CL_user.userReg1.getId();
            MessageBox.Show("Modification enregisté");
            CL_Bdd.Connexion_Bdd("update", sqlUserUp, "oups");
            CL_Dossier.Dossier1.UpdateDossier(RIB, Revenu, nom, pre, mail, adresse, cp.ToString(), ville, banque, tel.ToString(), date1);
            CL_user.userReg1.UpdateUser(date1, nom, pre, mail, adresse, cp.ToString(), ville, tel.ToString());
        }

        private void PRN_PN_LOC_LOGEMENT_SEL_B_RETOUR_Click(object sender, EventArgs e)
        {
            PRN_PN_LOC_LOGEMENT.BringToFront();
        }

        private void PRN_PN_LOC_LOGEMENT_SEL_B_DOSSIER_Click(object sender, EventArgs e)
        {
            if (CL_Dossier.Dossier1.VerifDossierCOMPLET())
            {
                if (int.Parse(CL_Bdd.Recup_first_val_bdd("SELECT * FROM demandes WHERE `id_Appart`=" + indxDT + " AND id_locataire=" + CL_user.userReg1.getId(), "marche pas")) == 0)
                {

                    DateTime myDateTime = DateTime.Now;
                    string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    String sqlLocLog = "INSERT INTO demandes (`id_locataire`, `id_Appart`, `DATE_Demande`) VALUES (" + CL_user.userReg1.getId() + "," + indxDT + ",'" + sqlFormattedDate + "')";
                    CL_Bdd.Connexion_Bdd("Lecture", sqlLocLog, "march ap");
                    MessageBox.Show("Dossier envoyer pour le logement");
                }
                else
                {
                    MessageBox.Show("vous avez déjà déposer votre dossier pour cette appartement");
                }
            }
            else
            {
                MessageBox.Show("Dossier Incomplet");
            }

        }

        private void PRN_PN_MAIN_BUTTON_DOSSIER_Click_1(object sender, EventArgs e)
        {
            PRN_PN_LOC_DOSSIER.BringToFront();
            PRN_PN_LOC_DOSSIER_T1.Text = CL_user.userReg1.getNom();
            PRN_PN_LOC_DOSSIER_T2.Text = CL_user.userReg1.getPrenom();
            PRN_PN_LOC_DOSSIER_T3.Text = CL_user.userReg1.getMail();
            PRN_PN_LOC_DOSSIER_T4.Text = CL_user.userReg1.getAdresse();
            PRN_PN_LOC_DOSSIER_T5.Text = CL_user.userReg1.getCp();
            PRN_PN_LOC_DOSSIER_T6.Text = CL_user.userReg1.getVille();
            PRN_PN_LOC_DOSSIER_T7.Text = CL_user.userReg1.getTelephone();
            PRN_PN_LOC_DOSSIER_DATE1.Value = CL_user.userReg1.getDateNaissance();
            PRN_PN_LOC_DOSSIER_T8.Text = CL_Dossier.Dossier1.getRIB();
            PRN_PN_LOC_DOSSIER_T9.Text = CL_Dossier.Dossier1.getBanque();
            PRN_PN_LOC_DOSSIER_T10.Text = CL_Dossier.Dossier1.getRevenu().ToString();
        }

        private void PRN_PN_MAIN_BUTTON_DEMANDES_Click(object sender, EventArgs e)
        {
            indxDT = 0;
            PRN_PN_LOC_DEMANDE.BringToFront();
            var dgv = PRN_PN_LOC_DEMANDE_DGV_DOS;
            String sql = " SELECT `id`,`TYPAPPART`,`PRIX_LOC`,`PRIX_CHARG`,`RUE`,`ETAGE`,`DATE_LIBRE`,`Ville`,`CP`,`ASCENSEUR` FROM appartements  WHERE id IN(SELECT id FROM demandes WHERE id_locataire=" + CL_user.userReg1.getId() + ")";
            CL_Bdd.Connexion_Bdd("Lecture", sql, "nop", dgv);
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].HeaderText = "Type appartement";
            dgv.Columns[2].HeaderText = "Prix location";
            dgv.Columns[3].HeaderText = "Prix charge";
            dgv.Columns[4].HeaderText = "adresse";
            dgv.Columns[5].HeaderText = "Etage";
            dgv.Columns[6].HeaderText = "Date logement libre";
            dgv.Columns[7].HeaderText = "Ville";
            dgv.Columns[8].HeaderText = "Code postale";
            dgv.Columns[9].HeaderText = "Presence d'un ascenseur";
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }




        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            PRN_PN_LOC_MAIN.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PRN_PN_LOC_DEMANDE.BringToFront();
        }

        private void PRN_PN_LOC_DEMANDE_DGV_DOS_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_LOC_DEMANDE_DGV_DOS;
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }
        }

        private void PRN_PN_LOC_LOGEMENT_DGV_APPART_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (PRN_PN_LOC_LOGEMENT_DGV_APPART.SelectedRows.Count != 0)
            {
                indxDT = int.Parse(PRN_PN_LOC_LOGEMENT_DGV_APPART.SelectedCells[0].RowIndex.ToString());
                float tot = float.Parse(this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[2].Value.ToString()) + float.Parse(PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[3].Value.ToString());
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_2.Text = this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[1].Value.ToString();
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_3.Text = this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[2].Value.ToString() + "€";
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_4.Text = PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[3].Value.ToString() + "€";
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_5.Text = "" + tot + "€";
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_1.Text = this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[4].Value.ToString();
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_8.Text = this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[5].Value.ToString();
                PRN_PN_LOC_LOGEMENT_SEL_Date_1.Value = DateTime.Parse(this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[6].Value.ToString());
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_6.Text = this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[7].Value.ToString();
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_7.Text = this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[8].Value.ToString();
                PRN_PN_LOC_LOGEMENT_SEL_LABEL_9.Text = this.PRN_PN_LOC_LOGEMENT_DGV_APPART.Rows[indxDT].Cells[9].Value.ToString();

                //MessageBox.Show("id appart="+ valueIdnx);
                PRN_PN_LOC_LOGEMENT_SEL.BringToFront();
            }
        }

        private void PRN_PN_LOC_LOGEMENT_DGV_APPART_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_LOC_LOGEMENT_DGV_APPART;
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }
        }

        private void PRN_PN_LOC_DEMANDE_DGV_DOS_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_LOC_DEMANDE_DGV_DOS;

            if (dgv.SelectedRows.Count != 0)
            {
                indxDT = int.Parse(dgv.SelectedCells[0].RowIndex.ToString());
                float tot = float.Parse(dgv.Rows[indxDT].Cells[2].Value.ToString()) + float.Parse(dgv.Rows[indxDT].Cells[3].Value.ToString());
                PRN_PN_LOC_DOSSIER_SEL_LABEL_2.Text = dgv.Rows[indxDT].Cells[1].Value.ToString();
                PRN_PN_LOC_DOSSIER_SEL_LABEL_3.Text = dgv.Rows[indxDT].Cells[2].Value.ToString() + "€";
                PRN_PN_LOC_DOSSIER_SEL_LABEL_4.Text = dgv.Rows[indxDT].Cells[3].Value.ToString() + "€";
                PRN_PN_LOC_DOSSIER_SEL_LABEL_5.Text = "" + tot + "€";
                PRN_PN_LOC_DOSSIER_SEL_LABEL_1.Text = dgv.Rows[indxDT].Cells[4].Value.ToString();
                PRN_PN_LOC_DOSSIER_SEL_LABEL_8.Text = dgv.Rows[indxDT].Cells[5].Value.ToString();
                PRN_PN_LOC_DOSSIER_SEL_Date_1.Value = DateTime.Parse(dgv.Rows[indxDT].Cells[6].Value.ToString());
                PRN_PN_LOC_DOSSIER_SEL_LABEL_6.Text = dgv.Rows[indxDT].Cells[7].Value.ToString();
                PRN_PN_LOC_DOSSIER_SEL_LABEL_7.Text = dgv.Rows[indxDT].Cells[8].Value.ToString();
                PRN_PN_LOC_DOSSIER_SEL_LABEL_9.Text = dgv.Rows[indxDT].Cells[9].Value.ToString();

                //MessageBox.Show("id appart=" + indxDT);
                PRN_PN_LOC_DOSSIER_SEL.BringToFront();
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            String DelSql = "DELETE FROM `demandes` Where `id_Appart`=" + indxDT + " and  `id_locataire`=" + CL_user.userReg1.getId() + "";
            CL_Bdd.Connexion_Bdd("n", DelSql, "ERROR DEL");
            MessageBox.Show("Dossier retirer avec succès     " + DelSql);
            PRN_PN_LOC_DEMANDE.BringToFront();
            indxDT = 0;
            var dgv = PRN_PN_LOC_DEMANDE_DGV_DOS;
            String sql = " SELECT `id`,`TYPAPPART`,`PRIX_LOC`,`PRIX_CHARG`,`RUE`,`ETAGE`,`DATE_LIBRE`,`Ville`,`CP`,`ASCENSEUR` FROM appartements  WHERE id IN(SELECT id FROM demandes WHERE id_locataire=" + CL_user.userReg1.getId() + ")";
            CL_Bdd.Connexion_Bdd("Lecture", sql, "nop", dgv);
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].HeaderText = "Type appartement";
            dgv.Columns[2].HeaderText = "Prix location";
            dgv.Columns[3].HeaderText = "Prix charge";
            dgv.Columns[4].HeaderText = "adresse";
            dgv.Columns[5].HeaderText = "Etage";
            dgv.Columns[6].HeaderText = "Date logement libre";
            dgv.Columns[7].HeaderText = "Ville";
            dgv.Columns[8].HeaderText = "Code postale";
            dgv.Columns[9].HeaderText = "Presence d'un ascenseur";
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(36);
            String dt2 = dt.Date.ToString("yyyy-MM-dd");
            String sqlLloc = "UPDATE `locataire` SET `DateFinLoc`='" + dt2 + "' WHERE `id_locataire`=" + CL_user.userReg1.getId() + " AND `id_appartement`=" + CL_user.userReg1.getIdLogement() + " AND DateFinLoc is null";
            CL_Bdd.Connexion_Bdd("", sqlLloc, "");
            PRN_LABEL_CompteAReBour.Text = " Vous devez quittez votre logement le :" + dt2;
            MessageBox.Show("Votre demande pour quitter votre logement a été enregistrer. Vous devrez quittez votre logement le :" + dt2);
            PRN_PN_LOC_MON_LOG_B_ANNULE_QUITTE_LOG.BringToFront();
            PRN_LABEL_CompteAReBour.Visible = true;
            PRN_PN_LOC_MAIN.BringToFront();

        }

        private void PRN_TIMER_Tick(object sender, EventArgs e)
        {

        }

        private void PRN_PN_LOC_MON_LOG_B_ANNULE_QUITTE_LOG_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            String date1 = dt.Date.ToString("yyyy-MM-dd");
            MessageBox.Show("Vous avez décider d'annuler le départ de votre logement");
            PRN_PN_LOC_MON_LOG_B_QUITTERLOG.BringToFront();

            String sql = "UPDATE `locataire` SET `DateFinLoc`=NULL WHERE `id_locataire`=" + CL_user.userReg1.getId() + " AND `id_appartement`=" + CL_user.userReg1.getIdLogement() + " AND `DateFinLoc`>='" + date1 + "'";
            CL_Bdd.Connexion_Bdd("", sql, "");
            PRN_LABEL_CompteAReBour.Visible = false;
            PRN_PN_LOC_MAIN.BringToFront();
        }
    }
}
