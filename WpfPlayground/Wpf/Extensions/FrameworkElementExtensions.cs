using System.Windows;
using System.Windows.Media;

namespace WpfPlayground.Wpf.Extensions
{
    public static class FrameworkElementExtensions
    {
        public static FrameworkElement FindAncestor<T>(this FrameworkElement frameworkElement) where T : FrameworkElement
        {
            while (frameworkElement != null)
            {
                if (frameworkElement is T) return frameworkElement;
                frameworkElement = VisualTreeHelper.GetParent(frameworkElement) as FrameworkElement;
            }

            return null;
        }
    }
}