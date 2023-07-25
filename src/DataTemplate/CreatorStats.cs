using System;
using System.Collections.Generic;

namespace CookinGest.src.DataTemplate
{
	public class CreatorStats
	{
		public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public long NbRecette { get; set; }
        public long NbRecetteCommande { get; set; }

        public int Solde { get; set; }
        public string Initiale
        {
            get => Prenom[0].ToString() + Nom[0].ToString();
        }
        public static List<CreatorStats> Parse(List<Dictionary<string, object>> d)
        {
            List<CreatorStats> r = new List<CreatorStats>();
            foreach (Dictionary<string, object> l in d)
            {
                r.Add(Parse(l));
            }

            return r;
        }

        public static CreatorStats Parse(Dictionary<string, object> dic)
        {
            return new CreatorStats
            {
                Id = (int)dic["createurID"],
                Nom = (string)dic["nomClient"],
                Prenom = (string)dic["prenomClient"],

                Solde = (int)dic["soldePoint"],

                NbRecette = (long)dic["nbRecettes"],
                NbRecetteCommande = (long)dic["nbRecettesCreeCommandes"],
            };
        }

    }
}

