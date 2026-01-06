using TMPro;
using UnityEngine;

namespace NoSuchStudio.Localization.Localizers {
    /// <summary>
    /// Localizes <see cref="TextMeshProUGUI"/> by setting its <see cref="TMP_Text.text"/> property
    /// based on the <see cref="PhrasedComponentLocalizer{LT, CT}.phrase"/> assigned to it and
    /// <see cref="LocalizationService.CurrentLanguage"/>. This component also uses the <see cref="Variables.VariablesService"/>
    /// and substitutes any variable names that occur in the text.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu(LocalizationService.ComponentMenuPath + "/TextMeshPro Text Localizer Lite (Phrased)")]
    public class TMProTextLocalizerLite : PhrasedComponentLocalizer<TMProTextLocalizerLite, TextMeshProUGUI> {
        public override void UpdatePhrasedComponent() {
            _component.text = _translation;
        }
    }
}
