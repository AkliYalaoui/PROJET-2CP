﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using PROJET_2CP.Niveau2;
using PROJET_2CP.Niveau1;
using PROJET_2CP.Niveau3;

namespace PROJET_2CP.Pages
{
    /// <summary>


    public partial class Tests3 : Page
    {


        // lire cette donnée à partir de la base de donnée de l'utilisateur
        public static int testActuel { get; set; } = 1;
        public static int[] testDejaPasse = new int[10];
        public static int _testChoisi { get; set; }
        public Tests3()
        {
            InitializeComponent();
            logoimg.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/EDautoEcole.png", UriKind.RelativeOrAbsolute));
            b1.Tag = 1;
            b2.Tag = 2;
            b3.Tag = 3;
            b4.Tag = 4;
            b5.Tag = 5;
            b6.Tag = 6;
            langue();
            for (int i = 0; i < testActuel - 1; i++)
            {
                testDejaPasse[i] = i + 1;
            }
            getLastTest();


        }
        private void langue()
        {
            if(MainWindow.langue == 0)
            {
                back.Text = "Retour";
                b1.Content = "Test 1";
                b2.Content = "Test 2";
                b3.Content = "Test 3";
                b4.Content = "Test 4";
                b5.Content = "Test 5";
                b6.Content = "Test 6";
                labelTest.Content = "Test Niveau 3";
            }
            else
            {
                b1.Content = "اختبار 1";
                b2.Content = "اختبار 2";
                b3.Content = "اختبار 3";
                b4.Content = "اختبار 4";
                b5.Content = "اختبار 5";
                b6.Content = "اختبار 6";
                labelTest.Content = "امتحان المستوى 3";
                back.Text = "عودة";
            }
        }
        static int[] reorder(int a)
        {
            int[] tab = new int[a];
            int size = 0, i;
            Random rd = new Random();
            while (size < a)
            {
                i = rd.Next(1, a + 1);
                if (!exist(i, tab))
                {
                    tab[size] = i;
                    size++;
                }
            }
            return tab;
        }
        static bool exist(int a, int[] t)
        {
            bool ext = false;

            foreach (int el in t)
            {
                if (el == a)
                {
                    ext = true;
                }
            }
            return ext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new TestNiveau3(13, 31, 37);
            _testChoisi = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new TestNiveau3(15, 33, 39);
            _testChoisi = 2;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new TestNiveau3(17, 35, 41);
            _testChoisi = 3;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new TestNiveau3(55, 61, 67);
            _testChoisi = 4;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new TestNiveau3(57, 63, 69);
            _testChoisi = 5;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new TestNiveau3(59, 65, 72);
            _testChoisi = 6;
        }





        private void choix_Test(object sender, RoutedEventArgs e)
        {
            if (getnote(3, 2) > 3)
                b2.Background = Brushes.GreenYellow;
            else if (getnote(3, 2) == 3)
                b2.Background = Brushes.Orange;
            else if (getnote(3, 2) == -1)
                b2.Background = Brushes.BlueViolet;
            else if (getnote(3, 2) < 3)
                b2.Background = Brushes.Red;


            if (getnote(3, 3) > 3)
                b3.Background = Brushes.GreenYellow;
            else if (getnote(3, 3) == 3)
                b3.Background = Brushes.Orange;
            else if (getnote(3, 3) == -1)
                b3.Background = Brushes.BlueViolet;
            else if (getnote(3, 3) < 3)
                b3.Background = Brushes.Red;


            if (getnote(3, 1) > 3)
                b1.Background = Brushes.GreenYellow;
            else if (getnote(3, 1) == 3)
                b1.Background = Brushes.Orange;
            else if (getnote(3, 1) == -1)
                b1.Background = Brushes.BlueViolet;
            else if (getnote(3, 1) < 3)
                b1.Background = Brushes.Red;


            if (getnote(3, 4) > 3)
                b4.Background = Brushes.GreenYellow;
            else if (getnote(3, 4) == 3)
                b4.Background = Brushes.Orange;
            else if (getnote(3, 4) == -1)
                b4.Background = Brushes.BlueViolet;
            else if (getnote(3, 4) < 3)
                b4.Background = Brushes.Red;


            if (getnote(3, 5) > 3)
                b5.Background = Brushes.GreenYellow;
            else if (getnote(3, 5) == 3)
                b5.Background = Brushes.Orange;
            else if (getnote(3, 5) == -1)
                b5.Background = Brushes.BlueViolet;
            else if (getnote(3, 5) < 3)
                b5.Background = Brushes.Red;


            if (getnote(3, 6) > 3)
                b6.Background = Brushes.GreenYellow;
            else if (getnote(3, 6) == 3)
                b6.Background = Brushes.Orange;
            else if (getnote(3, 6) == -1)
                b6.Background = Brushes.BlueViolet;
            else if (getnote(3, 6) < 3)
                b6.Background = Brushes.Red;


            if (testActuel == 1)
            {
                b2.IsEnabled = false;
                b3.IsEnabled = false;
                b4.IsEnabled = false;
                b5.IsEnabled = false;
                b6.IsEnabled = false;

                saveLastTest(1);
            }
            if (testActuel == 2)
            {
                b2.IsEnabled = true;
                b3.IsEnabled = false;
                b4.IsEnabled = false;
                b5.IsEnabled = false;
                b6.IsEnabled = false;

                saveLastTest(2);
            }
            if (testActuel == 3)
            {
                b3.IsEnabled = true;
                b4.IsEnabled = false;
                b5.IsEnabled = false;
                b6.IsEnabled = false;

                saveLastTest(3);
            }
            if (testActuel == 4)
            {
                b4.IsEnabled = true;
                b5.IsEnabled = false;
                b6.IsEnabled = false;

                saveLastTest(4);
            }
            if (testActuel == 5)
            {
                b5.IsEnabled = true;
                b6.IsEnabled = false;

                saveLastTest(5);
            }
            if (testActuel == 6)
            {
                b6.IsEnabled = true;

                saveLastTest(6);
            }


        }

        /// <summary>
        /// get l'état d'avancement 
        /// </summary>
        /// <param name="testCode"></param> le dérnier test
        private void getLastTest()
        {
            // Code == ID //
            string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            DataTable savedData = new DataTable();
            SqlConnection connectToUtilisateur = new SqlConnection(connString);

            SqlCommand cmd;

            string query = "SELECT * FROM Utilisateur WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

            if (connectToUtilisateur.State == ConnectionState.Closed)
                connectToUtilisateur.Open();

            try
            {
                cmd = new SqlCommand(query, connectToUtilisateur);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(savedData);
                da.Dispose();

                if (savedData.Rows[0]["Test3"].ToString().Equals(""))
                    testActuel = 1;
                else
                    testActuel = Int32.Parse(savedData.Rows[0]["Test3"].ToString());

                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();
            }
            catch (Exception)
            {
                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();

                MessageBox.Show("error Get LastTest Testniveau 3 ");
            }
        }
        /// <summary>
        /// Save the last test
        /// </summary>
        /// <param name="testCode"></param> the code of the test
        private void saveLastTest(int testCode)
        {
            // Code == ID //
            string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            string query = "UPDATE Utilisateur SET Test3='" + testCode.ToString() + "' WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

            SqlConnection connecttoUsers = new SqlConnection(connString);

            if (connecttoUsers.State == ConnectionState.Closed)
                connecttoUsers.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(query, connecttoUsers);

                cmd = new SqlCommand(query, connecttoUsers);

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if (connecttoUsers.State == ConnectionState.Open)
                    connecttoUsers.Close();
            }
            catch (Exception)
            {
                if (connecttoUsers.State == ConnectionState.Open)
                    connecttoUsers.Close();

                MessageBox.Show("error save last test Testniveau 3 ");
            }
        }
        private int getnote(int niveau, int test)
        {
            // Code == ID //
            string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            DataTable savedData = new DataTable();
            SqlConnection connectToUtilisateur = new SqlConnection(connString);

            SqlCommand cmd;

            string query = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID + "NoteTest WHERE Niveau = '" + niveau.ToString() + "' AND Test = '" + test.ToString() + "'";

            if (connectToUtilisateur.State == ConnectionState.Closed)
                connectToUtilisateur.Open();

            try
            {
                cmd = new SqlCommand(query, connectToUtilisateur);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(savedData);
                da.Dispose();
                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();
                if (savedData.Rows.Count == 1)
                {
                    if (savedData.Rows[0]["Note"].ToString().Equals(""))
                        return -1;
                    else
                        return Int32.Parse(savedData.Rows[0]["Note"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();

                MessageBox.Show("error Get note Testniveau 3 ");
                return -1;
            }
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niv3Main();
        }

        private void b1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;
            button.BorderThickness = new Thickness(8);
            button.BorderBrush = Brushes.Azure;
            if(getnote(3, ((int)button.Tag)) ==-1)
            {
                if (MainWindow.langue == 0)
                {
                    noteShow.Content = "Vous n'avez pas passé ce test !";
                }
                else
                {
                    noteShow.Content = "انت لم تجتز هدا الامتحان";
                }
            }
            else
            {
                if (MainWindow.langue == 0)
                {
                    noteShow.Content = "Test Note : " + getnote(3, ((int)button.Tag)).ToString();
                }
                else
                {
                    noteShow.Content = "نقطة الامتحان " + getnote(3, ((int)button.Tag)).ToString();
                }
            }
        }

        private void b1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;
            button.BorderThickness = new Thickness(0);
            button.BorderBrush = null;
            noteShow.Content = "";
        }

    }

}

