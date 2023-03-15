using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using SdgApps.Common.DotnetSealedUnions.Generic;
using System.Reactive.Subjects;
using System.Linq;

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

        private static Streams<int, int, Unit> CycleMain(Streams<int, int, Unit> sources)
        {
            var sinksFactory = GenericUnions.TripletFactory<IObservable<int>, IObservable<int>, IObservable<Unit>>();
            var sinks = new Streams<int, int, Unit>
            {
                sinksFactory.First(sources.SelectMany(s => s.Join(
                    mapFirst: s1 => Enumerable.Empty<IObservable<int>>(),
                    mapSecond: s2 => new List<IObservable<int>> { s2 },
                    mapThird: s3 => Enumerable.Empty<IObservable<int>>())).Single()),
            };

            return sinks;
        }
        static void Main(string[] args)
        {
            var drivers = new Drivers<int, int, int, int, Unit, Unit>(
                onFirst: LogDriver,
                onSecond: KeyInputDriver,
                onThird: _ => _);
            Runner<int, int, int, int, Unit, Unit>.Run(CycleMain, drivers);
            Console.ReadLine();
        }
    }
}
