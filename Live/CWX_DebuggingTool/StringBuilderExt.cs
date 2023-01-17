using System.Text;

namespace CWX_DebuggingTool
{
    public static class MyExtensions
    {
        public static StringBuilder Prepend(this StringBuilder sb, string content)
        {
            return sb.Insert(0, content);
        }

        public static StringBuilder PrependLine(this StringBuilder sb, string content)
        {
            return sb.Insert(0, content + "\n");
        }
    }
}
