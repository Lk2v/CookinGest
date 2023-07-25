using System;
namespace CookinGest.src.DataTemplate
{
	public class DialogResult<T>
    {
		public T? Value { get; set; }

		public bool IsNull
		{
			get => Value == null;
		}

		public DialogResult(T v)
		{
			Value = v;
		}
	}
}

