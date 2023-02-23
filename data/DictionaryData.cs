using Godot;
using Godot.Collections;

namespace HamsterUtils
{
    public abstract partial class DictionaryData<[MustBeVariant] K, [MustBeVariant] V> : BaseData
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

        public static void Save() => Save(datas);

        public static V GetValue(K key, V defaultValue)
        {
            if (!datas.ContainsKey(key)) datas.Add(key, defaultValue);
            return datas[key];
        }

        public static void SetValue(K key, V value)
        {
            datas[key] = value;
        }
    }
}