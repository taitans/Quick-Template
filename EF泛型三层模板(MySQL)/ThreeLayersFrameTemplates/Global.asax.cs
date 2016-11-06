using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

#if DEBUG
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.Windows.config", Watch = true)]
#else
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.Linux.config", Watch = true)]
#endif
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
