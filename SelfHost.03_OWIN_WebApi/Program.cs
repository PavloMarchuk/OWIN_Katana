using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SelfHost._03_OWIN_WebApi
{
	class Program
	{
		static void Main(string[] args)
		{
			string baseAddress = "http://localhost:9000/";

			// Start OWIN host 
			using (WebApp.Start<Startup1>(url: baseAddress))
			{
				// Create HttpCient and make a request to api/values 
				Console.WriteLine("Сервер стартанув\n");
				Console.ReadLine();
			}
		}
	}

}
