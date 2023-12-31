# WPF MVVM 实现列表项动态右键菜单

在某个项目需求中，需要为列表中的每一项都添加一个右键菜单，但是每一项的菜单项是不一样的，有的菜单项需要显示，有的菜单项不需要显示，这就需要动态的去判断哪些菜单项需要显示，哪些菜单项不需要显示。

一种实现的方式是为每种可能的组合预定义多组 `ContextMenu`，然后在右键事件里面去判断需要采用哪组 `ContextMenu`，但这不够优雅，不够 MVVM。

这里我才用另一种方法，核心是通过 `Binding` 时的 `IValueConverter` 动态的去判断哪些菜单项需要显示，哪些不需要显示。

```csharp
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
```

`CommandEx.ItemsSourceProperty` 用于传递 ContextMenu 的数据绑定，定义如下：
```csharp
public class CommandEx
{
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.RegisterAttached(
        "ItemsSource",
        typeof(IEnumerable),
        typeof(CommandEx));

    public static IEnumerable GetItemsSource(DependencyObject obj)
    {
        return (IEnumerable) obj.GetValue(ItemsSourceProperty);
    }

    public static void SetItemsSource(DependencyObject obj, object value)
    {
        obj.SetValue(ItemsSourceProperty, value);
    }
}
```

## 效果：

![](imgs/img1.png)
![](imgs/img2.png)
![](imgs/img3.png)
![](imgs/img4.png)
