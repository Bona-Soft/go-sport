using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers;
using System;

namespace MYB.BaseApplication.Domain.Messenger
{
   public class BaseMessage : IBaseMessage
   {
      public key<long> MessageID { get; private set; }
      public IBaseMessageState State { get; set; }
      public string Text { get; set; }
      public IBaseUser UserSender { get; set; }
      public DateTime DateTimeCreated { get; set; }

      public BaseMessage(long id)
      {
         MessageID = id;
      }

      public BaseMessage()
      {
         MessageID = 0;
         State = BaseApp.Resolve<IBaseMessageState,int>(1);
      }

      public static IBaseMessage New(IBaseUser user, string message)
      {
         //TODO: Desestimar y usar signalR
         //https://code.msdn.microsoft.com/Signalr-angular-chat-95dc8f06
         //BaseApp.DB.Execute<long>()
         return null;
      }
   }
}