using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMOBRJ.Helpers;
using XamarinMOBRJ.Services;
using XamarinMOBRJ.ViewModels;
using XamarinMOBRJ.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinMOBRJ
{
    public partial class App : PrismApplication
    {
        //public static SQLiteAsyncConnection SqlConnection;

        public App()
            : this(null)
        {

        }

        public App(IPlatformInitializer initializer)
            : this(initializer, true)
        {

        }

        public App(IPlatformInitializer initializer, bool setFormsDependencyResolver)
            : base(initializer, setFormsDependencyResolver)
        {

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/NavigationPage/MenuPage?createTab=Pagina1&createTab=Pagina2");
        }

       

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<Pagina1, Pagina1ViewModel>();
            containerRegistry.RegisterForNavigation<Pagina2, Pagina2ViewModel>();
            containerRegistry.RegisterForNavigation<Pagina3, Pagina3ViewModel>();

            containerRegistry.RegisterSingleton<IApiService, ApiService>();
            //containerRegistry.RegisterSingleton<IDatabaseAccess, DatabaseHelper>();

        }

    }
}
