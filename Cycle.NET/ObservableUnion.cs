using System;
using System.Reactive.Linq;
using SdgApps.Common.DotnetSealedUnions;
using SdgApps.Common.DotnetSealedUnions.Generic;

namespace Cycle.NET
{
    public static class ObservableUnion
    {
        public static IObservable<IUnion2<TFirst, TSecond>> Merge<TFirst, TSecond>(
            IObservable<TFirst> firsts,
            IObservable<TSecond> seconds)
        {
            var fac = GenericUnions.DoubletFactory<TFirst, TSecond>();

            var unionFirsts = firsts.Select(fac.First);
            var unionSeconds = seconds.Select(fac.Second);

            return Observable.Merge(
                unionFirsts,
                unionSeconds);
        }
    }
}
