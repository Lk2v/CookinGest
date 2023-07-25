using System;
using System.Collections.Generic;

namespace CookinGest.src.DataTemplate
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public int RecetteId { get; set; }

        public string NomRecette { get; set; }
        public int Prix { get; set; }

        public long GainPtsCreateur { get; set; }
        public DateTime Date { get; set; }

        public static List<OrderHistory> Parse(List<Dictionary<string, object>> d)
        {
            List<OrderHistory> r = new List<OrderHistory>();
            foreach (Dictionary<string, object> l in d)
            {
                r.Add(Parse(l));
            }

            return r;
        }

        public static OrderHistory Parse(Dictionary<string, object> dic)
        {
            return new OrderHistory
            {
                
                NomRecette = (string)dic["nomRecette"],

                Prix = (int)dic["prix"],

                GainPtsCreateur = dic.ContainsKey("gainPoints") ? (long) dic["gainPoints"] : -1,

                Date = (DateTime)dic["dateCommande"],
            };
        }
    }
}
