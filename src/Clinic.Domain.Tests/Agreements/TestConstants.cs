using Core.SharedKernels;

namespace Clinic.Domain.Tests.Agreements;

public static class TestConstants
{

    public static Range<DateOnly> ValidAgreementPeriod => 
        new(new DateOnly(2024, 1, 1), new DateOnly(2050, 1, 1));
}