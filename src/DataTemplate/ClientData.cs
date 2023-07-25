using System;
using System.Collections.Generic;

namespace CookinGest.src.DataTemplate
{
	public class ClientData
	{
		public int Id { get; set; }
        public int? CreateurId { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Initiale
        {
            get => Prenom[0].ToString().ToUpper() + Nom[0].ToString().ToUpper();
        }

        public string Mail { get; set;  }
        public string Adresse { get; set; }
        public decimal Telephone { get; set;  }

        public int SoldeBanque { get; set; }

        public bool EstCreateur {
            get => CreateurId != null;
        }

        public bool EstAdmin { get; set; }

        //Champ specifique (Top)
        public decimal? MontantDepense { get; set; }
        public decimal? BeneficeSemaine { get; set; }

        //

        public static List<ClientData> Parse(List<Dictionary<string, object>> d)
        {
            List<ClientData> r = new List<ClientData>();
            foreach (Dictionary<string, object> l in d)
            {
                r.Add(Parse(l));
            }

            return r;
        }

        public static ClientData Parse(Dictionary<string, object> dic)
        {
            return new ClientData
            {

                Id = (int)dic["clientID"],
                CreateurId = (dic.ContainsKey("createurID")) ? (int?)dic["createurID"] : null,
                Mail = (string)dic["mail"],
                Nom = (string)dic["nomClient"],
                Prenom = (string)dic["prenomClient"],
                Telephone = (decimal)dic["telephoneClient"],

                EstAdmin = (bool)dic["estAdmin"],
                Adresse = (string)dic["adresseClient"],

                SoldeBanque = (int)dic["soldeBanque"],

                //
                MontantDepense = dic.ContainsKey("montantDepense") ? (decimal?)dic["montantDepense"] : null,
                BeneficeSemaine = dic.ContainsKey("beneficeSemaine") ? (decimal?)dic["beneficeSemaine"] : null,
            };
        }

    }
}

