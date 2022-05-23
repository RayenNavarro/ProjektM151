using System.Web;
using System.Web.Mvc;

namespace ProjektM151ManageWithRaamel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
