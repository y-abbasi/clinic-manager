using Core.Domain;
using Core.SharedKernels;

namespace Clinic.Domain.Parties.Exceptions;

public class ClientDailyAppointmentLimitExceeded : DomainException
{
    public ClientDailyAppointmentLimitExceeded() : base("APT-01", "مشتری نمی‌تواند بیش از ۲ قرار ملاقات در یک روز داشته باشد.")
    {
    }
} 