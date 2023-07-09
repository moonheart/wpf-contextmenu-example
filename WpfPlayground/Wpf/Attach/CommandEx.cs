using System.Collections;
using System.Windows;

namespace WpfPlayground.Wpf.Attach
{
    public class CommandEx
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable), typeof(CommandEx), new PropertyMetadata(null));

        public static IEnumerable GetItemsSource(DependencyObject obj)
        {
            return (IEnumerable) obj.GetValue(ItemsSourceProperty);
        }

        public static void SetItemsSource(DependencyObject obj, object value)
        {
            obj.SetValue(ItemsSourceProperty, value);
        }
    }
}