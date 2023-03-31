using System;
using System.Reactive.Linq;
using SdgApps.Common.DotnetSealedUnions;
using SdgApps.Common.DotnetSealedUnions.Generic;

namespace Cycle.NET
{
    public class Streams<TFirst, TSecond>
    {
        public Streams(
            IObservable<TFirst> first,
            IObservable<TSecond> second)
        {
            First = first;
            Second = second;
        }

        public IObservable<TFirst> First { get; }
        public IObservable<TSecond> Second { get; }

        public IObservable<IUnion2<TFirst, TSecond>> ToStream()
        {
            var fac = GenericUnions.DoubletFactory<TFirst, TSecond>();

            var first = this.First.Select(fac.First);
            var second = this.Second.Select(fac.Second);

            return Observable.Merge(
                first,
                second);
        }
    }
}
