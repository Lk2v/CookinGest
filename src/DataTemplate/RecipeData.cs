using System;
using System.Collections.Generic;

namespace CookinGest.src.DataTemplate
{
    public class RecipeData
    {
        public int Id { get; set; }
        public int CreateurId { get; set; }

        public string Nom { get; set; } = "";
        public string Description { get; set; } = "";

        public DateTime Date { get; set; }

        public decimal Prix { get; set;  } // donee brut pour mysql

        public string PrixString {
            get => Prix.ToString();
        }

        public string Ingredients { get; set; } = "";

        public List<ProductData> IngredientsListe { get; set; } = new List<ProductData>();

        public int PrixIngredients { // donnee calculee
            get
            {
                int p = 0;
                foreach(ProductData i in IngredientsListe)
                {
                    p += i.Prix;
                }
                return p;
            }
        }

        public long NbCommande { get; set; }


        public static List<RecipeData> Parse(List<Dictionary<string, object>> d)
        {
            List<RecipeData> r = new List<RecipeData>();
            foreach (Dictionary<string, object> l in d)
            {
                r.Add(Parse(l));
            }

            return r;
        }

        public static RecipeData Parse(Dictionary<string, object> dic)
        {
            return new RecipeData
            {
                Id = (int)dic["recetteID"],

                CreateurId = (int)dic["createurID"],
                Nom = (string)dic["nomRecette"],
                Description = (string)dic["descriptif"],
                Date = (DateTime)dic["dateRecette"],

                Ingredients = (string)dic["ingredients"],
                Prix = (decimal)dic["prix"],

                NbCommande = (dic.ContainsKey("nbCommande")) ? (long)dic["nbCommande"] : 0,
            };
        }
    }
}
