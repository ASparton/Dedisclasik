using System;
using System.Windows.Forms;

namespace PT2_Team1._1_Dédisclasik
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Lancement de l'application sur la page d'acceuil
            HomePageView homePageView = new HomePageView();
            homePageView.ShowDialog();

            if (homePageView.LoginResult.Key != -1)
            {
                // Quand elle se ferme, si l'administrateur s'est connecté on affiche son interface
                if (homePageView.LoginResult.Value)
                {
                    Application.Run(new AdminView());
                }
                // Sinon on affiche l'interface de l'abonné
                else
                {
                    Application.Run(new SubscriberView(homePageView.LoginResult.Key));
                }
            }
        }
    }
}
