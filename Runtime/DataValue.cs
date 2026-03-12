using System;
using UnityEngine;

namespace LazyCoder.Data
{
#if LAZYCODER_MEMORYPACK
    [MemoryPack.MemoryPackable]
#else
    [System.Serializable]
#endif
    public partial class DataValue<T>
    {
#if LAZYCODER_MEMORYPACK
        [MemoryPack.MemoryPackInclude]
#else
        [SerializeField]
#endif        
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    T oldValue = _value;
                    
                    _value = value;
                    
                    EventValueChanged?.Invoke(oldValue, value);
                }
            }
        }

        /// <summary>
        /// Value changed event, first argument is the old value, second is the new value
        /// </summary>
        public event Action<T,T> EventValueChanged;

        public DataValue(T _value)
        {
            this._value = _value;
        }
    }
}