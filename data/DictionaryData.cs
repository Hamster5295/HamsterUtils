using Godot.Collections;

namespace HamsterUtils
{
    public abstract partial class DictionaryData<K, V> : BaseData
    {
        private static Dictionary<K, V> datas = new Dictionary<K, V>();

        public override void _Ready()
        {
            Read();
        }

        protected void Read()
        {
            if (Read<Dictionary>(out var result))
            {
                datas = new Dictionary<K, V>(result);
            }
        }

        public static V Get(K key, V defaultValue)
        {
            if (!datas.ContainsKey(key)) datas.Add(key, defaultValue);
            return datas[key];
        }

        public static void Set(K key, V value)
        {
            if (datas.ContainsKey(key)) datas[key] = value;
            else datas.Add(key, value);
        }
    }
}