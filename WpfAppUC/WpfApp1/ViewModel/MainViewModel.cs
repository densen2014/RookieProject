using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private ViewModelBase _currentViewModel;   
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged(() => CurrentViewModel);
            }
        }

        public ICommand OneCommand { get; private set; }

        public ICommand TwoCommand { get; private set; }

        public ICommand ThreeCommand { get; private set; }

        public MainViewModel()
        {
            CurrentViewModel = ServiceLocator.Current.GetInstance<OneViewmodel>();
            OneCommand = new RelayCommand(() => OneCommandSub());
            TwoCommand = new RelayCommand(() => TwoCommandSub());
            ThreeCommand = new RelayCommand(() => ThreeCommandSub());
        }


        private void OneCommandSub()
        {
            CurrentViewModel = ServiceLocator.Current.GetInstance<OneViewmodel>();
        }

        private void TwoCommandSub()
        {
            CurrentViewModel = ServiceLocator.Current.GetInstance<TwoViewmodel>();
        }
        private void ThreeCommandSub()
        {
            CurrentViewModel = ServiceLocator.Current.GetInstance<ThreeViewModel>();
        }
    }
}