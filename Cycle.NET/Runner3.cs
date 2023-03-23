using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cycle.NET
{
    public interface IComponent
    {
        ISinks Main();
    }

    public interface ISinks
    {
        void Subscribe();
    }

    public interface IDrivers
    {
        IFakeSinks CreateFakeSinks();
    }

    public interface IFakeSinks
    {
        IComponent Invoke();
    }

    public static class Runner<TD, TM>
    {
        public static void Run(
            IDrivers drivers)
        {
            // Create fake sinks to use to call the drivers to get around the interdependency between
            // main and the drivers.
            var fakeSinks = drivers.CreateFakeSinks();

            // Call the drivers and collate the returned sources.
            var sources = fakeSinks.Invoke();

            var sinks = sources.Main();

            // Update the sinks returned from main with the sinks used by the drivers.
            sinks.Subscribe();
        }
    }
}
