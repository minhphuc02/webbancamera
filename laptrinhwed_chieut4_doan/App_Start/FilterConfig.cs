using System.Web;
using System.Web.Mvc;

namespace laptrinhwed_chieut4_doan
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
