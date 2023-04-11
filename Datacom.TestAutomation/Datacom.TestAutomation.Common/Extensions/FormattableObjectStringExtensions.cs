using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using ServiceStack.Host;

namespace Datacom.TestAutomation.Common
{
    public static class FormattableObjectStringExtensions
    {
        public static string ToJsonString(this object data, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(data, options);
        }

        public static string ToFormattedString(this object data)
        {
            if (data == null) return string.Empty;

            var outStr = new StringBuilder();
            Type type = data.GetType();
            PropertyInfo[] props = type.GetProperties();
            int longestName = props.OrderByDescending(p => p.Name.Length).First().Name.Length;
            outStr.Append($"\n{type.Name}:\n");
            outStr.Append($"\n\t{"Name".PadRight(longestName, ' ')} Value");
            outStr.Append($"\n\t{new string('-', longestName).PadRight(longestName, ' ')} {new string('-', longestName)}");

            foreach (var prop in props)
            {
                if (!string.IsNullOrEmpty(prop.GetValue(data, null)?.ToString()) && prop.GetValue(data, null)?.ToString() != Guid.Empty.ToString())
                    outStr.Append($"\n\t{prop.Name.PadRight(longestName, ' ')} {prop.GetValue(data, null)}");
            }

            return outStr.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        public static string ToString(this object source, string format, IFormatProvider? provider = null)
        {
            Regex r = new(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
              RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            List<object> values = new();
            string rewrittenFormat = r.Replace(format, delegate (Match m)
            {
                Group startGroup = m.Groups["start"];
                Group propertyGroup = m.Groups["property"];
                Group formatGroup = m.Groups["format"];
                Group endGroup = m.Groups["end"];

                values.Add((propertyGroup.Value == "0")
                  ? source
                  : DataBinder.Eval(source, propertyGroup.Value));

                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value
                  + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }
    }
}
