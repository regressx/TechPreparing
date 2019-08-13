using System;

namespace NavisElectronics.TechPreparation.Services
{
    /// <summary>
    /// The save service event args.
    /// </summary>
    public class SaveServiceEventArgs : EventArgs
    {
        /// <summary>
        /// The _percent.
        /// </summary>
        private readonly int _percent;

        /// <summary>
        /// The _message.
        /// </summary>
        private readonly string _message;

        public SaveServiceEventArgs(int percent, string message)
        {
            _percent = percent;
            _message = message;
        }

        /// <summary>
        /// The _percent.
        /// </summary>
        public int Percent
        {
            get { return _percent; }
        }

        /// <summary>
        /// The _message.
        /// </summary>
        public string Message
        {
            get { return _message; }
        }
    }
}