using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using CookinGest.src.DataTemplate;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI;

namespace CookinGest.src.DB
{
	public class Service
	{
		static DB? BDD;
        static Client? _client = null;

        #region Charger service
        static public bool Initialiser()
		{
            BDD = LoadBDD();
            Console.WriteLine("L'initialisation a été tenté");

            return (BDD != null);
        }


        static DB? LoadBDD()
        {
            DB DataBase = new DB("Cookingest", "C#ROOT", "9521094370");
            (bool estOK, string err) = DataBase.Connection();
            if (!estOK)
            {
                return null;
            }
            return DataBase;
        }

        #endregion

        #region Client
        static public (bool, ClientData) AuthentifierClient(string mail, string motDePasse)
        {
            Resultats res_client = BDD!.Procedure("ObtenirCLient", new Dictionary<string, object>() {
                { "email", mail }, { "mdp", motDePasse }
            });

            if (res_client == null || res_client.Count == 0) return (false, null);

            Dictionary<string, object> c = res_client.ObtenirLigne(0);

            _client = new Client(c!);
            Console.WriteLine(_client.Debug());
            return (true, ClientData.Parse(c!));
        }

        static public (bool, ClientData) CreeClient(string mail, string mdp, string prn, string nm, decimal tel, string addr)
        {
            Resultats res_client = BDD!.Procedure("CreeClient", new Dictionary<string, object>() {
                { "email", mail },
                { "mdp", mdp },
                { "nom", nm },
                { "prenom", prn },
                { "telephone", tel },
                { "addr", addr }
            });

            if (res_client == null || res_client.Count == 0) return (false, null);

            Dictionary<string, object> c = res_client.ObtenirLigne(0)!;

            _client = new Client(c);
            Console.WriteLine(_client.Debug());

            return (true, ClientData.Parse(c));
        }

        public static Client? ObtenirClient() {
            return _client;
        }

        public static Resultats RechargerSoldeBancaire(int cID)
        {
            return BDD!.Requete("CALL RechargerSoldeBancaire(@cID);", new Dictionary<string, object>() {
                {"cID",  cID}
            });
        }
        // Utils

        public static List<ClientData> ListeClients()
        {
            Resultats r = BDD!.Requete("SELECT * FROM ClientsInformations;");
            return ClientData.Parse(r.Lignes);
        }

        public static Resultats ListeCreateur()
        {
            Resultats r = BDD!.Requete("SELECT * FROM Cookingest.Createur");
            return r;
        }

        public static CreatorStats ObtenirStatsCreateur(int createurId)
        {
            Resultats r = BDD!.Requete("CALL ObtenirInformationsCreateur(@cId)", new Dictionary<string, object>() {
                {"cID",  createurId}
            });
            CreatorStats c = CreatorStats.Parse(r.ObtenirLigne(0)!);
            return c;
        }

        #endregion

        #region Gestion
        public static List<ProductData> ListeIngredients()
        {
            Resultats r = BDD!.Procedure("ListeIngredients");
            return ProductData.Parse(r.Lignes);
        }

        public static List<ProductData> ListeIngredientsAvecNbRecettes()
        {
            Resultats r = BDD!.Requete("SELECT * FROM ListeIngredientsRecetteView;");
            return ProductData.Parse(r.Lignes);
        }

        public static List<IngredientCategorie> ListeCategoriesIngredient()
        {
            Resultats r = BDD!.Requete("SELECT * FROM IngredientCategorie;");
            return IngredientCategorie.Parse(r.Lignes);
        }

        public static List<SupplierData> ListeFournisseurs()
        {
            Resultats r = BDD!.Requete("SELECT * FROM Fournisseur;");
            return SupplierData.Parse(r.Lignes);
        }

        #endregion

        #region Recette
        public static List<RecipeData> ListeRecettes(SearchFilter q = SearchFilter.New, int limit = 40)
        {
            string nomVue = "";

            switch (q)
            {
                case SearchFilter.New:
                    nomVue = "RecettesCommandeViewRecent";
                    break;

                case SearchFilter.MostPopular:
                    nomVue = "RecettesCommandeViewPopulaire";
                    break;

                case SearchFilter.NewAndFresh:
                    nomVue = "RecettesCommandeViewTopSemaine";
                    break;
            }

            Resultats r = BDD!.Requete($"SELECT * FROM {nomVue} LIMIT @max", new Dictionary<string, object>() {
                { "max", limit }
            });

            return RecipeData.Parse(r.Lignes);
        }

        public static List<RecipeData> ListeRecetteParIngredient(int idIngr)
        {
            Resultats r = BDD!.Requete($"CALL ListeRecettesParIngredient(@iId)", new Dictionary<string, object>() {
                { "iId", idIngr }
            });

            return RecipeData.Parse(r.Lignes);
        }

        public static bool AjouterRecette(int createurID, string nomRecette, string descriptif, List<int> ingredientsID)
        {
            Resultats r = BDD!.Requete("CALL CreeHeaderRecette(@cID, @nom, @descriptif);", new Dictionary<string, object>() {
                { "cID", createurID },
                { "nom", nomRecette },
                { "descriptif", descriptif },
            });
            if(r.Count > 0)
            {
                int recetteID = Int32.Parse(r.ObtenirLigne(0)["LAST_INSERT_ID()"].ToString());
                
                string req = string.Format("INSERT INTO IngredientRecette (recetteID, ingredientID) VALUES {0};", string.Join(",", ingredientsID.Select(id => string.Format("( {0}, {1} )", recetteID, id ))));

                BDD!.Requete(req);

                return true;
            }
            
            return false;
        }
        #endregion

        #region Ingredient
        public static ProductData ApprovisionnerIngredient(int idIngredient)
        {
            Resultats res = BDD!.Requete($"CALL Approvisionner(@idIng);", new Dictionary<string, object>() {
                { "idIng", idIngredient }
            });

            return ProductData.Parse(res.ObtenirLigne(0)!);
        }
        #endregion

        #region Createur

        public static Resultats? CreeProfilCreateur(int idClient)
        {
            Resultats res_client = BDD!.Requete($"CALL CreeProfilCreateur(@idClient);", new Dictionary<string, object>() { { "idClient", idClient } });
            return (res_client == null || res_client.Count == 0) ? null : res_client;
        }

        public static List<RecipeData> ObtenirRecettesCreateur(int createurId)
        {
            Resultats r = BDD!.Procedure("ObtenirRecettesCreateur", new Dictionary<string, object>() {
                { "creaId", createurId }
            });

            return RecipeData.Parse(r.Lignes);
        }

        public static Resultats ConvertirSoldeCreateur(int creaID, int solde)
        {
            return BDD!.Requete($"CALL ConvertirSoldeCreateur(@cID, @s, @resultat);", new Dictionary<string, object>() {
                { "cID", creaID },
                { "s", solde }
            }, new Dictionary<string, object>() {
                { "resultat", MySqlDbType.Bit }
            });
        }

        // Nombre de commande qu'un createur a recu pour l'ensemble de ses recettes
        public static (int, int) StatsCreateur(int creaId)
        {
            Resultats r = BDD!.Procedure("ObtenirStatsCreateur", new Dictionary<string, object>() {
                { "creaId", creaId },
            }, new Dictionary<string, object>() {
                { "nombreCommandes", MySqlDbType.Int32 },
                { "points", MySqlDbType.Int32 }
            });

            return ((int)r.Parameters["@nombreCommandes"].Value, (int)r.Parameters["@points"].Value);
        }

        public static List<OrderHistory> ObtenirListeCommandesRecettesCreateur(int creaId)
        {
            Resultats r = BDD!.Procedure("ObtenirListeCommandesRecettesCreateur", new Dictionary<string, object>() {
                { "creaId", creaId },
            });

            return OrderHistory.Parse(r.Lignes);
        }
        #endregion


        #region Commander
        public static (bool, int, int) Commander(int clientId, int recetteId, bool utiliserSoldeCrea)
        {
            Resultats r = BDD!.Procedure("Commander", new Dictionary<string, object>() {
                { "cliID", clientId },
                { "recID", recetteId },
                { "utiliserPointCrea", utiliserSoldeCrea }
            }, new Dictionary<string, object>
            {
                { "succes", MySqlDbType.Bit },
                { "prixSoldeCrea", MySqlDbType.Int32 },
                { "prixSoldeBanque", MySqlDbType.Int32 },
                { "idCommande", MySqlDbType.Int32 }
            });

            bool ok = (int) r.Parameters["@succes"].Value == 1;
            int prixBanque = (int) r.Parameters["@prixSoldeBanque"].Value;
            int prixSoldeCrea = (int) r.Parameters["@prixSoldeCrea"].Value;
            
            return (ok, prixBanque, prixSoldeCrea);
        }

        public static List<OrderHistory> ListeCommandesClient(int clientId)
        {
            Resultats r = BDD!.Requete($"CALL ObtenirListeCommandes (@cID);", new Dictionary<string, object>() {
                { "cID", clientId },
            });

            return OrderHistory.Parse(r.Lignes);
        }
        #endregion

        #region Admin

        public static ProductData  AjouterIngredient(ProductData ingredient)
        {
            Resultats r = BDD!.Requete($"CALL AjouterIngredient (@nom, @descr, @catId, @fourId, @stockActuelle, @stockMin, @stockMax, @prix);", new Dictionary<string, object>() {
                { "nom", ingredient.Titre },
                { "descr", ingredient.Description },
                { "catId", ingredient.CategorieId },
                { "fourId", ingredient.FournisseurId },
                { "stockActuelle", ingredient.Stock },
                { "stockMin", ingredient.StockMin },
                { "stockMax", ingredient.StockMax },
                { "prix", ingredient.Prix },
            });

            return ProductData.Parse(r.ObtenirLigne(0)!);
        }
        public static ClientData PromotionAdmin(int clientId)
        {
            Resultats r = BDD!.Requete($"CALL PromotionAdminClient (@cID);", new Dictionary<string, object>() {
                { "cID", clientId },
            });

            if(r.RetourValeur != "FALSE")
            {
                return ClientData.Parse(r.ObtenirLigne(0));
            } else
            {
                return null;
            }
        }

        public static bool SupprimerClient(int clientId)
        {
            BDD!.Requete($"CALL SupprimerClient (@cID);", new Dictionary<string, object>() {
                { "cID", clientId },
            });
            return true;
        }

        public static bool SupprimerRecette(int recetteId)
        {
            BDD!.Requete($"CALL SupprimerRecette (@rID);", new Dictionary<string, object>() {
                { "rID", recetteId },
            });
            return true;
        }

        #endregion

        #region Stats

        public static List<TopData<ClientData>> TopClientSemaine(int max = 5)
        {
            Resultats r = BDD!.Requete("SELECT * FROM TopClientSemaineView LIMIT @max;", new Dictionary<string, object>() {
                { "max", max },
            });

            List<TopData<ClientData>> td = new List<TopData<ClientData>>();

            for(int i = 0; i < r.Lignes.Count; i++)
            {
                td.Add(new TopData<ClientData>()
                {
                    Index = i+1,
                    Data = ClientData.Parse(r.Lignes[i])
                });
            }

            return td;
        }

        public static List<TopData<ClientData>> TopCreateurSemaine(int max = 5)
        {
            Resultats r = BDD!.Requete("SELECT * FROM TopCreateurSemaineView LIMIT @max;", new Dictionary<string, object>() {
                { "max", max },
            });

            List<TopData<ClientData>> td = new List<TopData<ClientData>>();

            for (int i = 0; i < r.Lignes.Count; i++)
            {
                td.Add(new TopData<ClientData>()
                {
                    Index = i+1,
                    Data = ClientData.Parse(r.Lignes[i])
                });
            }

            return td;
        }

        public static List<RecipeData> TopRecetteSemaine(int max = 5)
        {
            // TOP SEMAINE
            return ListeRecettes(SearchFilter.NewAndFresh, max);
        }

        #endregion
    }
}

