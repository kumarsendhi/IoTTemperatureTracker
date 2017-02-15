using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RoomTemperatureTracker
{
	public class App : Application
	{
		public App ()
		{
            MainPage = new TabbedPage
            {
                Children ={
                   new TemperatureList() {Title="Temperature" },
                   new HumidityList() {Title="Humidity" }
               }
            };
           
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
