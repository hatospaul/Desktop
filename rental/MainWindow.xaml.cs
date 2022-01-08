using rentalmodel;
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
using System.Data.Entity;
using System.Data;

namespace rental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RentalEntitiesModel rentalEntitiesModel = new RentalEntitiesModel();
        CollectionViewSource customerViewSource;
        CollectionViewSource inventoryViewSource;
        CollectionViewSource rentalViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            customerViewSource.Source = rentalEntitiesModel.Customers.Local;
            rentalEntitiesModel.Customers.Load();
            inventoryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventoryViewSource")));
            inventoryViewSource.Source = rentalEntitiesModel.Inventories.Local;
            rentalEntitiesModel.Inventories.Load();
            rentalViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rentalViewSource")));
            rentalViewSource.Source = rentalEntitiesModel.Rentals.Local;
            rentalEntitiesModel.Rentals.Load();

            BindingOperations.ClearBinding(nameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(emailTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(phoneTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idMovieTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idCustomerTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idInventoryTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(statusTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }

        public void SetValidationBinding()
        {
            Binding nameValidationBinding = new Binding();
            nameValidationBinding.Source = customerViewSource;
            nameValidationBinding.Path = new PropertyPath("Name");
            nameValidationBinding.NotifyOnValidationError = true;
            nameValidationBinding.Mode = BindingMode.TwoWay;
            nameValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            nameValidationBinding.ValidationRules.Add(new StringValidation());
            nameTextBox.SetBinding(TextBox.TextProperty, nameValidationBinding);

            Binding emailValidationBinding = new Binding();
            emailValidationBinding.Source = customerViewSource;
            emailValidationBinding.Path = new PropertyPath("Email");
            emailValidationBinding.NotifyOnValidationError = true;
            emailValidationBinding.Mode = BindingMode.TwoWay;
            emailValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            emailValidationBinding.ValidationRules.Add(new StringValidation());
            emailTextBox.SetBinding(TextBox.TextProperty, emailValidationBinding);

            Binding phoneValidationBinding = new Binding();
            phoneValidationBinding.Source = customerViewSource;
            phoneValidationBinding.Path = new PropertyPath("Phone");
            phoneValidationBinding.NotifyOnValidationError = true;
            phoneValidationBinding.Mode = BindingMode.TwoWay;
            phoneValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            phoneValidationBinding.ValidationRules.Add(new StringValidation());
            phoneTextBox.SetBinding(TextBox.TextProperty, phoneValidationBinding);

            Binding idMovieValidationBinding = new Binding();
            idMovieValidationBinding.Source = inventoryViewSource;
            idMovieValidationBinding.Path = new PropertyPath("IdMovie");
            idMovieValidationBinding.NotifyOnValidationError = true;
            idMovieValidationBinding.Mode = BindingMode.TwoWay;
            idMovieValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            idMovieValidationBinding.ValidationRules.Add(new IdValidation());
            idMovieTextBox.SetBinding(TextBox.TextProperty, idMovieValidationBinding);

            Binding idCustomerValidationBinding = new Binding();
            idCustomerValidationBinding.Source = rentalViewSource;
            idCustomerValidationBinding.Path = new PropertyPath("IdCustomer");
            idCustomerValidationBinding.NotifyOnValidationError = true;
            idCustomerValidationBinding.Mode = BindingMode.TwoWay;
            idCustomerValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            idCustomerValidationBinding.ValidationRules.Add(new IdValidation());
            idCustomerTextBox.SetBinding(TextBox.TextProperty, idCustomerValidationBinding);

            Binding idInventoryValidationBinding = new Binding();
            idInventoryValidationBinding.Source = rentalViewSource;
            idInventoryValidationBinding.Path = new PropertyPath("IdInventory");
            idInventoryValidationBinding.NotifyOnValidationError = true;
            idInventoryValidationBinding.Mode = BindingMode.TwoWay;
            idInventoryValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            idInventoryValidationBinding.ValidationRules.Add(new IdValidation());
            idInventoryTextBox.SetBinding(TextBox.TextProperty, idInventoryValidationBinding);

            Binding statusValidationBinding = new Binding();
            statusValidationBinding.Source = rentalViewSource;
            statusValidationBinding.Path = new PropertyPath("Status");
            statusValidationBinding.NotifyOnValidationError = true;
            statusValidationBinding.Mode = BindingMode.TwoWay;
            statusValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            statusValidationBinding.ValidationRules.Add(new StringValidation());
            statusTextBox.SetBinding(TextBox.TextProperty, statusValidationBinding);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tabControl.SelectedItem as TabItem;

            try
            {
                switch (ti.Header)
                {
                    case "Customers":
                        Customer customer = new Customer()
                        {
                            Name = nameTextBox.Text.Trim(),
                            Email = emailTextBox.Text.Trim(),
                            Phone = phoneTextBox.Text.Trim()
                        };
                        rentalEntitiesModel.Customers.Add(customer);
                        customerViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                    case "Inventory Items":
                        Inventory inventory = new Inventory()
                        {
                            IdMovie = Int32.Parse(idMovieTextBox.Text.Trim())
                        };
                        rentalEntitiesModel.Inventories.Add(inventory);
                        inventoryViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                    case "Rentals":
                        Rental rental = new Rental()
                        {
                            IdInventory = Int32.Parse(idInventoryTextBox.Text.Trim()),
                            IdCustomer = Int32.Parse(idCustomerTextBox.Text.Trim()),
                            Status = statusTextBox.Text.Trim(),
                            StartDate = startDateDatePicker.SelectedDate,
                            EndDate = endDateDatePicker.SelectedDate
                        };
                        rentalEntitiesModel.Rentals.Add(rental);
                        rentalViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tabControl.SelectedItem as TabItem;

            try
            {
                switch (ti.Header)
                {
                    case "Customers":
                        Customer customer = (Customer)customerDataGrid.SelectedItem;
                        customer.Name = nameTextBox.Text.Trim();
                        customer.Email = emailTextBox.Text.Trim();
                        customer.Phone = phoneTextBox.Text.Trim();
                        customerViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                    case "Inventory Items":
                        Inventory inventory = (Inventory)inventoryDataGrid.SelectedItem;
                        inventory.IdMovie = Int32.Parse(idMovieTextBox.Text.Trim());
                        inventoryViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                    case "Rentals":
                        Rental rental = (Rental)rentalDataGrid.SelectedItem;
                        rental.IdInventory = Int32.Parse(idInventoryTextBox.Text.Trim());
                        rental.IdCustomer = Int32.Parse(idCustomerTextBox.Text.Trim());
                        rental.StartDate = startDateDatePicker.SelectedDate;
                        rental.EndDate = endDateDatePicker.SelectedDate;
                        rental.Status = statusTextBox.Text.Trim();
                        rentalViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tabControl.SelectedItem as TabItem;

            try
            {
                switch (ti.Header)
                {
                    case "Customers":
                        Customer customer = (Customer)customerDataGrid.SelectedItem;
                        rentalEntitiesModel.Customers.Remove(customer);
                        customerViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                    case "Inventory Items":
                        Inventory inventory = (Inventory)inventoryDataGrid.SelectedItem;
                        rentalEntitiesModel.Inventories.Remove(inventory);
                        inventoryViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                    case "Rentals":
                        Rental rental = (Rental)rentalDataGrid.SelectedItem;
                        rentalEntitiesModel.Rentals.Remove(rental);
                        rentalViewSource.View.Refresh();
                        rentalEntitiesModel.SaveChanges();
                        break;
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            customerDataGrid.SelectedItem = null;
            customerViewSource.View.Refresh();
            inventoryDataGrid.SelectedItem = null;
            inventoryViewSource.View.Refresh();
            rentalDataGrid.SelectedItem = null;
            rentalViewSource.View.Refresh();

            BindingOperations.ClearBinding(nameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(emailTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(phoneTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idMovieTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idCustomerTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idInventoryTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(statusTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }

    }
}
