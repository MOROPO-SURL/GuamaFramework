namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients.Contract
{
    public abstract class AbstractEnableableEntityConsultAPIConnectClient<TEntity, TId, TDisplayModel> : AbstractEntityConsultAPIConnectClient<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEnableableEntityConsultAPIConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual System.Threading.Tasks.Task<TDisplayModel[]> GetEnabledAsync()
        {
            return httpClientHelperProvider.HttpClientHelper.GetAsync<TDisplayModel[]>($"{TargetController}/enabled");
        }

        public virtual System.Threading.Tasks.Task<TDisplayModel[]> GetDisabledAsync()
        {
            return httpClientHelperProvider.HttpClientHelper.GetAsync<TDisplayModel[]>($"{TargetController}/disabled");
        }
    }
}
