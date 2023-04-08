namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients
{
    public abstract class AbstractEntityManagementApiConnectClient<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityConsultApiConnectClient<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEntityManagementApiConnectClient(Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual async System.Threading.Tasks.Task CreateAsync(TInputModel inputModel)
        {
            await httpClientHelperProvider.HttpClientHelper.PostAsync(TargetController, inputModel);
        }

        public virtual async System.Threading.Tasks.Task EditAsync(TInputModel inputModel)
        {
            await httpClientHelperProvider.HttpClientHelper.PutAsync(TargetController, inputModel);
        }

        public virtual async System.Threading.Tasks.Task Delete(TId key)
        {
            await httpClientHelperProvider.HttpClientHelper.DeleteAsync(string.Format("{0}/{1}", TargetController, key));
        }
    }
}
