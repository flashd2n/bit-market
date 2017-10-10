[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Flash.BitMarket.Web.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Flash.BitMarket.Web.App_Start.NinjectConfig), "Stop")]

namespace Flash.BitMarket.Web.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject.Extensions.Conventions;
    using Ninject;
    using Ninject.Web.Common;
    using AutoMapper;
    using Flash.BitMarket.Interfaces.Services;
    using Flash.BitMarket.Services.Price_Quotes;
    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;
    using Microsoft.AspNet.SignalR.Infrastructure;
    using Flash.BitMarket.Web.Infrastructure.SignalR;
    using Flash.BitMarket.Web.Infrastructure.Providers;

    public static class NinjectConfig 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IQuoteTicker>()
                .To<QuoteTicker>()
                .InSingletonScope();

            kernel.Bind<IHubConnectionContext<dynamic>>().ToMethod(context =>
                GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<QuoteTickerHub>().Clients
                ).WhenInjectedInto<IQuoteTicker>();

            kernel.Bind<IPriceQuoteService>().To<PriceQuoteService>().InSingletonScope();
            kernel.Bind<IMapper>().ToMethod(x => Mapper.Instance).InSingletonScope();

            kernel.Bind<ITimerProvider>().To<TimerProvider>().InSingletonScope();
        }        
    }
}
