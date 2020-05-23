using System;
using System.IO;
using System.Collections.Generic;

namespace netCore_program_testowy
{
    class Row
    {

        public string Customer { get; set; }
        public string Product { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Cost { get; set; }

        public Row()
        {
            Customer = "";
            Product = "";
            Price = "";
            Quantity = "";
            Cost = "";
        }
        public Row(string customer, string product, string price, string quantity, string cost = "")
        {
            Customer = customer;
            Product = product;
            Price = price;
            Quantity = quantity;
            Cost = cost;
        }

        public Row(string customer, string product, string quantity, string cost)
        {
            Customer = customer;
            Product = product;
            Quantity = quantity;
            Cost = cost;
        }
        public void printRow()
        {
            Console.WriteLine($"{Customer},{Product},{Price},{Quantity},{Cost}");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var sales = File.ReadAllText(@"sales.csv");
            var new_sales = File.ReadAllText(@"new_sales.csv");

            var sales_rows = sales.Split("\r\n");
            var new_sales_rows = new_sales.Split("\r\n");

            var listOfSales = new List<Row>();
            var listOfNewSales = new List<Row>();


            foreach (var row in sales_rows)
            {
                var fields = row.Split(',');
                Row bufferRow = new Row();
                    bufferRow.Customer = fields[0];
                    bufferRow.Product = fields[1];
                    bufferRow.Price = fields[2];
                    bufferRow.Quantity = fields[3];

                listOfSales.Add(bufferRow);
            }

            foreach (var row in new_sales_rows)
            {
                var fields = row.Split(',');
                Row bufferRow = new Row();
                    bufferRow.Customer = fields[0];
                    bufferRow.Product = fields[1];
                    bufferRow.Quantity = fields[2];
                    bufferRow.Cost = fields[3];
                listOfNewSales.Add(bufferRow);
            }


            var mergedList = new List<Row>();

            foreach(var row in listOfNewSales)
            {
                foreach(var oldRow in listOfSales)
                {
                    if (row.Customer == oldRow.Customer && row.Product == oldRow.Product)
                    {
                        Row newRow = new Row();
                        newRow.Customer = row.Customer;
                        newRow.Product = row.Product;
                        newRow.Price = oldRow.Price;
                        newRow.Quantity = row.Quantity;
                        newRow.Cost = row.Cost;

                        mergedList.Add(newRow);
                        continue;
                    }
                    if (row.Customer == oldRow.Customer && row.Product != oldRow.Product)
                    {
                        Row newRow = new Row();
                        newRow.Customer = row.Customer;
                        newRow.Product = oldRow.Product;
                        newRow.Price = oldRow.Price;
                        newRow.Quantity = row.Quantity;

                        mergedList.Add(newRow);
                        break;
                    }
                    if(row.Customer != oldRow.Customer)
                    {
                        Row newRow = new Row();
                        newRow.Customer = row.Customer;
                        newRow.Product = row.Product;
                        newRow.Quantity = row.Quantity;
                        newRow.Cost = row.Cost;

                        mergedList.Add(newRow);
                        break;
                    }
                }
            }


            foreach (var row in mergedList)
                row.printRow();
        }
    }
}
