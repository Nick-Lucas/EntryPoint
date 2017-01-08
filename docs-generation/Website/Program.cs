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

            var files = GetFiles();
            foreach (var file in files) {
                var fileName = GetFileName(file);
                ProduceFile(file, fileName);
            }
        }

        static List<string> GetFiles() {
            var files = Directory.EnumerateFiles(
                "./", "article_*", SearchOption.TopDirectoryOnly);
            return files.ToList();
        }

        static string GetFileName(string file) {
            return Path
                .GetFileNameWithoutExtension(file)
                .Replace("article_", "");
        }

        static void ProduceFile(string fileName, string outputName) {
            var markdownBody = TransformBodyToMarkdown(fileName);
            File.WriteAllText($"./www/{outputName}.md", markdownBody);
        }

        static string GetVersion() {
            return typeof(Cli)
                .Assembly
                .GetName()
                .Version
                .ToString()
                .TrimEnd(".0".ToCharArray());
        }

        static string TransformBodyToMarkdown(string fileName) {
            var source = File.ReadAllLines(fileName);
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

    }

}
