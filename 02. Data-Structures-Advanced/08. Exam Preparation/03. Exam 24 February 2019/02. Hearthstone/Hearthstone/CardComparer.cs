using System;
using System.Collections.Generic;

namespace Hearthstone
{
    public class CardComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            var cmp = 0;

            var length = Math.Min(x.Name.Length, y.Name.Length);
            var xLastIndex = x.Name.Length - 1;
            var yLastIndex = y.Name.Length - 1;

            for (var i = 0; i < length; i++)
            {
                var xChar = (int)x.Name[xLastIndex - i];
                var yChar = (int)y.Name[yLastIndex - i];

                cmp = xChar.CompareTo(yChar);

                if (cmp != 0)
                {
                    return cmp;
                }
            }

            if (x.Name.Length.CompareTo(y.Name.Length) != 0)
            {
                return x.Name.Length.CompareTo(y.Name.Length);
            }

            return x.Level.CompareTo(y.Level);
        }
    }
}
