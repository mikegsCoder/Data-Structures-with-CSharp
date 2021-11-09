using System.Collections.Generic;

public interface IProductStock : IEnumerable<Product>
{
    //Properties
    int Count { get; }
    
    //Validations
    bool Contains(Product product);
    
    //Modifications
    void Add(Product product);

    void ChangeQuantity(string product, int quantity);

    //Retrievals
    Product Find(int index);

    Product FindByLabel(string label);

    IEnumerable<Product> FindFirstByAlphabeticalOrder(int count);

    //Querying
    IEnumerable<Product> FindAllInRange(double lo, double hi);

    IEnumerable<Product> FindAllByPrice(double price);

    IEnumerable<Product> FindFirstMostExpensiveProducts(int count);

    IEnumerable<Product> FindAllByQuantity(int quantity);
}