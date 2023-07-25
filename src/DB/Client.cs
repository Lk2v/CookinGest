using System;
using System.Collections.Generic;
using CookinGest.src.DataTemplate;
using Tmds.DBus;

namespace CookinGest.src.DB
{
	public class Client
	{
		int clientID;
        int? createurID;

		string mail;
		string nom;
		string prenom;

		decimal telephone;
        string adresse;

        int soldeBanque;
        int soldePoint;

        bool estAdmin;

        public Client(Dictionary<string, object> dic)
		{
            MiseAJourProfil(dic);
        }

		// Prop
		public int ClientID { get => clientID; }

        public int? CreateurID { get => createurID; }

        public string Mail { get => mail; }

        public string Nom { get => nom; }
        public string Prenom { get => prenom; }
        public string Initiale
        {
            get => Prenom[0].ToString().ToUpper() + Nom[0].ToString().ToUpper();
        }

        public decimal Telephone { get => telephone; }

        public int SoldeBanque { get => soldeBanque; }

        public int SoldePoint {
            get => (soldePoint == null) ? 0 : (int)soldePoint;
        }

        public string Adresse { get => adresse; }

        public bool EstCreateur { get => CreateurID != null; }

        public bool EstAdmin { get => estAdmin; }

        public string Denomination
        {
            get => Prenom + " " + nom;
        }

        public void MiseAJourProfil(Dictionary<string, object> dic)
        {
            clientID = (int)dic["clientID"];
            
            mail = (string)dic["mail"];
            //motDePasse = client["motDePasse"];
            nom = (string)dic["nomClient"];
            prenom = (string)dic["prenomClient"];
            telephone = (decimal)dic["telephoneClient"];
            adresse = (string)dic["adresseClient"];

            estAdmin = (bool)dic["estAdmin"];

            MiseAJourProfilCreateur(dic);
        }

        public void MiseAJourProfilCreateur(Dictionary<string, object> dic)
        {
            createurID = (int?)dic["createurID"];
            soldeBanque = (int)dic["soldeBanque"];
            soldePoint = (dic["soldePoint"] == null) ? 0 : ((int)dic["soldePoint"]);
        }

        // Methode
        public bool CreeProfilCreateur() {
            if(!EstCreateur) {
                Resultats? res = Service.CreeProfilCreateur(ClientID);
                if(res == null)
                {
                    //Erreur dans le processus, le profil createur n'a pas pu être crée
                    return false;
                } else
                {
                    MiseAJourProfilCreateur(res.ObtenirLigne(0)!);
                    return true;
                }
            } else {
                return false;
            }
        }

        public bool RechargerCompteBancaire()
        {
            Resultats r = Service.RechargerSoldeBancaire(ClientID);
            if(r.RetourValeur == "FALSE")
            {
                return false;
            } else
            {
                MiseAJourProfil(r.ObtenirLigne(0)!);
                return true;
            }
        }

        public bool ConvertirSoldeCreateur(int montantSoldeAConvertir)
        {
            if (CreateurID == null)
            {
                return false;
            }
            else
            {
                Resultats r = Service.ConvertirSoldeCreateur((int) CreateurID, montantSoldeAConvertir);
                object res = r.GetParam("@resultat")!;
                bool ok = ((res == null ? 0 : (int)res)) == 1;

                if (ok)
                {
                    MiseAJourProfil(r.ObtenirLigne(0)!);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Commande
        public void Commander(int recetteID , bool depuisSoldeCrea)
        {
            bool ok;
            int prixEuro;
            int prixSoldeCrea;

           (ok, prixEuro, prixSoldeCrea) = Service.Commander(ClientID, recetteID, depuisSoldeCrea);
           if(ok) 
            {
                soldeBanque -= prixEuro;
                soldePoint -= prixSoldeCrea;
            }
        }


        // 
        public bool CreeRecette(string nomRecette, string descriptif, List<int> ingredientsID)
        {
            return (CreateurID == null) ? false : Service.AjouterRecette((int) CreateurID, nomRecette, descriptif, ingredientsID);
        }

        public List<OrderHistory> ListeCommandesClient()
        {
            return Service.ListeCommandesClient(ClientID);
        }
        

        public List<OrderHistory> ObtenirListeCommandesRecettesCreateur()
        {
            return (EstCreateur != null) ? Service.ObtenirListeCommandesRecettesCreateur((int) CreateurID!) : new List<OrderHistory>() { };
        }

        public (int, int) StatsCreateur()
        {
            if (EstCreateur) {
                int nbCommande, soldeCrea;

                (nbCommande, soldeCrea) = Service.StatsCreateur((int)CreateurID);
                soldePoint = soldeCrea;
                return (nbCommande, soldeCrea);
            } else
            {
                return (-1, -1);
            }
        }

        public List<RecipeData> ObtenirListeRecetteCreateur()
        {
            return EstCreateur ? Service.ObtenirRecettesCreateur((int) CreateurID) : new List<RecipeData>();
        }

        public string Debug() {
            return $"{ClientID} : {Prenom} {Nom} <{Mail}> : {SoldeBanque}€";
        }
    }
}

