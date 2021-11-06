using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    Dictionary<int, Competitor> competitors;
    Dictionary<int, Competition> competitions;

    public Olympics()
    {
        this.competitors = new Dictionary<int, Competitor>();
        this.competitions = new Dictionary<int, Competition>();
    }

    public int CompetitorsCount()
    {
        return this.competitors.Count;
    }

    public int CompetitionsCount()
    {
        return this.competitions.Count;
    }

    public void AddCompetitor(int id, string name)
    {
        if (this.competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.competitors.Add(id, new Competitor(id, name));
    }

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.competitions.Add(id, new Competition(name, id, participantsLimit));
    }

    public Competition GetCompetition(int id)
    {
        if (!this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.competitions[id];
    }

    public void Compete(int competitorId, int competitionId)
    {
        this.CheckCompetitionAndCompetitor(competitorId, competitionId);

        this.competitions[competitionId].AddCompetitor(this.competitors[competitorId]);
    }

    private void CheckCompetitionAndCompetitor(int competitorId, int competitionId)
    {
        if (!this.competitors.ContainsKey(competitorId) || !this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        this.CheckCompetitionAndCompetitor(competitorId, competitionId);

        var isSuccess = this.competitions[competitionId].Disqualify(this.competitors[competitorId]);

        if (!isSuccess)
        {
            throw new ArgumentException();
        }
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        var result = this.competitors.Values.Where(x => x.Name == name).OrderBy(x => x.Id);

        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result;
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        return this.competitors.Values
            .Where(x => x.TotalScore > min && x.TotalScore <= max)
            .OrderBy(x => x.Id);
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        return this.competitors.Values
            .Where(x => x.Name.Length >= min && x.Name.Length <= max)
            .OrderBy(x => x.Id);
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        return this.competitions[competitionId].Competitors.Contains(comp);
    }
}