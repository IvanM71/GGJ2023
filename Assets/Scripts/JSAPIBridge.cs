using System.Runtime.InteropServices;

namespace Apollo11
{
    public static class JSAPIBridge
    {
        // Import the javascript function that redirects to another URL
        /*[DllImport("__Internal")]
        public static extern void RedirectTo();*/
        
        [DllImport("__Internal")]
        public static extern void StartGameEvent();
        
        [DllImport("__Internal")]
        public static extern void StartLevelEvent(int level);
        
        [DllImport("__Internal")]
        public static extern void ReplayEvent(int level);
    }
}