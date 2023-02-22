using Godot;

namespace HamsterUtils
{
    public abstract partial class CommonData : DictionaryData<string, Variant>
    {
        public static T Get<[MustBeVariant] T>(string key, T defaultValue)
            => DictionaryData<string, Variant>.GetValue(key, Variant.From<T>(defaultValue)).As<T>();

        public static void Set<[MustBeVariant] T>(string key, T value)
            => SetValue(key, Variant.From(value));
    }
}