using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System;
using System.Net.Http;
using System.Web;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IVirtualPagesManager : ISingleton
	{
		bool ExecutePage(string PageName, HttpRequest request, HttpResponse response);

		bool RegisterPage(string pageName, HttpMethod httpMethod, Func<HttpRequest, HttpResponse, bool> func);
	}
}