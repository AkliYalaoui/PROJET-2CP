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
using System.Data;
using System.Data.SqlClient;
using PROJET_2CP.Classes;
using System.Xml;

namespace PROJET_2CP
{
    /// <summary>
    /// Interaction logic for LogIN.xaml
    /// </summary>
    public partial class LogIN : Page
    {
        private string _ConnectString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security = True";
        private DBAccess _ConnectBDD;
        private DataTable _Utilisateurs;
        private static Apprenant _LoggedUser;

        internal static Apprenant LoggedUser { get => _LoggedUser; set => _LoggedUser = value; }

        public LogIN()
        {
            InitializeComponent();
            initialiserLangue();
            _ConnectBDD = new DBAccess(_ConnectString);
            _Utilisateurs = new DataTable();

            //pour le mode invité
            guestMode.Tag = new Apprenant(0, "Guest", "", "", "", "");
            ((Apprenant)guestMode.Tag).Theme = true;
            loadUsers();
        }

        private void loadUsers()
        {
            try
            {
                string selectQuery = "SELECT * FROM Utilisateur";

                _ConnectBDD.createConn();
                _ConnectBDD.readDatathroughAdapter(selectQuery, _Utilisateurs);
                _ConnectBDD.closeConn();

                StackPanel utilisateur = new StackPanel();
                Button userBtn = new Button();

                //  User avatre :
                Ellipse avatareImage = new Ellipse();


                for (int nbUsers = 0; nbUsers < _Utilisateurs.Rows.Count; nbUsers++)
                {
                    // source.BeginInit();
                    if (_Utilisateurs.Rows[nbUsers]["Image"].ToString().Equals(""))
                        avatareImage.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\Default.png")));
                    else
                        avatareImage.Fill = new ImageBrush(new BitmapImage(new Uri(_Utilisateurs.Rows[nbUsers]["Image"].ToString())));

                    //-----------------------

                    avatareImage.StrokeThickness = 10;
                    avatareImage.Height = 180;
                    avatareImage.Width = 180;
                    avatareImage.HorizontalAlignment = HorizontalAlignment.Center;
                    avatareImage.Margin = new Thickness(0, 0, 0, 10);


                    userBtn.Content = _Utilisateurs.Rows[nbUsers]["UtilisateurID"].ToString();

                    userBtn.Tag = new Apprenant(Int32.Parse(_Utilisateurs.Rows[nbUsers]["Id"].ToString()), _Utilisateurs.Rows[nbUsers]["UtilisateurID"].ToString(), _Utilisateurs.Rows[nbUsers]["Nom"].ToString(), _Utilisateurs.Rows[nbUsers]["Prenom"].ToString(), _Utilisateurs.Rows[nbUsers]["Password"].ToString(), _Utilisateurs.Rows[nbUsers]["Image"].ToString());
                    userBtn.Height = 50;
                    userBtn.Width = 150;
                    userBtn.VerticalAlignment = VerticalAlignment.Bottom;
                    userBtn.HorizontalAlignment = HorizontalAlignment.Center;
                    userBtn.Margin = new Thickness(0, 10, 0, 0);
                    SolidColorBrush color = new SolidColorBrush();
                    color.Color = Color.FromArgb(50, 0, 0, 0);
                    userBtn.Background = color;
                    userBtn.BorderBrush = null;
                    userBtn.Click += new RoutedEventHandler(this.connect);
                    userBtn.HorizontalAlignment = HorizontalAlignment.Center;


                    utilisateur.Margin = new Thickness(0, 0, 20, 0);
                    utilisateur.Background = Brushes.Transparent;
                    utilisateur.VerticalAlignment = VerticalAlignment.Center;

                    utilisateur.Children.Add(avatareImage);
                    utilisateur.Children.Add(userBtn);

                    stackUsers.Children.Add(utilisateur);

                    userBtn = new Button();
                    utilisateur = new StackPanel();
                    avatareImage = new Ellipse();
                }
            }
            catch (Exception)
            {
                _ConnectBDD.closeConn();
                MessageBox.Show("Erreur dans load users login");
            }
        }

        private void connect(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            _LoggedUser = ((Apprenant)btn.Tag);

            if (getTheme().Equals("") || getTheme().Equals("True"))
                _LoggedUser.Theme = true;
            else
                _LoggedUser.Theme = false;


            if (((Apprenant)btn.Tag).Password.Equals(""))
            {
                MainWindow.quizFrame.Content = new Home();
            }
            else
            {
                btn.Command = MaterialDesignThemes.Wpf.DialogHost.OpenDialogCommand;
            }
        }

        private void signINclick(object sender, RoutedEventArgs e)
        {
            MainWindow.quizFrame.Content = new SignIN();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                creercptTxt.Text = "créer compte";
                apropostxt.Text = "A propos";
                cncttxt.Content = "Se connecter";
                slogantxt.Content = "qui révolutionne le permis !";
                guestMode.Content = "Mode invité";
                guestMode.ToolTip = "Dans ce mode vous avez moin d'avantages";
                dhMsg.Text = "Se compte est sécurisé";
                dhconfirme.Content = "Confirmer";
                dhretoure.Content = "Retoure";
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordtxt, "Mot de passe");
                langueComboText.Text = "langue";
            }
            else
            {
                //la langue arabe
                creercptTxt.Text = "انشاء حساب";
                apropostxt.Text = "عن التطبيق";
                cncttxt.Content = "تسجيل الدخول";
                slogantxt.Content = "! الأفضل في رخصة السياقة";
                guestMode.Content = "نمط المجرب";
                guestMode.ToolTip = "في هدا النمط ستكون الخصائص محدودة";
                dhMsg.Text = "هدا الحساب مؤمن بكلمة مرور";
                dhconfirme.Content = "تأكيد";
                dhretoure.Content = "عودة";
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordtxt, "كلمة المرور");
                langueComboText.Text = "اللغة";
            }
        }

        private void guestMode_Click(object sender, RoutedEventArgs e)
        {
            _LoggedUser = ((Apprenant)guestMode.Tag);
            _LoggedUser.Theme = true;
            MainWindow.quizFrame.Content = new Home();
        }

        private void connectWithPassword(object sender, RoutedEventArgs e)
        {

            if (_LoggedUser.Password.Equals(passwordtxt.Password))
            {
                MainWindow.quizFrame.Content = new Home();
            }
            else
            {
                passwordtxt.Password = "";

                if (MainWindow.langue == 0)
                    dherrpass.Content = "Mot de passe est incorrect";
                else
                    dherrpass.Content = "كلمة المرور خاطئة";
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.SkyBlue;
            creercptTxt.Foreground = Brushes.White;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.White;
            creercptTxt.Foreground = Brushes.SkyBlue;
        }

        private void Button_MouseLeave_1(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.White;
            apropostxt.Foreground = Brushes.SkyBlue;
        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.SkyBlue;
            apropostxt.Foreground = Brushes.White;
        }

        private string getTheme()
        {
            string connectionToUser = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            SqlConnection userConn = new SqlConnection(connectionToUser);
            SqlCommand Gettheme;
            string themeQuery = "SELECT * FROM Utilisateur WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";
            SqlDataAdapter adapter;
            DataTable user = new DataTable();

            if (userConn.State == ConnectionState.Closed)
                userConn.Open();

            using (userConn)
            {
                try
                {
                    Gettheme = new SqlCommand(themeQuery, userConn);
                    adapter = new SqlDataAdapter(Gettheme);
                    adapter.Fill(user);
                    adapter.Dispose();
                    if (userConn.State == ConnectionState.Open)
                        userConn.Close();

                    return user.Rows[0]["Theme"].ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error get theme !");
                    if (userConn.State == ConnectionState.Open)
                        userConn.Close();
                    return "";
                }
            }
        }
        private void langueCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (langueCmb.SelectedIndex == 0 && MainWindow.langue == 1)
            {
                MainWindow.langue = 0;
                MainWindow.quizFrame.Content = new LogIN();
            }
            else if (langueCmb.SelectedIndex == 1 && MainWindow.langue == 0)
            {
                MainWindow.langue = 1;
                MainWindow.quizFrame.Content = new LogIN();
            }
        }
    }
}