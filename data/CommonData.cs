using Godot;

namespace HamsterUtils
{
    public abstract class CommonData : DictionaryData<string, Variant>
    {
        public static T Get<[MustBeVariant] T>(string key, T defaultValue)
            => DictionaryData<string, Variant>.Get(key, Variant.From<T>(defaultValue)).As<T>();

        public static void Set<[MustBeVariant] T>(string key, T value)
            => Set(key, Variant.From(value));
    }
}