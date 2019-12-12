/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MVVMLightDemo"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MVVMLightDemo.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region Code Example
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            #endregion

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WelcomeViewModel>();
            SimpleIoc.Default.Register<BothWayBindViewModel>();
            SimpleIoc.Default.Register<BindingAdvancedViewModel>();
            SimpleIoc.Default.Register<CommandViewModel>();
            SimpleIoc.Default.Register<CommandAdvanceViewModel>();
            SimpleIoc.Default.Register<FruitInfoViewModel>();

            #region Validate
            SimpleIoc.Default.Register<ValidateExceptionViewModel>();
            SimpleIoc.Default.Register<BindingFormViewModel>();
            SimpleIoc.Default.Register<ValidationRuleViewModel>();
            SimpleIoc.Default.Register<BindDataAnnotationsViewModel>();
            SimpleIoc.Default.Register<PackagedValidateViewModel>();
            #endregion

            #region Messenger
            SimpleIoc.Default.Register<ForSourceSenderViewModel>();
            SimpleIoc.Default.Register<PropertyChangedViewModel>();
            #endregion
        }

        #region สตภýปฏ
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public WelcomeViewModel Welcome
        {
            get
            { 
               return ServiceLocator.Current.GetInstance<WelcomeViewModel>();
            }
        }

        public BothWayBindViewModel BothWayBind
        {
            get
            { 
                return ServiceLocator.Current.GetInstance<BothWayBindViewModel>();
            }
        }

        public BindingAdvancedViewModel BindingAdvanced
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BindingAdvancedViewModel>();
            }
        }

        public CommandViewModel Command
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CommandViewModel>();
            }
        }

        public CommandAdvanceViewModel CommandAdvance
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<CommandAdvanceViewModel>();
            }
        }

        public FruitInfoViewModel FruitInfox
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FruitInfoViewModel>();
            }
        }


        public ValidateExceptionViewModel ValidateException
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ValidateExceptionViewModel>();
            }
        }

        public BindingFormViewModel BindingForm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BindingFormViewModel>();
            }
        }

        public ValidationRuleViewModel ValidationRule
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ValidationRuleViewModel>();
            }
        }

        public BindDataAnnotationsViewModel BindDataAnnotations
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BindDataAnnotationsViewModel>();
            }
        }
        public PackagedValidateViewModel PackagedValidate
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PackagedValidateViewModel>();
            }
        }


        public ForSourceSenderViewModel ForSourceSender
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ForSourceSenderViewModel>();
            }
        }

        public PropertyChangedViewModel PropertyChanged
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PropertyChangedViewModel>();
            }
        }
        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}