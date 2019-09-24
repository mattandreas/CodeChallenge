using System.Windows;
using System.Windows.Controls;
using Service;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IService _service;
        
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
            _service.CheckWord(input);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var input = SpeedSelector.SelectedIndex;
            _service.SwitchGen(input);
        }
    }
}