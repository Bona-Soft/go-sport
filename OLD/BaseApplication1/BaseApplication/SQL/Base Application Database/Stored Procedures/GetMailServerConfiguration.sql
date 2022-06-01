CREATE PROCEDURE [dbo].[GetMailServerConfiguration]
AS
	SELECT 
		[Name],
		[Subject],
		[Body],
		[IsBodyHtml],
		[Priority],
		[From],
		[Host],
		[Port],
		[EnableSsl] ,
		[UseDefaultCredentials],
		[MailAddress],
		[Password]
	FROM
		[MailServerConfigurations]
