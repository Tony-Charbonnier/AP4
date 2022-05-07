using System;
using System.Data;
using System.Windows.Forms;


namespace viteMonLogement
{
    public partial class userType2 : Form
    {
        public userType2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// BOUTON RETOUR
        /// </summary>
       
        private void PRN_PN_MAIN_BUTTON_DEC_Click(object sender, System.EventArgs e)
        {
            UserTest.Text = "Bonjour " + CL_user.userReg1.getPrenom() + " " + CL_user.userReg1.getNom() + ". Bienvenue sur ViteMonLogement !";
            CL_user.userReg1.destrucUser();
            this.Hide();
            new Connexion().Show();
        }

        private void PRN_PN_PRO_MES_LOG_SEL_B_RETOUR_Click(object sender, System.EventArgs e)
        {
            PRN_PN_PRO_MES_LOG.BringToFront();
            CL_Appartement.Appart1.destrucAppart();

        }

        private void PRN_PN_PRO_LES_DEMANDES_B_RETOUR_Click(object sender, EventArgs e)
        {
            PRN_PN_PRO_MAIN.BringToFront();
        }

        private void PRN_PN_PRO_MES_LOG_B_LOG_Click(object sender, System.EventArgs e)
        {
            PRN_PN_PRO_MAIN.BringToFront();
        }

        private void PRN_PN_PRO_LES_DEMANDES_SEl_B_RETOUR_Click(object sender, EventArgs e)
        {
            PRN_PN_PRO_LES_DEMANDES.BringToFront();
        }
        private void PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_B_RETROUR_Click(object sender, EventArgs e)
        {
            PRN_PN_PRO_LES_DEMANDES_SEl.BringToFront();
        }


        //////////////////////


        private void PRN_PN_MAIN_BUTTON_MES_LOGEMENTS_Click(object sender, System.EventArgs e)
        {
            PRN_PN_PRO_MES_LOG.BringToFront();
            var dgv = PRN_PN_PRO_MES_LOG_DGV_LOG;
            string sql = " SELECT `id`,`TYPAPPART`,`PRIX_LOC`,`PRIX_CHARG`,`RUE`,`ETAGE`,`DATE_LIBRE`,`Ville`,`CP` FROM appartements  WHERE  `id_Propriétaire`=" + CL_user.userReg1.getId() + "";
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
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }
        }

        private void PRN_PN_PRO_MES_LOG_DGV_LOG_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_PRO_MES_LOG_DGV_LOG;
            if (dgv.SelectedRows.Count != 0)
            {
                int indxDT = int.Parse(dgv.SelectedCells[0].RowIndex.ToString());
                CL_Appartement.Appart1 = new CL_Appartement(int.Parse(dgv.Rows[indxDT].Cells[0].Value.ToString()), float.Parse(dgv.Rows[indxDT].Cells[2].Value.ToString()), float.Parse(dgv.Rows[indxDT].Cells[3].Value.ToString()), int.Parse(dgv.Rows[indxDT].Cells[5].Value.ToString()), dgv.Rows[indxDT].Cells[1].Value.ToString(), dgv.Rows[indxDT].Cells[4].Value.ToString(), dgv.Rows[indxDT].Cells[7].Value.ToString(), dgv.Rows[indxDT].Cells[8].Value.ToString());
                PRN_PN_PRO_MES_LOG_SEL_TEXTE_ADRESSE.Text = CL_Appartement.Appart1.AdrCplt();
                PRN_PN_PRO_MES_LOG_SEL_TEXTE_TYPE_APPART.Text = CL_Appartement.Appart1.getTyapprt();
                PRN_PN_PRO_MES_LOG_SEL_TEXTE_LOYER.Value = decimal.Parse(CL_Appartement.Appart1.getLocation().ToString());
                PRN_PN_PRO_MES_LOG_SEL_TEXTE_CHARGE.Value = decimal.Parse(CL_Appartement.Appart1.getCharge().ToString());
                PRN_PN_PRO_MES_LOG_SEL.BringToFront();

            }           
        }

        private void PRN_PN_PRO_MES_LOG_DGV_LOG_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_PRO_MES_LOG_DGV_LOG;
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }
        }

        private void PRN_PN_PRO_MES_LOG_SEL_TEXTE_CHARGE_ValueChanged(object sender, System.EventArgs e)
        {
            CL_Appartement.Appart1.SetCharge(float.Parse(PRN_PN_PRO_MES_LOG_SEL_TEXTE_CHARGE.Value.ToString()));
            float? tot = CL_Appartement.Appart1.TotalCHargeLoyer();
            PRN_PN_PRO_MES_LOG_SEL_TEXTE_TOT.Text = tot.ToString()+ "€";
        }

        private void PRN_PN_PRO_MES_LOG_SEL_TEXTE_LOYER_ValueChanged(object sender, System.EventArgs e)
        {
            CL_Appartement.Appart1.SetLoyer(float.Parse(PRN_PN_PRO_MES_LOG_SEL_TEXTE_LOYER.Value.ToString()));
            float? tot = CL_Appartement.Appart1.TotalCHargeLoyer();
            PRN_PN_PRO_MES_LOG_SEL_TEXTE_TOT.Text = tot.ToString()+"€";
            Decimal ChatgeGsb= Decimal.Parse((CL_Appartement.Appart1.getLocation() * 0.07).ToString());
            PRN_PN_PRO_MES_LOG_SEL_TEXTE_GSB.Text = Math.Round(ChatgeGsb, 2).ToString()+ "€";
        }

        private void PRN_PN_PRO_MES_LOG_SEL_B_VALIDER_Click(object sender, System.EventArgs e)
        {
            string sql = "UPDATE `appartements` SET `PRIX_LOC`=" + CL_Appartement.Appart1.getLocation() + ",`PRIX_CHARG`=" + CL_Appartement.Appart1.getCharge() + " WHERE `id`=" + CL_Appartement.Appart1.getId();
            
            Console.WriteLine(sql);
            CL_Bdd.Connexion_Bdd("Lecture", sql, "");
            MessageBox.Show("Informations enregistrer");
            PRN_PN_PRO_MES_LOG.BringToFront(); 
            var dgv = PRN_PN_PRO_MES_LOG_DGV_LOG;
            string sql2 = " SELECT `id`,`TYPAPPART`,`PRIX_LOC`,`PRIX_CHARG`,`RUE`,`ETAGE`,`DATE_LIBRE`,`Ville`,`CP` FROM appartements  WHERE  `id_Propriétaire`=" + CL_user.userReg1.getId() + "";
            CL_Bdd.Connexion_Bdd("Lecture", sql2, "nop", dgv);
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].HeaderText = "Type appartement";
            dgv.Columns[2].HeaderText = "Prix location";
            dgv.Columns[3].HeaderText = "Prix charge";
            dgv.Columns[4].HeaderText = "adresse";
            dgv.Columns[5].HeaderText = "Etage";
            dgv.Columns[6].HeaderText = "Date logement libre";
            dgv.Columns[7].HeaderText = "Ville";
            dgv.Columns[8].HeaderText = "Code postale";
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";
            }
        }

        /// <summary>
        /// LES DEMANDES DE LOGEMENT
        /// </summary>

        private void PRN_PN_MAIN_BUTTON_DEMANDES_Click(object sender, EventArgs e)
        {
            PRN_PN_PRO_LES_DEMANDES.BringToFront();
            String sql = "SELECT `appartements`.`id`, `appartements`.`TYPAPPART`, `appartements`.`PRIX_LOC`, `appartements`.`PRIX_CHARG`, `appartements`.`RUE`,`appartements`.`Ville`,`appartements`.`CP`, COUNT(`demandes`.`id_Appart`)" +
                         "FROM `demandes`, `appartements`" +
                         "WHERE `demandes`.`id_Appart`=`appartements`.`id` AND `appartements`.`id_Propriétaire`= "+CL_user.userReg1.getId()+ " AND `Decision` is NULL GROUP BY `demandes`.`id_Appart`";
            var dgv = PRN_PN_PRO_LES_DEMANDES_DGV;
            CL_Bdd.Connexion_Bdd("Lecture", sql,"", dgv);
            int count = CL_Bdd.DT.Rows.Count;
            Console.WriteLine(count);
            if(count == 0)
            {
                MessageBox.Show("Aucun dossier déposer sur vos logement");
                PRN_PN_PRO_MAIN.BringToFront();
                
            }
            else if (count >= 1)
            {
                dgv.Columns[0].Visible = false;
                dgv.Columns[1].HeaderText = "Type appartement";
                dgv.Columns[2].HeaderText = "Prix location";
                dgv.Columns[3].HeaderText = "Prix charge";
                dgv.Columns[4].HeaderText = "adresse";
                dgv.Columns[5].HeaderText = "Ville";
                dgv.Columns[6].HeaderText = "Code postale";
                dgv.Columns[7].HeaderText = "Nombre dossier";

                for (int i = 0; i < count; i++)
                {
                    dgv.Rows[i].HeaderCell.Value = "voir plus";
                }
            }

            


        }

        private void PRN_PN_PRO_LES_DEMANDES_DGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_PRO_LES_DEMANDES_DGV;
            if (dgv.SelectedRows.Count != 0)
            {
                int indxDT = int.Parse(dgv.SelectedCells[0].RowIndex.ToString());
                int iDLoc = int.Parse(dgv.Rows[indxDT].Cells[0].Value.ToString());

                PRN_PN_PRO_LES_DEMANDES_SEl.BringToFront();
                String sql = "SELECT `id_locataire`,`demandes`.`id`, `utilisateur`.`nom`, `utilisateur`.`Prenom`, `utilisateur`.`adresse`, `utilisateur`.`CP`, `utilisateur`.`ville`, `utilisateur`.`Telephone` " +
                             "FROM `demandes`,`utilisateur`" +
                             "WHERE `utilisateur`.`id`=`id_locataire` AND `id_Appart`= "+ iDLoc + " AND `Decision` IS NULL";
                var dgv2 = PRN_PN_PRO_LES_DEMANDES_SEl_DGV;
                CL_Bdd.Connexion_Bdd("Lecture", sql, "", dgv2);
                dgv2.Columns[0].Visible = false;
                dgv2.Columns[1].HeaderText = "Numéro dossier ";
                dgv2.Columns[2].HeaderText = "Nom ";
                dgv2.Columns[3].HeaderText = "Prénom";
                dgv2.Columns[4].HeaderText = "adresse";
                dgv2.Columns[5].HeaderText = "Code postale";
                dgv2.Columns[6].HeaderText = "Ville";
                dgv2.Columns[7].HeaderText = "Téléphone";

                int count = CL_Bdd.DT.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dgv2.Rows[i].HeaderCell.Value = "voir plus";
                }

            }
        }

        private void PRN_PN_PRO_LES_DEMANDES_DGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_PRO_LES_DEMANDES_DGV;
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";

            }
        }



        /// <summary>
        /// LES DOSSIER DE LOGEMENT SELECTIONNER
        /// </summary>
        /// 

        private void PRN_PN_PRO_LES_DEMANDES_SEl_DGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_PRO_LES_DEMANDES_SEl_DGV;
            if (dgv.SelectedRows.Count != 0)
            {
                int indx = int.Parse(dgv.SelectedCells[0].RowIndex.ToString());
                int iDLoc = int.Parse(dgv.Rows[indx].Cells[0].Value.ToString());
                //int idLogemeng = int.Parse(dgv.Rows[iDLoc].Cells[0].Value.ToString());
                String idDossier = dgv.Rows[indx].Cells[1].Value.ToString();

                String sql = " SELECT nom, prenom, mail, adresse, CP, Ville, RIB, Telephone, RevenuAnnuel,Banque  FROM utilisateur WHERE id="+ iDLoc;
                CL_Bdd.Connexion_Bdd("Lecture", sql, "");
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_DOSSIER.Text = idDossier;
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_NOM.Text = CL_Bdd.DT.Rows[0].ItemArray[0].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_PRE.Text = CL_Bdd.DT.Rows[0].ItemArray[1].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_MAIL.Text = CL_Bdd.DT.Rows[0].ItemArray[2].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_ADR.Text = CL_Bdd.DT.Rows[0].ItemArray[3].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_CP.Text = CL_Bdd.DT.Rows[0].ItemArray[4].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_VILLE.Text = CL_Bdd.DT.Rows[0].ItemArray[5].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_RIB.Text = CL_Bdd.DT.Rows[0].ItemArray[6].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_TEL.Text = CL_Bdd.DT.Rows[0].ItemArray[7].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_REVENU.Text = CL_Bdd.DT.Rows[0].ItemArray[8].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_BANQUE.Text = CL_Bdd.DT.Rows[0].ItemArray[9].ToString();
                PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL.BringToFront();

            }
        }
        private void PRN_PN_PRO_LES_DEMANDES_SEl_DGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = PRN_PN_PRO_LES_DEMANDES_SEl_DGV;
            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv.Rows[i].HeaderCell.Value = "voir plus";

            }
        }

        private void PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_B_REFUSER_Click(object sender, EventArgs e)
        {
            String idDoss = PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_DOSSIER.Text;
            String sql1 = "UPDATE `demandes` SET `Decision`='R' WHERE `id`=" + idDoss;
            CL_Bdd.Connexion_Bdd("", sql1, "");
            MessageBox.Show("La candidature n°" + idDoss + " a bien été refuser");

            PRN_PN_PRO_LES_DEMANDES_SEl.BringToFront();
            String sql = "SELECT `id_locataire`,`demandes`.`id`, `utilisateur`.`nom`, `utilisateur`.`Prenom`, `utilisateur`.`adresse`, `utilisateur`.`CP`, `utilisateur`.`ville`, `utilisateur`.`Telephone` " +
                         "FROM `demandes`,`utilisateur`" +
                         "WHERE `utilisateur`.`id`=`id_locataire` AND `id_Appart`= (SELECT `id_Appart` FROM `demandes` WHERE `id`="+ idDoss + " )  AND `Decision` IS NULL";
            var dgv2 = PRN_PN_PRO_LES_DEMANDES_SEl_DGV;
            CL_Bdd.Connexion_Bdd("Lecture", sql, "", dgv2);
            dgv2.Columns[0].Visible = false;
            dgv2.Columns[1].HeaderText = "Numéro dossier ";
            dgv2.Columns[2].HeaderText = "Nom ";
            dgv2.Columns[3].HeaderText = "Prénom";
            dgv2.Columns[4].HeaderText = "adresse";
            dgv2.Columns[5].HeaderText = "Code postale";
            dgv2.Columns[6].HeaderText = "Ville";
            dgv2.Columns[7].HeaderText = "Téléphone";

            int count = CL_Bdd.DT.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dgv2.Rows[i].HeaderCell.Value = "voir plus";
            }
        }

        private void PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_B_VALIDER_Click(object sender, EventArgs e)
        {
            String idDoss = PRN_PN_PRO_LES_DEMANDES_SEl_CANDIDAT_SEL_DOSSIER.Text;
            String sql1 = "UPDATE `demandes` SET `Decision`='A' WHERE `id`=" + idDoss;
            CL_Bdd.Connexion_Bdd("", sql1, "");
            MessageBox.Show("Bienvenue a votre nouveau locataire 🎉🎉🎉🎉🎉🍾🍾🍾🍾 ");
            String sql2 = "(SELECT `id_Appart`,`id_locataire` FROM `demandes` WHERE `id`= " + idDoss + " )";
            CL_Bdd.Connexion_Bdd("Lecture",sql2, "");
            string idApp = CL_Bdd.DT.Rows[0].ItemArray[0].ToString();
            String idLoc = CL_Bdd.DT.Rows[0].ItemArray[1].ToString();
            String sql3 = "UPDATE `demandes` SET `Decision`='R ' WHERE `Decision` IS NULL AND `id_Appart`="+ idApp;
            CL_Bdd.Connexion_Bdd("", sql3, "");
            DateTime timeNow = DateTime.Now;
            String date = timeNow.Date.ToString("yyyy-MM-dd");
            String sql4 = "INSERT INTO `locataire`(`id_locataire`, `id_appartement`, `DateDebutLoc`) VALUES ("+ idLoc+","+idApp+",'"+ date+ "')";
            CL_Bdd.Connexion_Bdd("", sql4, "");
            String sql5= "UPDATE `demandes` SET `Decision`='R ' WHERE `Decision` IS NULL AND `id_locataire`=" + idLoc;
            CL_Bdd.Connexion_Bdd("", sql5, "");
            PRN_PN_PRO_MAIN.BringToFront();
        }
    }
}
