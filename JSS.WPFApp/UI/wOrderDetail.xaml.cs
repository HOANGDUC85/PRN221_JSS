using JSS.Business;
using JSS.Business.Base;
using JSS.Data;
using JSS.Data.Models;
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
using System.Xml.Linq;

namespace JSS.WPFApp.UI
{
    /// <summary>
    /// Interaction logic for wOrderDetail.xaml
    /// </summary>
    public partial class wOrderDetail : Window
    {
        private readonly OrderDetailBusiness _business;

        public wOrderDetail()
        {
            InitializeComponent();
            _business = new OrderDetailBusiness();
            LoadGrdOrderDetail();
        }


        private async void LoadGrdOrderDetail()
        {
            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdOrderDetail.ItemsSource = result.Data as List<OrderDetail>;
            }
            else
            {
                grdOrderDetail.ItemsSource = new List<OrderDetail>();
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderIdTemp = 1;
                int.TryParse(txtOrderId.Text, out orderIdTemp);

                int productIdTemp = 1;
                int.TryParse(txtProductId.Text, out productIdTemp);

                int orderDetailIdTemp;
                bool isUpdate = int.TryParse(txtOrderDetailId.Text, out orderDetailIdTemp);
                var item = isUpdate ? await _business.GetById(orderDetailIdTemp) : null;

                if (item == null || item.Data == null)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = orderIdTemp,
                        ProductId = productIdTemp,
                        Price = decimal.Parse(txtPrice.Text),
                        Amount= int.Parse(txtAmount.Text),
                        CreatedAt = DateTime.Now,
                    };

                    var result =  _business.Save(orderDetail);
                    MessageBox.Show( "Save");
                }
                else
                {
                    var orderDetail = item.Data as OrderDetail;
                    orderDetail.OrderId = orderIdTemp;
                    orderDetail.ProductId = productIdTemp;
                    orderDetail.Price = decimal.Parse(txtPrice.Text);
                    orderDetail.Amount = int.Parse(txtAmount.Text);
                    if (DateTime.TryParse(txtCreatedAt.Text, out DateTime createdAtTemp))
                    {
                        orderDetail.CreatedAt = createdAtTemp;
                    }
                    else
                    {
                        orderDetail.CreatedAt = DateTime.Now;
                    }

                    orderDetail.UpdatedAt = DateTime.Now;

                    var result = _business.Update(orderDetail);
                    MessageBox.Show( "Update");
                }

                txtOrderDetailId.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtProductId.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtCreatedAt.Text = string.Empty;
                txtUpdatedAt.Text = string.Empty;
                this.LoadGrdOrderDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }   

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtOrderDetailId.Text = string.Empty;
            txtOrderId.Text = string.Empty;
            txtProductId.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtCreatedAt.Text = string.Empty;
            txtUpdatedAt.Text = string.Empty;

            this.LoadGrdOrderDetail();
        }
        private async void grdOrderDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string orderdetail_id = btn.CommandParameter.ToString();

            //MessageBox.Show(currencyCode);

            if (!string.IsNullOrEmpty(orderdetail_id))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(int.Parse( orderdetail_id));
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdOrderDetail();
                }
            }
        }

        private async void grdOrderDetail_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as OrderDetail;
                    if (item != null)
                    {
                        var orderdetailResult = await _business.GetById(item.OrderDetailId);

                        if (orderdetailResult.Status > 0 && orderdetailResult.Data != null)
                        {
                            item = orderdetailResult.Data as OrderDetail;
                            txtOrderDetailId.Text= item.OrderDetailId.ToString();
                            txtOrderId.Text = item.OrderId.ToString();
                            txtProductId.Text = item.ProductId.ToString();
                            txtPrice.Text = item.Price.ToString();
                            txtAmount.Text = item.Amount.ToString();
                            txtCreatedAt.Text=item.CreatedAt.ToString();
                            txtUpdatedAt.Text=item.UpdatedAt.ToString();
                            
                        }
                    }
                }
            }
        }
    }
}
