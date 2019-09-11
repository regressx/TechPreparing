using System;

namespace NavisElectronics.Orders
{
    public interface IView
    {
        event EventHandler Load;
        void Show();
        void Close();
    }
}