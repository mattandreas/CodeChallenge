using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using Service;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly GeneratorService _service;
        
        public MainWindow()
        {
            _service = new GeneratorService();

            InitializeComponent();

            WordList.ItemsSource = _service.Words;

            DataContext = this;
        }

        private void Generate_OnClick(object sender, RoutedEventArgs e)
        {
            var input = EnteredText.Text.ToUpper();
            _service.CheckWord((string) input);
        }
    }
}