using System;
using System.Collections.Generic;

namespace CookinGest.src.DataTemplate
{
	public class SupplierData
	{

        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public decimal? NumSiret { get; set; }

        public string Adresse { get; set; } = "";
        public decimal? Telephone { get; set; }

        
        public static List<SupplierData> Parse(List<Dictionary<string, object>> d)
        {
            List<SupplierData> r = new List<SupplierData>();
            foreach (Dictionary<string, object> l in d)
            {
                r.Add(Parse(l));
            }

            return r;
        }

        public static SupplierData Parse(Dictionary<string, object> dic)
        {
            return new SupplierData
            {
                Id = (int)dic["fournisseurID"],

                Nom = (string)dic["nomFournisseur"],
                NumSiret = (decimal?)dic["num_siret"],
                Adresse = (string?)dic["adresseFournisseur"],
                Telephone = (decimal?)dic["telephoneFournisseur"]
            };
        }

	}
}

