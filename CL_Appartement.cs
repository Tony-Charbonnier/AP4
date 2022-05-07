using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viteMonLogement
{
    internal class CL_Appartement
    {
        public static CL_Appartement Appart1;
        private int id;
        private float? location;
        private float? Charge;
        private int? etage;
        private string Tyapprt;
        private string Rue;
        private string Ville;
        private string CP;
        private Boolean? ascenseur;
        private DateTime? DateLibre;

        public CL_Appartement(int unid, float? pLocation=null, float? pCharge=null, int? unEtage=null, String unType=null, String uneRue=null, String uneVille=null, String unCP=null, Boolean? unasc=null, DateTime? uneDate=null)
        {
            id = unid;
            location = pLocation;
            Charge = pCharge;
            etage = unEtage;
            Tyapprt = unType;
            Rue = uneRue;
            Ville = uneVille;
            CP = unCP;
            ascenseur = unasc;
            DateLibre = uneDate;
        }
        public int getId() { return id; }
        public float? getLocation() { return location; }
        public float? getCharge() { return Charge; }
        public int? getEtage() { return etage; }
        public String getTyapprt() { return Tyapprt; }
        public String getCP() { return CP; }
        public String getRue() { return Rue; }
        public String getVille() { return Ville; }
        public Boolean? getAscenseur() { return ascenseur; }
        public DateTime? getDateLibre() { return DateLibre; }
        public void destrucAppart() { CL_Appartement.Appart1 = null; }

        public void SetLoyer(float unLoyer)
        {
            Appart1.location = unLoyer;
        }

        public void SetCharge(float unCharge)
        {
            Appart1.Charge = unCharge;
        }

        public float? TotalCHargeLoyer()
        {
            return Appart1.Charge + Appart1.location;
        }

        public String AdrCplt()
        {
            return Appart1.Rue + " " + Appart1.Ville + " " + Appart1.CP;
        }

    }
}
