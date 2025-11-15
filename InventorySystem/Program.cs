namespace InventorySystem
{
    class Program
    {

        abstract class Product
        {
            public string Name {get; set;}
            public decimal Price {get; set;}

            public int ProductId {get; private set;}
            
            // constructor for the Product class
            public Product(string name, decimal price, int productId )
            {
                this.Name = name;
                this.Price = price;
                this.ProductId = productId;
            }

            public abstract void DisplayDetails();

        }

        class BookProduct: Product
        {
            public string Author{get;set;}


            public BookProduct(string name, decimal price, int productId, string author): base(name, price, productId)
            {
                this.Author = author;
            }

            public override void DisplayDetails()
            {
                Console.WriteLine($"  [{ProductId}] {Name} (Book)");
                Console.WriteLine($"  |-- Author: {Author}");
                Console.WriteLine($"  |-- Price: {Price:C}");
            }
             
        }

        class ElectronicProduct: Product
        {
            public double Warranty{get; set;}

            public ElectronicProduct(string name, decimal price, int productId, double warranty): base(name, price, productId)
            {
                this.Warranty = warranty;
            }
            public override void DisplayDetails()
            {
                Console.WriteLine($"  [{ProductId}] {Name} (Electronics)");
                Console.WriteLine($"  |-- Warranty: {Warranty}");
                Console.WriteLine($"  |-- Price: {Price:C}");
            }
        }

        private static Product[] inventory = new Product[2];

        static void Initializer()
        {
            inventory[0] = new BookProduct("name", 230m, 130, "kevlmiso somm");
            inventory[1] = new ElectronicProduct("yours", 133.33m, 122, 12);
        }

        static void DisplayInventory(){
            foreach(Product item in inventory){
                if(item != null){
                    item.DisplayDetails();
                }
            }
        }
        static void Main()
        {
            Initializer();
            Console.WriteLine("These are the inventory items:");
            DisplayInventory();
            
        }
    }
}