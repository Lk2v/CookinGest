using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace CookinGest.src.DB
{
    public class DB
    {
        MySqlConnection? conn;
        string baseDeDonnee;
        string utilisateur;
        string motDePasse;

        public DB(string bdd, string utl, string mdp)
        {
            utilisateur = utl;
            motDePasse = mdp;
            baseDeDonnee = bdd;
        }

        public (bool, string) Connection()
        {
            string errMsg = "";

            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        errMsg = "Cannot connect to server.  Contact administrator";
                        break;
                    case 1045:
                        errMsg = "Invalid username/password, please try again";
                        break;

                    default:
                        errMsg = ex.Message;
                        break;
                }
            }

            return (conn.State == System.Data.ConnectionState.Open, errMsg);
        }

        // Propriété
        protected string ConnectionString
        {
            get => "SERVER=localhost;PORT=3306;" + "DATABASE= " + baseDeDonnee + "; UID=" + utilisateur + "; PASSWORD=" + motDePasse + ";";
        }

        // Methodes
        public Resultats Procedure(string req, Dictionary<string, object>? param = null, Dictionary<string, object>? outParams = null)
        {
            return Requete(req, param, outParams, callProcedure: true);
        }

        public Resultats Requete(string req, Dictionary<string, object>? param = null, Dictionary<string, object>? outParams = null, bool callProcedure = false)
        {
            if (conn.State != System.Data.ConnectionState.Open) {
                Console.WriteLine("La connexion est fermée");
                return null;
            }

            List<Dictionary<string, object>> resultat = new List<Dictionary<string, object>>();
            string[] cN;

            try
            {
                using (MySqlCommand cmd = new MySqlCommand(req, conn))
                {
                    // Parametre SQL
                    if(param != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in param)
                        {
                            cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                        }
                    }

                    if(outParams != null)
                    {
                        foreach(KeyValuePair<string, object> k in outParams)
                        {
                            MySqlParameter p = new MySqlParameter($"@{k.Key}" , k.Value);
                            p.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(p);
                        }
                        
                    }

                    if(callProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cN = new string[reader.FieldCount];
                        bool cFill = false;

                        // Lecture
                        while (reader.Read())
                        {
                            Dictionary<string, object> champs = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (cFill == false)
                                {
                                    cN[i] = reader.GetName(i);
                                }

                                object value = reader.GetValue(i);
                                if (DBNull.Value.Equals(value)) {
                                    value = null;
                                }

                                champs[reader.GetName(i)] = value;
                            }

                            if (cFill == false) cFill = true;

                            
                            resultat.Add(champs);
                        }
                    }

                    return new Resultats(cN, resultat, cmd.Parameters);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
                Console.WriteLine("Programme en pause appuyer sur n'importe quelle touche pour relancer");
                Console.ReadKey();
                return null;
            }
        }

        public void Fermer()
        {
            conn.Close();
        }
	}

    public class Resultats
    {
        string[] nomColonne;
        List<Dictionary<string, object>> lignes;
        MySqlParameterCollection _params;
        public Resultats(string[] c, List<Dictionary<string, object>> v, MySqlParameterCollection prm)
        {
            nomColonne = c;
            lignes = v;

            _params = prm;
        }

        public MySqlParameterCollection Parameters
        {
            get => _params;
        }

        public object? GetParam(string key)
        {
            return Parameters.Contains(key) ? Parameters[key].Value : null;
        }

        public string[] Colonne {
            get => nomColonne;
        }

        public string? RetourValeur
        {
            get
            {
                if(this.Colonne.Length == 1)
                {
                    return this.Colonne[0].ToUpper();
                } else
                {
                    return null;
                }
            }
        }
        public List<Dictionary<string, object>> Lignes
        {
            get => lignes;
        }

        public int Count
        {
            get => lignes.Count;
        }

        public bool estVide
        {
            get => Count == 0;
        }

        public Dictionary<string, object>? ObtenirLigne(int i)
        {
            return (i < Count) ? Lignes.ElementAt(i) : null;
        }
    }
}

