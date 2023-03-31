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

        private static IObservable<IUnion2<int, int>> Driver(IObservable<IUnion2<int, int>> sinks)
        {
            var sinkStreams = sinks.ToStreams();

            var logSinks = sinkStreams.First;
            var logSources = LogDriver(logSinks);

            var keyInputSinks = sinkStreams.Second;
            var keyInputSources = KeyInputDriver(keyInputSinks);

            return new Streams<int, int>(
                logSources,
                keyInputSources)
                .ToStream();
        }

        private static IObservable<IUnion2<int, int>> CycleMain(IObservable<IUnion2<int, int>> sources)
        {
            var sourceStreams = sources.ToStreams();

            var keyInputSource = sourceStreams.Second;
            var logSink = keyInputSource;
            var sinkStreams = new Streams<int, int>(
                logSink,
                Observable.Empty<int>());

            return sinkStreams.ToStream();
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
