CREATE PROCEDURE [dbo].[POP_MatchPlayerRequestStates]
AS
BEGIN

	;MERGE INTO [dbo].[MatchPlayerRequestStates] AS Target
	USING (VALUES
		(1,'pending','Pendiente','El jugador todavía no a aceptado o rechazado la invitación','I'),
		(2,'tentative_not','Tal vez no pueda','Posibilemente no pueda ir a jugar, todavía no confirmó','T'),
		(3,'tentative','No sabe todavia','Tal vez pueda ir a jugar, todavía no confirmó','T'),
		(4,'tentative_yes','Tal vez pueda','Posiblemente pueda ir a jugar, todavía no confirmó','T'),
		(5,'proposal','Propone una modificacion','Propone cambiar algun detalle del partido','T'),
		(6,'confirmed','Confirmado','Confirmado que puede ir a jugar','P'),
		(7,'reconfirmation_required','Pendiente de reconfirmar','Habia confirmado anteriormente, pero no reconfirmo todavia el cambio dado','I'),
		(8,'rejected','Rechazado','Rechazó la solicitud, no puede ir a jugar','N'),
		(9,'cancelled','Cancelado','La solicitud fue cancelada','N'),
		(10,'completed','Finalizado','La solicitud a finalizado','C'),
		(11,'approval_required','Aprobacion pendiente','La solicitud todavia no fue aprobada por el creador del partido','I'),
		(12,'confirmed_substitute','Suplente','Jugador confirmado como suplente','P'),
		(13,'expired','Expirado','La solicitud a expirado','C'),
		(14,'not_completed','No jugado','No participó','C')
	) AS Source ([MatchPlayerRequestStateID],[Value],[Description],[DefaultComment],[Type])
	ON (Target.[MatchPlayerRequestStateID] = Source.[MatchPlayerRequestStateID])
	WHEN MATCHED THEN
		UPDATE 
		SET [Value] = Source.[Value],
			[Description] = Source.[Description],
			[DefaultComment] = Source.[DefaultComment],
			[Type] = Source.[Type]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([MatchPlayerRequestStateID],
			[Value],
			[Description],
			[DefaultComment],
			[Type])
		VALUES
			(Source.[MatchPlayerRequestStateID],
			Source.[Value],
			Source.[Description],
			Source.[DefaultComment],
			Source.[Type])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

END

