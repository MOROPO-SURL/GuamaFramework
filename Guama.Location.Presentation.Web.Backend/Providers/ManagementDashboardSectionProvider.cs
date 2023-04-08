using System.Collections.Generic;
using MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Models.View;

namespace MOROPO.Framework.Guama.Location.Presentation.Web.Backend.Providers
{
    public sealed class ManagementDashboardSectionProvider : Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade
    {
        public ICollection<ManagementDashboardSectionViewModel> GetManagementDashboardSections()
        {
            return new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Action = "index",
                    Area = "location",
                    Controller = "countries",
                    Role = Constants.Permissions.CountryViewer,
                    Title = "Countries"
                },
                new ManagementDashboardSectionViewModel
                {
                    Action = "index",
                    Area = "location",
                    Controller = "regions",
                    Role = Constants.Permissions.RegionViewer,
                    Title = "Regions"
                },
                new ManagementDashboardSectionViewModel
                {
                    Action = "index",
                    Area = "location",
                    Controller = "states",
                    Role = Constants.Permissions.StateViewer,
                    Title = "States"
                },
                new ManagementDashboardSectionViewModel
                {
                    Action = "index",
                    Area = "location",
                    Controller = "municipalities",
                    Role = Constants.Permissions.MunicipalityViewer,
                    Title = "Municipalities"
                },
                new ManagementDashboardSectionViewModel
                {
                    Action = "index",
                    Area = "location",
                    Controller = "localities",
                    Role = Constants.Permissions.LocalityViewer,
                    Title = "Localities"
                }
            };
        }
    }
}
