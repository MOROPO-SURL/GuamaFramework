namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients.Contract
{
    public abstract class AbstractEntityConsultAPIConnectClient<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        protected readonly Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider;

        public AbstractEntityConsultAPIConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider)
        {
            this.httpClientHelperProvider = httpClientHelperProvider;
        }

        protected abstract string TargetController { get; }

        public virtual System.Threading.Tasks.Task<TDisplayModel[]> GetAsync()
        {
            return httpClientHelperProvider.HttpClientHelper.GetAsync<TDisplayModel[]>(TargetController);
        }

        public virtual System.Threading.Tasks.Task<Models.PaginationModel<TDisplayModel>> GetPaginateAsync(int page = 1, int limit = 25)
        {
            return httpClientHelperProvider.HttpClientHelper.GetAsync<Models.PaginationModel<TDisplayModel>>(string.Format("{0}/page/{1}/limit/{2}", TargetController, page, limit));
        }

        public virtual System.Threading.Tasks.Task<TDisplayModel> GetAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.GetAsync<TDisplayModel>(string.Format("{0}/{1}", TargetController, key));
        }
    }
}
