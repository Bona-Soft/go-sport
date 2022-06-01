CREATE PROCEDURE [dbo].[GetDefaultUserPrivacy]
AS
	
	SELECT 
		0 AS UserPrivacyID,
		0 AS UserID,
		D.Entity,
		D.Property,
		D.DefValue AS 'Value'
	FROM 
		UserPrivacyDefault D


		