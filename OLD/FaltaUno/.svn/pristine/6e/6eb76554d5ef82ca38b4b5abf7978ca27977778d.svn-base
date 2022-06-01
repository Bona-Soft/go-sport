CREATE PROCEDURE [dbo].[POP_MatchTypes]
AS
BEGIN

	;MERGE INTO [dbo].[MatchTypes] AS Target
	USING (VALUES
		(1,'futbol_indoor','Futbol Sala',0,3,0),
		(1,'futbol_five','Futbol 5',1,4,0),
		(1,'futbol_seven','Futbol 7',0,4,0),
		(1,'futbol_big','Futbol 11',0,1,0),
		(1,'futbol_beach','Futbol Playa',0,5,1),
		(1,'futbol_tennis','Futbol Tenis',0,5,0),
		(1,'futbol_other','Otros',0,5,0),
		(2,'basket_other','Indistinto',1,6,0),
		(3,'volley_normal','Normal',1,9,0),
		(3,'volley_beach','Playa',0,10,1),
		(3,'volley_other','Otros',0,12,0),
		(4,'tennis_single','Singles',1,13,0),
		(4,'tennis_doubles','Dobles',0,13,0),
		(4,'tennis_other','Otros / Indistinto',0,16,0),
		(5,'squash_single','Singles',1,17,0),
		(5,'squash_vela','Vela',0,17,0),
		(5,'squash_doubles','Dobles',0,19,0),
		(5,'squash_other','Indistinto',0,21,0),
		(6,'rugby_union','Union (15)',1,22,0),
		(6,'rugby_league','League (13)',0,22,0),
		(6,'rugby_seven','Rugby de 7',0,22,0),
		(6,'rugby_other','Otros',0,27,0),
		(7,'american_other','Indistinto',1,28,1)
	) AS Source ([SportID],[Value],[Description],[Default],[DefaultFieldID],[UniqueField])
	ON (Target.[SportID] = Source.[SportID] AND Target.[Value] = Source.[Value] )
	WHEN MATCHED THEN
		UPDATE 
		SET [SportID] = Source.[SportID],
			[Value] = Source.[Value],
			[Description] = Source.[Description],
			[Default] = Source.[Default],
			[DefaultFieldID] = Source.[DefaultFieldID],
			[UniqueField] = Source.[UniqueField]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([SportID],
			[Value],
			[Description],
			[Default],
			[DefaultFieldID],
			[UniqueField])
		VALUES
			(Source.[SportID],
			Source.[Value],
			Source.[Description],
			Source.[Default],
			Source.[DefaultFieldID],
			Source.[UniqueField]);

END

