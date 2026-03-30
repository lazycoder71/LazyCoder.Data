using LazyCoder.Core;
using System;

namespace LazyCoder.Data
{
    [Serializable]
    public class DataBlock<T> where T : DataBlock<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = DataFileHandler.LoadFromDevice<T>(typeof(T).ToString());

                    if (_instance == null)
                        _instance = (T)Activator.CreateInstance(typeof(T));

                    _instance.Init();
                }

                return _instance;
            }
        }

        protected virtual void Init()
        {
            MonoCallback.SafeInstance.EventApplicationPause += MonoCallback_ApplicationOnPause;
            MonoCallback.SafeInstance.EventApplicationQuit += MonoCallback_ApplicationOnQuit;
            MonoCallback.SafeInstance.EventApplicationFocus += MonoCallback_EventApplicationFocus;
        }

        private void MonoCallback_EventApplicationFocus(bool isFocus)
        {
            if (!isFocus)
                Save();
        }

        private void MonoCallback_ApplicationOnQuit()
        {
            Save();
        }

        private void MonoCallback_ApplicationOnPause(bool paused)
        {
            if (paused)
                Save();
        }

        public static void Save()
        {
            DataFileHandler.SaveToDevice(Instance, typeof(T).ToString());
        }

        public static void Delete()
        {
            _instance = null;

            DataFileHandler.DeleteInDevice(typeof(T).ToString());
        }
    }
}