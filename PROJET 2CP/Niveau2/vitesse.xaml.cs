﻿using System;
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

namespace PROJET_2CP.Niveau2
{
    /// <summary>
    /// Logique d'interaction pour vitesse.xaml
    /// </summary>
    public partial class vitesse : Page
    {
        public vitesse()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/vit1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/vit2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/vit3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/vit4.png", UriKind.Relative));
                img4.Source = btm;

            }
            if (MainWindow.langue == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/vitAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/vitAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/vitAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/vitAR4.png", UriKind.Relative));
                img4.Source = btm;
            }
        }
    }
}