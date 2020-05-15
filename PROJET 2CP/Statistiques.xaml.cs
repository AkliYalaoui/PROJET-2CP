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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MaterialDesignThemes.Wpf;
using PROJET_2CP.Noyau;

namespace PROJET_2CP
{
    /// <summary>
    /// Interaction logic for Statistiques.xaml
    /// </summary>
    public partial class Statistiques : Page
    {
        private int _nbRepTrue;
        private int _nbRepFalse;
        private int _nbReponses;
        private string[] lesson;
        private int[] moyenne_lesson;
        private List<string> tmp;
        private int _NbTests;

        private DataTable _StatesTableNote;
        private DataTable _StatesTableReponse;
        public Statistiques()
        {
            InitializeComponent();
            langue();
        }

        private void langue()
        {
            if(MainWindow.langue == 0)
            {
                Stateslbl.Content = "Statistiques";
                niveau1.Header = "Niveau 1";
                niv1thm1.Header = "Signalisation";
                niv1thm2.Header = "intersection et priorité";
                niveau2.Header = "Niveau 2";
                niveau3.Header = "Niveau 3";
                moyenneLbl.Content = "Moyenne";
            }
            else
            {
                Stateslbl.Content = "احصائيات";
                niveau1.Header = "المستوى 1";
                niv1thm1.Header = "الاشارات";
                niv1thm2.Header = "التقاطعات و الأولوية";
                niveau2.Header = "المستوى 2";
                niveau3.Header = "المستوى 3";
                moyenneLbl.Content = "المعدلات";
            }
        }
        private void creeStates()
        {
            string connectionStringtoSaveDB = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            SqlConnection saveConn = new SqlConnection(connectionStringtoSaveDB);
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable temp;

            string querySelectNoteTestsNiv;
            string querySelectReponseNiv;
            string querySelectReponseTrueNiv;
            string querySelectReponseFalseNiv;

            int niveauSelected = 0;

            if (niveau1.IsSelected)
                niveauSelected = 1;
            else if (niveau2.IsSelected)
                niveauSelected = 2;
            else if (niveau3.IsSelected)
                niveauSelected = 3;

            if (niveauSelected != 0)
                {
                    querySelectNoteTestsNiv = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "NoteTest WHERE ( Niveau = '" + niveauSelected.ToString() + "' )";
                    querySelectReponseNiv = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "Trace WHERE ( Niveau = '" + niveauSelected.ToString() + "' )";

                    querySelectReponseTrueNiv = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "Trace WHERE ( Niveau = '" + niveauSelected.ToString() + "' AND Reponse = 'True' )";
                    querySelectReponseFalseNiv = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "Trace WHERE ( Niveau = '" + niveauSelected.ToString() + "' AND Reponse = 'False' )";


                    try
                    {
                        if (saveConn.State == ConnectionState.Closed)
                            saveConn.Open();

                        cmd = new SqlCommand(querySelectNoteTestsNiv, saveConn);
                        adapter = new SqlDataAdapter(cmd);
                        _StatesTableNote = new DataTable();
                        adapter.Fill(_StatesTableNote);
                        adapter.Dispose();

                        //sauvegarde nb des tests de niveau
                        _NbTests = _StatesTableNote.Rows.Count;

                        // *** save reponses ***

                        cmd = new SqlCommand(querySelectReponseNiv, saveConn);
                        adapter = new SqlDataAdapter(cmd);
                        _StatesTableReponse = new DataTable();
                        adapter.Fill(_StatesTableReponse);
                        adapter.Dispose();

                        //sauvegarde nb reponses 'Totale'
                        _nbReponses = _StatesTableReponse.Rows.Count;

                        //Récuperation de nb rep fausse
                        temp = new DataTable();
                        cmd = new SqlCommand(querySelectReponseFalseNiv, saveConn);
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(temp);
                        adapter.Dispose();

                        //sauvegarde nb reponses 'fausse'
                        _nbRepFalse = temp.Rows.Count;

                        //Récupération nb rep vrais
                        temp = new DataTable();
                        cmd = new SqlCommand(querySelectReponseTrueNiv, saveConn);
                        adapter = new SqlDataAdapter(cmd);

                        adapter.Fill(temp);
                        adapter.Dispose();

                        //sauvegarde nb reponses 'Vrais'
                        _nbRepTrue = temp.Rows.Count;

                        if (saveConn.State == ConnectionState.Open)
                            saveConn.Close();
                    }
                    catch (Exception)
                    {
                        if (saveConn.State == ConnectionState.Open)
                            saveConn.Close();
                        MessageBox.Show("Error states");
                    }
                }
        }

        private void stateSelect_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            choixChart.Visibility = Visibility.Visible;
            pieCheck.IsChecked = false;
            diagCheck.IsChecked = false;
            moyenneCheck.IsChecked = false;
            creeStates();

            if (_nbReponses == 0 && (niveau1.IsSelected || niveau2.IsSelected || niveau3.IsSelected))
            {
                if (MainWindow.langue == 0)
                    MessageBox.Show(" vous n'avez pas fait des testes ! ");
                else
                    MessageBox.Show(" انت لم تقم بأي امتحان  ");
            }
            else
            {
                if (niveau1.IsSelected || niveau2.IsSelected || niveau3.IsSelected)
                {
                    choixChart.Visibility = Visibility.Visible;
                    if (MainWindow.langue == 0)
                    {
                        moyenneLbl.Content = "Moyenne : " + float.Parse(caluclerMoyenne().ToString()) +
                                             "\nnombre des tests :" + _NbTests.ToString() +
                                             "\nnombre des questions : " + _nbReponses.ToString();
                    }
                    else
                    {
                        moyenneLbl.Content = "المعدل : " + float.Parse(caluclerMoyenne().ToString()) +
                                             "\nعدد الامنحانات المجتازة :" + _NbTests.ToString() +
                                             "\nعدد الأسئلة : " + _nbReponses.ToString();
                    }

                    pieCheck.IsChecked = true;
                }
            }
        }

        private void pieCheck_Checked(object sender, RoutedEventArgs e)
        {

            // affichage un Piechart (cercle avec des pourcentage) 
            //avec live Charts
            string falseAnswers;
            string trueAnswers;
            
            if(MainWindow.langue == 0)
            {
                falseAnswers = "Nombre des réponses fausses";
                trueAnswers = "Nombre des réponses vrais";
            }
            else
            {
                falseAnswers = "عدد الأجوبة الخاطئة";
                trueAnswers = "عدد الأجوبة الصحيحة";
            }

            chartsGrid.Children.Clear();

            PieChart pieStates = new PieChart();
            SeriesCollection PieSeriesCollection;

            PieSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Values = new ChartValues<int> {_nbRepFalse},
                        Title =falseAnswers ,
                        DataLabels = true,
                        Stroke = Brushes.Red,
                        Fill = Brushes.Red
                    },

                    new PieSeries
                    {
                        Values = new ChartValues<int>{_nbRepTrue},
                        Title = trueAnswers,
                        DataLabels = true,
                        Stroke = Brushes.GreenYellow,
                        Fill = Brushes.GreenYellow
                    }
                };

            pieStates.Series = PieSeriesCollection;
            pieStates.DataContext = this;

            pieStates.Height = 400;
            pieStates.Width = 400;
            pieStates.VerticalAlignment = VerticalAlignment.Center;
            pieStates.HorizontalAlignment = HorizontalAlignment.Center;
            pieStates.LegendLocation = LegendLocation.Right;

            chartsGrid.Children.Add(pieStates);
        }

        private void diagCheck_Checked(object sender, RoutedEventArgs e)
        {

            // affichage un diagramme des column
            //avec live Charts
            string falseAnswers;
            string trueAnswers;

            if (MainWindow.langue == 0)
            {
                falseAnswers = "Nombre des réponses fausses";
                trueAnswers = "Nombre des réponses vrais";
            }
            else
            {
                falseAnswers = "عدد الأجوبة الخاطئة";
                trueAnswers = "عدد الأجوبة الصحيحة";
            }

            chartsGrid.Children.Clear();

            CartesianChart cartStates = new CartesianChart();
            SeriesCollection cartSeriesCollection;

            cartSeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Values = new ChartValues<int> {_nbRepFalse},
                        Title = falseAnswers,
                        DataLabels = true,
                        Stroke = Brushes.Red,
                        Fill = Brushes.Red
                    },

                    new ColumnSeries
                    {
                        Values = new ChartValues<int>{_nbRepTrue},
                        Title = trueAnswers,
                        DataLabels = true,
                        Stroke = Brushes.GreenYellow,
                        Fill = Brushes.GreenYellow
        }
                };

            cartStates.Series = cartSeriesCollection;
            cartStates.DataContext = this;

            cartStates.Height = 400;
            cartStates.Width = 500;
            cartStates.VerticalAlignment = VerticalAlignment.Center;
            cartStates.HorizontalAlignment = HorizontalAlignment.Center;
            cartStates.LegendLocation = LegendLocation.Right;

            Axis axisx = new Axis();
            axisx.Separator.Visibility = Visibility.Hidden;
            Axis axisy = new Axis();

            if (MainWindow.langue == 0)
            {
                axisy.Title = "Nombre des reponses";
                axisx.Title = "Reponse";
            }

            else
            {
                axisy.Title = "عدد الأسئلة";
                axisx.Title = "الاجوبة";
            }
            axisy.Separator.Step = 1;
            cartStates.AxisX.Add(axisx);
            cartStates.AxisY.Add(axisy);

            chartsGrid.Children.Add(cartStates);
        }

        //Une méthode pour calculer la moyenne des tests de l'apprenant
        private float caluclerMoyenne()
        {

            // si l'apprenant n'a pas fait des tests elle retourne 0 
            // sinon retourne la moyenne ==> la somme des notes div nb tests

            float moyenne = 0;

            if (_NbTests == 0)
            {
                return 0;
            }
            else
            {
                //Calcule du moyenne 
                for (int nbRows = 0; nbRows < _StatesTableNote.Rows.Count; nbRows++)
                {
                    moyenne = moyenne + float.Parse((_StatesTableNote.Rows[nbRows]["Note"].ToString()));
                }
                return (moyenne / _NbTests);
            }
        }

        private void moyenneCheck_Checked(object sender, RoutedEventArgs e)
        {
            chartsGrid.Children.Clear();

            Label moyenne = new Label();

            if (MainWindow.langue == 0)
                moyenne.Content = "nombre des tests :" + _NbTests.ToString() +
                                  "\nnombre des questions : " + _nbReponses.ToString() +
                                  "\nnombre des reponses vrais : " + _nbRepTrue.ToString() +
                                  "\nnombre des reponses fausses : " + _nbRepFalse.ToString() +
                                  "\nMoyenne des tests : " + float.Parse(caluclerMoyenne().ToString());
            else
                moyenne.Content = "عدد الامتحانات :" + _NbTests.ToString() +
                                              "\nعدد الأسئلة : " + _nbReponses.ToString() +
                                              "\nالاجابات الصحيحة : " + _nbRepTrue.ToString() +
                                              "\nالاجابات الخاطئة : " + _nbRepFalse.ToString() +
                                              "\n المعدل الامتحانات: " + float.Parse(caluclerMoyenne().ToString());


            moyenne.FontSize = 22;

            moyenne.VerticalAlignment = VerticalAlignment.Center;
            moyenne.HorizontalAlignment = HorizontalAlignment.Center;
            chartsGrid.Children.Add(moyenne);

        }

        private void niv1thm1Andniv2thm1_Selected(object sender, RoutedEventArgs e)
        {
            recuperer_QuestionReponse(1);
            creerstatLessonPerTheme();
          /* string connectionStringtoSaveDB = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
             SqlConnection saveConn = new SqlConnection(connectionStringtoSaveDB);
             SqlCommand cmd;
             SqlDataAdapter adapter;
             DataTable temp = new DataTable(); ;

             string querySelectTheme1Niv1;
             int niveauSelected = 1;

             if (niv1thm1.IsSelected)
                 niveauSelected = 1;
             if (niv2thm1.IsSelected)
                 niveauSelected = 2;

             querySelectTheme1Niv1 = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "Trace WHERE ( Niveau = '" + niveauSelected.ToString() + "' AND Test = '1' )";

             try
             {
                 if (saveConn.State == ConnectionState.Closed)
                     saveConn.Open();

                 cmd = new SqlCommand(querySelectTheme1Niv1, saveConn);
                 adapter = new SqlDataAdapter(cmd);
                 _StatesTableReponse = new DataTable();
                 adapter.Fill(temp);
                 adapter.Dispose();

                 if (saveConn.State == ConnectionState.Open)
                     saveConn.Close();
             }
             catch (Exception)
             {
                 if (saveConn.State == ConnectionState.Open)
                     saveConn.Close();
                 MessageBox.Show("Error states");
             }

             //Affichage des reponse de l'utilisateur
             chartsGrid.Children.Clear();
             StackPanel userReponses = new StackPanel();//pour toutes les questions
             Border qstreponse; // pour une seul question
             ScrollViewer scrollViewer = new ScrollViewer();
             scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
             scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
             for (int nbQuestion = 0; nbQuestion < temp.Rows.Count; nbQuestion++)
             {
                 qstreponse = new Border();
                 qstreponse = creatAnswerUser(Int32.Parse(temp.Rows[nbQuestion]["Code"].ToString()), temp.Rows[nbQuestion]["ReponseText"].ToString(), temp.Rows[nbQuestion]["ReponseTextAr"].ToString(), bool.Parse(temp.Rows[nbQuestion]["Reponse"].ToString()));
                 userReponses.Children.Add(qstreponse);
             }
             scrollViewer.Content = userReponses;
             chartsGrid.Children.Add(scrollViewer);
             choixChart.Visibility = Visibility.Collapsed;
     */   
        }

        private Border creatAnswerUser(int id , string reponse, string reponseAr, bool isItTrue) // id == Code
        {
            string connStringToPanneauxDB = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True";
            SqlConnection connectToPanneauxDB = new SqlConnection(connStringToPanneauxDB);

            string querySelect = "SELECT * FROM Quiz WHERE Id = '" + id.ToString() + "'";
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable questionContent = new DataTable();

            if (connectToPanneauxDB.State == ConnectionState.Closed)
                connectToPanneauxDB.Open();
            using (connectToPanneauxDB)
            {
                try
                {
                    cmd = new SqlCommand(querySelect,connectToPanneauxDB);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(questionContent);
                    adapter.Dispose();
                    if (connectToPanneauxDB.State == ConnectionState.Open)
                        connectToPanneauxDB.Close();
                }
                catch(Exception)
                {
                    if (connectToPanneauxDB.State == ConnectionState.Open)
                        connectToPanneauxDB.Close();
                }
            }

            Border borderForStackrep = new Border();
            borderForStackrep.CornerRadius = new CornerRadius(27);

            SolidColorBrush lightBlack = new SolidColorBrush();
            lightBlack.Color = Color.FromArgb(60,0,0,0);

            borderForStackrep.Background = lightBlack;

            StackPanel qstrep = new StackPanel();
            qstrep.Margin = new Thickness(3);

            TextBlock questiontxt = new TextBlock();
            questiontxt.Margin = new Thickness(10);
            questiontxt.VerticalAlignment = VerticalAlignment.Center;

            TextBlock reponsetxt = new TextBlock();
            reponsetxt.Margin = new Thickness(10);
            reponsetxt.VerticalAlignment = VerticalAlignment.Center;

            TextBlock lecon = new TextBlock();
            lecon.Margin = new Thickness(10);
            lecon.VerticalAlignment = VerticalAlignment.Center;

            Image imageQst = new Image();
            imageQst.Height = 60;
            imageQst.Width = 60;


            if (questionContent.Rows.Count == 1)
            {
                if(MainWindow.langue == 0)
                {
                    questiontxt.Text = "La question : " + questionContent.Rows[0]["questionFr"].ToString();
                    reponsetxt.Text = "Votre réponse : " + questionContent.Rows[0][reponse].ToString();
                    lecon.Text = "Voir : " + questionContent.Rows[0]["leçon"].ToString();
                }
                else
                {
                    questiontxt.Text = questionContent.Rows[0]["questionAr"].ToString() + "السؤال";
                    reponsetxt.Text = questionContent.Rows[0][reponseAr].ToString() + "جوابك ";
                    lecon.Text = "";
                }

                //Desgin
                qstrep.Orientation = Orientation.Horizontal;
                qstrep.Children.Add(questiontxt);

                if (Int32.Parse(questionContent.Rows[0]["hasImage"].ToString()) == 1)
                {
                    imageQst.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + questionContent.Rows[0]["idImage"].ToString() + "_off.png", UriKind.RelativeOrAbsolute));
                    qstrep.Children.Add(imageQst);
                }

                if(isItTrue == true)
                {
                    reponsetxt.Background = Brushes.GreenYellow;
                }
                else
                {
                    reponsetxt.Background = Brushes.LightSalmon;
                }

                qstrep.Children.Add(reponsetxt);

                StackPanel fullStack = new StackPanel();
                fullStack.Margin = new Thickness(5);
                fullStack.Children.Add(qstrep);
                lecon.HorizontalAlignment = HorizontalAlignment.Center;
                fullStack.Children.Add(lecon);

                borderForStackrep.Child = fullStack;
                borderForStackrep.Margin = new Thickness(10);
            }
            return borderForStackrep;
        }


        private void niv2thm234_Selected(object sender, RoutedEventArgs e)
        {
            string connectionStringtoSaveDB = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            SqlConnection saveConn = new SqlConnection(connectionStringtoSaveDB);
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable temp = new DataTable(); ;

            string querySelectTheme1Niv1;

            int niveauSelected = 2;
            int themeSelected = 0;

            if (niv2thm2.IsSelected)
            {
                niveauSelected = 2;
                themeSelected = 2;
            }
            if (niv2thm3.IsSelected)
            {
                niveauSelected = 2;
                themeSelected = 3;
            }
            if (niv2thm4.IsSelected)
            {
                niveauSelected = 2;
                themeSelected = 4;
            }

            if (niv3thm1.IsSelected)
            {
                niveauSelected = 3;
                themeSelected = 1;
            }
            if (niv3thm2.IsSelected)
            {
                niveauSelected = 3;
                themeSelected = 2;
            }
            if (niv3thm3.IsSelected)
            {
                niveauSelected = 3;
                themeSelected = 3;
            }

            querySelectTheme1Niv1 = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "Trace WHERE ( Niveau = '" + niveauSelected.ToString() + "' AND Test = '" + themeSelected.ToString() + "' )";

            try
            {
                if (saveConn.State == ConnectionState.Closed)
                    saveConn.Open();

                cmd = new SqlCommand(querySelectTheme1Niv1, saveConn);
                adapter = new SqlDataAdapter(cmd);
                _StatesTableReponse = new DataTable();
                adapter.Fill(temp);
                adapter.Dispose();

                if (saveConn.State == ConnectionState.Open)
                    saveConn.Close();
            }
            catch (Exception)
            {
                if (saveConn.State == ConnectionState.Open)
                    saveConn.Close();
                MessageBox.Show("Error states");
            }
            //Affichage des reponse de l'utilisateur
            chartsGrid.Children.Clear();
            StackPanel userReponses = new StackPanel();//pour toutes les questions
            Border qstreponse; // pour une seul question
            ScrollViewer scrollViewer = new ScrollViewer();
            for (int nbQuestion = 0; nbQuestion < temp.Rows.Count; nbQuestion++)
            {
                qstreponse = new Border();
                qstreponse = creatAnswerUserForLVL23(Int32.Parse(temp.Rows[nbQuestion]["Code"].ToString()), temp.Rows[nbQuestion]["ReponseText"].ToString(), temp.Rows[nbQuestion]["ReponseTextAr"].ToString(), bool.Parse(temp.Rows[nbQuestion]["Reponse"].ToString()));
                userReponses.Children.Add(qstreponse);
            }
            scrollViewer.Content = userReponses;
            chartsGrid.Children.Add(scrollViewer);
            choixChart.Visibility = Visibility.Collapsed;
        }

        private Border creatAnswerUserForLVL23(int id, string reponse, string reponseAr, bool isItTrue) // id == Code
        {
            string connStringToPanneauxDB = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True";
            SqlConnection connectToPanneauxDB = new SqlConnection(connStringToPanneauxDB);

            string querySelect = "SELECT * FROM Question WHERE Id = '" + id.ToString() + "'";
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable questionContent = new DataTable();

            if (connectToPanneauxDB.State == ConnectionState.Closed)
                connectToPanneauxDB.Open();
            using (connectToPanneauxDB)
            {
                try
                {
                    cmd = new SqlCommand(querySelect, connectToPanneauxDB);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(questionContent);
                    adapter.Dispose();
                    if (connectToPanneauxDB.State == ConnectionState.Open)
                        connectToPanneauxDB.Close();
                }
                catch (Exception)
                {
                    if (connectToPanneauxDB.State == ConnectionState.Open)
                        connectToPanneauxDB.Close();
                }
            }

            Border borderForStackrep = new Border();
            borderForStackrep.CornerRadius = new CornerRadius(27);

            SolidColorBrush lightBlack = new SolidColorBrush();
            lightBlack.Color = Color.FromArgb(50, 0, 0, 0);

            borderForStackrep.Background = lightBlack;

            StackPanel qstrep = new StackPanel();
            qstrep.Margin = new Thickness(3);

            TextBlock questiontxt = new TextBlock();
            questiontxt.Margin = new Thickness(10);
            questiontxt.VerticalAlignment = VerticalAlignment.Center;

            TextBlock reponsetxt = new TextBlock();
            reponsetxt.Margin = new Thickness(10);
            reponsetxt.VerticalAlignment = VerticalAlignment.Center;

            TextBlock lecon = new TextBlock();
            lecon.Margin = new Thickness(10);
            lecon.VerticalAlignment = VerticalAlignment.Center;

            Image imageQst = new Image();
            imageQst.Height = 60;
            imageQst.Width = 60;


            if (questionContent.Rows.Count == 1)
            {
                if (MainWindow.langue == 0)
                {
                    questiontxt.Text = "La question : " + questionContent.Rows[0]["contenu"].ToString();
                    reponsetxt.Text = "Votre réponse : " + questionContent.Rows[0][reponse].ToString();
                    lecon.Text = "Voir Le cour : " + questionContent.Rows[0]["leson"].ToString();
                }
                else
                {
                    questiontxt.Text = questionContent.Rows[0]["contenuar"].ToString() + "السؤال";
                    reponsetxt.Text = questionContent.Rows[0][reponseAr].ToString() + "جوابك ";
                    lecon.Text = "";
                }

                if (isItTrue == true)
                {
                    reponsetxt.Background = Brushes.GreenYellow;
                }
                else
                {
                    reponsetxt.Background = Brushes.LightSalmon;
                }

                //Desgin
                qstrep.Orientation = Orientation.Horizontal;
                qstrep.Children.Add(questiontxt);

                qstrep.Children.Add(reponsetxt);

                qstrep.Children.Add(lecon);

                borderForStackrep.Child = qstrep;
                borderForStackrep.Margin = new Thickness(10);
            }
            return borderForStackrep;
        }
        void recuperer_QuestionReponse(int niveau)
        {
            string connectionStringtoSaveDB = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            SqlConnection saveConn = new SqlConnection(connectionStringtoSaveDB);
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable temp = new DataTable(); ;

            string querySelectTheme1Niv1;

            querySelectTheme1Niv1 = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "Trace WHERE ( Niveau = '" + niveau.ToString() + "' AND Test = '1' )";

            try
            {
                if (saveConn.State == ConnectionState.Closed)
                    saveConn.Open();

                cmd = new SqlCommand(querySelectTheme1Niv1, saveConn);
                adapter = new SqlDataAdapter(cmd);
                _StatesTableReponse = new DataTable();
                adapter.Fill(temp);
                adapter.Dispose();

                if (saveConn.State == ConnectionState.Open)
                    saveConn.Close();
            }
            catch (Exception)
            {
                if (saveConn.State == ConnectionState.Open)
                    saveConn.Close();
                MessageBox.Show("Error states");
            }
            tmp = new List<string>(temp.Rows.Count);
            List<statLesson> duplicated = new List<statLesson>(temp.Rows.Count);
            string laleçon;
            for (int nbQuestion = 0; nbQuestion < temp.Rows.Count; nbQuestion++)
            {
                laleçon = getlesson(Int32.Parse(temp.Rows[nbQuestion]["Code"].ToString()));
                duplicated.Add(new statLesson(laleçon, bool.Parse(temp.Rows[nbQuestion]["Reponse"].ToString())));


            }
            lesson = deleteDuplicateElements(tmp);
            ClaclulerMoyenneDeChaqueLesson(duplicated);
        }
        private void ClaclulerMoyenneDeChaqueLesson(List<statLesson> L)
        {
            int moyenne;
            for (int i = 0; i < lesson.Length; i++)
            {
                moyenne = 0;
                for (int j = 0; j < L.Count; j++)
                {
                    if (L[j].Lesson == lesson[i])
                    {
                        if (L[j].IsGoodAnswer)
                        {
                            moyenne++;
                        }
                    }
                }
                moyenne_lesson[i] = moyenne;
            }
        }
        private string[] deleteDuplicateElements(List<string> table)
        {
            int size = 0;
            List<string> tmp = new List<string>();
            for (int i = 0; i < table.Count; i++)
            {
                if(!tmp.Contains(table[i]))
                {
                    tmp.Add(table[i]);
                    size++;
                }
               // table.RemoveAll(item => item == table[i]);
            }
            string[] _newTable = new string[tmp.Count];
            moyenne_lesson = new int[tmp.Count];
            lesson = new string[tmp.Count];
            for (int j = 0; j < tmp.Count; j++)
            {
                _newTable[j] = tmp[j];
            }
            return _newTable;
        }

        private string getlesson(int id)
        {
            string lesson;
            string connStringToPanneauxDB = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True";
            SqlConnection connectToPanneauxDB = new SqlConnection(connStringToPanneauxDB);

            string querySelect = "SELECT * FROM Quiz WHERE Id = '" + id.ToString() + "'";
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable questionContent = new DataTable();

            if (connectToPanneauxDB.State == ConnectionState.Closed)
                connectToPanneauxDB.Open();
            using (connectToPanneauxDB)
            {
                try
                {
                    cmd = new SqlCommand(querySelect, connectToPanneauxDB);
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(questionContent);
                    adapter.Dispose();
                    if (connectToPanneauxDB.State == ConnectionState.Open)
                        connectToPanneauxDB.Close();
                }
                catch (Exception)
                {
                    if (connectToPanneauxDB.State == ConnectionState.Open)
                        connectToPanneauxDB.Close();
                }
            }
            if (MainWindow.langue == 0)
            {
                lesson = questionContent.Rows[0]["leçon"].ToString();
                tmp.Add(lesson);
            }
            else
            {
                lesson = questionContent.Rows[0]["leçonAr"].ToString();
                tmp.Add(lesson);
            }
            return lesson;
        }

        private void creerstatLessonPerTheme()
        {

            // affichage un diagramme des column
            //avec live Charts
            chartsGrid.Children.Clear();

            CartesianChart cartStates = new CartesianChart();
            SeriesCollection cartSeriesCollection;
            cartSeriesCollection = new SeriesCollection();
            ColumnSeries b;
            for (int i = 0; i < lesson.Length; i++)
            {
                b = new ColumnSeries();
                b.Values = new ChartValues<int> { moyenne_lesson[i] };
                b.Title = lesson[i];
                b.DataLabels = true;
                b.Stroke = Brushes.GreenYellow;
                b.Fill = Brushes.GreenYellow;
                cartSeriesCollection.Add(b);
            }

            cartStates.Series = cartSeriesCollection;
            cartStates.DataContext = this;

            cartStates.Height = 400;
            cartStates.Width = 500;
            cartStates.VerticalAlignment = VerticalAlignment.Center;
            cartStates.HorizontalAlignment = HorizontalAlignment.Center;
            cartStates.LegendLocation = LegendLocation.Right;

            Axis axisx = new Axis();
            axisx.Separator.Visibility = Visibility.Hidden;
            Axis axisy = new Axis();

            if (MainWindow.langue == 0)
            {
                axisy.Title = "moyenne du Quiz dans une leçon";
                axisx.Title = "Leçons";
            }

            else
            {
                axisy.Title = "عدد الأسئلة";
                axisx.Title = "الاجوبة";
            }
            axisy.Separator.Step = 1;
            cartStates.AxisX.Add(axisx);
            cartStates.AxisY.Add(axisy);

            chartsGrid.Children.Add(cartStates);
        }
    }
}