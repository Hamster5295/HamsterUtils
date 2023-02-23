using Godot;

namespace HamsterUtils
{
    public abstract partial class BaseData : Node
    {
        private static FileAccess _file;

        public override void _ExitTree()
        {
            _file.Close();
        }

        private bool Read(out Variant result)
        {
            result = new Variant();
            var path = GetSavePath();
            if (!FileAccess.FileExists(path)) return false;
            _file = FileAccess.Open(path, FileAccess.ModeFlags.ReadWrite);
            result = Json.ParseString(_file.GetLine());
            return true;
        }

        protected bool Read<[MustBeVariant] T>(out T result)
        {
            var isSuccessful = Read(out var r);
            result = r.As<T>();
            return isSuccessful;
        }

        protected static void Save(Variant obj)
        {
            _file.StoreLine(Json.Stringify(obj));
            _file.Flush();
        }

        protected abstract string GetSavePath();
    }
}