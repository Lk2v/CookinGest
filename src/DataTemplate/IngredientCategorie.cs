using System;
using System.Collections.Generic;

namespace CookinGest.src.DataTemplate
{
	public class IngredientCategorie
	{
		public int Id { get; set; }
		public string Nom { get; set; }

        public static List<IngredientCategorie> Parse(List<Dictionary<string, object>> d)
        {
            List<IngredientCategorie> r = new List<IngredientCategorie>();
            foreach (Dictionary<string, object> l in d)
            {
                r.Add(Parse(l));
            }

            return r;
        }

        public static IngredientCategorie Parse(Dictionary<string, object> dic)
        {
            return new IngredientCategorie
            {
                Id = (int)dic["categorieID"],

                Nom = (string)dic["nomCategorie"],
            };
        }
    }
}

