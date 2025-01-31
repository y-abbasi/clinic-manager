using Clinic.Domain.Contracts.Agreements;
using Core.SharedKernels;

namespace Clinic.Domain.Tests.Agreements;

public static class TestConstants
{

    public static Range<DateOnly> ValidAgreementPeriod => 
        new(new DateOnly(2024, 1, 1), new DateOnly(2050, 1, 1));

    public static DateTime SomeDateTimeAtSunday_9h_30mAm => new(2027, 1, 3, 9, 30, 0);
    public static DateTime SomeDateTimeAtWednesday_9h_30mAm => new(2027, 1, 6, 9, 30, 0);
    public static DateTime SomeDateTimeAtMonday_9h_30mAm => new(2027, 1, 4, 9, 30, 0);

    public static ScheduleOption ScheduleAtMondayFrom8_00To20_00 => new(DayOfWeek.Monday, [
        new Range<TimeOnly>(new TimeOnly(8, 0), new TimeOnly(20, 0))
    ]);
    public static ScheduleOption ScheduleAtWednesdayFrom8_00To20_00 => new(DayOfWeek.Wednesday, [
        new Range<TimeOnly>(new TimeOnly(8, 0), new TimeOnly(20, 0))
    ]);

}