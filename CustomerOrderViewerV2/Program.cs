using System;
using System.Collections.Generic;
using System.Linq;
using CustomerOrderViewerV2.Models;
using CustomerOrderViewerV2.Repository;

namespace CustomerOrderViewerV2
{
    class Program
    {
        private static string _connectionString = @"Data Source=localhost;Initial Catalog=CustomerOrderViewer;Integrated Security=True";
        private static readonly CustomerOrderDetailCommand _customerOrderCommand = new CustomerOrderDetailCommand(_connectionString);
        private static readonly CustomerCommand _customerCommand = new CustomerCommand(_connectionString);
        private static readonly ItemCommand _itemCommand = new ItemCommand(_connectionString);
        static void Main(string[] args)
        {
            try
            {
                var continueManaging = true;
                var userId = string.Empty;

                Console.WriteLine("What is your username?");
                userId = Console.ReadLine();

                do
                {
                    Console.WriteLine("1 - Show All | 2 - Upsert Customer Order | 3 - Delete Customer Order | 4 - Exit");
                    int option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            ShowAll();
                            break;
                        case 2:
                            UpsertCustomerOrder(userId);
                            break;
                        case 3:
                            DeleteCustomerOrder(userId);
                            break;
                        case 4:
                            continueManaging = false;
                            break;
                        default:
                            Console.WriteLine("Option not found");
                            break;
                    }
                } while (continueManaging == true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: {0}", e.Message);
                throw;
            }
        }

        private static void DeleteCustomerOrder(string userId)
        {
            Console.WriteLine("Enter CustomerOrderId to delete: ");
            int customerOrderId = Convert.ToInt32(Console.ReadLine());
            _customerOrderCommand.Delete(customerOrderId, userId);
        }

        private static void UpsertCustomerOrder(string userId)
        {
            Console.WriteLine("Note: For updating insert existing CustomerOrderId, for new entries enter -1.");
            Console.WriteLine("Enter CustomerOrderId: ");
            int newCustomerOrderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter CustomerId: ");
            int newCustomerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter ItemId: ");
            int newItemId = Convert.ToInt32(Console.ReadLine());

            _customerOrderCommand.Upsert(newCustomerOrderId, newCustomerId, newItemId, userId);
        }

        private static void ShowAll()
        {
            Console.WriteLine("{0} All Customer Orders: {1}", Environment.NewLine, Environment.NewLine);
            DisplayCustomerOrders();

            Console.WriteLine("{0} All Customers: {1}", Environment.NewLine, Environment.NewLine);
            DisplayCustomers();

            Console.WriteLine("{0} All Items: {1}", Environment.NewLine, Environment.NewLine);
            DisplayItems();

            Console.WriteLine();
        }

        private static void DisplayItems()
        {
            IList<ItemModel> items = _itemCommand.GetList();
            if (items.Any())
            {
                foreach (ItemModel i in items)
                {
                    Console.WriteLine("{0}: Description: {1}, Price {2}", i.ItemId, i.Description, i.Price);

                }
            }
        }

        private static void DisplayCustomers()
        {
            IList<CustomerModel> customers = _customerCommand.GetList();
            if (customers.Any())
            {
                foreach (CustomerModel c in customers)
                {
                    Console.WriteLine("{0}: First Name: {1}, Middle Name {2}, Last Name: {3}, Age: {4}",
                        c.CustomerId, c.FirstName, c.MiddleName ?? "N/A", c.LastName, c.Age);
                }
            }
        }

        private static void DisplayCustomerOrders()
        {
            IList<CustomerOrderDetailModel> customerOrderDetails = _customerOrderCommand.GetList();
            if (customerOrderDetails.Any())
            {
                foreach (CustomerOrderDetailModel c in customerOrderDetails)
                {
                    Console.WriteLine("{0}: Full Name: {1} {2} (Id: {3}) - purchased {4} for {5} (Id: {6}",
                        c.CustomerOrderId, c.FirstName, c.LastName, c.CustomerId, c.Description, c.Price, c.ItemId);
                }
            }
        }
    }
}
