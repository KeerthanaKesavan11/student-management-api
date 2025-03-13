using System.Collections.Generic;

namespace StudentManagement.Domain.Parsers
{
    public record class StudentQueryFilter : BaseQueryFilter
    {
        protected override List<string> ValidEqualsAttributes { get; } = new List<string> { "StudentName" };
        protected override List<string> ValidContainsAttributes { get; } = new List<string> { "StudentName" };
    }
}