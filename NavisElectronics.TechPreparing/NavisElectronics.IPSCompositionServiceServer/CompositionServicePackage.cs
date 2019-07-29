using NavisElectronics.IPS1C.IntegratorService;

namespace NavisElectronics.IPSCompositionService
{
    using System;
    using System.ServiceModel;
    using Intermech.Interfaces.Plugins;

    /// <summary>
    /// Плагин для запуска сервиса передачи составов
    /// </summary>
    public class CompositionServicePackage : IPackage
    {
        private ServiceHost _host;

        /// <summary>
        /// Метод загрузки
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>
        public void Load(IServiceProvider serviceProvider)
        {
            // создаем наш сервис и запускаем
            _host = new ServiceHost(typeof(Service));
            _host.Open();
        }

        /// <summary>
        /// Метод закрытия плагина
        /// </summary>
        public void Unload()
        {
            _host.Close();
        }

        /// <summary>
        /// Возвращает имя плагина
        /// </summary>
        public string Name
        {
            get { return "Расширение-сервис для передачи составов изделий и заказов"; }     
        }
    }
}
