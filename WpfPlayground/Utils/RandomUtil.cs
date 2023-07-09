using System;

namespace WpfPlayground.Utils
{
    public class RandomUtil
    {
        private static Random _random = new Random( (int) DateTime.Now.Ticks );
        
        public static int GetRandomInt(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}