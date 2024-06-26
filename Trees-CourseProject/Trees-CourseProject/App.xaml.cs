﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Trees_CourseProject
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() 
        {
            //Startup += ApplicationStartup;
        }

        private async void ApplicationStartup(object sender, StartupEventArgs e)
        {
            // Create and show the splash screen window
            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();

            // Ожидание, пока окно с заставкой не будет закрыто
            await Task.Delay(TimeSpan.FromSeconds(2)); // Длительность анимации плюс дополнительное время
            // System.Threading.Thread.Sleep(2000);
            splashScreen.Close();
            // (sender as Window).Close();

            // Create and show the main window
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
        // protected override void OnStartup(StartupEventArgs e)
        // {
        //     // Create and show the splash screen window
        //     SplashScreen splashScreen = new SplashScreen();
        //     splashScreen.Show();
        //     //
        //     // // Ожидание, пока окно с заставкой не будет закрыто
        //     // Thread.Sleep(2000);
        //     // // System.Threading.Thread.Sleep(2000);
        //     // splashScreen.Close();
        //     // // (sender as Window).Close();
        //     //
        //     // // Create and show the main window
        //     // MainWindow mainWindow = new MainWindow();
        //     // //Application.Current.MainWindow = mainWindow;
        //     // mainWindow.Show();
        //     //
        //     base.OnStartup(e);
        // }
    }
}
