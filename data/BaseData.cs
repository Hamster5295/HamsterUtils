using Godot;

namespace HamsterUtils
{
    public abstract partial class BaseData : Node
    {
        private FileAccess file;

        protected bool Read(out Variant result)
        {
            result = new Variant();
            var path = GetSavePath();
            if (!FileAccess.FileExists(path)) return false;
            file = FileAccess.Open(path, FileAccess.ModeFlags.ReadWrite);
            result = Json.ParseString(file.GetLine());
            return true;
        }

        protected bool Read<T>(out T result)
        {
            bool isSuccessful = Read(out var r);
            result = r.As<T>();
            return isSuccessful;
        }

        protected void Save(Variant obj)
        {
            file.StoreLine(Json.Stringify(obj));
            file.Flush();
        }

        protected abstract string GetSavePath();
    }
}