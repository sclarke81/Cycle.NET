using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cycle.NET
{
    public static class Runner<TSources, TSinks>
    {
        public static void Run(
            Func<IObservable<TSources>, IObservable<TSinks>> main,
            Func<IObservable<TSinks>, IObservable<TSources>> drivers)
        {
            // Create fake sinks to use to call the drivers to get around the interdependency between
            // main and the drivers.
            var fakeSinks = new ReplaySubject<TSinks>();

            // Call the drivers and collate the returned sources.
            var sources = drivers(fakeSinks);

            var sinks = main(sources);

            // Update the sinks returned from main with the sinks used by the drivers.
            sinks.Subscribe(fakeSinks.OnNext);
        }
    }
}
