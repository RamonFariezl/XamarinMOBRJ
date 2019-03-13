using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMOBRJ.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        public MenuPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService,pageDialogService)
        {
            Title = "Projeto MOBRJ";
        }
    }
}
