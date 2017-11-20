﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.IO;

[assembly: OwinStartup(typeof(SelfHost._02_OWIN_Katana.Startup1))]

namespace SelfHost._02_OWIN_Katana
{
	using AppFunc = Func<IDictionary<string, object>, Task>;
	public class Startup1
	{
		public AppFunc MyMiddleWare(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				// Do something with the incoming request:
				var response = environment["owin.ResponseBody"] as Stream;
				using (var writer = new StreamWriter(response))
				{
					await writer.WriteAsync("<h1>Hello from My First Middleware</h1>");
				}
				// Call the next Middleware in the chain:
				await next.Invoke(environment);//!!!
			};
			return appFunc;
		}


		public AppFunc MyOtherMiddleWare(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				// Do something with the incoming request:
				var response = environment["owin.ResponseBody"] as Stream;
				using (var writer = new StreamWriter(response))
				{
					await writer.WriteAsync("<h1>Hello from My Second Middleware</h1>");
				}
				// Call the next Middleware in the chain:
				await next.Invoke(environment);
			};
			return appFunc;
		}
		public AppFunc ThirdMiddleware(AppFunc next)
		{
			AppFunc appFunc = async (IDictionary<string, object> environment) =>
			{
				// Do something with the incoming request:
				var response = environment["owin.ResponseBody"] as Stream;
				using (var writer = new StreamWriter(response))
				{
					await writer.WriteAsync("<h1>Hello from My Third Middleware</h1>");
				}
				// Call the next Middleware in the chain:
				await next.Invoke(environment);
			};
			return appFunc;
		}

		public void Configuration(IAppBuilder app)
		{

			var middleware = new Func<AppFunc, AppFunc>(MyMiddleWare);
			var otherMiddleware = new Func<AppFunc, AppFunc>(MyOtherMiddleWare);
			var thirdMiddleware = new Func<AppFunc, AppFunc>(ThirdMiddleware);
			app.Use(middleware);
			app.Use(otherMiddleware);
			app.Use(thirdMiddleware);
		}
	}
}
