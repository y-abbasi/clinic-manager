using System.Collections.Immutable;
using Clinic.Domain.Agreements.Exceptions;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Sessions;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Sessions;
using Core.Domain;
using Core.SharedKernels;

namespace Clinic.Domain.Agreements;

public partial class Agreement : AggregateRoot<AgreementId>, IAgreement
{
    public Agreement(IAgreementCreatorOptions options)
    {
        Id = new AgreementId(Guid.NewGuid());
        CheckInvariants(options);
        SetupProperties(options);
        //CheckPostConditions();
    }

    private void SetupProperties(IAgreementCreatorOptions options)
    {
        OrganizationId = options.OrganizationId;
        PractitionerId = options.PractitionerId;
        AgreementPeriod = options.AgreementPeriod;
        Schedules = options.Schedules.GroupBy(s => s.DayOfWeek)
            .Select(a =>
                new Schedule(a.Key,
                    a.SelectMany(s => s.WorkingTimes).ToImmutableList()))
            .ToImmutableList();
    }

    public PartyId OrganizationId { get; private set; }
    public PartyId PractitionerId { get; private set; }
    public Range<DateOnly> AgreementPeriod { get; private set; }
    private ImmutableList<Schedule> Schedules { get; set; } = ImmutableList<Schedule>.Empty;

    public async Task<ISession> GetOrCreateSessionAsync(ISessionService sessionService, DateTime date)
    {
        return await sessionService.GetAsync(new SessionId(OrganizationId, PractitionerId, DateOnly.FromDateTime(date))) ??
              await CreateNewSession(date);
    }

    private async Task<ISession> CreateNewSession(DateTime date)
    {
        if (Schedules.All(w => w.DayOfWeek != date.DayOfWeek))
            throw new OrganizationOrPractitionerNotAvailableAtTheRequestedDate();
        return new Session(new SessionManager()
            .WithOrganization(OrganizationId)
            .WithPractitioner(PractitionerId)
            .WithDate(DateOnly.FromDateTime(date)));
    }

    IEnumerable<IScheduleOption> IAgreementOptions.Schedules => Schedules;
}