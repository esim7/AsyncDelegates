using DataContext;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для ActionWindow.xaml
    /// </summary>
    public partial class ActionWindow : Page
    {
        public Func<string, string, string, string, bool> DataBaseOperator;
        public MainWindow Window;
        public List<Category> Categories = new List<Category>();
        public List<Product> Products = new List<Product>();
        public Context Context = new Context();

        public ActionWindow(MainWindow window)
        {
            InitializeComponent();
            Window = window;
            Categories = Context.Categories.ToList();
            Products = Context.Products.ToList();
            lvCategories.ItemsSource = Categories;
            lvProducts.ItemsSource = Products;
        }

        private void categoriesButton_Click(object sender, RoutedEventArgs e)
        {
            createCategory_button.Visibility = Visibility.Visible;
            editCategory_button.Visibility = Visibility.Visible;
            createItem_button.Visibility = Visibility.Hidden;
            editProduct_Button.Visibility = Visibility.Hidden;
            grid_Categories.Visibility = Visibility.Hidden;
            grid_CategoriesEdit.Visibility = Visibility.Hidden;
            grid_Products.Visibility = Visibility.Hidden;
            grid_ProductsEdit.Visibility = Visibility.Hidden;
        }

        private void itemsButton_Click(object sender, RoutedEventArgs e)
        {
            createItem_button.Visibility = Visibility.Visible;
            editProduct_Button.Visibility = Visibility.Visible;
            createCategory_button.Visibility = Visibility.Hidden;
            editCategory_button.Visibility = Visibility.Hidden;
            grid_Categories.Visibility = Visibility.Hidden;
            grid_CategoriesEdit.Visibility = Visibility.Hidden;
            grid_Products.Visibility = Visibility.Hidden;
            grid_ProductsEdit.Visibility = Visibility.Hidden;
        }

        private void saveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = newCotegoryName_textbox.Text;
            string categoryInfo = newCategoryInfo_textbox.Text;
            DataBaseOperator.BeginInvoke(categoryName, categoryInfo, null, null, Result, null);
            Thread.Sleep(1500); // Олег Сергеевич тут задержка потому что запись в бд происходит раньше чем обновление записей на странице
            Window.frame.Navigate(new ActionWindow(Window));
        }

        private void saveProductButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = newProductName_textbox.Text;
            string productInfo = newProductInfo_textbox.Text;
            DataBaseOperator.BeginInvoke(productName, productInfo, null, null, Result, null);
            Thread.Sleep(1500);
            Window.frame.Navigate(new ActionWindow(Window));
        }

        private void saveEditedProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = lvProducts.SelectedIndex;
                string oldProductName = Products.ElementAt(index).Name;
                string oldProductInfo = Products.ElementAt(index).Description;
                string productName = EditProductName_textbox.Text;
                string productInfo = EditProductInfo_textbox.Text;

                DataBaseOperator.BeginInvoke(productName, productInfo, oldProductName, oldProductInfo, Result, null);
                Thread.Sleep(1500);
                Window.frame.Navigate(new ActionWindow(Window));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                MessageBox.Show("Ошибка!");
            }
            
        }

        private void saveEditedCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = lvCategories.SelectedIndex;
                string oldCategoryName = Categories.ElementAt(index).Name;
                string oldCategoryInfo = Categories.ElementAt(index).Description;
                string categoryName = editCategoryName_textbox.Text;
                string categoryInfo = editCategoryInfo_textbox.Text;

                DataBaseOperator.BeginInvoke(categoryName, categoryInfo, oldCategoryName, oldCategoryInfo, Result, null);
                Thread.Sleep(1500);
                Window.frame.Navigate(new ActionWindow(Window));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void createCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            createCategory_button.Visibility = Visibility.Hidden;
            editCategory_button.Visibility = Visibility.Hidden;
            grid_Categories.Visibility = Visibility.Visible;
            grid_Products.Visibility = Visibility.Hidden;
            grid_ProductsEdit.Visibility = Visibility.Hidden;
            grid_CategoriesEdit.Visibility = Visibility.Hidden;
            DataBaseOperator = new Func<string, string, Object, Object, bool>(AddCategory);          
        }

        private void createProductButton_Click(object sender, RoutedEventArgs e)
        {
            grid_Products.Visibility = Visibility.Visible;
            editProduct_Button.Visibility = Visibility.Hidden;
            createItem_button.Visibility = Visibility.Hidden;
            DataBaseOperator = new Func<string, string, Object, Object, bool>(AddProduct);
        }
       
        private void editProduct_Click(object sender, RoutedEventArgs e)
        {

            grid_ProductsEdit.Visibility = Visibility.Visible;
            editProduct_Button.Visibility = Visibility.Hidden;
            createItem_button.Visibility = Visibility.Hidden;
            DataBaseOperator = new Func<string, string, string, string, bool>(EditProduct);     
        }

        private void editCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            grid_CategoriesEdit.Visibility = Visibility.Visible;
            editCategory_button.Visibility = Visibility.Hidden;
            createCategory_button.Visibility = Visibility.Hidden;
            DataBaseOperator = new Func<string, string, string, string, bool>(EditCategory);
        }

        public bool AddCategory(string name, string description, Object obj, Object obj1)
        {
            using (var context = new Context())
            {
                var newCategory = new Category(name, description);
                context.Categories.Add(newCategory);
                context.SaveChanges();
            }
            return true;
        }

        public bool AddProduct(string name, string description, Object obj, Object obj1)
        {
            using (var context = new Context())
            {
                var newProduct = new Product(name, description);
                context.Products.Add(newProduct);
                context.SaveChanges();
            }
            return true;
        }

        public bool EditProduct(string name, string description, string oldName, string oldDescription)
        {
            using (var context = new Context())
            {
                var product = context.Products.Where(x => x.Name == oldName && x.Description == oldDescription).FirstOrDefault();
                product.Name = name;
                product.Description = description;
                context.SaveChanges();
            }
            return true;
        }

        public bool EditCategory(string name, string description, string oldName, string oldDescription)
        {
            using (var context = new Context())
            {
                var category = context.Categories.Where(x => x.Name == oldName && x.Description == oldDescription).FirstOrDefault();
                category.Name = name;
                category.Description = description;
                context.SaveChanges();
            }
            return true;
        }

        private void Result(IAsyncResult result)
        {
            var processResult = DataBaseOperator.EndInvoke(result);
            if (processResult == true)
            {
                MessageBox.Show("Успешно!!!");
            }
            else
            {
                MessageBox.Show("Произошла ошибка");
            }
        }
    }
}
