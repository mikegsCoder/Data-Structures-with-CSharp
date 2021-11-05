using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;

public class Board : IBoard
{
    private Dictionary<string, Card> byName;
    private CardComparer cardComparer;

    public Board()
    {
        this.byName = new Dictionary<string, Card>();
        this.cardComparer = new CardComparer();
    }

    public bool Contains(string name)
    {
        return this.byName.ContainsKey(name);
    }

    public int Count()
    {
        return this.byName.Count;
    }

    public void Draw(Card card)
    {
        if (this.Contains(card.Name))
        {
            throw new ArgumentException();
        }

        this.byName.Add(card.Name, card);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!this.Contains(attackerCardName) || !this.Contains(attackedCardName))
        {
            throw new ArgumentException();
        }

        var attackerCard = this.byName[attackerCardName];
        var attackedCard = this.byName[attackedCardName];

        if (attackerCard.Level != attackedCard.Level || attackerCard.Health <= 0)
        {
            throw new ArgumentException();
        }

        if (attackedCard.Health <= 0)
        {
            return;
        }

        attackedCard.Health -= attackerCard.Damage;

        if (attackedCard.Health <= 0)
        {
            attackerCard.Score += attackedCard.Level;
        }
    }

    public void Remove(string name)
    {
        if (!this.Contains(name))
        {
            throw new ArgumentException();
        }

        this.byName.Remove(name);
    }

    public void RemoveDeath()
    {
        this.byName = this.byName.Values.Where(x => x.Health > 0).ToDictionary(x => x.Name, x => x);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        return this.byName.Values
            .Where(x => x.Score >= start && x.Score <= end)
            .OrderByDescending(x => x.Level);
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        return this.byName.Values
           .Where(x => x.Name.StartsWith(prefix))
           .OrderBy(x => x, this.cardComparer);
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return this.byName.Values
            .Where(x => x.Level == level)
            .OrderByDescending(x => x.Score);
    }

    public void Heal(int health)
    {
        Card toHeal = this.byName.Values
            .OrderBy(x => x.Health)
            .First();

        toHeal.Health += health;
    }
}