using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Chainblock : IChainblock
{
    private Dictionary<int, Transaction> transactionsById;
    private Dictionary<TransactionStatus, HashSet<Transaction>> transactionsByStatus;
    private Dictionary<string, HashSet<Transaction>> transactionsBySender;
    private Dictionary<string, HashSet<Transaction>> transactionsByReceiver;
    HashSet<Transaction> insertionOrder;

    public Chainblock()
    {
        this.transactionsById = new Dictionary<int, Transaction>();
        this.transactionsByStatus = new Dictionary<TransactionStatus, HashSet<Transaction>>();
        this.transactionsBySender = new Dictionary<string, HashSet<Transaction>>();
        this.transactionsByReceiver = new Dictionary<string, HashSet<Transaction>>();
        this.insertionOrder = new HashSet<Transaction>();
    }

    public int Count => this.insertionOrder.Count;

    public void Add(Transaction tx)
    {
        if (this.Contains(tx))
        {
            return;
        }

        this.transactionsById.Add(tx.Id, tx);

        if (!this.transactionsByStatus.ContainsKey(tx.Status))
        {
            this.transactionsByStatus.Add(tx.Status, new HashSet<Transaction>());
        }

        this.transactionsByStatus[tx.Status].Add(tx);

        if (!this.transactionsBySender.ContainsKey(tx.From))
        {
            this.transactionsBySender.Add(tx.From, new HashSet<Transaction>());
        }

        this.transactionsBySender[tx.From].Add(tx);

        if (!this.transactionsByReceiver.ContainsKey(tx.To))
        {
            this.transactionsByReceiver.Add(tx.To, new HashSet<Transaction>());
        }

        this.transactionsByReceiver[tx.To].Add(tx);

        this.insertionOrder.Add(tx);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!this.Contains(id))
        {
            throw new ArgumentException();
        }

        Transaction tx = this.transactionsById[id];

        this.transactionsByStatus[tx.Status].Remove(tx);

        tx.Status = newStatus;

        if (!this.transactionsByStatus.ContainsKey(tx.Status))
        {
            this.transactionsByStatus.Add(tx.Status, new HashSet<Transaction>());
        }

        this.transactionsByStatus[tx.Status].Add(tx);
    }

    public bool Contains(Transaction tx)
    {
        return this.insertionOrder.Contains(tx);
    }

    public bool Contains(int id)
    {
        return this.transactionsById.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        return this.insertionOrder.Where(x => x.Amount >= lo && x.Amount <= hi);
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return this.transactionsById.Values.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        if (!this.transactionsByStatus.ContainsKey(status) 
            || this.transactionsByStatus.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.transactionsByStatus[status]
            .OrderByDescending(x => x.Amount)
            .Select(x => x.To);
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        if (!this.transactionsByStatus.ContainsKey(status) 
            || this.transactionsByStatus.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.transactionsByStatus[status]
            .OrderByDescending(x => x.Amount)
            .Select(x => x.From);
    }

    public Transaction GetById(int id)
    {
        if (!this.Contains(id))
        {
            throw new InvalidOperationException();
        }

        return this.transactionsById[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        if (!this.transactionsByReceiver.ContainsKey(receiver)
            || this.transactionsByReceiver[receiver].Count == 0
            || !this.transactionsByReceiver[receiver]
                .Any(x => x.Amount >= lo && x.Amount < hi))
        {
            throw new InvalidOperationException();
        }

        return this.transactionsByReceiver[receiver]
            .Where(x => x.Amount >= lo && x.Amount < hi)
            .OrderByDescending(x => x.Amount)
            .ThenBy(x => x.Id);
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        if (!this.transactionsByReceiver.ContainsKey(receiver)
            || this.transactionsByReceiver[receiver].Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.transactionsByReceiver[receiver]
            .OrderByDescending(x => x.Amount)
            .ThenBy(x => x.Id);
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        if (!this.transactionsBySender.ContainsKey(sender)
            || this.transactionsBySender[sender].Count == 0
            || !this.transactionsBySender[sender].Any(x => x.Amount > amount))
        {
            throw new InvalidOperationException();
        }

        return this.transactionsBySender[sender]
            .Where(x => x.Amount > amount)
            .OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        if (!this.transactionsBySender.ContainsKey(sender) || this.transactionsBySender[sender].Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.transactionsBySender[sender].OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        if (!this.transactionsByStatus.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }

        return this.transactionsByStatus[status].OrderByDescending(x => x.Amount);
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        if (!this.transactionsByStatus.ContainsKey(status)
            || this.transactionsByStatus[status].Count == 0)
        {
            return Enumerable.Empty<Transaction>();
        }

        return this.transactionsByStatus[status]
            .Where(x => x.Amount <= amount)
            .OrderByDescending(x => x.Amount);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        foreach (var transaction in transactionsById.Values)
        {
            yield return transaction;
        }
    }

    public void RemoveTransactionById(int id)
    {
        var toRemove = this.GetById(id);

        this.transactionsById.Remove(id);
        this.transactionsByStatus[toRemove.Status].Remove(toRemove);
        this.transactionsBySender[toRemove.From].Remove(toRemove);
        this.transactionsByReceiver[toRemove.To].Remove(toRemove);
        this.insertionOrder.Remove(toRemove);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

