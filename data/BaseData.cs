using Godot;

namespace HamsterUtils
{
    public abstract class BaseData : Node
    {
        private File file = new File();

        public override void _ExitTree()
        {
            file.Close();
        }

        protected bool Read(out JSONParseResult result)
        {
            result = null;
            var path = GetSavePath();
            if (!file.FileExists(path)) return false;
            file.Open(path, File.ModeFlags.ReadWrite);
            result = JSON.Parse(file.GetLine());
            return true;
        }

        protected void Save(object obj)
        {
            file.StoreLine(JSON.Print(obj));
        }

        protected abstract string GetSavePath();
    }
}