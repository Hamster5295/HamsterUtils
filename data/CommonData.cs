namespace HamsterUtils
{
    public class CommonData : DictionaryData<string, object>
    {
        public static int GetInt(string key, int defaultVal = 0)
        {
            if (Get(key, defaultVal) is int i) return i;
            else
            {
                Log.E(key + "对应的值不是int类型!");
                return defaultVal;
            }
        }

        public static float GetFloat(string key, float defaultVal = 0)
        {
            if (Get(key, defaultVal) is float i) return i;
            else
            {
                Log.E(key + "对应的值不是float类型!");
                return defaultVal;
            }
        }

        public static string GetString(string key, string defaultVal = "")
        {
            if (Get(key, defaultVal) is string i) return i;
            else
            {
                Log.E(key + "对应的值不是string类型!");
                return defaultVal;
            }
        }

        protected override string GetSavePath() => "common_data.json";
    }
}