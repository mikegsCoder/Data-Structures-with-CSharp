using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Instock : IProductStock
{
    private Dictionary<string, Product> byLabel;
    private List<Product> byIndex;
    private Dictionary<int, HashSet<Product>> byQuantity;
    private SortedSet<Product> sortedLabels;

    public Instock()
    {
        this.byLabel = new Dictionary<string, Product>();
        this.byIndex = new List<Product>();
        this.byQuantity = new Dictionary<int, HashSet<Product>>();
        this.sortedLabels = new SortedSet<Product>();
    }

    public int Count => this.byLabel.Count;

    public void Add(Product product)
    {
        if (this.Contains(product))
        {
            return;
        }

        this.byLabel.Add(product.Label, product);

        this.byIndex.Add(product);

        if (!this.byQuantity.ContainsKey(product.Quantity))
        {
            this.byQuantity.Add(product.Quantity, new HashSet<Product>());
        }

        this.byQuantity[product.Quantity].Add(product);

        this.sortedLabels.Add(product);
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.byLabel.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        var toChange = this.byLabel[product];

        this.byQuantity[toChange.Quantity].Remove(toChange);

        toChange.Quantity = quantity;

        if (!this.byQuantity.ContainsKey(quantity))
        {
            this.byQuantity.Add(quantity, new HashSet<Product>());
        }

        this.byQuantity[quantity].Add(toChange);
    }

    public bool Contains(Product product)
    {
        return this.byLabel.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index < 0 || index >= this.byIndex.Count)
        {
            throw new IndexOutOfRangeException();
        }

        return this.byIndex[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        return this.byLabel.Values.Where(x => x.Price == price);
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        if (!this.byQuantity.ContainsKey(quantity) || this.byQuantity[quantity].Count == 0)
        {
            return Enumerable.Empty<Product>();
        }

        return this.byQuantity[quantity];
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        return this.byLabel.Values
            .Where(x => x.Price > lo && x.Price <= hi)
            .OrderByDescending(x => x.Price);
    }

    public Product FindByLabel(string label)
    {
        if (!this.byLabel.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return this.byLabel[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count < 0 || count > this.Count)
        {            
            throw new ArgumentException();
        }

        return this.sortedLabels.Take(count);
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentException();
        }

        return this.byLabel.Values.OrderByDescending(x => x.Price).Take(count);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in this.byLabel.Values)
        {
            yield return product;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }    
}