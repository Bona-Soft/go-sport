using System;
using System.Collections.Generic;

namespace MYB.BaseApplication.Framework.Helpers.TypesExt
{
   public static class TypeExt
   {
      /// <summary>
      ///   Get the name of the type without the generic arity [`1]
      /// </summary>
      public static string GetNameWithoutGenericArity(this Type t)
      {
         string name = t.Name;
         int index = name.IndexOf('`');
         return index == -1 ? name : name.Substring(0, index);
      }

      /// <summary>
      ///   Get the type of the parent of the inerhated object as an inumerable. i.e. "YourClass : ParentClass, IYourClass, IAnotherClass", the ParentType of Your class is the ParentClass and the interfaces.
      /// </summary>
      public static IEnumerable<Type> ParentTypes(this Type type)
      {
         foreach (Type i in type.GetInterfaces())
         {
            yield return i;
            foreach (Type t in i.ParentTypes())
            {
               yield return t;
            }
         }

         if (type.BaseType != null)
         {
            yield return type.BaseType;
            foreach (Type b in type.BaseType.ParentTypes())
            {
               yield return b;
            }
         }
      }
   }
}