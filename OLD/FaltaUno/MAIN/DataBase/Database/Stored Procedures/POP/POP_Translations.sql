CREATE PROCEDURE [dbo].[POP_Translations]
AS
BEGIN
	;MERGE INTO [dbo].[Translations] AS Target
	USING (VALUES
		('ES','','UserAddressNotRegister',N'La dirección de correo electrónico no se encuentra registrada.')
		,('ES','','UserAlreadyRegister',N'La dirección de correo electrónico ya se encuentra registrada.')
		,('ES','','VerificationCodeNotMatch',N'El código de verificacion es incorrecto.')
		,('ES','','MandatoryEmail',N'La dirección de correo electrónico es obligatoria.')
		,('ES','','InvalidEmail',N'La dirección de correo electrónico es inválida.')
		,('ES','','PasswordMandatory',N'La contraseña es obligatoria.')
		,('ES','','Password should contain at least one lower case letter',N'La contraseña debe contener al menos una letra en minúscula.')
		,('ES','','Password should contain at least one upper case letter',N'La contraseña debe contener al menos una letra en mayúscula')
		,('ES','','Password should not be less than 8 characters or greater than 32 characters',N'La contraseña debe contener mas de 8 caracteres y menos de 32.')
		,('ES','','Password should contain at least one numeric value',N'La contraseña debe contener al menos un número.')
		,('ES','','EmailAlreadyRegister',N'La dirección de correo electrónico ya se encontraba registrada.')
		,('ES','','VerifyEmailTitle',N'Hay Equipo: Verificación de correo electrónico')
		,('ES','','UserOrPasswordInvalid',N'El usuario o la contraseña son incorrectos.')
		
		,('EN','','UserAddressNotRegister',N'The email address is not registered.')
		,('EN','','UserAlreadyRegister',N'The email address is already registered.')
		,('EN','','VerificationCodeNotMatch',N'The verification code is invalid.')
		,('EN','','MandatoryEmail',N'The email address is mandatory.')
		,('EN','','InvalidEmail',N'The email address is invalid.')
		,('EN','','PasswordMandatory',N'The password is mandatory.')
		,('EN','','EmailAlreadyRegister',N'The email address was already registered yet.')
		,('EN','','VerifyEmailTitle',N'Email address verification.')
	) AS Source ([LanguageCode],[Module],[ConstantName],[Translation])
	ON (Target.[LanguageCode] = Source.[LanguageCode] 
		AND Target.[Module] = Source.[Module]
		AND Target.[ConstantName] = Source.[ConstantName])
	WHEN MATCHED AND (
			NULLIF(Source.[Translation], Target.[Translation]) IS NOT NULL 
			OR NULLIF(Target.[Translation], Source.[Translation]) IS NOT NULL) THEN
		UPDATE 
		SET	[Translation] = Source.[Translation]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([LanguageCode],
			[Module],
			[ConstantName],
			[Translation])
		VALUES
			(Source.[LanguageCode],
			Source.[Module],
			Source.[ConstantName],
			Source.[Translation])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;
END
