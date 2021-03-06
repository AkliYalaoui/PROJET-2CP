﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PROJET_2CP.Niveau1;
using PROJET_2CP.Noyau;

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour Leçons.xaml
    /// </summary>
    public partial class Leçons : Page
    {
        public static int langue { get; set; } = PROJET_2CP.MainWindow.langue;
        public Leçons()
        {
            NLeçon lesson = new NLeçon();
            InitializeComponent();
            quiz1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz4.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz6.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz7.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz8.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            lecon1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/1_off.png", UriKind.RelativeOrAbsolute));
            lecon2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/40_off.png", UriKind.RelativeOrAbsolute));
            lecon3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/theme1quiz14.png", UriKind.RelativeOrAbsolute));

            initialiserLangue();
            configurerLaLangue();
            guestMode();
        }
        private void configurerLaLangue()
        {

            if (langue == 0)
            {
                
            }
            if (langue == 1)
            {
               
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Home.mainFrame.Content = new signalisation_gen();

            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            PanneauxInterdiction.panneau = 0;
            PanneauxObligation.panneau = 0;
            Home.mainFrame.Content = new Panneaux();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Panneaux.panneau = 0;
            PanneauxObligation.panneau = 0;
            Home.mainFrame.Content = new PanneauxInterdiction();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Panneaux.panneau = 0;
            PanneauxInterdiction.panneau = 0;
            Home.mainFrame.Content = new PanneauxObligation();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Home.mainFrame.Content = new Quiz(1, 7);
            Quiz._lastPageQuiz = 0;
            Home.mainFrame.Content = new Quiz(7, 13);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Home.mainFrame.Content = new Quiz(7, 13);
            Quiz._lastPageQuiz = 0;
            Home.mainFrame.Content = new Quiz(13, 19);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            // Home.mainFrame.Content = new Quiz(13, 19);
            Quiz._lastPageQuiz = 0;
            Home.mainFrame.Content = new Quiz(1, 7);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Quiz._lastPageQuiz = 0;
            Home.mainFrame.Content = new Quiz(19, 25);
        }
        private void switch_lang_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            if (langue == 0 && !changed)
            {
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                b0.Content = "قواعد عامة";
                switch_lang.Margin = new Thickness(692, 82, 26, 558);
                langue = 1;
                MainWindow.langue = 1;
                changed = true;
            }
            if (langue == 1 && !changed)
            {
                MainWindow.langue = 0;
                b0.Content = "Règle générale";
                switch_lang.Content = "changer la langue en arabe";
                switch_lang.Margin = new Thickness(26, 82, 692, 558);
                langue = 0;
                changed = true;
            }
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveau1.Niv1Main();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                h0.Content = "Niveau 1 : themes";
                singtxt.Text = "Signalisation";
                intertxt.Text = "Intersection et priorité";
                back.Text = "Retour";
                quiz_1.Content = quiz_2.Content = quiz_3.Content = "Questions";
                b0.Content = "règles générales";
                switch_lang.Content = "changer la langue en arabe";
                switch_lang.Margin = new Thickness(26, 82, 692, 558);
                qst0.Text = "Questions";
                qst1.Text = "Questions";
                qst2.Text = "Questions";
                qst3.Text = "Questions";
                h0.Content = "NIVEAU I : Thèmes";

                thm1.Content = "Désignation";
                thm2.Content = "Désignation";
                designationTitle.Content = "Désignation";
            }
            else
            {
                //la langue arabe
                h0.Content = "المستوى 1 : المواضيع";
                singtxt.Text = "الاشارات";
                intertxt.Text = "التقاطعات والأولوية";
                back.Text = "عودة";
                regle.Content = "قواعد الأولوية";
                intersection.Content = "التقاطعات";
                depassement.Content = "التجاوز والعبور";
                quiz_1.Content = quiz_2.Content = quiz_3.Content = "أسئلة";
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                switch_lang.Margin = new Thickness(692, 82, 26, 558);
                b0.Content = "قواعد عامة";
                qst0.Text = "أسئلة";
                qst1.Text = "أسئلة";
                qst2.Text = "أسئلة";
                qst3.Text = "أسئلة";

                h0.Content = "المستوى الأول: مواضيع";
                thm1.Content = "حول الدرس";
                thm2.Content = "حول الدرس";
                lbl2.Content = "الدروس";
                lbl2.HorizontalAlignment = HorizontalAlignment.Right;
                b1.ToolTip = "لافتات الخطر";
                b2.ToolTip = "لافتات المنع";
                b3.ToolTip = "لافتات الالزام";
                designationTitle.Content = "حول الدرس";
            }
        }
        /*<<
            Decrease advantages of the guest user
        >>*/
        private void guestMode()
        {
            if (LogIN.LoggedUser.ID == 0 && LogIN.LoggedUser.UtilisateurID.Equals("Guest"))
            {
                themeinterspriot.IsEnabled = false;
                b2.IsEnabled = false;
                b3.IsEnabled = false;
                q0.IsEnabled = false;
                q1.IsEnabled = false;
                q2.IsEnabled = false;
                q3.IsEnabled = false;

                if (MainWindow.langue == 0)
                {   //la langue français
                }
                else
                {
                    //la langue arabe
                }
            }
        }

        private void quitdesignationClick(object sender, RoutedEventArgs e)
        {
            designationGrid.Visibility = Visibility.Collapsed;
        }
        private void Intersection_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new intersections();
        }

        private void Depassement_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new depassement();
        }
        private void Pdf_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new pdf();
        }
        private void quiz_intersection(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new test(130, 139);
        }
        private void quiz_depassement(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new test(75, 84);
        }

        private void thm1_Click(object sender, RoutedEventArgs e)
        {
            designationGrid.Visibility = Visibility.Visible;
            if (MainWindow.langue == 0)
            {
                designatiotxt.TextAlignment = TextAlignment.Left;
                designatiotxt.Text = "       La circulation routière est la thématique la plus dense du code de la route\n" +
                                     "       La signalisation routière : divisé en :\n\n" +
                                     "            *les règles générales de la signalisation routière (verticale et horizontale)\n" +
                                     "            *les panneaux d’interdictions, d’obligations et de dangers";
            }
            else
            {
                designatiotxt.TextAlignment = TextAlignment.Right;
                designatiotxt.Text = "      حركة المرور على الطرق تمثل الموضوع الأكثر أهمية في قانون المرور والذي يشمل:\n" +
                                     "      إشارات الطريق: مقسمة على\n\n" +
                                     "            *يجمع القواعد العامة لإشارات الطرق )العمودية والأفقية(.\n" +
                                     "            *يشمل إشارات المنع، إشارات الإجبار وإشارات الخطر.";

            }
        }

        private void thm2_Click(object sender, RoutedEventArgs e)
        {
            designationGrid.Visibility = Visibility.Visible;
            if (MainWindow.langue == 0)
            {
                designatiotxt.TextAlignment = TextAlignment.Left;
                designatiotxt.Text = "      Les règles de priorité.\n\n" +
                                     "           *Les placements de conduite (les vois de stockage et d’insertion, les changements\n" +
                                     "            de direction, les sens uniques et les sens giratoires).\n" +
                                     "           *Autres règles de circulation (Les intersections, le stationnement et l’arrêt, \n" +
                                     "            les croisements et dépassements, la vitesse).";
            }
            else
            {
                designatiotxt.TextAlignment = TextAlignment.Right;
                designatiotxt.Text = "      قواعد الأولوية\n\n" +
                                     "    *مواضع القيادة )مسارات التخزين والإدخال، تغييرات الاتجاه،\n" +
                                     "      الطرق ذات الاتجاه الواحد والتقاطع الدائري(.\n" +
                                     "    *قواعد المرور الأخرى )التقاطعات، الوقوف والتوقف، العبور والتجاوز،\n" +
                                     "     السرعة(.";

            }
        }

        private void quiz_regle(object sender, RoutedEventArgs e)
        {
            // Niveau2.Quiz.niv = 1;

            // Home.mainFrame.Content = new Niveau2.Quiz(85, 91);
            Home.mainFrame.Content = new test_reg(85, 91);
        }
    }
}
