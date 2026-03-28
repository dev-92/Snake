using Microsoft.UI.Xaml.Controls;
using SnakeUi.Controller;
using SnakeUi.Enums;
using System;

namespace SnakeUi.MVVM.View.Settings;

internal sealed partial class DesignSettingsView : UserControl
{
    private ThemeManager? _themeManager {  get; set; }
    public Action? ThemeSelectionChanged;

    public DesignSettingsView()
    {
        this.InitializeComponent();
    }

    public void Initialize(ThemeManager themeManager)
    {
        this._themeManager = themeManager;
    }

    /// <summary>
    /// Handles the click event for the "Back" button.
    /// Returns the player to the main menu screen.
    /// </summary>
    private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (this._themeManager == null) return;

        ColorTheme chosenColorTheme = this.GetChosenColorTheme(sender);
        this._themeManager.ChangeThemeTo(chosenColorTheme);

        this.ThemeSelectionChanged?.Invoke();
    }

    private ColorTheme GetChosenColorTheme(object sender)
    {
        if (sender is not ComboBox themeBox)
        {
            return ColorTheme.Dark;
        }

        if (themeBox.SelectedItem is not ComboBoxItem selectedItem)
        {
            return ColorTheme.Dark;
        }

        return selectedItem.Content switch
        {
            "Dark"      => ColorTheme.Dark,
            "Retro"     => ColorTheme.Retro,
            "OceanTech" => ColorTheme.OceanTech,
            "Cyberpunk" => ColorTheme.Cyberpunk,
            _           => ColorTheme.Dark,
        };
    }

    /// <summary>
    /// Handles changes to the gridsize slider.
    /// Updates the amount of the gameboards cells.
    /// </summary>
    private void GridSizeSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        // TODO
    }

    /// <summary>
    /// Handles changes to the cellshape slider.
    /// Handles if the cell is represented more edgy or round.
    /// </summary>
    private void CellShapeSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        // TODO
    }
}
