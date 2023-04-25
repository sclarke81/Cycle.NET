using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Cycle.NET.Demo
{
    internal static class Program
    {
#if ORIG_DEMO
        private static IObservable<int> LogDriver(IObservable<int> sinks)
        {
            sinks.Subscribe(i => Console.WriteLine("Log " + i.ToString()));

            return Observable.Empty<int>();
        }

        private static IObservable<int> KeyInputDriver(IObservable<int> sinks)
        {
            return Observable.Range(20, 5);
        }

        private static (
            IObservable<int> LogSinks,
            IObservable<int> KeyInputSinks)
            CycleMain(
            IObservable<int> logSources,
            IObservable<int> keyInputSources)
        {
            _ = logSources;
            var logSink = keyInputSources;

            return (
                logSink,
                Observable.Empty<int>());
        }
        static void Main(string[] args)
        {
            Runner.Run(
                (
                    IObservable<int> logSources,
                    IObservable<int> keyInputSources) => CycleMain(
                        logSources,
                        keyInputSources),
                LogDriver,
                KeyInputDriver);
            Console.ReadLine();
        }
#else
        private static IObservable<bool> DomDriver(IObservable<string> sinks)
        {
            sinks.Subscribe(Console.WriteLine);

            return new List<bool>
            {
                false,
                true,
                false,
                true,
            }
            .ToObservable();
        }

        private static
            IObservable<string>
            CycleMain(
            IObservable<bool> domSources)
        {
            var domSink =
                domSources
                .Select(toggled => toggled ? "ON" : "off");

            return domSink;
        }
        static void Main(string[] args)
        {
            Runner.Run(
                (
                    IObservable<bool> domSources) => CycleMain(
                        domSources),
                DomDriver);
            Console.ReadLine();
        }
#endif
    }
}
