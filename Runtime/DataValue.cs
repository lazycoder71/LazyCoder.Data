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
#endif
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                EventValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> EventValueChanged;

        public DataValue(T value)
        {
            _value = value;
        }
    }
}