using Godot;

namespace HamsterUtils
{
    public partial class Log
    {
        public static bool EnableLog { get; set; } = true;

        public static void I(Node from, object message)
        {
            if (EnableLog) GD.Print("[" + from.GetPath() + "]: " + message.ToString());
        }

        public static void I(object message)
        {
            if (EnableLog) GD.Print("[I]: " + message.ToString());
        }

        public static void W(Node from, object message)
        {
            if (EnableLog) GD.PushWarning("[" + from.GetPath() + "]: " + message.ToString());
        }

        public static void W(object message)
        {
            if (EnableLog) GD.PushWarning("[W]: " + message.ToString());
        }

        public static void E(Node from, object message)
        {
            if (EnableLog) GD.PushError("[" + from.GetPath() + "]: " + message.ToString());
        }

        public static void E(object message)
        {
            if (EnableLog) GD.PushError("[E]: " + message.ToString());
        }

        public static void Line(int count = 8, char divider = '=')
        {
            if (!EnableLog) return;
            char[] line = new char[count];
            for (int i = 0; i < count; i++) line[i] = divider;
            GD.Print(line);
        }
    }
}