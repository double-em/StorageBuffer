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
using System.Windows.Shapes;
using StorageBuffer.Application;
using StorageBuffer.Domain;

public delegate void OrderChanged(Order order);

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private Controller control;
        private Order orderBefore;
        private Order order;
        public OrderWindow(Controller control, Order order)
        {
            InitializeComponent();

            this.orderBefore = order;
            this.order = new Order(order.Id, order.CustomerObj, order.OrderStatus, order.Name, order.Date, order.Deadline);
            this.order.orderlines.AddRange(order.orderlines);
            this.control = control;
            Setup();
            this.Show();
        }

        private void Setup()
        {
            GetAllOrderlines();

            Title = "Ordre - " + order.Id + " : " + order.Name;
            lOrderNumber.Content = "Ordrenummer: " + order.Id;
            lOrderDate.Content = "Ordredato: " + order.Date;
            lDeadline.Content = "Deadline: " + order.Deadline;
            lCustomerName.Content = "Kunde: " + order.CustomerObj.Name;

            switch (order.OrderStatus)
            {
                case Status.Received:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[0];
                    break;

                case Status.InProgress:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[1];
                    break;

                case Status.Done:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[2];
                    break;

                case Status.Shipped:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[3];
                    break;

                case Status.Billed:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[4];
                    break;

                case Status.Paid:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[5];
                    break;
                case Status.Canceled:
                    cbOrderChoice.SelectedItem = cbOrderChoice.Items[6];
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddMaterial_Click(object sender, RoutedEventArgs e)
        {
            MaterialChooseWindow chooseWindow = new MaterialChooseWindow(control);

            chooseWindow.Owner = this;
            chooseWindow.Top = this.Top;
            chooseWindow.Left = this.Left + 8;
            chooseWindow.ShowDialog();

            if (chooseWindow.DialogResult.Value)
            {
                bool materialAlreadyAdded = false;
                foreach (Orderline orderline in order.orderlines)
                {
                    if (orderline.MaterialId == chooseWindow.ChoosenMaterial.Id)
                    {
                        materialAlreadyAdded = true;
                        break;
                    }
                }

                if (!materialAlreadyAdded)
                {
                    Orderline orderlineNew = new Orderline(chooseWindow.ChoosenMaterial, 0);
                    order.orderlines.Add(orderlineNew);
                    lvResult.Items.Add(orderlineNew);
                }
            }
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var listItem = (ListViewItem)sender;
            var orderline = (Orderline) listItem.Content;

            ChangeOrderline changeOrderline = new ChangeOrderline(orderline);
            changeOrderline.Owner = this;
            changeOrderline.Top = this.Top;
            changeOrderline.Left = this.Left;
            changeOrderline.ShowDialog();

            if (changeOrderline.delete)
            {
                order.orderlines.Remove(orderline);
            }

            GetAllOrderlines();
        }

        private void GetAllOrderlines()
        {
            lvResult.Items.Clear();
            foreach (Orderline orderline in order.orderlines)
            {
                lvResult.Items.Add(orderline);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            orderBefore.OrderStatus = order.OrderStatus;
            orderBefore.orderlines = new List<Orderline>();
            foreach (Orderline orderline in order.orderlines)
            {
                control.RegisterUsedMaterial(order.Id, orderline.MaterialObj, orderline.Quantity);
            }

            control.UpdateOrder(orderBefore);
        }

        private void CbOrderChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbOrderChoice.SelectedIndex)
            {
                case 0:
                    order.OrderStatus = Status.Received;
                    break;

                case 1:
                    order.OrderStatus = Status.InProgress;
                    break;

                case 2:
                    order.OrderStatus = Status.Done;
                    break;

                case 3:
                    order.OrderStatus = Status.Shipped;
                    break;

                case 4:
                    order.OrderStatus = Status.Billed;
                    break;

                case 5:
                    order.OrderStatus = Status.Paid;
                    break;

                case 6:
                    order.OrderStatus = Status.Canceled;
                    break;
            }
        }
    }
}
