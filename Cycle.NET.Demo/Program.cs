using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;

namespace Cycle.NET.Demo
{
    class Program
    {
        private static IObservable<object> LogDriver(IObservable<object> sinks)
        {
            sinks.Cast<int>().Subscribe(i => Console.WriteLine("Log " + i.ToString()));

            return Observable.Empty<object>();
        }

        private static IObservable<int> LogDriver(IObservable<int> sinks)
        {
            sinks.Subscribe(i => Console.WriteLine("Log " + i.ToString()));

            return Observable.Empty<int>();
        }

        private static IObservable<object> KeyInputDriver(IObservable<object> sinks)
        {
            return Observable.Range(20, 5).Select(i => (object)i);
        }

        private static IObservable<int> KeyInputDriver(IObservable<int> sinks)
        {
            return Observable.Range(20, 5);
        }

        private static Streams CycleMain(Streams sources)
        {
            var sinks = new Streams
            {
                { "log", sources["keys"] }
            };

            return sinks;
        }

        private static (
            IObservable<int> LogSinks,
            IObservable<int> KeyInputSinks)
            CycleMain(
            IObservable<int> logSources,
            IObservable<int> keyInputSources)
        {
            var logSink = keyInputSources;

            return (
                logSink,
                Observable.Empty<int>());
        }

        static void Main(string[] args)
        {
            var drivers = new Drivers
            {
                { "log", LogDriver },
                { "keys", KeyInputDriver }
            };
            Runner.Run(CycleMain, drivers);

            Runner<int, int, int, int>.Run(
                CycleMain,
                LogDriver,
                KeyInputDriver);

            Console.ReadLine();
        }
    }
}
