using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class Test24
{
    //GetBySenderWithTransactionStatus
    [TestCase]
    public void GetAllSendersWithTransactionStatus_ShouldWorkCorrectly()
    {
        //Arrange
        IChainblock cb = new Chainblock();
        Transaction tx1 = new Transaction(5, TransactionStatus.Successfull, "joro", "pesho", 1);
        Transaction tx2 = new Transaction(6, TransactionStatus.Aborted, "joro", "pesho", 5.5);
        Transaction tx3 = new Transaction(7, TransactionStatus.Successfull, "joro", "pesho", 5.5);
        Transaction tx4 = new Transaction(12, TransactionStatus.Unauthorized, "boro", "pesho", 15.6);
        Transaction tx5 = new Transaction(15, TransactionStatus.Unauthorized, "moro", "pesho", 7.8);
        List<string> expected = new List<string>()
        {
            "boro", "moro"
        };
        //Act
        cb.Add(tx1);
        cb.Add(tx3);
        cb.Add(tx2);
        cb.Add(tx4);
        cb.Add(tx5);
        List<string> actual = cb
            .GetAllSendersWithTransactionStatus(TransactionStatus.Unauthorized)
            .ToList();
        //Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}

