namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients.Contract
{
    public abstract class AbstractEntityManagementAPIConnectClient<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityConsultAPIConnectClient<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEntityManagementAPIConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual System.Threading.Tasks.Task CreateAsync(TInputModel model)
        {
            return httpClientHelperProvider.HttpClientHelper.PostAsync(TargetController, model);
        }

        public virtual System.Threading.Tasks.Task EditAsync(TInputModel model)
        {
            return httpClientHelperProvider.HttpClientHelper.PutAsync(TargetController, model);
        }

        public virtual System.Threading.Tasks.Task DeleteAsync(TId key)
        {
            return httpClientHelperProvider.HttpClientHelper.DeleteAsync(string.Format("{0}/{1}", TargetController, key));
        }
    }
}
