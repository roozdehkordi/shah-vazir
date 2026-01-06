#if NEWTONSOFTJSON_PRESENT
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace NoSuchStudio.Localization.Source {
    /// <summary>
    /// Parses a JSON file and provides the entries to <see cref="LocalizationService"/>.
    /// The Json should be in this format:
    /// <code>
    /// {
    ///     "phrase-title": {
    ///         "en": "Title",
    ///         "es": "Topico",
    ///         "ar": "عربی"
    ///     },
    ///     "phrase-back": {
    ///        ...
    ///     }
    /// }
    /// </code>
    /// </summary>
    [ExecuteAlways]
    public class JsonTranslationSource : FileTranslationSource, ITranslationSource {
        private void ParseJsonString(string rawText) {
            _translations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(rawText);
        }

        public static string ExportAsJsonString(Dictionary<string, Dictionary<string, string>> translations) {
            return JsonConvert.SerializeObject(translations, Formatting.Indented);
        }

        protected override void ImportTranslations() {
            _translations.Clear();
            if (_textAsset == null) return;
            string rawText = _textAsset.text;
            if (string.IsNullOrEmpty(rawText)) return;
            ParseJsonString(rawText);
        }
    }
}
#endif