namespace MYB.BaseApplication.Security.Configuration
{
	public class SecuritySettings
	{
		private static SecuritySection _securitySection;
		private static PagesSection _publicPages;

		public static SecuritySection GetSecuritySection()
		{
			if (_securitySection == null)
			{
				_securitySection = (SecuritySection)System.Configuration.ConfigurationManager.GetSection("Security/securitySection");
			}
			return _securitySection;
		}

		public static PagesSection GetPublicPagesSection()
		{
			if (_publicPages == null)
			{
				_publicPages = (PagesSection)System.Configuration.ConfigurationManager.GetSection("Security/publicPages");
			}
			return _publicPages;
		}
	}
}