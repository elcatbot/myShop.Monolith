namespace myShop.Tests.Application;

public static class ProductFakeList
{
    public static List<Product> GetFakeProductList()
    {
        return new List<Product>()
        {
            new Product("Fake aProduct 1", "Fake Description 1", 11, "fakeImageUrl 1", 1),
            new Product("Fake eProduct 2", "Fake Description 2", 22, "fakeImageUrl 2", 2),
            new Product("Another fake 3", "Fake Description 3", 33, "fakeImageUrl 3", 2),
            new Product("Test four 4", "Fake Description 4", 44, "fakeImageUrl 4", 3),
            new Product("eProduct test 5", "Fake Description 5", 55, "fakeImageUrl 5", 3),

        };
    }
    
    public static List<Brand> GetFakeBrandList()
    {
        return new List<Brand>()
        {
            new Brand("Fake Brand 1"),
            new Brand("Fake Brand 2"),
            new Brand("Fake Brand 3"),
        };
    }
}