using ModuleCameraSettings.Infrastructures;
using System;
using System.Windows;

namespace ModuleCameraSettings.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public SettingsViewModel Settings { get; } = new SettingsViewModel();

        /// <summary>
        /// Save settings command to a new file.
        /// </summary>
        public DelegateCommand SaveFileCommand => _saveFileCommand ??= new DelegateCommand(SaveFile, () => Settings.Valid);
        private DelegateCommand? _saveFileCommand;

        /// <summary>
        /// Save settings to a new file.
        /// </summary>
        private void SaveFile()
        {
            const string defaultSaveFilename = "MCS-INIT";

            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Save connection setting",
                Filter = $"\"Connection setting|{defaultSaveFilename}\"",
                FileName = defaultSaveFilename
            };
            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value && dialog.FileName is string savePath)
            {
                try
                {
                    Settings.Save(savePath);
                }
                catch (Exception)
                {
                    _ = MessageBox.Show("Failed to save file.");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public MainWindowViewModel()
        {
            // Each event occurs when a property is changed on a component.
            Settings.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Settings.Valid))
                {
                    SaveFileCommand.UpdateCanExecute();
                }
            };
        }
    }
}
