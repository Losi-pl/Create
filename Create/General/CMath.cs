namespace Create.General;

public static class CMath
{
    extension(System.Math)
    {
        public static int Floor(float x)
        {
            if (x >= 0)
                return (int)x;
            return x % 1 != 0 ? (int)x - 1 : (int)x;
        }
        public static int Floor(decimal x)
        {
            if (x >= 0)
                return (int)x;
            return x % 1 != 0 ? (int)x - 1 : (int)x;
        }
        public static int Floor(double x)
        {
            if (x >= 0)
                return (int)x;
            return x % 1 != 0 ? (int)x - 1 : (int)x;
        }
        
        public static int Ceil(float x)
        {
            if (x >= 0)
                return x % 1 != 0 ? (int)x + 1 : (int)x;
            return (int)x;
        }
        public static int Ceil(decimal x)
        {
            if (x >= 0)
                return x % 1 != 0 ? (int)x + 1 : (int)x;
            return (int)x;
        }
        public static int Ceil(double x)
        {
            if (x >= 0)
                return x % 1 != 0 ? (int)x + 1 : (int)x;
            return (int)x;
        }
        
        public static int Round(float x)
        {
            if (x >= 0)
                return (int)(x + 0.5f);
            return (int)(x - 0.5f);
        }
        public static int Round(decimal x)
        {
            if (x >= 0)
                return (int)(x + 0.5m);
            return (int)(x - 0.5m);
        }
        public static int Round(double x)
        {
            if (x >= 0)
                return (int)(x + 0.5);
            return (int)(x - 0.5);
        }
    }
}