using Clinic.Domain.Contracts.Agreements;

namespace Clinic.Domain.Tests.Agreements;

public class AgreementTests
{
    AgreementTestBuilder SutBuilder = new();

    [Fact]
    public void Constructor_Should_CreatesAgreement_Properly()
    {
        //arrange

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo<IAgreementOptions>(SutBuilder);
    }
}