using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;

namespace MYB.BaseApplication.Infrastructure.BaseUnitTesting
{
	public abstract class BaseUnitTest
	{
		public static class FailIf
		{
			public static void True(bool val)
			{
				if (val)
					throw new Exception("return false when expected true");
			}

			public static void False(bool val)
			{
				if (!val)
					throw new Exception("return true when expected false");
			}

			public static void NotRaiseEx<TResult>(Func<TResult> function)
			{
				try
				{
					function.DynamicInvoke();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return;
				}
				throw new Exception("the function " + function.Method.Name + " do not raise an exception when was expected");
			}

			public static void NotRaiseEx<TResult, T1>(Func<T1, TResult> function, params object[] args)
			{
				try
				{
					function.DynamicInvoke(args);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return;
				}
				throw new Exception("the function " + function.Method.Name + " do not raise an exception when was expected");
			}

			public static void NotRaiseEx<TResult, T1, T2>(Func<T1, T2, TResult> function, params object[] args)
			{
				try
				{
					function.DynamicInvoke(args);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return;
				}
				throw new Exception("the function " + function.Method.Name + " do not raise an exception when was expected");
			}

			public static void NotRaiseEx<TResult>(Delegate function, params object[] args)
			{
				try
				{
					function.DynamicInvoke(args);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					return;
				}
				throw new Exception("the function " + function.Method.Name + " do not raise an exception when was expected");
			}

			public static void NotEqual<T1,T2>(T1 obj1, T2 obj2)
			{
				if (obj1.Equals(obj2))
				{
					return;
				}
				throw new Exception("This two objects are not equals: " + Environment.NewLine + Environment.NewLine + obj1.ToDefString() + Environment.NewLine + Environment.NewLine + "----" + Environment.NewLine + Environment.NewLine + obj2.ToDefString());
			}
		}
	}	
}