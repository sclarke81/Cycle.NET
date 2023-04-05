using System;
using System.Reactive;
using System.Reactive.Linq;
using Cycle.NET.Extensions;
using SdgApps.Common.DotnetSealedUnions;
using SdgApps.Common.DotnetSealedUnions.Generic;

namespace Cycle.NET.Demo
{
    class Program
    {
        private static IObservable<int> LogDriver(IObservable<int> sinks)
        {
            sinks.Subscribe(i => Console.WriteLine("Log " + i.ToString()));

            return Observable.Empty<int>();
        }

        private static IObservable<int> KeyInputDriver(IObservable<int> sinks)
        {
            return Observable.Range(20, 5);
        }

        private static IObservable<IUnion2<int, int>> CycleMain(IObservable<IUnion2<int, int>> sources)
        {
            var (
                _,
                keyInputSources) = sources.Split();

            var logSink = keyInputSources;

            return ObservableUnion.Merge(
                logSink,
                Observable.Empty<int>());
        }
        static void Main(string[] args)
        {
            Runner.Run(
                CycleMain,
                (IObservable<IUnion2<int, int>> sinks) => sinks.CallDrivers(
                    LogDriver,
                    KeyInputDriver));
            Console.ReadLine();
        }
    }
}
