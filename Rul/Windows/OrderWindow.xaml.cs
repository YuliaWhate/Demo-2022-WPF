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
using System.Windows.Shapes;
using Rul.Entities;

namespace Rul.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        Product product = new Product();
        List<Product> productList = new List<Product>();
        public OrderWindow(List<Product> products, User user)
        {
            InitializeComponent();

            DataContext = this;
            productList = products;
            lViewOrder.ItemsSource = productList;
            cmbPickupPoint.ItemsSource = RulEntities.GetContext().PickupPoint.Select(p => p.Address).ToList();

            if (user != null)
                txtUser.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
        }
        public string Total
        {
            get
            {
                var total = productList.Sum(p => Convert.ToDouble(p.ProductCost) - Convert.ToDouble(p.ProductCost) * Convert.ToDouble(p.ProductDiscountAmount / 100.00));
                return total.ToString();
            }
        }

        private void btnOrderSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
