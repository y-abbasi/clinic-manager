using Clinic.Domain.Contracts.Patients;
using FluentAssertions;

namespace Clinic.Domain.Tests.Patients;

public class PatientTests
{
    private PatientTestBuilder CreateSutBuilder()
    {
        return new();
    }

    [Fact]
    public void PatientCreatedSuccessfully()
    {
        //arrange
        var sutBuilder = CreateSutBuilder();
        
        //act
        var sut = sutBuilder.Build();
        
        //assert
        sut.Should().BeEquivalentTo(sutBuilder);
        sut.Id.Should().NotBeNull();
    }
}

