using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris_WPF_B2
{
    class Forme
    {  
        //Initialisation Variables Necessaires
       private Brush couleurForme;
       private Point positionCourante;
       private Point[] formeCourante;       
       private bool peutTourner;

       //Constructeur
       public Forme()
       {
           positionCourante = new Point(0, 0);
           couleurForme = Brushes.Transparent;
           formeCourante = choisirPieceRandom();
       }
        
       #region EnsembleGet
       public Brush getCouleurForme(){ return couleurForme; }
       
       public Point getPositionForme(){ return positionCourante; }

       public Point[] getForme(){ return formeCourante; }
       #endregion

       #region GenerationPieceAleatoire
       private Point[] choisirPieceRandom()
        {
            Random random = new Random();
            int formeChoisie = random.Next() % 7;
            switch (formeChoisie)
            {
                case 0: //Forme : O
                    peutTourner = true;
                    couleurForme = Brushes.Green;
                    return new Point[]{
                        new Point(0, -2),
                        new Point(0, -1),
                        new Point(1, -2),
                        new Point(1, -1)
                    };
                case 1: //Forme : I
                    peutTourner = true;
                    couleurForme = Brushes.Cyan;
                    return new Point[]{
                        new Point(0, -2),
                        new Point(-1, -2),
                        new Point(1, -2),
                        new Point(2, -2)
                    };
                case 2: //Forme : T
                    peutTourner = true;
                    couleurForme = Brushes.Yellow;
                    return new Point[]{
                        new Point(0, -2),
                        new Point(1, -2),
                        new Point(2, -2),
                        new Point(1, -1)
                    };
                case 3: //Forme : L
                    peutTourner = true;
                    couleurForme = Brushes.Orange;
                    return new Point[]{
                        new Point(0, -2),
                        new Point(-1, -2),
                        new Point(1, -2),
                        new Point(1, -1)
                    };
                case 4: //Forme : J
                    peutTourner = true;
                    couleurForme = Brushes.Blue;
                    return new Point[]{
                        new Point(1, -2),
                        new Point(-1, -1),
                        new Point(0, -1),
                        new Point(1, -1)
                    };
                case 5: //Forme : S
                    peutTourner = true;
                    couleurForme = Brushes.Purple;
                    return new Point[]{
                        new Point(0, -1),
                        new Point(-1, -1),
                        new Point(0, -2),
                        new Point(1, -2)
                    };
                case 6: //Forme : Z
                    peutTourner = true;
                    couleurForme = Brushes.Red;
                    return new Point[]{
                        new Point(0, -2),
                        new Point(-1, -2),
                        new Point(0, -1),
                        new Point(1, -1)
                    };
                default:
                    return null;
            }
        }
       #endregion

       #region DeplacementsPiece
       public void deplacementGauche()
       {
           positionCourante.X -= 1;
       }
       public void deplacementDroit()
       {
           positionCourante.X += 1;
       }
       public void deplacementBas()
       {
           positionCourante.Y += 1;
       }
       #endregion

       #region RotationPiece
       public void rotationPiece()
       {
           if (peutTourner)
           {
               for (int i = 0; i < formeCourante.Length; i++) {
                   double x = formeCourante[i].X;
                   formeCourante[i].X = formeCourante[i].Y * -1;
                   formeCourante[i].Y = x;
               }
           }
       }
       #endregion


    }
}
