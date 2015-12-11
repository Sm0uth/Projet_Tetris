using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris_WPF_B2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Déclaration timer et grille de jeu
        DispatcherTimer Timer;
        Plateau maGrille;


        //Initialisation du jeu
        void MainWindow_Initialize(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(InitPlateauJeu);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            DebutJeu();
        }

        #region CorpsDuJeu
        private void DebutJeu()
        {
            MainGrid.Children.Clear();
            maGrille = new Plateau(MainGrid);
            Timer.Start();
        // PlaySound("pack://application:,,,/Media/movepoint.wav");
        }

        

        void InitPlateauJeu (object sender, EventArgs e)
        {
            Score.Content = maGrille.getScore().ToString("000000000");
            Lignes.Content = maGrille.getLignes().ToString("0000");
            maGrille.DeplacementBasPiece();
        }

        private void PauseJeu()
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }
        
        // private void PlaySound(string uriPath) -- Essai Non-concluant d'ajout de musique
        //{
        //    Uri uri = new Uri(@uriPath);
        //    var player = new MediaPlayer();
        //    player.Open(uri);
        //    player.Play();
        //}

        #region Gestion des touches
        private void HandleKeyDown(object sender, KeyEventArgs touche)
        {
            switch (touche.Key)
            {
                case Key.Left:
                    if (Timer.IsEnabled) maGrille.DeplacementGauchePiece();
                    break;
                case Key.Right:
                    if (Timer.IsEnabled) maGrille.DeplacementDroitPiece();
                    break;
                case Key.Down:
                    if (Timer.IsEnabled) maGrille.DeplacementBasPiece();
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) maGrille.RotationPiece();
                    break;
                case Key.Escape:
                    DebutJeu();
                    break;
                case Key.Space:
                    PauseJeu();
                    break;
                default:
                    break;
            }

        #endregion

        #endregion

        }
    }
}
