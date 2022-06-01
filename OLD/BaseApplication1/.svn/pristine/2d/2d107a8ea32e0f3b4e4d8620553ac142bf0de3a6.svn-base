CREATE PROCEDURE [dbo].[GetLoginPasswords]
	@LoginName VARCHAR(MAX),
	@IP VARCHAR(255),
	@ImplementationID INT = 0
AS
BEGIN

	DECLARE
		@_LoginName VARCHAR(MAX) = @LoginName,
		@_IP VARCHAR(255) = @IP,
		@_ImplementationID INT = @ImplementationID

	DECLARE @Passwords TABLE
		(UserID BIGINT,
		LoginID BIGINT,
		[Password] VARCHAR(255),
		[Sequence] TINYINT)

	IF NOT EXISTS (SELECT 1 FROM LoginNames WHERE ImplementationID = @_ImplementationID AND LoginName = @_LoginName)
		INSERT INTO @Passwords (UserID) VALUES (-1)
	ELSE IF EXISTS (SELECT 1 FROM LoginNames WHERE ImplementationID = @_ImplementationID AND LoginName = @_LoginName AND Deleted = 1)
		INSERT INTO @Passwords (UserID) VALUES (-2)
	ELSE IF EXISTS (SELECT 1 FROM LoginNames WHERE ImplementationID = @_ImplementationID AND LoginName = @_LoginName AND Blocked = 1)
		INSERT INTO @Passwords (UserID) VALUES (-3)
	ELSE IF EXISTS (SELECT 1 FROM LoginNames WHERE ImplementationID = @_ImplementationID AND LoginName = @_LoginName AND Locked = 1)
		INSERT INTO @Passwords (UserID) VALUES (-4)
	ELSE IF EXISTS (SELECT 1 FROM LoginNames LN 
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE LoginName = @_LoginName
								AND UIP.IP = '0'
								AND UIP.Status = 1
								AND LN.ImplementationID = @_ImplementationID)
	BEGIN
		IF EXISTS (SELECT 1 FROM LoginNames LN 
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE LoginName = @_LoginName
								AND UIP.IP = @_IP
								AND UIP.Status = 2
								AND LN.ImplementationID = @_ImplementationID)
			INSERT INTO @Passwords (UserID) VALUES (-5)								
	END	ELSE IF EXISTS (SELECT 1 FROM LoginNames LN 
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE LoginName = @_LoginName
								AND UIP.IP = '0'
								AND UIP.Status = 2
								AND LN.ImplementationID = @_ImplementationID)
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM LoginNames LN 
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE LoginName = @_LoginName
								AND UIP.IP = @_IP
								AND UIP.Status = 1
								AND LN.ImplementationID = @_ImplementationID)
			INSERT INTO @Passwords (UserID) VALUES (-6)	
	END ELSE BEGIN
		
		INSERT INTO @Passwords (UserID, LoginID, [Password], [Sequence]) 
		SELECT 		
			N.UserID,
			N.LoginID,
			P.[Password],
			P.[Sequence]
		FROM 
			LoginNames N
			INNER JOIN LoginPasswords P ON (P.LoginID = N.LoginID)
		WHERE 
			LoginName = @_LoginName
			AND ImplementationID = @_ImplementationID
		ORDER BY
			[Sequence] ASC
	END

	SELECT 
		P.UserID, 
		P.LoginID, 
		P.[Password],
		P.[Sequence]
	FROM
		@Passwords P

END 