CREATE PROCEDURE [dbo].[GetAllGeneralParameters]
	@ImplementationID INT = 0
AS
BEGIN
	DECLARE @_ImplementationID INT = @ImplementationID

	SELECT 
		GeneralParameterID,
		[Type],
		[Value]
	FROM
		GeneralParameters
	WHERE 
		ImplementationID = @_ImplementationID
		OR ( NOT EXISTS(SELECT 1 FROM GeneralParameters WHERE ImplementationID = @_ImplementationID)
			AND ImplementationID = 0)
END

