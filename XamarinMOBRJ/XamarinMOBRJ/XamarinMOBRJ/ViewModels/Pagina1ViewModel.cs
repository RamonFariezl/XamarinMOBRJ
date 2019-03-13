using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinMOBRJ.Models.ClassesAPI;
using XamarinMOBRJ.Services;

namespace XamarinMOBRJ.ViewModels
{
    public class Pagina1ViewModel : ViewModelBase
    {
        #region propriedades

        private string _textoSearchbar;
        public string TextoSearchbar
        {
            get
            {
                if (_textoSearchbar == "" && IsBusy == false) // verifica se o Searchbar está preenchido e se o programa está ocupado
                    RaisePropertyChanged("ListaEstados"); // se for verdadeiro, Iremos preencher o listview com todos os estados, sem nenhum filtro
                return _textoSearchbar;
            }
            set { if (_textoSearchbar != value) { SetProperty(ref _textoSearchbar, value); } }
        }

        private ObservableCollection<Record> _listaEstados;
        public ObservableCollection<Record> ListaEstados
        {
            get
            {
                IsBusy = true;

                if(TextoSearchbar == null) // caso não tenha nenhum filtro digitado, retornará todos os estados
                {
                    IsBusy = false;
                    return _listaEstados;
                }
               
                else //efetua a busca de acordo com o filtro
                {
                    ObservableCollection<Record> theCollection = new ObservableCollection<Record>();

                    List<Record> entities = (from e in _listaEstados
                                                 where e.fields.Capital.ToUpper().Contains(TextoSearchbar.ToUpper()) || e.fields.Estado.ToUpper().Contains(TextoSearchbar.ToUpper())
                                                 select e ).ToList<Record>();

                    if (entities != null && entities.Any())
                    {
                        theCollection = new ObservableCollection<Record>(entities);
                    }
                    IsBusy = false;
                    return theCollection;
                }

             
            }
            set
            {
                _listaEstados = value;
                RaisePropertyChanged("ListaEstados");
            }
        }

        #endregion

       
        private IApiService _apiService;

        public Pagina1ViewModel(INavigationService navigationService,
                IPageDialogService pageDialogService, IApiService apiService) : base(navigationService, pageDialogService)
        {
            
            _apiService = apiService;
            Title = "Pagina1";
            ListaEstados = new ObservableCollection<Record>();
        }

        #region comandos

        private DelegateCommand comandoPesquisar;
        public DelegateCommand ComandoPesquisar =>
            comandoPesquisar ?? (comandoPesquisar = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            //ListaEstados.Where(name => name.fields.Capital.ToUpper().Contains(TextoSearchbar.ToUpper()));
            //ListaEstados.Where(x => x.fields.Capital.ToUpper().Contains(TextoSearchbar.ToUpper())).ToList();
            RaisePropertyChanged("ListaEstados");
        }

        #endregion

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            try
            {
                IsBusy = true;

                var listaEstados = await _apiService.GetEstadosAsync();

                ListaEstados.Clear();

                foreach (var estado in listaEstados.records)
                {
                    ListaEstados.Add(estado);

                }
                

            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Erro", "Não foi possível carregar a lista. Erro:" + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
