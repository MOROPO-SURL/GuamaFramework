namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients.Contract
{
    public abstract class AbstractEnableableEntityManagementAPIConnectClient<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityManagementAPIConnectClient<TEntity, TId, TInputModel, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEnableableEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEnableableEntityManagementAPIConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual System.Threading.Tasks.Task EnableAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/enable/{1}", TargetController, key));
        }

        public virtual System.Threading.Tasks.Task DisableAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.PutAsync(string.Format("{0}/disable/{1}", TargetController, key));
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
