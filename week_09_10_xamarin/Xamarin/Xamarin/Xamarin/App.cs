using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Xamarin
{
    public class App : Application
    {
        public DateTime StartTijd { get; set; }

        public App()
        {
            //Lijst van tests
            string[] testenarray = new string[] { "test 1", "test 2", "test 25", "Doet zot zijn zeer?" };
            //Lijst van patienten
            string[] patientenarray = new string[] { "Jef", "Jantje", "Piet", "Joris", "En Korneel", "Baarden", "Varen" };



            //Aanmaken van controls
            Label tekst = new Label
            {
                XAlign = TextAlignment.Center,
                Text = "Welcome to Xamarin Forms!"

            };
            Picker tests = new Picker
            {
                Title = "Test",
                Rotation = 45.0
                //HorizontalOptions = LayoutOptions.Center,         
            };
            Picker patienten = new Picker
            {
                Title = "Patient",
                HorizontalOptions = LayoutOptions.Center
            };
            Button startbtn = new Button
            {
                Text = "Start"
            };
            startbtn.Clicked += StartButtonClicked;


            Stepper noname = new Stepper
            {
                HorizontalOptions = LayoutOptions.Center
            };

            //Alle logica
            //testen in combobox steken
            for (int i = 0; i < testenarray.Count(); i++)
            {
                tests.Items.Add(testenarray[i]);
            }
            //Patienten in combobox steken
            for (int i = 0; i < patientenarray.Count(); i++)
            {
                patienten.Items.Add(patientenarray[i]);
            }

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        tests,
                        patienten,
                        startbtn
                    }
                }
            };
        }



        private void StartButtonClicked(object sender, EventArgs e)
        {
            StartTijd = DateTime.Now;
        }





        //Dit stond er al 
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
