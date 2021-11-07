using System;

public class Task : IComparable<Task>
{
    public Task(int id, int consumption, Priority priority)
    {
        this.Id = id;
        this.Consumption = consumption;
        this.TaskPriority = priority;
    }

    public int Id { get; private set; }

    public int Consumption { get; private set; }

    public Priority TaskPriority { get; set; }

    public int CompareTo(Task other)
    {
        int compare = this.Consumption.CompareTo(other.Consumption);
        
        if (compare == 0)
        {
            return other.TaskPriority.CompareTo(this.TaskPriority);
        }

        return compare;
    }

    public override bool Equals(object obj)
    {
        if (this == obj) return true;
        if (obj == null) return false;
        Task that = (Task)obj;
        return Id == that.Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}