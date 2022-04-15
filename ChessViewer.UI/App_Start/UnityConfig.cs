using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

using ChessViewer.Services.Abstract;
using ChessViewer.Services.Implementation;

namespace ChessViewer.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IGameService, GameService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}