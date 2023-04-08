using MOROPO.Framework.Guama.Core.Presentation.API.Connect.Providers.Facade;

namespace MOROPO.Framework.Guama.Core.Presentation.API.Connect.Clients
{
    public abstract class AbstractEntityConsultFilteredApiConnectClient<TEntity, TId, TDisplayModel, TFilterModel> : AbstractEntityConsultApiConnectClient<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
        where TFilterModel : class, Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<TEntity, TId>
    {
        protected AbstractEntityConsultFilteredApiConnectClient(IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        public virtual async System.Threading.Tasks.Task<TDisplayModel[]> FilterAsync(TFilterModel filterModel)
        {
            return await httpClientHelperProvider.HttpClientHelper.PostAsync<TFilterModel, TDisplayModel[]>(string.Format("{0}/{1}", TargetController, "filtered"), filterModel);
        }

        public virtual async System.Threading.Tasks.Task<Models.PaginationModel<TDisplayModel>> FilterPaginateAsync(TFilterModel filterModel, int page = 1, int limit = 25)
        {
            return await httpClientHelperProvider.HttpClientHelper.PostAsync<TFilterModel, Models.PaginationModel<TDisplayModel>>(string.Format("{0}/filtered/page/{1}/limit/{2}", TargetController, page, limit), filterModel);
        }
    }
}
