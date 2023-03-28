using System;
using System.Reactive;
using System.Reactive.Linq;
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

        private static IObservable<IUnion2<int, int>> Driver(IObservable<IUnion2<int, int>> sinks)
        {
            var fac = GenericUnions.DoubletFactory<int, int>();

            var logSinks = sinks.SelectMany(s => s.Join(
                mapFirst: Observable.Return,
                mapSecond: _ => Observable.Empty<int>()));
            var logSources = LogDriver(logSinks);

            var keyInputSinks = sinks.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<int>(),
                mapSecond: Observable.Return));
            var keyInputSources = KeyInputDriver(keyInputSinks);

            return Observable.Merge(
                logSources.Select(fac.First),
                keyInputSources.Select(fac.Second));
        }

        private static IObservable<IUnion2<int, int>> CycleMain(IObservable<IUnion2<int, int>> sources)
        {
            var fac = GenericUnions.DoubletFactory<int, int>();

            var keyInputSources = sources.SelectMany(s => s.Join(
                mapFirst: _ => Observable.Empty<int>(),
                mapSecond: Observable.Return));
            var sinks = keyInputSources.Select(fac.First);

            return sinks;
        }
        static void Main(string[] args)
        {
            var component = new Component<IUnion2<int, int>, IUnion2<int, int>>(
                CycleMain);
            var drivers = new Drivers<IUnion2<int, int>, IUnion2<int, int>>(
                onFirst: Driver);
            Runner<IUnion2<int, int>, IUnion2<int, int>>.Run(component, drivers);
            Console.ReadLine();
        }
    }
}
