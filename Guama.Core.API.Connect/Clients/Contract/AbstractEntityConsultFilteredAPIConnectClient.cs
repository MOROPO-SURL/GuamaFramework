using MOROPO.Framework.Guama.Core.Presentation.API.Connect.Providers.Facade;

namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients.Contract
{
    public abstract class AbstractEntityConsultFilteredAPIConnectClient<TEntity, TId, TDisplayModel, TFilterModel> : AbstractEntityConsultAPIConnectClient<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        protected AbstractEntityConsultFilteredAPIConnectClient(IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual System.Threading.Tasks.Task<TDisplayModel[]> FilterAsync(TFilterModel model)
        {
            return httpClientHelperProvider.HttpClientHelper.PostAsync<TFilterModel, TDisplayModel[]>(string.Format("{0}/{1}", TargetController, "filtered"), model);
        }

        public virtual System.Threading.Tasks.Task<Models.PaginationModel<TDisplayModel>> FilterPaginateAsync(TFilterModel model, int page = 1, int limit = 25)
        {
            return httpClientHelperProvider.HttpClientHelper.PostAsync<TFilterModel, Models.PaginationModel<TDisplayModel>>(string.Format("{0}/filtered/page/{1}/limit/{2}", TargetController, page, limit), model);
        }
    }
}
