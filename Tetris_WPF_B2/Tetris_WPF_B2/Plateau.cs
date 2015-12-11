using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Drawing;

namespace Tetris_WPF_B2
{
    class Plateau
    {
        // Declaration et initialisation variables
        private int Lignes, colonnes, score, lignesCompletes;
        private Forme piece;
        private Label[,] gestionQuadrillage;
        private Brush NoBrush = Brushes.Transparent;
        private Brush SilverBrush = Brushes.Gray;


        #region Constructeur
        public Plateau(Grid grilleGame)
        {
            Lignes = grilleGame.RowDefinitions.Count;
            colonnes = grilleGame.ColumnDefinitions.Count;
            score = 0;
            lignesCompletes = 0;

            gestionQuadrillage = new Label[colonnes, Lignes];
            for (int i = 0; i < colonnes; i++)
            {
                for (int j = 0; j < Lignes; j++)
                {
                    gestionQuadrillage[i, j] = new Label();
                    gestionQuadrillage[i, j].Background = NoBrush;
                    gestionQuadrillage[i, j].BorderBrush = SilverBrush;
                    gestionQuadrillage[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetColumn(gestionQuadrillage[i, j], i);
                    Grid.SetRow(gestionQuadrillage[i, j], j);
                    grilleGame.Children.Add(gestionQuadrillage[i, j]);
                }
            }
            piece = new Forme();
            NouvellePiece();

        }
        #endregion

        #region GetNecessaires
        public int getScore() { return score; }

        public int getLignes() { return lignesCompletes; }
        #endregion

        #region Ajout/SupprPiece
        private void NouvellePiece()
        {
            Point Position = piece.getPositionForme();
            Point[] Piece = piece.getForme();
            Brush Color = piece.getCouleurForme();
            foreach (Point S in Piece)
            {
                gestionQuadrillage[(int)(S.X + Position.X) + ((colonnes / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = Color;
            }
        }

        private void SupprimerPiece()
        {
            Point Position = piece.getPositionForme();
            Point[] Piece = piece.getForme();

            foreach (Point S in Piece)
            {
                gestionQuadrillage[(int)(S.X + Position.X) + ((colonnes / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = NoBrush;
            }
        }
        #endregion

        #region DeplacementPieces
        public void DeplacementGauchePiece()
        {
            Point Position = piece.getPositionForme();
            Point[] Piece = piece.getForme();
            bool move = true;
            SupprimerPiece();
            foreach (Point S in Piece)
            {
                if (((int)(S.X + Position.X) + ((colonnes / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                else if (gestionQuadrillage[((int)(S.X + Position.X) + ((colonnes / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                piece.deplacementGauche();
                NouvellePiece();
            }
            else
            {
                NouvellePiece();
            }
        }
        public void DeplacementDroitPiece()
        {
            Point Position = piece.getPositionForme();
            Point[] Piece = piece.getForme();
            bool move = true;
            SupprimerPiece();
            foreach (Point S in Piece)
            {
                if (((int)(S.X + Position.X) + ((colonnes / 2) - 1) + 1) >= colonnes)
                {
                    move = false;
                }
                else if (gestionQuadrillage[((int)(S.X + Position.X) + ((colonnes / 2) - 1) + 1),
                    (int)(S.Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                piece.deplacementDroit();
                NouvellePiece();
            }
            else
            {
                NouvellePiece();
            }
        }
        public void DeplacementBasPiece()
        {
            Point Position = piece.getPositionForme();
            Point[] Piece = piece.getForme();
            bool move = true;
            SupprimerPiece();
            foreach (Point S in Piece)
            {
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Lignes)
                {
                    move = false;
                }
                else if (gestionQuadrillage[((int)(S.X + Position.X) + ((colonnes / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2 + 1].Background != NoBrush)
                {
                    move = false;
                }
            }
            if (move)
            {
                piece.deplacementBas();
                NouvellePiece();
            }
            else
            {
                NouvellePiece();
                LigneRemplie();
                piece = new Forme();
            }
        }
        #endregion

        #region RotationPieces
        public void RotationPiece()
        {
            Point Position = piece.getPositionForme();
            Point[] S = new Point[4];
            Point[] Piece = piece.getForme();
            bool move = true;
            Piece.CopyTo(S, 0);
            SupprimerPiece();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;

                if (((int)((S[i].Y + Position.Y) + 2)) >= Lignes)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((colonnes / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((colonnes / 2) - 1)) >= Lignes)
                {
                    move = false;
                }
                else if (gestionQuadrillage[((int)(S[i].X + Position.X) + ((colonnes / 2) - 1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != NoBrush)
                {
                    move = false;
                }
            }

            if (move)
            {
                piece.rotationPiece();
                NouvellePiece();
            }
            else
            {
                NouvellePiece();
            }
        }
        #endregion

        #region GestionLignes
        private void LigneRemplie()
        {
            bool complet;
            for (int i = Lignes - 1; i > 0; i--)
            {
                complet = true;
                for (int j = 0; j < colonnes; j++)
                {
                    if (gestionQuadrillage[j, i].Background == NoBrush)
                    {
                        complet = false;
                    }
                }
                if (complet)
                {
                    SupprimerLigne(i);
                    score += 100;
                    lignesCompletes += 1;
                }
            }
        }


        private void SupprimerLigne(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    gestionQuadrillage[j, i].Background = gestionQuadrillage[j, i - 1].Background;
                }
            }
        }
        #endregion

    }
}
