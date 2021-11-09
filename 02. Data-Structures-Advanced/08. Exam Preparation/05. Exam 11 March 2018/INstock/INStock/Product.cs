using System;

public class Product : IComparable<Product>
{
    public Product(string label, double price, int quantity)
    {
        this.Label = label;
        this.Price = price;
        this.Quantity = quantity;
    }

    public string Label { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public int CompareTo(Product other)
    {
        return this.Label.CompareTo(other.Label);
    }

    public override bool Equals(object obj)
    {
        if (this == obj) return true;
        if (obj == null) return false;
        Product that = (Product)obj;
        return Label == that.Label;
    }

    public override int GetHashCode()
    {
        return this.Label.GetHashCode();
    }
}