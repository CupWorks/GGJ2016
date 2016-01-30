using System;
using System.Collections.Generic;

namespace Game.Core
{
    public static class Extensions
    {
        private static Random Random { get; set; } = new Random();

        public static TValue GetRandomValue<TValue>(this IList<TValue> list)
        {
            return list[Random.Next(0, list.Count)];
        } 
    }
}