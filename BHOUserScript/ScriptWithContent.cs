namespace BHOUserScript
{
    class ScriptWithContent
    {
        public Script ScriptData { get; set; }

        public string Content { get; set; }

        public ScriptWithContent(Script s)
        {
            ScriptData = s;
        }
    }
}
