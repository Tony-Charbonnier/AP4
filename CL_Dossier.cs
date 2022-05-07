using System;

namespace viteMonLogement
{
    internal class CL_Dossier
    {
        public static CL_Dossier Dossier1;
        private int? id_user;
        private String Rib;
        private int? RevenuAnnuel;
        private String nom;
        private String prenom;
        private String mail;
        private DateTime? DateNaissance;
        private String adresse;
        private String CP;
        private String ville;
        private String Banque;
        private String Telephone;

        public CL_Dossier(int unId, String unRib = null, int? unRevenu = null, String unN = null, String unP = null, String unMail = null, String uneAdr = null, String unCP = null, String uneVille = null, String uneBanque = null, String unTel = null, DateTime? uneDate = null)
        {
            id_user = unId;
            Rib = unRib;
            RevenuAnnuel = unRevenu;
            nom = unN;
            prenom = unP;
            mail = unMail;
            DateNaissance = uneDate;
            adresse = uneAdr;
            CP = unCP;
            ville = uneVille;
            Banque = uneBanque;
            Telephone = unTel;
        }

        public void UpdateDossier(String unRib = null, int? unRevenu = null, String unN = null, String unP = null, String unMail = null, String uneAdr = null, String unCP = null, String uneVille = null, String uneBanque = null, String unTel = null, DateTime? uneDate = null)
        {
            Rib = unRib;
            RevenuAnnuel = unRevenu;
            nom = unN;
            prenom = unP;
            mail = unMail;
            DateNaissance = uneDate;
            adresse = uneAdr;
            CP = unCP;
            ville = uneVille;
            Banque = uneBanque;
            Telephone = unTel;
        }
        public String getBanque() { return Banque; }
        public String getRIB() { return Rib; }
        public int? getRevenu() { return RevenuAnnuel; }

        public void destrucDossier() { CL_Dossier.Dossier1 = null; }

        public bool VerifDossierCOMPLET()
        {
            if (Rib != null && RevenuAnnuel != null && nom != null && prenom != null && mail != null && DateNaissance != null && adresse != null && CP != null && ville != null && Banque != null && Telephone != null)
            {
                return true;
            }
            else { return false; }
        }
    }
}
