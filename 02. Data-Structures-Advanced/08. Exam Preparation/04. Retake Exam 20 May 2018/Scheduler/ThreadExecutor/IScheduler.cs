using System.Collections.Generic;

public interface IScheduler : IEnumerable<Task>
{
    int Count { get; }

    void Execute(Task task);

    bool Contains(Task task);

    Task GetById(int id);

    Task GetByIndex(int index);

    int Cycle(int cycles);

    void ChangePriority(int id, Priority newPriority);

    IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive);

    IEnumerable<Task> GetByPriority(Priority type);
    IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo);
}
