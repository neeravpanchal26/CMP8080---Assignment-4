namespace AuthenticatedApi_Library;

public class Product
{
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category ProductCategory { get; set; }
}
