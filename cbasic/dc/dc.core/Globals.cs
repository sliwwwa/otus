using System;

namespace dc.core
{
    public static class Globals
    {
        public static string UserName = "";
        public static LinkedList<string> Menu = new(); //Используем связный список LinkedList<T> из пространства имен System.Collections.Generic
        public static string Version = "v1.0.1";
        public static string ReleaseDate = "Release date: 20.08.2025";
//        public static List<string> Schedule = new();
    }
}