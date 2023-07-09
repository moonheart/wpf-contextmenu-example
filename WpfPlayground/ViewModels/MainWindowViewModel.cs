using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using WpfPlayground.Utils;
using WpfPlayground.Wpf;

namespace WpfPlayground.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<MenuItemInfo> ContextMenuInfos { get; set; } =
            new ObservableCollection<MenuItemInfo>();

        public ObservableCollection<MyTreeNode> TreeNodes { get; } = new ObservableCollection<MyTreeNode>();

        public ObservableCollection<MyTreeNode> DataGridItems { get; } = new ObservableCollection<MyTreeNode>();

        public MainWindowViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                var index = RandomUtil.GetRandomInt(1, 1000);
                TreeNodes.Add(new MyTreeNode(null, true) {Name = $"Item {index}", Index = index});
            }

            for (int i = 0; i < 10; i++)
            {
                var index = RandomUtil.GetRandomInt(1, 1000);
                DataGridItems.Add(new MyTreeNode(null, true) {Name = $"Item {index}", Index = index});
            }

            ContextMenuInfos.AddRange(new[]
            {
                new MenuItemInfo
                {
                    Header = "偶数菜单",
                    Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "偶数菜单")),
                    CanExecute = o => o is MyTreeNode node && node.Index % 2 == 0,
                    Children = new List<MenuItemInfo>()
                    {
                        new MenuItemInfo()
                        {
                            Header = "整除4的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除4的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 4 == 0,
                        },
                        new MenuItemInfo()
                        {
                            Header = "整除6的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除6的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 6 == 0,
                        },
                        new MenuItemInfo()
                        {
                            Header = "整除8的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除8的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 8 == 0,
                        },
                        new MenuItemInfo()
                        {
                            Header = "整除10的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除10的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 10 == 0,
                        }
                    }
                },
                new MenuItemInfo
                {
                    Header = "奇数菜单",
                    Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "奇数菜单")),
                    CanExecute = o => o is MyTreeNode node && node.Index % 2 == 1,
                    Children = new List<MenuItemInfo>()
                    {
                        new MenuItemInfo()
                        {
                            Header = "整除3的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除3的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 3 == 0,
                        },
                        new MenuItemInfo()
                        {
                            Header = "整除5的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除5的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 5 == 0,
                        },
                        new MenuItemInfo()
                        {
                            Header = "整除7的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除7的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 7 == 0,
                        },
                        new MenuItemInfo()
                        {
                            Header = "整除9的菜单",
                            Command = new DelegateCommand<MyTreeNode>(node => MessageBox.Show(node.Name, "整除9的菜单")),
                            CanExecute = o => o is MyTreeNode node && node.Index % 9 == 0,
                        }
                    }
                }
            });
        }
    }

    public class MyTreeNode : TreeViewItemViewModel
    {
        private int _index;
        private string _name;

        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        protected override void LoadChildren()
        {
            for (int i = 0; i < 3; i++)
            {
                var index = RandomUtil.GetRandomInt(1, 1000);
                Children.Add(new MyTreeNode(this, true) {Name = $"Item {index}", Index = index});
            }
        }

        public MyTreeNode(TreeViewItemViewModel parent, bool lazyLoadChildren = false) : base(parent, lazyLoadChildren)
        {
        }
    }

}