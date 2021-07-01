using System;

namespace Plugins.MobileScreenResolutions.Scripts.Domain
{
    [Serializable]
    public class Resolution
    {
        public int Width;
        public int Height;

        public float Get()
        {
            return (float) Width / Height;
        }
    }
}