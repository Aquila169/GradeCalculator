using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using GradingApp.Domain;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

namespace GradeCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Subject LoadedSubject = new Subject();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetItemSources()
        {
            this.Title = "Grade Calculator - " + LoadedSubject.Name + " - " + LoadedSubject.Year + "-" + (LoadedSubject.Year + 1);
            this.ExamListView.ItemsSource = LoadedSubject.Exams;
            this.ExercisesListView.ItemsSource = LoadedSubject.Exercises;
            this.BonussesListView.ItemsSource = LoadedSubject.Bonuses;
            this.DemandsListView.ItemsSource = LoadedSubject.Demands;
            this.PracticalListView.ItemsSource = LoadedSubject.Practicals;
        }

        private async void CalculateGrade_OnClick(object sender, RoutedEventArgs e)
        {
            float grade = LoadedSubject.CalculateGrade();
            RoundedGradeTextBlock.Text =
                (Math.Round(grade*2, MidpointRounding.AwayFromZero)/2f).ToString();
            UnroundedGradeTextBlock.Text = grade.ToString();
            RoundedGradeTextBlock.Foreground = grade >= LoadedSubject.Min ? Brushes.LightGreen : Brushes.IndianRed;
            GradeFlyout.IsOpen = true;
        }

        private void LoadSubject_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".xml";
            fileDialog.Filter = "Subject XML Files|*.xml";

            bool? result = fileDialog.ShowDialog(this);

            if (result.Value)
            {
                string fileName = fileDialog.FileName;
                LoadedSubject = XMLParser.ParseSubject(fileName);
                SetItemSources();
            }
        }

        private void BonusTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox senderCast = (TextBox) sender;
            bool hasStrictVal = (bool)senderCast.Tag;

            if (hasStrictVal)
                senderCast.Visibility = Visibility.Collapsed;
        }

        private void StrictValPanel_OnLoaded(object sender, RoutedEventArgs e)
        {
            StackPanel senderCast = (StackPanel)sender;
            bool hasStrictVal = (bool)senderCast.Tag;

            if (!hasStrictVal)
                senderCast.Visibility = Visibility.Collapsed;
        }
    }
}
