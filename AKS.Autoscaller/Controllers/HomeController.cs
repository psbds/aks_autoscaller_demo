using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AKS.Autoscaller.Models;
using System.Threading;

namespace AKS.Autoscaller.Controllers
{
	public class HomeController : Controller
	{
		private static List<CancellationToken> tokens = new List<CancellationToken>();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			/*PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
			float cpuUsage = 0;
			while ((cpuUsage = cpuCounter.NextValue()) == 0)
			{
				Thread.Sleep(10);
			}
			ViewBag.cpuCounter = cpuUsage + "%";
			ViewBag.ramCounter = ramCounter.NextValue() + "MB";*/
			ViewBag.Tokens = tokens;
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult IncreaseCPU()
		{
			var token = new CancellationToken();
			tokens.Add(token);
			UseCPU(token);
			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		private void UseCPU(CancellationToken token)
		{
			for (int i = 0; i < Environment.ProcessorCount; i++)
			{
				Task.Run(UseCPUAction, token);
			}
		}

		private void UseCPUAction()
		{
			var count = 0;
			while (true)
			{
				var a = Math.PI * Math.PI / 1 + Math.PI * 200 - Math.PI;
				count++;
				if (count > 1000000)
				{
					count = 0;
					Thread.Sleep(1);
				}
			}
		}
	}
}
