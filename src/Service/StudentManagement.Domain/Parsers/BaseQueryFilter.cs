using System.Text.RegularExpressions;

namespace StudentManagement.Domain.Parsers
{
	public abstract record class BaseQueryFilter
	{
		protected static readonly Regex MultiFilterRegex = new(
			@"equals\((\w+),'([^']+)'\)|filter\[(\w+)\]=equals\(id,'([^']+)'\)|filter\[(\w+)\]=contains\(type,'([^']+)'\)",
			RegexOptions.Compiled,
			TimeSpan.FromMilliseconds(100));

		protected abstract List<string> ValidEqualsAttributes { get; }
		protected abstract List<string> ValidContainsAttributes { get; }

		public bool TryParseMultiFilter(string filter, out Dictionary<string, string>? parsedQuery, out string? error)
		{
			if (string.IsNullOrWhiteSpace(filter))
			{
				parsedQuery = new();
				error = null;
				return true;
			}

			var matches = MultiFilterRegex.Matches(filter);

			var results = new Dictionary<string, string>();

			foreach (Match match in matches)
			{
				if (!Parsefilter(match, results))
				{
					parsedQuery = null;
					error = "Invalid query parameter 'filter'. Please check API spec for valid filter query syntax.";
					return false;
				}
			}

			parsedQuery = results;
			error = null;
			return true;
		}

		private bool Parsefilter(Match match, Dictionary<string, string> results)
		{
			var propertyName = string.Empty;
			if (match.Groups[1].Success)
			{
				propertyName = match.Groups[1].Value;
				if (!ValidEqualsAttributes.Contains(propertyName))
					return false;
				results[propertyName] = match.Groups[2].Value;
			}
			else if (match.Groups[3].Success)
			{
				propertyName = match.Groups[3].Value;
				if (!ValidEqualsAttributes.Contains(propertyName))
					return false;
				results[propertyName] = match.Groups[4].Value;
			}
			else if (match.Groups[5].Success)
			{
				propertyName = match.Groups[5].Value;
				if (!ValidContainsAttributes.Contains(propertyName))
					return false;
				results[propertyName] = match.Groups[6].Value;
			}

			return true;
		}
	}
}
