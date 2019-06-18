using System;

using System.Windows.Forms;
using NavisArchiveWork.Data;

namespace UI
{
    using NavisArchiveWork.Model;

    using NavisElectronics.TechPreparation;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.IO;
    using NavisElectronics.TechPreparation.Presenters;
    using NavisElectronics.TechPreparation.Services;

    using Ninject;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommonModule module = new CommonModule();
            IKernel kernel = new StandardKernel();
            kernel.Load(module);
            kernel.Rebind<IRepository>().To<TempRepository>().InSingletonScope();
            kernel.Rebind<Search>().ToSelf().InSingletonScope();
            kernel.Rebind<IDataRepository>().To<TempReader>();
            kernel.Rebind<ITechPreparingSelector<IdOrPath>>().To<TempSelector>();
            kernel.Bind<IPresentationFactory>().To<PresentationFactory>().WithConstructorArgument("container", kernel);
            PresentationFactory presentationFactory = kernel.Get<PresentationFactory>();
            IPresenter<long> mainPresenter = presentationFactory.GetPresenter<MainPresenter, long>();
            mainPresenter.Run(-1);
        }
    }
}
