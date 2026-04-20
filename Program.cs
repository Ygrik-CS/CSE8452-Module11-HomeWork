using System;

namespace EcommerceDomain
{

    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Role { get; protected set; }

        public virtual void Register() { Console.WriteLine("Регистрация..."); }
        public virtual void Login() { Console.WriteLine("Вход..."); }
        public void UpdateData(string newAddress) { Address = newAddress; }
    }

    public class Client : User
    {
        public int LoyaltyPoints { get; set; } 
        public List<Order> OrderHistory { get; set; } = new();

        public Client() { Role = "Client"; }
    }

    public class Admin : User
    {
        public Admin() { Role = "Admin"; }

        public void LogAction(string action) 
        {
            Console.WriteLine($"[LOG] Admin {Name} выполнил: {action}");
        }
    }


    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public Category Category { get; set; } 

        public void Create() { }
        public void Update() { }
        public void Delete() { }
    }


    public enum OrderStatus { Processing, Delivering, Completed, Canceled }

    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string PromoCode { get; set; } 
        
        public Client Client { get; set; } 
        public List<Product> Products { get; set; } = new();
        public Delivery Delivery { get; set; } 
        public Payment Payment { get; set; } 

        public void Checkout() { }
        public void Cancel() { Status = OrderStatus.Canceled; }
        public void Pay() { Payment?.Process(); }
    }

    public class Delivery
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string CourierName { get; set; }

        public void Dispatch() { Status = "В пути"; }
        public void Track() { Console.WriteLine("Трекинг..."); }
        public void Complete() { Status = "Доставлен"; }
    }


    public abstract class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public abstract void Process();
        public virtual void Refund() { Status = "Refunded"; }
    }

    public class CardPayment : Payment
    {
        public override void Process() { Status = "Card Processed"; }
    }

    public class WalletPayment : Payment
    {
        public override void Process() { Status = "Wallet Processed"; }
    }


    public abstract class ProductFactory
    {
        public abstract Product CreateProduct(string name, decimal price);
    }

    public class PhysicalProductFactory : ProductFactory
    {
        public override Product CreateProduct(string name, decimal price)
        {
            return new Product 
            { 
                Name = name, 
                Price = price, 
                StockQuantity = 100 
            };
        }
    }
}
