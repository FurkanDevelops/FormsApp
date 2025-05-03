namespace FormsApp.Models
{
    public class Repository
    {
        //Private bir list tanımladık
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();
        static Repository(){
            _categories.Add(new Category{CategoryId=1,Name="Phone"});
            _categories.Add(new Category{CategoryId=2,Name="Mackbook"});

            _products.Add(new Product{ProductId=1,Name="Iphone 14",Price=2000,IsActive = true, ImageUrl="phone1.png",CategoryId=1});
            _products.Add(new Product{ProductId=2,Name="Iphone 13",Price=1500,IsActive = true, ImageUrl="phone2.png",CategoryId=1});
            _products.Add(new Product{ProductId=3,Name="Iphone 12",Price=1200,IsActive = true, ImageUrl="phone3.png",CategoryId=1});
            _products.Add(new Product{ProductId=4,Name="Iphone 11",Price=1000,IsActive = true, ImageUrl="phone4.png",CategoryId=1});
            _products.Add(new Product{ProductId=5,Name="Mackbook 1 ",Price=3000,IsActive = true, ImageUrl="mack1.png",CategoryId=2});
            _products.Add(new Product{ProductId=6,Name="Mackbook 2",Price=800,IsActive = true, ImageUrl="mack2.png",CategoryId=2});
            _products.Add(new Product{ProductId=7,Name="Mackbook 3",Price=600,IsActive = true, ImageUrl="mack3.png",CategoryId=2});
            _products.Add(new Product{ProductId=8,Name="Mackbook 4 ",Price=3500,IsActive = true, ImageUrl="mack4.png",CategoryId=2});

        }   

        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }
        public static List<Product> Products 
        {
            get
            {
                // dışarıya açmak için bu kısım kullanıldı
                // ve dışarıdan erişim sağlandı.
                return _products;
            }
        }
        public static List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }
        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);
            if (entity != null)
            {
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.IsActive = updatedProduct.IsActive;
                entity.ImageUrl = updatedProduct.ImageUrl;
                entity.CategoryId = updatedProduct.CategoryId;
            }
        }
        public static void DeleteProduct(Product entity)
        {
            var productToRemove = _products.FirstOrDefault(p => p.ProductId == entity.ProductId);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
        }
    }
}