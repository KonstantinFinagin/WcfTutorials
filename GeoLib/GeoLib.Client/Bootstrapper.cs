﻿namespace GeoLib.Client
{
    using System.Windows;
    using Caliburn.Micro;
    using GeoLib.Client.ViewModels;

    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}