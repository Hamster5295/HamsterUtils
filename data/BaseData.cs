using Godot;

namespace HamsterUtils
{
    public abstract partial class BaseData : Node
    {
        private FileAccess file;

        private bool Read(out Variant result)
        {
            result = new Variant();
            var path = GetSavePath();
            if (!FileAccess.FileExists(path)) return false;
            file = FileAccess.Open(path, FileAccess.ModeFlags.ReadWrite);
            result = Json.ParseString(file.GetLine());
            return true;
        }

        protected bool Read<[MustBeVariant] T>(out T result)
        {
            var isSuccessful = Read(out var r);
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