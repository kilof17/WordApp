using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WordApp.DataAccessLayer;

namespace WordApp
{
    public static class UnityConfig 
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();            
                    
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IControlsRepository, ControlsRepository>();
            container.RegisterType<IFilesRepository, FilesRepository>();
            container.RegisterType<IFormatsRepository, FormatsRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}