﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace StorageBuffer
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class MaterialWindow : Window
    {
        private Controller control;
        private int materialId;

        public MaterialWindow(Controller control, int materialId)
        {
            InitializeComponent();
            this.materialId = materialId;
            this.control = control;
            Setup();
            this.Show();
        }

        private void Setup()
        {
            GetMaterialInfo();
        }

        private void GetMaterialInfo()
        {
            List<string> material = control.GetMaterialLong(materialId);

            tbMaterialName.Text = material[1];
            tbMaterialComment.Text = material[2];
            tbMaterialQuantity.Text = material[3];
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveMaterial();
        }

        private void SaveMaterial()
        {
            string messsage = "";
            if (control.UpdateMaterial(materialId, tbMaterialName.Text, tbMaterialComment.Text, tbMaterialQuantity.Text))
            {
                messsage = "Materialet blev gemt.";
            }
            else
            {
                messsage = "Materialet kunne IKKE gemmes.";
            }

            MessageWindow messageWindow = new MessageWindow(messsage);
            messageWindow.Owner = this;
            messageWindow.Top = this.Top;
            messageWindow.Left = this.Left + 8;
            messageWindow.ShowDialog();
        }

        private void MaterialWindow_OnClosing(object sender, CancelEventArgs e)
        {
            ConfirmationWindow confirmationWindow = new ConfirmationWindow("Vil du gemme ændringerne?");
            confirmationWindow.Owner = this;
            confirmationWindow.Top = this.Top;
            confirmationWindow.Left = this.Left + 8;
            confirmationWindow.ShowDialog();

            if ((bool)confirmationWindow.DialogResult)
            {
                SaveMaterial();
            }
        }
    }
}
