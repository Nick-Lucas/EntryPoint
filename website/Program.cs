using MarkdownDeep;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using EntryPoint;
using System.Reflection;

namespace Website {

    class Program {

        static void Main(string[] args) {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en");

            var layout = File.ReadAllText("../layout.html");
            layout = layout.Replace("{{description}}", "Composable CLI Argument Parser");
            layout = layout.Replace("{{version}}", GetVersion());
            layout = layout.Replace("{{updated_on}}", DateTime.Now.ToString("MMM d, yyyy"));

            var headerIdList = new List<string>();
            var body = new Markdown().Transform(PreprocessBody());
            body = AddHeaderAnchors(body, headerIdList);            

            layout = layout.Replace("{{body}}", body);
            ValidateHeaderAnchors(headerIdList, layout);
            File.WriteAllText("../www/index.html", layout);
        }

        static string GetVersion() {
            return typeof(EntryPointApi)
                .Assembly
                .GetName()
                .Version
                .ToString()
                .TrimEnd(".0".ToCharArray());
        }

        static string PreprocessBody() {
            var source = File.ReadAllLines("../Body.cs");
            var result = new List<string>();
            List<string> currentCode = null;

            foreach(var line in source) {
                if(currentCode == null && line.Trim() == "#if CODE") {
                    result.Add("");
                    currentCode = new List<string>();
                    continue;
                }

                if(currentCode != null && line.Trim() == "#endif") {
                    var minIndent = currentCode
                        .Where(c => !String.IsNullOrWhiteSpace(c))
                        .Min(s => Regex.Match(s, "^\\s*").Length) - 4;

                    result.AddRange(currentCode.Select(c => {
                        if(String.IsNullOrWhiteSpace(c))
                            return "";
                        return c.Substring(minIndent);
                    }));

                    result.Add("");

                    currentCode = null;
                    continue;
                }

                if(currentCode != null) {
                    currentCode.Add(line);
                    continue;
                }

                var trimmed = line.Trim();
                if(!trimmed.StartsWith("///"))
                    continue;

                if(trimmed.Length < 4)
                    result.Add("");
                else
                    result.Add(trimmed.Substring(4));
            }

            return String.Join("\n", result);
        }

        static string AddHeaderAnchors(string html, List<string> idList) {
            return Regex.Replace(html, @"(\<h2)([^>]*\>(.+?)\</h2\>)", m => {
                var id = Regex.Replace(m.Groups[3].Value.ToLower(), "\\W+", "-").Trim('-');
                idList.Add(id);
                return m.Groups[1].Value + " id=\"" + id + "\"" + m.Groups[2].Value;
            });        
        }

        static void ValidateHeaderAnchors(List<string> knownIdList, string html) {
            var refs = Regex.Matches(html, "href=\"#(.+?)\"").Cast<Match>().Select(m => m.Groups[1].Value);
            var wrong = refs.Except(knownIdList);
            if(wrong.Any())
                // If this throws - Please ensure you've updated any links to page sections after changing header titles.
                // "Getting Started: CRUD" would become "#getting-started-crud" as a link
                throw new Exception("Wrong header refs found.  " + String.Join(" ", wrong));
        }

    }

}
