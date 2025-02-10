using System.Reflection;
using Core.SharedKernels;
using FluentAssertions;

namespace Core.Tests;

public class RangeTests
{
    [Theory]
    [InlineData(10, 10, typeof(int))]
    [InlineData(10, 11, typeof(long))]
    [InlineData("2025-1-1", "2025-01-02", typeof(DateTime))]
    [InlineData("2025-1-1", "2025-01-02", typeof(DateOnly))]
    [InlineData("9:00:00", "9:00:01", typeof(TimeOnly))]
    public void Constructor_Should_Create_Range_Properly(object start, object end, Type genericType)
    {
        //arrange
        var ctor = typeof(Range<>).MakeGenericType(genericType).GetConstructors()[0];
        start = ChangeType(start, genericType);
        end = ChangeType(end, genericType);

        //act
        var range = ctor.Invoke([start, end]);

        //assert
        range.Should().BeEquivalentTo(new { Start = start, End = end });
    }

    [Theory]
    [InlineData(10, 1, typeof(int))]
    [InlineData(10, 9, typeof(int))]
    [InlineData("2025-1-2", "2025-01-01", typeof(DateTime))]
    [InlineData("2025-1-1", "2024-01-01", typeof(DateOnly))]
    [InlineData("9:00:00", "8:59:59", typeof(TimeOnly))]
    public void Constructor_Should_Throw_Exception_If_Start_Is_After_End(object start, object end, Type genericType)
    {
        //arrange
        var ctor = typeof(Range<>).MakeGenericType(genericType).GetConstructors()[0];
        //act
        var action = () => ctor.Invoke([ChangeType(start, genericType), ChangeType(end, genericType)]);

        //assert
        action.Should().Throw<TargetInvocationException>().And.InnerException.Should().BeOfType<RangeIsInvalid>();
    }

    [Theory]
    [InlineData(10, 10, 10, typeof(int))]
    [InlineData(10, 11, 10, typeof(long))]
    [InlineData("2025-1-1", "2025-01-02", "2025-1-1", typeof(DateTime))]
    [InlineData("2025-1-1", "2025-01-02", "2025-1-1", typeof(DateOnly))]
    [InlineData("9:00:00", "9:00:01", "9:00:01", typeof(TimeOnly))]
    public void InRange_Should_Return_True_If_Value_Be_In_Range(object start, object end, object value,
        Type genericType)
    {
        //arrange
        var ctor = typeof(Range<>).MakeGenericType(genericType).GetConstructors()[0];
        start = ChangeType(start, genericType);
        end = ChangeType(end, genericType);
        value = ChangeType(value, genericType);
        var range = ctor.Invoke([start, end]);

        //act
        var result = (bool)range.GetType().GetMethod("InRange").Invoke(range, parameters: [value]);

        //assert
        result.Should().Be(true);
    }

    [Theory]
    [InlineData(10, 10, 9, typeof(int))]
    [InlineData(10, 11, 12, typeof(long))]
    [InlineData("2025-1-1", "2025-01-02", "2025-1-3", typeof(DateTime))]
    [InlineData("2025-1-1", "2025-01-02", "2024-12-30", typeof(DateOnly))]
    [InlineData("9:00:00", "9:00:01", "9:00:02", typeof(TimeOnly))]
    public void InRange_Should_Return_False_If_Value_Not_Be_In_Range(object start, object end, object value,
        Type genericType)
    {
        //arrange
        var ctor = typeof(Range<>).MakeGenericType(genericType).GetConstructors()[0];
        start = ChangeType(start, genericType);
        end = ChangeType(end, genericType);
        value = ChangeType(value, genericType);
        var range = ctor.Invoke([start, end]);

        //act
        var result = (bool)range.GetType().GetMethod("InRange").Invoke(range, parameters: [value]);

        //assert
        result.Should().Be(false);
    }

    [Theory]
    [InlineData(5, 10, 10, 12)]
    [InlineData(5, 10, 7, 12)]
    [InlineData(5, 10, 7, 8)]
    [InlineData(5, 10, 1, 5)]
    [InlineData(5, 10, 1, 12)]
    [InlineData(10, 12, 5, 10)]
    [InlineData(7, 12, 5, 10)]
    [InlineData(7, 8, 5, 10)]
    [InlineData(1, 5, 5, 10)]
    [InlineData(1, 12, 5, 10)]
    public void HasOverlap_Should_Return_True_If_Overlap_Exists_Between_Two_Ranges(int start1, int end1,
        int start2, int end2)
    {
        //arrange
        var range1 = new Range<int>(start1, end1);
        var range2 = new Range<int>(start2, end2);

        //act
        var result = range1.HasOverlap(range2);

        //assert
        result.Should().Be(true);
    }

    [Theory]
    [InlineData(5, 10, 11, 12)]
    [InlineData(5, 10, 1, 4)]
    public void HasOverlap_Should_Return_False_If_Overlap_Exists_Between_Two_Ranges(int start1, int end1,
        int start2, int end2)
    {
        //arrange
        var range1 = new Range<int>(start1, end1);
        var range2 = new Range<int>(start2, end2);

        //act
        var result = range1.HasOverlap(range2);

        //assert
        result.Should().Be(false);
    }


    public static object ChangeType(object value, Type targetType)
    {
        if (targetType == typeof(TimeOnly) && value is string tValue)
            return TimeOnly.Parse(tValue);
        if (targetType == typeof(DateOnly) && value is string dValue)
            return DateOnly.Parse(dValue);
        return Convert.ChangeType(value, targetType);
    }
}