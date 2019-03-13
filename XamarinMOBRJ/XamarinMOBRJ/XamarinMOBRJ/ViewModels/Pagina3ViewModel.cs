using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using SQLite;
using XamarinMOBRJ.Helpers;
using XamarinMOBRJ.Models;
using XamarinMOBRJ.Models.ClassesAPI;

namespace XamarinMOBRJ.ViewModels
{
    public class Pagina3ViewModel : ViewModelBase
    {
        #region propriedades
        private List<Estados>  estadosSqLite;
        public List<Estados>  EstadosSqLite //Estados que serão exibidos no listview
        {
            get { return estadosSqLite; }
            set => SetProperty(ref estadosSqLite , value);
        }


        private ObservableCollection<Record> listaEstados;
        public ObservableCollection<Record> ListaEstados //Recebe a lista de Estados
        {
            get => listaEstados;
            set => SetProperty(ref listaEstados, value);
        }
        #endregion

        private IDatabaseAccess _databaseAccess;
        private SQLiteAsyncConnection _sqliteConnection;
        private DatabaseHelper _databaseHelper;

        public Pagina3ViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDatabaseAccess  databaseAccess) 
            : base(navigationService, pageDialogService)
        {
            Title = "Pagina3";
            _databaseAccess = databaseAccess;
            _sqliteConnection = _databaseAccess.GetConnection();// Pega a conexão
            _databaseHelper = new DatabaseHelper(_sqliteConnection); // cria a conexão
        }

        #region Comandos

        private DelegateCommand comandoGravar;
        public DelegateCommand ComandoGravar =>
            comandoGravar ?? (comandoGravar = new DelegateCommand(ExecuteComandoGravar, CanExecuteComandoGravar));

        async void ExecuteComandoGravar()
        {
            try
            {
                IsBusy = true;
                
                

                Estados estadoGravacao = new Estados();
                foreach (var estado in ListaEstados)//Grava registros um a um
                {
                    estadoGravacao = new Estados { Capital = estado.fields.Capital, Estado = estado.fields.Estado, ImgUrl = estado.fields.ImgUrl, Sigla = estado.fields.Sigla };
                    var resultado = await _databaseHelper.InsertEstado(estadoGravacao);
                }

                EstadosSqLite = await _databaseHelper.GetEstadosAsync(); //Pega lista de estados gravados para exibição
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Atenção", "Ocorreu um Erro", "ok");
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        bool CanExecuteComandoGravar()
        {
            return IsNotBusy;
        }


        #endregion

        public override async  void OnNavigatingTo(INavigationParameters parameters)
        {
            EstadosSqLite = await _databaseHelper.GetEstadosAsync();

            if (parameters.ContainsKey("ListaEstados"))
                ListaEstados = (ObservableCollection<Record>)parameters["ListaEstados"];
        }


    }
}
