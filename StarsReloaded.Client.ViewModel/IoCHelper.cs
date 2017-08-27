namespace StarsReloaded.Client.ViewModel
{
    using Castle.Windsor;

    public static class IoCHelper
    {
        public static IWindsorContainer Container { private get; set; }

        public static T Resolve<T>()
        {
            return Container == null ? default(T) : Container.Resolve<T>();
        }
    }
}