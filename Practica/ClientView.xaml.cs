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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practica
{
    /// <summary>
    /// Логика взаимодействия для ClientView.xaml
    /// </summary>
    public partial class ClientView : Page
    {
        public ClientView()
        {
            InitializeComponent();
           
        }

        private void BtnBackToMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HelloPage());
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage((sender as Button).DataContext as Client));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPage(null));
        }

        

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible) 
            {
                pro19Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridSaloon.ItemsSource = pro19Entities.GetContext().Client.ToList();
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var ClientForRemoving = DGridSaloon.SelectedItems.Cast<Client>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {ClientForRemoving.Count()}элементов ?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
            {
                try
                {
                    pro19Entities.GetContext().Client.RemoveRange(ClientForRemoving);
                    pro19Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены! ");
                    DGridSaloon.ItemsSource = pro19Entities.GetContext().Client.ToList();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }


    }
}
