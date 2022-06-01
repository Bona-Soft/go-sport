CREATE PROCEDURE [dbo].[POP_Headquarters]
AS
BEGIN

	;MERGE INTO [dbo].[Locations] AS Target
	USING (VALUES
		((SELECT S.SportID FROM Sports S WHERE S.[Value] = 'futbol'),-34.609812,-58.46125,'Av. Tte. Gral. Donato Álvarez 1481, C1416BTD CABA, Argentina','Av. Tte. Gral. Donato Álvarez 1481, C1416BTD CABA, Argentina'),
		((SELECT S.SportID FROM Sports S WHERE S.[Value] = 'basket'),-34.61858,-58.44764,'Estadio echeberria','Estadio echeberria'),
		((SELECT S.SportID FROM Sports S WHERE S.[Value] = 'volley'),-66.66666,-66.66666,'Calle Falsa 123','Calle Falsa 123'),
		(null,-34.61989,-58.41877,'México 3739, C1217 CABA, Argentina','México 3739, C1217 CABA, Argentina')
	) AS Source ([SportID],[Lat],[Lng],[Display],[Value])
	ON (ROUND(Target.Lat,4,1) = ROUND(Source.Lat,4,1) AND ROUND(Target.Lng,4,1) = ROUND(Source.Lng,4,1))
	WHEN MATCHED THEN
		UPDATE 
		SET	[SportID] = Source.[SportID],
			[Lat] = Source.[Lat],
			[Lng] = Source.[Lng],
			[Display] = Source.[Display],
			[Value] = Source.[Value]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([SportID],
			[Lat],
			[Lng],
			[Display],
			[Value])
		VALUES
			(Source.[SportID],
			Source.[Lat],
			Source.[Lng],
			Source.[Display],
			Source.[Value])
	
	;MERGE INTO [dbo].[Headquarters] AS Target
	USING (VALUES
			('Caballito Sport',(SELECT S.SportID FROM Sports S WHERE S.[Value] = 'futbol'), (SELECT TOP 1 L.LocationID FROM Locations L WHERE L.Display = 'Av. Tte. Gral. Donato Álvarez 1481, C1416BTD CABA, Argentina')),
			('Club Ferrocarril Oeste',(SELECT S.SportID FROM Sports S WHERE S.[Value] = 'basket'), (SELECT TOP 1 L.LocationID FROM Locations L WHERE L.Display = 'Estadio echeberria')),
			('Test HQ Sport 3 Falso',(SELECT S.SportID FROM Sports S WHERE S.[Value] = 'volley'), (SELECT TOP 1 L.LocationID FROM Locations L WHERE L.Display ='Calle Falsa 123')),
			('Test HQ Sport 3 BonaHouse',null, (SELECT TOP 1 L.LocationID FROM Locations L WHERE L.Display ='México 3739, C1217 CABA, Argentina' ))
		) AS Source ([Name],[SportID],[LocationID])
	ON (Target.[Name] = Source.[Name])
	WHEN MATCHED THEN
		UPDATE 
		SET	[Name] = Source.[Name],
			[SportID] = Source.[SportID],
			[LocationID] = Source.[LocationID]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([Name],
			[SportID],
			[LocationID])
		VALUES
			(Source.[Name],
			Source.[SportID],
			Source.[LocationID]);
END

