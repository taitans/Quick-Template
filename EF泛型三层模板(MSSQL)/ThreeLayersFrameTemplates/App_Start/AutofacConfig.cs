using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ThreeLayersFrameTemplates
{
    public class AutofacConfig
    {
        public static void Register()
        {
            //创建Ioc容器
            var build = new ContainerBuilder();

            //注册控制器
            build.RegisterControllers(Assembly.GetExecutingAssembly());

            //注册属性注入动作
            build.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var service = Assembly.Load("Services").GetTypes();
            //注册业务层 转换成接口形式
            build.RegisterTypes(service).AsImplementedInterfaces();
            var repositry = Assembly.Load("Repositories").GetTypes();
            //注册仓储层 转换成接口形式
            build.RegisterTypes(repositry).AsImplementedInterfaces();

            //注入Log4net
            build.Register(c => log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)).SingleInstance();

            //创建Ioc对象
            IContainer container = build.Build();

            //替换工作底层
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}