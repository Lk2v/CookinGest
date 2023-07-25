using System;
namespace CookinGest.src.StoreConnections
{
	public class AccountData
	{
        public int Id { get; set; }
        public string Mail { get; set; } = "";
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";

        //public AccountType Type { get; set; }

        public string Convertir()
        {
            return $"{Id},{Mail},{Nom},{Prenom}";
        }

        public static AccountData Parse(string ligne)
        {
            string[] values = ligne.Split(',');

            AccountData account = new AccountData()
            {
                Id = Int32.Parse(values[0]),
                Mail = values[1],
                Nom = values[2],
                Prenom = values[3],
                //Type = (AccountType)Enum.Parse(typeof(AccountType), values[3])
            };

            return account;
        }
    }

    public enum AccountType
    {
        Classique,
        Createur,
        Admin,
    }
}

