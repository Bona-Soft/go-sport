CREATE PROCEDURE [dbo].[SetGeneralParameter]
	@ImplementationID INT = 0,
	@GeneralParameterID VARCHAR(255),
	@Type VARCHAR(255),
	@Value VARCHAR(255)
AS
BEGIN
	DECLARE 
		@_ImplementationID INT = @ImplementationID,
		@_GeneralParameterID VARCHAR(255) = @GeneralParameterID,
		@_Type VARCHAR(255) = @Type,
		@_Value VARCHAR(255) = @Value

	IF EXISTS(SELECT 1 FROM GeneralParameters WHERE ImplementationID = @_ImplementationID AND GeneralParameterID = @_GeneralParameterID)
	BEGIN
		UPDATE GeneralParameters
		SET 
			[Type] = @_Type,
			[Value] = @_Value
		WHERE
			ImplementationID = @_ImplementationID
			AND GeneralParameterID = @_GeneralParameterID
	END ELSE BEGIN
		INSERT INTO GeneralParameters
			(ImplementationID,
			GeneralParameterID,
			[Type],
			[Value])
		VALUES
			(@_ImplementationID,
			@_GeneralParameterID,
			@_Type,
			@_Value)
	END
END
