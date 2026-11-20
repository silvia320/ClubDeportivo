namespace ClubDeportivo.Domain.Common;

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
}