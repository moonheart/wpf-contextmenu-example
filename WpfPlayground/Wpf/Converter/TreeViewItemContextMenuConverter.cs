using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using WpfPlayground.Wpf.Attach;
using WpfPlayground.Wpf.Extensions;

namespace WpfPlayground.Wpf.Converter
{
    public class TreeViewItemContextMenuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeViewItem treeViewItem)
            {
                var treeView = treeViewItem.FindAncestor<TreeView>();
                if (treeView.GetValue(CommandEx.ItemsSourceProperty) is IEnumerable<MenuItemInfo> itemInfos)
                {
                    return CheckMenuInfos(itemInfos, treeViewItem.DataContext);
                }
            }

            return value;
        }

        private List<MenuItemInfo> CheckMenuInfos(IEnumerable<MenuItemInfo> menuItemInfos, object dataContext)
        {
            var menus = new List<MenuItemInfo>();
            foreach (var itemInfo in menuItemInfos)
            {
                if (itemInfo.CanExecute?.Invoke(dataContext) ?? true)
                {
                    menus.Add(new MenuItemInfo()
                    {
                        Header = itemInfo.Header,
                        Command = itemInfo.Command,
                        Children = CheckMenuInfos(itemInfo.Children, dataContext)
                    });
                }
            }

            return menus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}