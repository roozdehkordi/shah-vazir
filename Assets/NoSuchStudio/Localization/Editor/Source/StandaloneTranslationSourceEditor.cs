using NoSuchStudio.Localization.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NoSuchStudio.Localization.Source.Editor {
    [CustomEditor(typeof(StandaloneTranslationSource))]
    public class StandaloneTranslationSourceEditor : BaseTranslationSourceEditor<StandaloneTranslationSourceEditor, StandaloneTranslationSource> {
        Dictionary<string, bool> phraseFoldStates;

        protected override void OnEnable() {
            base.OnEnable();
            phraseFoldStates = phraseFoldStates ?? new Dictionary<string, bool>();
        }

        public override void OnInspectorGUI() {
            // connection status
            DrawServiceConnectionStatus(tsTarget);
            EditorGUILayout.Separator();

            // export buttons
            if (GUILayout.Button("print as CSV")) {
                Debug.Log("print as CSV\n" + CSVTranslationSource.ExportAsCSVString('|', tsTarget.translations));
            }
#if NEWTONSOFTJSON_PRESENT
            if (GUILayout.Button("print as Json")) {
                Debug.Log("print as JSON\n" + JsonTranslationSource.ExportAsJsonString(tsTarget.translations));
            }
#endif
            EditorGUILayout.Separator();

            // translation stats
            int phrases = tsTarget.translations.Keys.Count();
            int translations = tsTarget.translations.Select(kvp => kvp.Value.Count).Sum();
            DrawTranslationStats(phrases, translations);
            EditorGUILayout.Separator();

            // editor 
            DrawDefaultInspector();
        }
    }
}