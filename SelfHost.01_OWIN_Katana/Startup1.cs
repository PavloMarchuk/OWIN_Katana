using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IO;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(SelfHost._01_OWIN_Katana.Startup1))]

namespace SelfHost._01_OWIN_Katana
{
	using AppFunc = Func<IDictionary<string, object>, Task>;

	public class Startup1
	{

		private const string htmlText = "<html>" +
											"<head>" +
												"<title>Hello OWIN</title>" +
											"</head>" +
											"<body>" +
												"<h1>Simple Owin Application<h1>" +
											"</body>" +
										"</html>";
		public void Configuration(IAppBuilder app)
		{

			///////////public static void Run(this IAppBuilder app, Func<Microsoft.Owin.IOwinContext, System.Threading.Tasks.Task> handler);
			//// 1 вариант
			//app.Run(context =>
			//{
			//	context.Response.ContentType = "text/html";
			//	return context.Response.WriteAsync(htmlText);
			//});




			// 2 вариант
			app.Use(new Func<AppFunc, AppFunc>(next => (async context =>
			{
				using (var writer = new StreamWriter(context["owin.ResponseBody"] as Stream))
				{
					await writer.WriteAsync(htmlText);
				}
				await next.Invoke(context);
			})));


		}
	}
}
