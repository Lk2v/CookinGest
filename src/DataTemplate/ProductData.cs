using System;
using System.Collections.Generic;

namespace CookinGest.src.DataTemplate
{
	public class ProductData
	{
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }

        public int CategorieId { get; set; }
        public string Categorie { get; set; }
        public int Prix { get; set; }

        public int Stock { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }

        public int FournisseurId { get; set; }

        public bool Restock
        {
            get => (Stock < StockMax * 0.1); // si stock < 10%
        }

        public long NbRecettesLiee { get; set; }
        public bool RecetteEstAssociee
        {
            get => NbRecettesLiee > 0;
        }
        public bool CheckBoxSelect { get; set; } //PublishRecipeWindow

        
        public static List<ProductData> Parse(List<Dictionary<string, object>> d)
        {
            List<ProductData> r = new List<ProductData>();
            foreach (Dictionary<string, object> l in d)
            {
                r.Add(Parse(l));
            }

            return r;
        }

        public static ProductData Parse(Dictionary<string, object> dic)
        {
            return new ProductData
            {
                Id = (int)dic["ingredientID"],

                Titre = (string)dic["nomIngredient"],
                Description = (string)dic["descriptif"],
                Categorie = (string)dic["nomCategorie"],
                Prix = (int)dic["prixIngredient"],
                Stock = (int)dic["stock"],
                StockMin = (int)dic["stockMin"],
                StockMax = (int)dic["stockMax"],

                NbRecettesLiee = (dic.ContainsKey("nbRecettes")) ? (long)dic["nbRecettes"] : 0,
                CheckBoxSelect = false,
            };
        }
    }
}

