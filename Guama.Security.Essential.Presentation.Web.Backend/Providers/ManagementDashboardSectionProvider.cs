namespace MOROPO.Framework.Guama.Security.Essential.Presentation.Web.Backend.Providers
{
    public class ManagementDashboardSectionProvider : Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade
    {
        public System.Collections.Generic.ICollection<Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel> GetManagementDashboardSections()
        {
            return new System.Collections.Generic.List<Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel>
            {
                new Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel
                {
                    Title = "Users",
                    Action = "index",
                    Controller = "users",
                    Area = "security",
                    Role = Core.Security.Presentation.Constants.Permissions.UserViewer
                },
                new Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel
                {
                    Title = "Roles",
                    Action = "index",
                    Controller = "roles",
                    Area = "security",
                    Role = Core.Security.Presentation.Constants.Permissions.RoleViewer
                }
            };
        }
    }
}
