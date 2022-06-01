namespace MYB.FaltaUno.Model.Entities
{
	public struct Telephone
	{
		private string Type;
		private string CountryCodeNumber;
		private string AreaCode;
		private string Number;

		string CompleteNumber
		{
			get
			{
				return CountryCodeNumber + AreaCode + CompleteNumber;
			}
		}
	}
}