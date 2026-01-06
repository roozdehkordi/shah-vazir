using NoSuchStudio.Common;
using NoSuchStudio.Common.Service;
using NoSuchStudio.Localization;
using NoSuchStudio.Localization.Localizers;
using NoSuchStudio.Localization.Source;
using UnityEngine;

[ExecuteAlways]
public class LogLevelManager : NoSuchMonoBehaviour {
    [SerializeField] LogType localizationFilter;

    private void Awake() {
        SyncLoggers();
    }

    private void OnValidate() {
        SyncLoggers();
    }

    public void SyncLoggers() {
        // Localizaiton
        UnityObjectLoggerExt.GetLoggerByType<Service<LocalizationService>>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<LocalizationService>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<LocaleDatabase>().logger.filterLogType = localizationFilter;
        // Translation Sources
        UnityObjectLoggerExt.GetLoggerByType<CSVTranslationSource>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<StandaloneTranslationSource>().logger.filterLogType = localizationFilter;

        // localized components
        UnityObjectLoggerExt.GetLoggerByType<AudioSourceClipMappedLocalizer>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<ImageTransformLocalizer>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<ImageSpriteMappedLocalizer>().logger.filterLogType = localizationFilter;

        // localized components -> text
        UnityObjectLoggerExt.GetLoggerByType<TextLocalizerLite>().logger.filterLogType = localizationFilter;
        // localized components -> TMPro
#if TMPRO_PRESENT
        UnityObjectLoggerExt.GetLoggerByType<TMProAlignLocalizer>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<TMProTextLocalizerLite>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<TMProFontMappedLocalizer>().logger.filterLogType = localizationFilter;
        UnityObjectLoggerExt.GetLoggerByType<TMProDropdownLocalizer>().logger.filterLogType = localizationFilter;
#endif
    }
}
