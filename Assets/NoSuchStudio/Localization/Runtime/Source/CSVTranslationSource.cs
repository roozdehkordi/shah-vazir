using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NoSuchStudio.Localization.Source {
    /// <summary>
    /// Parses a CSV file and provides the entries to <see cref="LocalizationService"/>.
    /// Each line should contains 3 values: phrase, language, translation.
    /// '|' is the delimiter character.
    /// </summary>
    [ExecuteAlways]
    public class CSVTranslationSource : FileTranslationSource, ITranslationSource {

        public struct CSVLineData {
            public string phrase;
            public string locale;
            public string translation;

            public string ToCSVString(char del) {
                return $"{phrase}{del}{locale}{del}{translation}";
            }
        }

        static readonly string[] lineDelimiter = new[] { "\r\n", "\r", "\n" };

        [SerializeField] string[] _delimiters;
        
        private List<string> _errors;

        private bool ParseCSVLine(string line, out CSVLineData retVal) {
            string error = null;
            var tokens = line.Split(_delimiters, System.StringSplitOptions.None);

            if (tokens.Length != 3) {
                error = $"has {tokens.Length} tokens.";
            } else if (tokens.ToList().Any(t => string.IsNullOrEmpty(t))) {
                error = "has empty tokens.";
            }

            if (error != null) {
                _errors.Add($"CSV parse failed for line \"{line}\": {error}\n");
                retVal.locale = retVal.translation = retVal.phrase = null;
                return false;
            } else {
                retVal.phrase = tokens[0];
                retVal.locale = tokens[1];
                retVal.translation = tokens[2];
                return true;
            }
        }
        private void ParseCSVString(string rawText) {
            _errors = _errors ?? new List<string>();
            _errors.Clear();

            string[] lines = rawText.Split(lineDelimiter, StringSplitOptions.RemoveEmptyEntries);
            CSVLineData lineData;
            foreach(var line in lines) {
                if (ParseCSVLine(line, out lineData)) {
                    if (!_translations.ContainsKey(lineData.phrase)) {
                        _translations[lineData.phrase] = new Dictionary<string, string>();
                    }
                    _translations[lineData.phrase][lineData.locale] = lineData.translation;
                }
            }
            if (_errors.Count > 0) {
                LogError($"{_errors.Count} lines failed to parse:\n{string.Join("\n", _errors)}");
            }
        }

        public string ExportAsCSVString(Dictionary<string, Dictionary<string, string>> translations) {
            var list = translations.ToList().SelectMany(kvp => kvp.Value.ToList().Select(kvp2 => new CSVLineData { phrase = kvp.Key, locale = kvp2.Key, translation = kvp2.Value }.ToCSVString(_delimiters[0][0])).ToList()).ToList();
            return string.Join(Environment.NewLine, list);
        }

        public static string ExportAsCSVString(char delimiter, Dictionary<string, Dictionary<string, string>> translations) {
            var list = translations.ToList().SelectMany(kvp => kvp.Value.ToList().Select(kvp2 => new CSVLineData { phrase = kvp.Key, locale = kvp2.Key, translation = kvp2.Value }.ToCSVString(delimiter)).ToList()).ToList();
            return string.Join(Environment.NewLine, list);
        }

        protected override void ImportTranslations() {
            _translations.Clear();
            if (_textAsset == null) return;
            string rawText = _textAsset.text;
            if (string.IsNullOrEmpty(rawText)) return;
            ParseCSVString(rawText);
        }
    }
}
