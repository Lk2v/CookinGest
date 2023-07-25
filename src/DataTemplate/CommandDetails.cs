using System;
namespace CookinGest.src.DataTemplate
{
	public class CommandDetails
	{
		public int Qte { get; set; }
		public PaymentMethod MethodPayement { get; set; }
    }


	public enum PaymentMethod
	{
		Banque = 0,
		SoldeCreateur = 1,
	}
}

