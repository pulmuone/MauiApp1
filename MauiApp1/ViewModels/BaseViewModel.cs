using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace MauiApp1.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        public ICommand SortedCommand { get; set; }

        public BaseViewModel()
        {

        }

        Color _scannerStatusColor;
        bool isBusy = false;
        bool isEnabled = true;
        bool _isControlEnable = true;
        bool _isCamera;
        string _scannerStatus;
        string _title = string.Empty;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }

        public bool IsControlEnable
        {
            get => _isControlEnable;
            set => SetProperty(ref this._isControlEnable, value);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool IsCamera
        {
            get => _isCamera;
            set => SetProperty(ref this._isCamera, value);
        }

        public string ScannerStatus
        {
            get => _scannerStatus;
            set => SetProperty(ref this._scannerStatus, value);
        }

        public Color ScannerStatusColor
        {
            get => _scannerStatusColor;
            set => SetProperty(ref this._scannerStatusColor, value);
        }
    }
}
