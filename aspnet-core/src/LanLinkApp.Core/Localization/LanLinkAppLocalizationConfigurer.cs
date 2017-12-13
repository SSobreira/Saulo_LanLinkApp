using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace LanLinkApp.Localization
{
    public static class LanLinkAppLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(LanLinkAppConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(LanLinkAppLocalizationConfigurer).GetAssembly(),
                        "LanLinkApp.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
