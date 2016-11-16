using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.Windows.config", Watch = true)]
namespace ThreeLayersFrameTemplates
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册控制反转
            AutofacConfig.Register();
		}
	}
}
