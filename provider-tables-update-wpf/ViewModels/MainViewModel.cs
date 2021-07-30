using Microsoft.Extensions.Options;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace provider_tables_update_wpf.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly AppSettings _settings;

        public MainViewModel(IOptions<AppSettings> appSettingsOptions)
        {
            _settings = appSettingsOptions.Value;
        }

        private ICommand? _uploadDataCommand;
        public ICommand UploadDataCommand
        {
            get { return _uploadDataCommand ??= new RelayCommand(UploadData); }
            set
            {
                _uploadDataCommand = value;
            }
        }

        private void UploadData()
        {
            //Do stuff;
        }
    }
}
