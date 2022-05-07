using System;

namespace viteMonLogement
{

    public class CL_user
    {
        public static CL_user userReg1;
        private String login;
        private int idType;
        private int id;
        private String nom;
        private String prenom;
        private String idLogement;
        private String mail;
        private DateTime DateNaissance;
        private String adresse;
        private String CP;
        private String ville;
        private String Telephone;


        public CL_user(string unLogin, int unIDtype, int unId, DateTime uneDate, String unNom = null, String unPrenom = null, String unMail = null, String uneAdresse = null, String unCP = null, String uneVille = null, String unTel = null)
        {
            login = unLogin;
            idType = unIDtype;
            id = unId;
            nom = unNom;
            prenom = unPrenom;
            idLogement = "";
            mail = unMail;
            DateNaissance = uneDate;
            adresse = uneAdresse;
            CP = unCP;
            ville = uneVille;
            Telephone = unTel;
        }

        public void UpdateUser(DateTime uneDate, String unNom = null, String unPrenom = null, String unMail = null, String uneAdresse = null, String unCP = null, String uneVille = null, String unTel = null)
        {
            nom = unNom;
            prenom = unPrenom;
            mail = unMail;
            DateNaissance = uneDate;
            adresse = uneAdresse;
            CP = unCP;
            ville = uneVille;
            Telephone = unTel;
        }
        public void destrucUser() { CL_user.userReg1 = null; }
        public String getLogin() { return login; }
        public int getIdType() { return idType; }
        public int getId() { return id; }
        public String getNom() { return nom; }
        public String getPrenom() { return prenom; }
        public String getIdLogement() { return idLogement; }
        public String getMail() { return mail; }
        public DateTime getDateNaissance() { return DateNaissance; }
        public String getAdresse() { return adresse; }
        public String getVille() { return ville; }
        public String getCp() { return CP; }
        public String getTelephone() { return Telephone; }
        public void setIdLogement(String unid) { idLogement = unid; }

    }
}
