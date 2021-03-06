using Microsoft.VisualStudio.TestTools.UnitTesting;
using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.BaseApplication.Infrastructure.BaseUnitTesting;
using MYB.FaltaUno.Application.UIService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using static MYB.BaseApplication.Infrastructure.BaseUnitTesting.BaseUnitTest;

namespace MYB.BaseApplication.Testing.UnitTest
{
	[TestClass]
	public class UT_WebServices : BaseUnitTest
	{
		#region "Test classes and methods"

		public class EntityTest
		{
			public key<long> TestID { get; set; }
			public string PropString { get; set; }
			public int PropInt { get; set; }
			public DateTime PropDateTime { get; set; }
			public int Prop1 { get; set; }
			public int Prop2 { get; set; }
			public int Prop3 { get; set; }
			public int Prop4 { get; set; }
			public int Prop5 { get; set; }
			public int Prop6 { get; set; }
			public int Prop7 { get; set; }
			public int Prop8 { get; set; }
			public int Prop9 { get; set; }
			public int Prop10 { get; set; }
			public int Prop11 { get; set; }
			public int Prop12 { get; set; }
			public int Prop13 { get; set; }
			public int Prop14 { get; set; }
			public int Prop15 { get; set; }
			public int Prop16 { get; set; }
			public int Prop17 { get; set; }
			public int Prop18 { get; set; }
			public int Prop19 { get; set; }
		}

		
		#endregion "Test classes and methods"

		[TestMethod]
		public void UT_GetSQLServerCreateTableFromEntity()
		{
			EntityTest eTest = new EntityTest();
			string result = Generics.GetSQLServerStrings.FullTableFromEntity(eTest);
			Console.Write(result);
		}

      [TestMethod]
      public void UT_GetSQLServerQuerysEntities()
      {
         IMatch match = BaseApp.Resolve<IMatchFactory>().New();
         string result = Generics.GetSQLServerStrings.FullTableFromEntity(match);
         Console.Write(result);
      }

      [TestMethod]
		public void UT_Validator()
		{
			Validator.Options.ValidationFlags.ValidateSymbols = true;
			Tuple<int, string> outVal;

			FailIf.True(Validator.ValidatePassword("", out outVal));
			FailIf.True(Validator.ValidatePassword("HOLAASFGAASG", out outVal));
			FailIf.True(Validator.ValidatePassword("asgsdhdshsdhs", out outVal));
			FailIf.True(Validator.ValidatePassword("123456789", out outVal));
			FailIf.True(Validator.ValidatePassword("12", out outVal));
			FailIf.True(Validator.ValidatePassword("ab", out outVal));
			FailIf.True(Validator.ValidatePassword("ASFGASDG!ASD3245", out outVal));
			FailIf.True(Validator.ValidatePassword("HolaAS_DFa43543.!215325ASFgfagsags3424asfasf5235dasgfHolaASD", out outVal));
			FailIf.True(Validator.ValidatePassword(".................", out outVal));
			FailIf.True(Validator.ValidatePassword("HolaASDFa", out outVal));
			FailIf.True(Validator.ValidatePassword("HolaASD..Fa", out outVal));
			FailIf.True(Validator.ValidatePassword("HolaASDF2523a", out outVal));
			FailIf.True(Validator.ValidatePassword("Holaaaaa....", out outVal));
			FailIf.True(Validator.ValidatePassword("ASFGASDG!ASD3245", out outVal));

			FailIf.False(Validator.ValidatePassword("PassworCorrect_Prueba24!", out outVal));
		}

		[TestMethod]
		public void UT_DictonionaryExt()
		{
			Dictionary<string, int, object> dic = new Dictionary<string, int, object>()
			{
				{"test1",1, new DateTime()},
				{"test2",1,235},
				{"test3",2,2352636},
				{"test3",3,"asd"},
				{"test5",4,235734734575475474},
				{"test6",6,66}
			};

			dic.Add("testAdd", 40, "value of TestAdd");

			dic.Rename("test3", "testRenamed");

			dic.Rename(1, 777);

			dic.Remove("test5", 4);

			FailIf.False(dic.ContainsKey("test6", 6));

			FailIf.NotEqual(dic.ToLongString(), "(test2, 777)=235;(test1, 777)=1/1/0001 12:00:00 AM;(testRenamed, 3)=asd;(testRenamed, 2)=2352636;(test6, 6)=66;(testAdd, 40)=value of TestAdd");
			Console.Write(dic.ToWrapString());

		}

		[TestMethod]
		public void UT_TypeExt()
		{
			typeof(string).ToSqlDbType();
			typeof(short).ToSqlDbType();
			typeof(int).ToSqlDbType();
			typeof(long).ToSqlDbType();

			typeof(string).ToOleDbType();
			typeof(short).ToOleDbType();
			typeof(int).ToOleDbType();
			typeof(long).ToOleDbType();

			TypeExtension.ToClrType(OleDbType.BigInt);
			TypeExtension.ToClrType(SqlDbType.BigInt);
		}

		[TestMethod]
		public void UT_WS_Match()
		{
			FakeHttpContext("MatchID=1");
			Console.Write(MatchUIService.Instance.GetCompleteMatch(1));
		}

		public static HttpContext FakeHttpContext(string queryString = "")
		{
			var httpRequest = new HttpRequest("", "https://hayequipo.127-0-0-1.org.uk", queryString);
			var stringWriter = new StringWriter();
			var httpResponse = new HttpResponse(stringWriter);
			var httpContext = new HttpContext(httpRequest, httpResponse);

			var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
																 new HttpStaticObjectsCollection(), 10, true,
																 HttpCookieMode.AutoDetect,
																 SessionStateMode.InProc, false);

			httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
												 BindingFlags.NonPublic | BindingFlags.Instance,
												 null, CallingConventions.Standard,
												 new[] { typeof(HttpSessionStateContainer) },
												 null)
									  .Invoke(new object[] { sessionContainer });

			return httpContext;
		}
	}
}