using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Collections.Generic;

namespace MYB.BaseApplication.Domain.Messenger
{
   public class Conversation : IBaseConversation
   {
      public List<IBaseMessage> Messages { get; private set; }
      public IBaseUser UserOwner { get; private set; }
      public DateTime DateTimeCreated { get; private set; }

      public List<IBaseUser> Users { get; private set; }

      public IBaseMessage AddMessage(long userID, string message)
      {
         return null;
      }

      public IBaseMessage AddMessage(IBaseUser user, string message)
      {
         return null;
      }

      public void AddMessage(IBaseMessage message)
      {
         if (!Users.Exists(user => user.UserID == message.UserSender.UserID))
         {
            Users.Add(message.UserSender);
         }
         Messages.Add(message);
      }

      public void RemoveMessage(IBaseMessage message)
      {
      }
   }
}