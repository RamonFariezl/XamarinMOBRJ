using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMOBRJ.ViewModels
{
    public class Pagina2ViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IPageDialogService _pageDialogService;

        public Pagina2ViewModel(INavigationService navigationService,
                IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            Title = "Pagina2";
        }
    }
}
