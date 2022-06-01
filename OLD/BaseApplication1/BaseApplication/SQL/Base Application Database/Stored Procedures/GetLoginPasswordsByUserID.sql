CREATE PROCEDURE [dbo].[GetLoginPasswordsByUserID]
	@UserID BIGINT,
	@IP VARCHAR(255) = NULL,
	@ImplementationID INT = 0
AS
BEGIN

	DECLARE
		@_UserID VARCHAR(MAX) = @UserID,
		@_IP VARCHAR(255) = @IP,
		@_ImplementationID INT = @ImplementationID

	SELECT N.LoginID,
			N.ImplementationID,
			N.UserID,
			N.LoginName,
			N.Blocked,
			N.Deleted,
			N.DateTimeCreated,
			N.VerificationCode,
			N.VerificationCodeExpiration,
			P.[Password],
			P.[Sequence]
	FROM 
		LoginNames N
		INNER JOIN LoginPasswords P ON N.LoginID = P.LoginID
	WHERE
		UserID = @_UserID
		AND Deleted = 0
	ORDER BY
		N.UserID,
		N.LoginID,
		P.[Sequence]

END 