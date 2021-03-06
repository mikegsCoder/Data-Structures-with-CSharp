using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using NUnit.Framework;

public class Perf03
{

    //ContainsById
    [TestCase]
    public void ContainsById_100000_ShouldWorkFast()
    {
        IArena ar = new RoyaleArena();
        int count = 100000;
        List<Battlecard> cds = new List<Battlecard>();
        CardType[] statuses = new CardType[]
        {
            CardType.MELEE,
            CardType.RANGED,
            CardType.SPELL,
            CardType.BUILDING
        };
        Random rand = new Random();
        Stopwatch sw = new Stopwatch();
        for (int i = 0; i < count; i++)
        {
            int status = rand.Next(0, 4);
            Battlecard cd = new Battlecard(i, statuses[status],
                i.ToString(), 0, 0);
            ar.Add(cd);
            cds.Add(cd);
        }

        Assert.AreEqual(count, ar.Count);

        Stopwatch watch = new Stopwatch();
        watch.Start();

        foreach (Battlecard cd in cds)
        {
            Assert.True(ar.Contains(cd));
        }

        watch.Stop();
        long l1 = watch.ElapsedMilliseconds;

        Assert.Less(l1, 200);
    }

}
