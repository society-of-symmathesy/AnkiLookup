﻿using System.Text;
using AnkiLookup.Core.Models;

namespace AnkiLookup.Core.Helpers
{
    public class TextFormatter : IWordInfoFormatter
    {
        public string Render(CambridgeWordInfo wordInfo)
        {
            if (wordInfo.Entries.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();

            for (var i = 0; i < wordInfo.Entries.Count; i++)
            {
                var entry = wordInfo.Entries[i];

                if (i == 0 || wordInfo.Entries.Count > 1 && wordInfo.Entries[i - 1].ActualWord != entry.ActualWord)
                    sb.AppendLine(entry.ActualWord);

                if (!string.IsNullOrWhiteSpace(entry.Label))
                    sb.AppendLine($"[{entry.Label}]");

                foreach (var definition in entry.Definitions)
                {
                    sb.AppendLine(definition.Definition);
                    if (definition.Examples == null)
                        continue;

                    foreach (var example in definition.Examples)
                    {
                        if (!string.IsNullOrWhiteSpace(example))
                            sb.AppendLine($" -> \"{example}\"");
                    }
                }

                sb.AppendLine();
            }
            if (sb.Length == 0)
                return string.Empty;

            var sbText = sb.ToString();
            return sbText.Substring(0, sbText.Length - 1);
        }
    }
}