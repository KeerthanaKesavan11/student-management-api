
namespace StudentManagement.Domain.Parsers
{
    public record class EnrollmentQueryFilter : BaseQueryFilter
    {
        protected override List<string> ValidEqualsAttributes { get; } = new List<string> { "StudentName" };
        protected override List<string> ValidContainsAttributes { get; } = new List<string> { "StudentName" };
    }
}
