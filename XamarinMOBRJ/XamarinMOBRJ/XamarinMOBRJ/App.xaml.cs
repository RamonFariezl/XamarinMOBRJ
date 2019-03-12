using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMOBRJ.ViewModels;
using XamarinMOBRJ.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinMOBRJ
{
    public partial class App : PrismApplication
    {
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

            await NavigationService.NavigateAsync("MenuPage?createTab=Pagina1&createTab=Pagina2");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<Pagina1>();
            containerRegistry.RegisterForNavigation<Pagina2>();

        }

    }
}
