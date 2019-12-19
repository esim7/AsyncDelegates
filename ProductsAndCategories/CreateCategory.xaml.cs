using System;
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

namespace ProductsAndCategories
{
    /// <summary>
    /// Логика взаимодействия для CreateCategory.xaml
    /// </summary>
    public partial class CreateCategory : Page
    {
        public MainWindow Window;
        public CreateCategory(MainWindow mainWindow)
        {
            InitializeComponent();
            Window = mainWindow;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancel_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
