using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using WpfPlayground.Wpf.Attach;
using WpfPlayground.Wpf.Extensions;

namespace WpfPlayground.Wpf.Converter
{
    public class ContextMenuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case TreeViewItem treeViewItem when treeViewItem.FindAncestor<TreeView>()?.GetValue(CommandEx.ItemsSourceProperty) is IEnumerable<MenuItemInfo> itemInfos:
                    return CheckMenuInfos(itemInfos, treeViewItem.DataContext);
                case DataGridRow dataGridRow when dataGridRow.FindAncestor<DataGrid>()?.GetValue(CommandEx.ItemsSourceProperty) is IEnumerable<MenuItemInfo> itemInfos:
                    return CheckMenuInfos(itemInfos, dataGridRow.DataContext);
                default:
                    return value;
            }
        }

        private List<MenuItemInfo> CheckMenuInfos(IEnumerable<MenuItemInfo> menuItemInfos, object dataContext)
        {
            var menus = new List<MenuItemInfo>();
            foreach (var itemInfo in menuItemInfos)
            {
                if (itemInfo.CanExecute?.Invoke(dataContext) ?? true)
                {
                    menus.Add(new MenuItemInfo
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