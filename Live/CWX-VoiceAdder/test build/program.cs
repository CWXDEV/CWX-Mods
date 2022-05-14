using Aki.Common.Utils;

namespace VoiceAdd
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log.Info("Loading: CWX-VoiceAdd");
            new VoicePatch().Enable();
        }
    }
}
