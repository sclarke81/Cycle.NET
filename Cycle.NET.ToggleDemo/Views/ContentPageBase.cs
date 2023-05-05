using Cycle.NET.ToggleDemo.ViewModels;
using ReactiveUI.Maui;

namespace Cycle.NET.ToggleDemo.Views
{
    /// <summary>
    /// A base page used for all our content pages. It is mainly used for interaction registrations.
    /// </summary>
    /// <typeparam name="TViewModel">The view model which the page contains.</typeparam>
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel>
        where TViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPageBase{TViewModel}"/> class.
        /// </summary>
        public ContentPageBase()
        {
        }
    }
}
