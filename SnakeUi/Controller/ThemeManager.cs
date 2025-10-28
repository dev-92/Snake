using Microsoft.UI.Xaml;

using SnakeUi.Config;
using SnakeUi.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeUi.Controller
{
    /// <summary>
    /// Manages the application theme by updating ResourceDictionaries in the app's resources.
    /// </summary>
    internal class ThemeManager
    {
        /// <summary>
        /// Initializes a new instance of the ThemeManager class.
        /// </summary>
        public ThemeManager()
        {

        }

        /// <summary>
        /// Changes the current application theme to the specified <see cref="ColorTheme"/>.
        /// Updates the merged ResourceDictionaries for colors and brushes.
        /// </summary>
        /// <param name="newColorTheme">The theme to apply (Dark or Light).</param>
        public void ChangeThemeTo(ColorTheme newColorTheme)
        {
            IList<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;

            this.RemoveCurrentTheme(dictionaries);
            this.ApplyNewTheme(dictionaries, newColorTheme);
            this.ReloadBrushes(dictionaries);

            this.ReloadComboBoxStyle(dictionaries);
            this.ReloadButtonStyle(dictionaries);
        }

        /// <summary>
        /// Removes the currently applied theme ResourceDictionary from the application's merged dictionaries.
        /// </summary>
        /// <param name="dictionaries">The merged ResourceDictionaries of the application.</param>
        private void RemoveCurrentTheme(IList<ResourceDictionary> dictionaries)
        {
            ResourceDictionary? oldTheme = dictionaries.FirstOrDefault(d => d.Source?.OriginalString.Contains("ColorThemes/") == true);

            if (oldTheme == null)
            {
                return;
            }

            dictionaries.Remove(oldTheme);
        }

        /// <summary>
        /// Inserts the ResourceDictionary corresponding to the specified <see cref="ColorTheme"/> into the application's merged dictionaries.
        /// </summary>
        /// <param name="dictionaries">The merged ResourceDictionaries of the application.</param>
        /// <param name="newTheme">The theme to apply.</param>
        private void ApplyNewTheme(IList<ResourceDictionary> dictionaries, ColorTheme newTheme)
        {
            ResourceDictionary colorDictionaryToInsert = new ResourceDictionary();

            colorDictionaryToInsert.Source = newTheme switch
            {
                ColorTheme.Dark      => new Uri(ThemeResourceConstants.PATH_TO_DARK_COLORS_THEME),
                ColorTheme.Retro     => new Uri(ThemeResourceConstants.PATH_TO_RETRO_COLORS_THEME),
                ColorTheme.OceanTech => new Uri(ThemeResourceConstants.PATH_TO_OCEANTECH_COLORS_THEME),
                ColorTheme.Cyberpunk => new Uri(ThemeResourceConstants.PATH_TO_CYBERPUNK_COLORS_THEME),
                _                    => new Uri(ThemeResourceConstants.PATH_TO_DARK_COLORS_THEME),
            };

            dictionaries.Insert(0, colorDictionaryToInsert);
        }

        /// <summary>
        /// Reloads the brushes ResourceDictionary to apply the new theme's brushes.
        /// Removes the old brushes dictionary if present and inserts the current one.
        /// </summary>
        /// <param name="dictionaries">The merged ResourceDictionaries of the application.</param>
        private void ReloadBrushes(IList<ResourceDictionary> dictionaries)
        {
            var oldBrushes = dictionaries.FirstOrDefault(d => d.Source?.OriginalString.Contains(ThemeResourceConstants.BRUSHES_FILENAME) == true);

            if (oldBrushes == null)
            {
                return;
            }

            dictionaries.Remove(oldBrushes);
            dictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(ThemeResourceConstants.PATH_TO_THEME_BRUSHES)
            });
        }

        private void ReloadButtonStyle(IList<ResourceDictionary> dictionaries)
        {
            var oldButtonStyle = dictionaries.FirstOrDefault(d => d.Source?.OriginalString.Contains(ThemeResourceConstants.BUTTONSTYLE_FILENAME) == true);

            if (oldButtonStyle == null)
            {
                return;
            }

            dictionaries.Remove(oldButtonStyle);
            dictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(ThemeResourceConstants.PATH_TO_BUTTON_STYLE)
            });
        }

        private void ReloadComboBoxStyle(IList<ResourceDictionary> dictionaries)
        {
            var oldComboBoxStlye = dictionaries.FirstOrDefault(d => d.Source?.OriginalString.Contains(ThemeResourceConstants.COMBOBOX_FILENAME) == true);

            if (oldComboBoxStlye == null)
            {
                return;
            }

            dictionaries.Remove(oldComboBoxStlye);
            dictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(ThemeResourceConstants.PATH_TO_COMBOBOX_STYLE)
            });
        }
    }
}
