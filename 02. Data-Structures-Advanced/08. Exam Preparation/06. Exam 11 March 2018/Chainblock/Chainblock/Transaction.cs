using System;

public class Transaction : IComparable<Transaction>
{
    public int Id { get; set; }

    public TransactionStatus Status { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public double Amount { get; set; }

    public Transaction(int id, TransactionStatus st, string from, string to, double amount)
    {
        this.Id = id;
        this.Status = st;
        this.From = from;
        this.To = to;
        this.Amount = amount;
    }

    public int CompareTo(Transaction other)
    {
        return other.Amount.CompareTo(this.Amount);
    }

    public override bool Equals(object obj)
    {
        if (this == obj) return true;
        if (obj == null) return false;
        Transaction that = (Transaction)obj;
        return Id == that.Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}