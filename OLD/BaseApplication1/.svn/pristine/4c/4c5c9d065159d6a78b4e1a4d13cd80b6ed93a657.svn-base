CREATE PROCEDURE [dbo].[SetSession]
	@LoginID BIGINT,
	@Session VARCHAR(255),
	@IP VARCHAR(255),
	@UserAgent VARCHAR(255),
	@Unique BIT
AS
BEGIN

	DECLARE 
		@Result BIGINT = -1,
		@_LoginID BIGINT = @LoginID

	IF EXISTS(SELECT TOP 1 1 FROM LoginNames WHERE LoginID = @_LoginID)
	BEGIN
		IF @Unique = 1
			DELETE FROM 
				[Sessions]
			WHERE 
				LoginID = @_LoginID

		INSERT INTO [Sessions] 
			(LoginID,
			[Session],
			DateTimeCreated,
			IP,
			UserAgent)
		VALUES
			(@LoginID,
			@Session,
			GETUTCDATE(),
			@IP,
			@UserAgent)
		
		SET @Result = SCOPE_IDENTITY()	
	END
	
	RETURN @Result

END
