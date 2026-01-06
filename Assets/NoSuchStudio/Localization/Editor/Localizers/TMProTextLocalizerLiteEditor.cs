using NoSuchStudio.Localization.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(TMProTextLocalizerLite))]
    public class TMProTextLocalizerLiteEditor : ComponentLocalizerEditor<TMProTextLocalizerLiteEditor, TMProTextLocalizerLite, TextMeshProUGUI> {

        [MenuItem("CONTEXT/TextMeshProUGUI/Localize Text")]
        static void Localize(MenuCommand command) {
            var c = (TextMeshProUGUI)command.context;
            c.gameObject.AddComponent<TMProTextLocalizerLite>();
        }
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize Text", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (TextMeshProUGUI)command.context;
            return !c.gameObject.GetComponent<TMProTextLocalizerLite>();
        }
    }
}