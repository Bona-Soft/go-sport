CREATE VIEW [dbo].[MatchesPlayers]
	AS 
	
SELECT M.MatchID, P.PlayerID 
FROM 
	Players P
	INNER JOIN MatchPlayerRequests MPR ON MPR.PlayerRequestReceiverID = P.PlayerID
	INNER JOIN Matches M ON MPR.MatchID = M.MatchID
WHERE 
	MPR.MatchPlayerRequestStateID = 6 --confirmed
	OR MPR.MatchPlayerRequestStateID= 10 --completed
