using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Cycle.NET.Demo
{
    internal static class Program
    {
        private static IObservable<object> LogDriver(IObservable<object> sinks)
        {
            sinks.Cast<int>().Subscribe(i => Console.WriteLine("Log " + i.ToString()));

            return Observable.Empty<object>();
        }

        private static IObservable<object> KeyInputDriver(IObservable<object> sinks)
        {
            _ = sinks;

            return Observable.Range(20, 5).Select(i => (object)i);
        }

        private static Streams CycleMain(Streams sources)
        {
            var sinks = new Streams
            {
                { "log", sources["keys"] }
            };

            return sinks;
        }
        static void Main()
        {
            var drivers = new Drivers
            {
                { "log", LogDriver },
                { "keys", KeyInputDriver }
            };
            Runner.Run(CycleMain, drivers);
            Console.ReadLine();
        }
    }
}
