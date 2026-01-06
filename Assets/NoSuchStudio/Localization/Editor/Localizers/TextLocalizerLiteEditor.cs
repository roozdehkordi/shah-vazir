using NoSuchStudio.Localization.Editor;
using UnityEditor;
using UnityEngine.UI;

namespace NoSuchStudio.Localization.Localizers.Editor {
    [CustomEditor(typeof(TextLocalizerLite))]
    public class TextLocalizerLiteEditor : ComponentLocalizerEditor<TextLocalizerLiteEditor, TextLocalizerLite, Text> {

        [MenuItem("CONTEXT/Text/Localize Text")]
        static void Localize(MenuCommand command) {
            var c = (Text)command.context;
            c.gameObject.AddComponent<TextLocalizerLite>();
        }
        [MenuItem("CONTEXT/Text/Localize Text", true)]
        static bool ValidateLocalize(MenuCommand command) {
            var c = (Text)command.context;
            return !c.gameObject.GetComponent<TextLocalizerLite>();
        }
    }
}
