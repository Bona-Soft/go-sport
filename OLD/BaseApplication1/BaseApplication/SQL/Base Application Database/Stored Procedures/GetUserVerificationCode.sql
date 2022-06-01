CREATE PROCEDURE [dbo].[GetUserVerificationCode]  
 @Username VARCHAR(255),  
 @ImplementationID int = 0  
AS  
BEGIN  
  
 SELECT  
  L.VerificationCode  
 FROM  
  LoginNames L  
 WHERE  
  L.LoginName = @Username  
  AND L.ImplementationID = @ImplementationID  
END  