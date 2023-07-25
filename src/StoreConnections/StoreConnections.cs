using System;
using System.Collections.Generic;
using System.IO;
using CookinGest.src.DataTemplate;

namespace CookinGest.src.StoreConnections
{
	public class StoreConnections
	{
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        static string storePath = Path.Combine(projectDirectory, "Assets", "Connections.csv");

      
        public static bool VerifierSiExiste()
        {
            bool exist = File.Exists(storePath);
            if (!exist)
            {
                // Si le fichier n'existe pas, on le crée
                File.Create(storePath).Dispose();
            }

            return exist;
        }

        public static bool ConnectionExiste(int id)
        {
            List<AccountData> liste = ConnectionsRecentes();

            return liste.Find(a => a.Id == id) != null;
        }

        public static void EnregistrerConnection(ClientData acc)
        {
            EnregistrerConnection(
                new AccountData
                {
                    Id = acc.Id,
                    Mail = acc.Mail,
                    Nom = acc.Nom,
                    Prenom = acc.Prenom,
                }
            );
        }

        public static void EnregistrerConnection(AccountData acc)
        {
            
            if(ConnectionExiste(acc.Id))
            {
                Console.WriteLine("La connexion existe deja");
                return;
            }

            using (StreamWriter writer = new StreamWriter(storePath, true))
            {
                writer.WriteLine(acc.Convertir());
            }
        }

        public static List<AccountData> ConnectionsRecentes()
        {
            VerifierSiExiste();

            List<AccountData> liste = new List<AccountData>();
            // On ouvre le fichier en mode lecture
            using (StreamReader sr = new StreamReader(storePath))
            {
                // On lit le contenu du fichier ligne par ligne
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    Console.WriteLine(ligne);
                    liste.Add(AccountData.Parse(ligne));
                }
            }
            return liste;
        }

        public static int NombreConnectionsRecentes()
        {
            return ConnectionsRecentes().Count;
        }

        public static bool SupprimerConnection(AccountData d)
        {
            List<AccountData> liste = ConnectionsRecentes();

            // On recherche l'élément à supprimer dans la liste
            AccountData elementASupprimer = liste.Find(a => a.Id == d.Id);

            if (elementASupprimer == null)
            {
                Console.WriteLine("La connexion n'existe pas");
                return false;
            }

            // On supprime l'élément de la liste et on réécrit le fichier
            liste.Remove(elementASupprimer);
            File.WriteAllLines(storePath, liste.ConvertAll(a => a.Convertir()));

            return true;
        }
    }
}

