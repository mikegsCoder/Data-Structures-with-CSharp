using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ThreadExecutor : IScheduler
{
    private Dictionary<int, Task> byId;
    private Dictionary<Priority, HashSet<Task>> byPriority;
    private List<Task> byInsertionOrder;
    private int cyclesCount;

    public ThreadExecutor()
    {
        this.byId = new Dictionary<int, Task>();
        this.byPriority = new Dictionary<Priority, HashSet<Task>>();
        this.byInsertionOrder = new List<Task>();
        this.cyclesCount = 0;
    }

    public int Count => this.byId.Count;

    public void ChangePriority(int id, Priority newPriority)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var toChange = this.byId[id];

        this.byPriority[toChange.TaskPriority].Remove(toChange);

        toChange.TaskPriority = newPriority;

        if (!this.byPriority.ContainsKey(newPriority))
        {
            this.byPriority.Add(newPriority, new HashSet<Task>());
        }

        this.byPriority[newPriority].Add(toChange);
    }

    public bool Contains(Task task) 
    {
        return this.byId.ContainsKey(task.Id);
    }
        
    public int Cycle(int cycles)
    {
        if (this.Count == 0)
        {
            throw new ArgumentException();
        }

        this.cyclesCount += cycles;

        int tasksCompleted = 0;

        this.byId.Values.Where(x => (x.Consumption - cyclesCount) <= 0).ToList().ForEach(x =>
            { 
                this.byPriority[x.TaskPriority].Remove(x);
                this.byInsertionOrder.Remove(x);
                tasksCompleted++;
            });

        this.byId = this.byId
            .Where(x => (x.Value.Consumption - cyclesCount) > 0)
            .ToDictionary(k => k.Key, v => v.Value);

        return tasksCompleted;
    }

    public void Execute(Task task)
    {
        if (this.Contains(task))
        {
            throw new ArgumentException();
        }

        this.byId.Add(task.Id, task);

        if (!this.byPriority.ContainsKey(task.TaskPriority))
        {
            this.byPriority.Add(task.TaskPriority, new HashSet<Task>());
        }

        this.byPriority[task.TaskPriority].Add(task);

        this.byInsertionOrder.Add(task);
    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
        if (inclusive)
        {
            return this.byId.Values
                .Where(x => x.Consumption >= lo && x.Consumption <= hi)
                .OrderBy(x => x);
        }

        return this.byId.Values
                .Where(x => x.Consumption > lo && x.Consumption < hi)
                .OrderBy(x => x);
    }

    public Task GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.byId[id];
    }

    public Task GetByIndex(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.byInsertionOrder[index];
    }

    public IEnumerable<Task> GetByPriority(Priority type)
    {
        if (!this.byPriority.ContainsKey(type) || this.byPriority[type].Count == 0)
        {
            return Enumerable.Empty<Task>();
        }

        return this.byPriority[type].OrderByDescending(x => x.Id);
    }


    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
    {
        if (!this.byPriority.ContainsKey(priority) || this.byPriority[priority].Count == 0)
        {
            return Enumerable.Empty<Task>();
        }

        return this.byPriority[priority]
            .Where(x => x.Consumption >= lo)
            .OrderByDescending(x => x.Id);
    }

    public IEnumerator<Task> GetEnumerator()
    {
        foreach (var task in byInsertionOrder)
        {
            yield return task;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
