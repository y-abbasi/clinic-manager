namespace Core.SharedKernels;

public record Range<T> where T : IComparable<T>
{
    public Range(T Start, T End)
    {
        if (Start.CompareTo(End) > 0) throw new RangeIsInvalid();
        this.Start = Start;
        this.End = End;
    }

    public bool InRange(T value) => Start.CompareTo(value) <= 0 && value.CompareTo(End) <= 0;
    public T Start { get; init; }
    public T End { get; init; }

    public void Deconstruct(out T Start, out T End)
    {
        Start = this.Start;
        End = this.End;
    }

    public bool HasOverlap(Range<T> another)
    {
        return another.InRange(this.Start) || another.InRange(this.End);
    }
}