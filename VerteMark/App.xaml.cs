﻿using System.Configuration;
using System.Data;
using System.Windows;

namespace VerteMark
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //kontrola verze win. pouzivame fonty, ktere nejsou (dle dokumentace) dostupne na win starsich nez 8.1. appka by fungovat mela, ale nejspise budou chybet nejake napisy :P
            //stejne jedeme na dotnetu, ktery na tehlech verzich neni ale clovek nikdy nevi, co user vymysli...
            if (!IsWindows81OrNewer())
            {
                MessageBoxResult result = MessageBox.Show("Tato aplikace je optimalizována pro Windows 8.1 a novější verze operačního systému. Některé funkce nemusí na starších verzích Windows fungovat správně.\n\nChcete pokračovat?", "Upozornění", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Cancel)
                {
                  this.Shutdown();
                }
            }
        }

        private bool IsWindows81OrNewer()
        {
            Version winVersion = Environment.OSVersion.Version;
            return (winVersion >= new Version(6, 3));
        }
    }

}
