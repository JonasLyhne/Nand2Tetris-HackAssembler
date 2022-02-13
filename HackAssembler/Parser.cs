using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackAssembler
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    
    public class Parser
    {
        private Translator translator;

        public Parser()
        {
            translator = new Translator();
        }
        public string Parse(string assemblyFile)
        {
            var assemblyText = assemblyFile.Split('\n');
            return JoinArrayToString(translator.TranslateToBinary(CleanUp(assemblyText)));
        }

        /// <summary>
        /// Joins the strings in the array into a single string.
        /// </summary>
        /// <param name="assemblyText">string array to be joined</param>
        /// <returns>a string</returns>
        private string JoinArrayToString(string[] assemblyText)
        {
            return string.Join("\n", assemblyText);
        }

        private string[] CleanUp(string[] assemblyText)
        {
            assemblyText = RemoveComments(assemblyText);
            assemblyText = RemoveEmptyLines(assemblyText);
            assemblyText = RemoveWhiteSpace(assemblyText);
            assemblyText = RemoveLabels(assemblyText);
            return assemblyText;
        }
        private string[] RemoveComments(string[] assemblyString)
        {
            for (var i = 0; i < assemblyString.Length; i++)
            {
                var commentSymbol = assemblyString[i].IndexOf("//", StringComparison.Ordinal);
                assemblyString[i] = commentSymbol >= 0
                    ? assemblyString[i].Remove(commentSymbol).Trim()
                    : assemblyString[i].Trim();
            }

            return assemblyString;
        }

        private string[] RemoveEmptyLines(string[] assemblyString)
        {
            return assemblyString.Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();
        }

        private string[] RemoveWhiteSpace(string[] assemblyString)
        {
            for (var i = 0; i < assemblyString.Length; i++)
            {
                assemblyString[i] = Regex.Replace(assemblyString[i], @"s", "");
            }

            return assemblyString;
        }

        private string[] RemoveLabels(string[] assemblyString)
        {
            List<string> list = new();
            foreach (var line in assemblyString)
            {
                if (line.Contains('(') && line.Contains(')'))
                {
                    var pointer = line.Trim('(', ')');
                    SymbolTable.Pointers?.Add(pointer, list.Count);
                }
                else
                {
                    list.Add(line);
                }
            }

            return list.ToArray();
        }
    }
}