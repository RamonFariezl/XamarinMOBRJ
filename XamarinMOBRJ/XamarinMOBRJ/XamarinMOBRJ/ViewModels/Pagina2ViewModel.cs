using DryIoc;
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
    public class Pagina2ViewModel : ViewModelBase
    {
        #region Propriedades

        private string _textoSearchbar;
        public string TextoSearchbar
        {
            get
            {
                if (_textoSearchbar == "" && IsBusy == false) // verifica se o Searchbar está preenchido e se o programa está ocupado
                    RaisePropertyChanged("DadosAgrupados"); // se for verdadeiro, Iremos preencher o listview com todos os estados, sem nenhum filtro
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

                if (TextoSearchbar == null) // caso não tenha nenhum filtro digitado, retornará todos os estados
                {
                    IsBusy = false;
                    return _listaEstados;
                }
                else //efetua a busca de acordo com o filtro
                {
                    ObservableCollection<Record> resultadoFiltro = new ObservableCollection<Record>();

                    List<Record> entidade = (from e in _listaEstados
                                             where e.fields.Capital.ToUpper().Contains(TextoSearchbar.ToUpper()) || e.fields.Estado.ToUpper().Contains(TextoSearchbar.ToUpper())
                                             select e).ToList<Record>();

                    if (entidade != null && entidade.Any())
                    {
                        resultadoFiltro = new ObservableCollection<Record>(entidade);
                    }
                    IsBusy = false;

                    return resultadoFiltro;
                }
            }
            set
            {
                _listaEstados = value;
                RaisePropertyChanged("ListaEstados");
            }
        }

        private List<ObservableCollection<string, Record>> _dadosAgrupados;
        public List<ObservableCollection<string, Record>> DadosAgrupados
        {
            get
            {
                return ListaEstados.OrderBy(p => p.fields.Regiao)
                            .GroupBy(p => p.fields.Regiao)
                            .Select(p => new ObservableCollection<string, Record>(p)).ToList(); ;
            }
            set
            {
                SetProperty(ref _dadosAgrupados, value);
            }
        }

        #endregion

        private IApiService _apiService;

        public Pagina2ViewModel(INavigationService navigationService,
                IPageDialogService pageDialogService, IApiService apiService) : base(navigationService, pageDialogService)
        {
            Title = "Pagina2";
            ListaEstados = new ObservableCollection<Record>();
            IsActiveChanged += HandleIsActiveTrue;
            IsActiveChanged += HandleIsActiveFalse;
            _apiService = apiService;
        }

        #region comandos

        private DelegateCommand comandoPesquisar;
        public DelegateCommand ComandoPesquisar =>
            comandoPesquisar ?? (comandoPesquisar = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            
            RaisePropertyChanged("DadosAgrupados");
        }

        private DelegateCommand comandoNavegar;
        public DelegateCommand ComandoNavegar =>
            comandoNavegar ?? (comandoNavegar = new DelegateCommand(ExecuteComandoNavegar, CanExecuteComandoNavegar));

        async void ExecuteComandoNavegar()
        {
            TextoSearchbar = ""; // Ao setar como "" garanto que todos os items serão passados para a proxima pagina.

            var navigationParams = new NavigationParameters
            {
                {"ListaEstados", ListaEstados}
            };

            await NavigationService.NavigateAsync("Pagina3", navigationParams);
        }

        bool CanExecuteComandoNavegar()
        {
            return IsNotBusy;
        }

        #endregion

        #region eventos

        private async void HandleIsActiveTrue(object sender, EventArgs args)
        {
            if (IsActive == false) return;

            await LoadAsync();
        }

        private void HandleIsActiveFalse(object sender, EventArgs args)
        {
            if (IsActive == true) return;
        }

        #endregion

        public override void Destroy()
        {
            IsActiveChanged -= HandleIsActiveTrue;
            IsActiveChanged -= HandleIsActiveFalse;
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

                ListaEstados.OrderBy(c => c.fields.Regiao).ToList();

                RaisePropertyChanged("DadosAgrupados");
                //DadosAgrupados = ListaEstados.OrderBy(p => p.fields.Regiao)
                //            .GroupBy(p => p.fields.Regiao)
                //            .Select(p => new ObservableGroupCollection<string, Record>(p)).ToList();

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
