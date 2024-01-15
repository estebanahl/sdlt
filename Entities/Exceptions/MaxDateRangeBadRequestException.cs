namespace sdlt.Entities.Exceptions;

public sealed class MaxDateRangeBadRequestException : BadRequestException
{
    public MaxDateRangeBadRequestException()
    : base("Max date/hour can't be less than min date/hour.")
    {
    }
}