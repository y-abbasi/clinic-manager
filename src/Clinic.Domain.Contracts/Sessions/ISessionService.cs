namespace Clinic.Domain.Contracts.Sessions;

public interface ISessionService
{
    Task<ISession?> GetAsync(SessionId id);
}