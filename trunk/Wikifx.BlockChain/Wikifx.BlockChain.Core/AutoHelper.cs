using Autofac;
using Module = Autofac.Module;

namespace Wikifx.BlockChain.Core
{
    public class AutoHelper : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<LogHelper>().SingleInstance();
            builder.RegisterInstance(new RedisHelper(ConfigUtil.GetSection("Cache:redis"))).InstancePerDependency();

            //builder.RegisterType<WikiArtcleService>().InstancePerDependency();
            //builder.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerDependency();
            //builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerDependency();
        }
    }
}
