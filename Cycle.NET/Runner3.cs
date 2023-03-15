using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
using SdgApps.Common.DotnetSealedUnions;
using SdgApps.Common.DotnetSealedUnions.Generic;

namespace Cycle.NET
{
    public class Streams<T1, T2, T3> : List<IUnion3<IObservable<T1>, IObservable<T2>, IObservable<T3>>>
    {
        public Streams() : base() { }
        public Streams(List<IUnion3<IObservable<T1>, IObservable<T2>, IObservable<T3>>> value) : base(value) { }
    }

    public class Drivers<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3>
    {
        public Drivers(
            Func<IObservable<TSink1>, IObservable<TSource1>> onFirst,
            Func<IObservable<TSink2>, IObservable<TSource2>> onSecond,
            Func<IObservable<TSink3>, IObservable<TSource3>> onThird)
        {
            OnFirst = onFirst;
            OnSecond = onSecond;
            OnThird = onThird;
        }

        public Func<IObservable<TSink1>, IObservable<TSource1>> OnFirst { get; }
        public Func<IObservable<TSink2>, IObservable<TSource2>> OnSecond { get; }
        public Func<IObservable<TSink3>, IObservable<TSource3>> OnThird { get; }
    }

    public static class Runner<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3>
    {
        public static void Run(
            Func<Streams<TSource1, TSource2, TSource3>, Streams<TSink1, TSink2, TSink3>> main,
            Drivers<TSource1, TSink1, TSource2, TSink2, TSource3, TSink3> drivers)
        {
            // Create fake sinks to use to call the drivers to get around the interdependency between
            // main and the drivers.
            var sinksFactory = GenericUnions.TripletFactory<ISubject<TSink1>, ISubject<TSink2>, ISubject<TSink3>>();
            var fakeSinks = new List<IUnion3<ISubject<TSink1>, ISubject<TSink2>, ISubject<TSink3>>>
            {
                sinksFactory.First(new ReplaySubject<TSink1>()),
                sinksFactory.Second(new ReplaySubject<TSink2>()),
                sinksFactory.Third(new ReplaySubject<TSink3>()),
            };

            // Call the drivers and collate the returned sources.
            var sourcesFactory = GenericUnions.TripletFactory<IObservable<TSource1>, IObservable<TSource2>, IObservable<TSource3>>();
            var sources = fakeSinks.Select(sink => sink.Join(
                s1s => sourcesFactory.First(drivers.OnFirst(s1s)),
                s2s => sourcesFactory.Second(drivers.OnSecond(s2s)),
                s3s => sourcesFactory.Third(drivers.OnThird(s3s))));

            Streams<TSink1, TSink2, TSink3> sinks = main(new Streams<TSource1, TSource2, TSource3>(sources.ToList()));

            // Update the sinks returned from main with the sinks used by the drivers.
            foreach (var sink in sinks)
            {
                sink.Join(
                    mapFirst: s1 => s1.Subscribe(s => fakeSinks.SelectMany(f => f.Join(
                        mapFirst: f1 => new List<ISubject<TSink1>> { f1 },
                        mapSecond: f2 => Enumerable.Empty<ISubject<TSink1>>(),
                        mapThird: f3 => Enumerable.Empty<ISubject<TSink1>>())).Single().OnNext(s)),
                    mapSecond: s2 => s2.Subscribe(s => fakeSinks.SelectMany(f => f.Join(
                        mapFirst: f1 => Enumerable.Empty<ISubject<TSink2>>(),
                        mapSecond: f2 => new List<ISubject<TSink2>> { f2 },
                        mapThird: f3 => Enumerable.Empty<ISubject<TSink2>>())).Single().OnNext(s)),
                    mapThird: s3 => s3.Subscribe(s => fakeSinks.SelectMany(f => f.Join(
                        mapFirst: f1 => Enumerable.Empty<ISubject<TSink3>>(),
                        mapSecond: f2 => Enumerable.Empty<ISubject<TSink3>>(),
                        mapThird: f3 => new List<ISubject<TSink3>> { f3 })).Single().OnNext(s)));
            }
        }
    }
}
