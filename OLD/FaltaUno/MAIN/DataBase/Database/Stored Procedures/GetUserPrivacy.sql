CREATE PROCEDURE [dbo].[GetUserPrivacy]
	@UserID BIGINT
AS
	
	SELECT 
		U.UserPrivacyID,
		ISNULL(U.UserID,@UserID) AS UserID,
		D.Entity,
		D.Property,
		ISNULL(U.[Value], D.DefValue) AS 'Value'
	FROM 
		UserPrivacyDefault D
		LEFT JOIN UserPrivacy U ON U.UserPrivacyDefaultID = D.UserPrivacyDefaultID AND U.UserID = @UserID

		