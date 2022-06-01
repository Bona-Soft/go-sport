CREATE PROCEDURE [dbo].[DisconnectUser]
	@UserID BIGINT,
	@ClearSessions BIT
AS
BEGIN
	
	DECLARE 
		@_UserID VARCHAR(255) = @UserID
	
	DELETE TOP (1) S 
		FROM 
			[Sessions] S
			INNER JOIN LoginNames LN ON LN.LoginID = S.LoginID
		WHERE 
			LN.UserID = @_UserID
			

	IF @ClearSessions=1 
	BEGIN

		DELETE S
		FROM 
			[Sessions] S
			INNER JOIN LoginNames LN ON LN.LoginID = S.LoginID
		WHERE 
			LN.UserID = @_UserID
	END
END
