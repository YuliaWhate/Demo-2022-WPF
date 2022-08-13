using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Win32;
using Rul.Entities;

namespace Rul.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product product = new Product();
        public AddEditProductPage(Product currentProduct)
        {
            InitializeComponent();

            if(currentProduct != null)
            {
                product = currentProduct;                               //Если переданный объект с прошлой страницы не пустой, тогда добавляем его в созданный.
                                                                        
                btnDeleteProduct.Visibility = Visibility.Visible;      //Показываем кнопку удаления
                txtArticle.IsEnabled = false;                         //Запрещаем редактирования артикула
            }
            DataContext = product;
            cmbCategory.ItemsSource = CategoryList; //Передаем список в ресурсы ComboBox'а
            
        }

        public string[] CategoryList =
        {
            "Аксессуары",
            "Автозапчасти",
            "Автосервис",
            "Съемники подшипников",
            "Ручные инструменты",
            "Зарядные устройства"
        };

        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (product.ProductCost < 0)
                errors.AppendLine("Стоимость не может быть отрицательной!");
            if(product.MinCount < 0)
                errors.AppendLine("Минимальное количество не может быть отрицательным!");                       //Прописываем проверки по заданию
            if (product.ProductDiscountAmount > product.MaxDiscountAmount)
                errors.AppendLine("Действующая скидка на товар не может быть больше максимальной скидки!");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (product.ProductArticleNumber == null)
                RulEntities.GetContext().Product.Add(product);
            try
            {
                RulEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);   //Сохраняем данные в БД
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы действительно хотите удалить {product.ProductName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    RulEntities.GetContext().Product.Remove(product);
                    RulEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись удалена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEnterImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();

            GetImageDialog.Filter = "Файлы изображений: (*.png, *.jpg, *.jpeg)| *.png; *.jpg; *.jpeg";
            GetImageDialog.InitialDirectory = "C:\\Users\\yulia\\Desktop\\Rul\\Rul\\Resources";
            if (GetImageDialog.ShowDialog() == true)
            {
                product.ProductImage = GetImageDialog.SafeFileName;
            }
        }
    }
}
