CREATE PROCEDURE [dbo].[AuthenticateSession]
	@Session VARCHAR(255),
	@IP VARCHAR(255) = NULL,
	@UserAgent VARCHAR(255) = NULL
AS
	
	DECLARE @Succeed INT = 0
	IF NOT EXISTS (SELECT 1 FROM [Sessions] S WHERE S.[Session] = @Session)
		SET @Succeed = -1
	--IF EXISTS (SELECT 1 FROM [Sessions] S	WHERE S.DateTimeCreated < GETUTCDATE - dbo.MaxDateTimeExpirationLogin) SET @Succeed = -2
	--IF EXISTS (SELECT 1 FROM [Sessions] S WHERE S.DateTimeExpiration < GETUTCDATE - dbo.DateTimeExpirationLogin) SET @Succeed = -3
	ELSE IF EXISTS (SELECT 1 FROM [Sessions] S
							INNER JOIN LoginNames LN ON S.LoginID = LN.LoginID
						WHERE S.[Session] = @Session AND LN.Deleted = 1)
		SET @Succeed = -4
	ELSE IF EXISTS (SELECT 1 FROM [Sessions] S
							INNER JOIN LoginNames LN ON S.LoginID = LN.LoginID
						WHERE S.[Session] = @Session AND LN.Blocked = 1)
		SET @Succeed = -5
	ELSE IF EXISTS (SELECT 1 FROM [Sessions] S
							INNER JOIN LoginNames LN ON S.LoginID = LN.LoginID
						WHERE S.[Session] = @Session AND LN.Locked = 1)
		SET @Succeed = -6
	ELSE IF EXISTS (SELECT 1 FROM [Sessions] S
							INNER JOIN LoginNames LN ON S.LoginID = LN.LoginID
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE S.[Session] = @Session
								AND UIP.IP = '0'
								AND UIP.Status = 1) 
	BEGIN --IF IP is blocked
		IF EXISTS (SELECT 1 FROM [Sessions] S
							INNER JOIN LoginNames LN ON S.LoginID = LN.LoginID
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE S.[Session] = @Session
								AND UIP.IP = @IP
								AND UIP.Status = 2)
			SET @Succeed = -7 
	END
	ELSE IF EXISTS (SELECT 1 FROM [Sessions] S
							INNER JOIN LoginNames LN ON S.LoginID = LN.LoginID
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE S.[Session] = @Session
								AND UIP.IP = '0'
								AND UIP.Status = 2) 
	BEGIN --IF IP is allowed
		IF NOT EXISTS (SELECT 1 FROM [Sessions] S
							INNER JOIN LoginNames LN ON S.LoginID = LN.LoginID
								INNER JOIN UserIPAccessStatus UIP ON LN.UserID = UIP.UserID 
							WHERE S.[Session] = @Session
								AND UIP.IP = @IP
								AND UIP.Status = 1)
			SET @Succeed = -8
	END 
	ELSE 
	BEGIN
		SELECT 		
			@Succeed = N.UserID
		FROM 
			LoginNames N
			INNER JOIN [Sessions] S ON (S.LoginID = N.LoginID)
		WHERE 
			S.[Session] = @Session

		UPDATE [Sessions]
		SET IP = @IP,
			UserAgent = @UserAgent
		WHERE
			[Session] = @Session
			
	END

	RETURN @Succeed