using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WpfPlayground.Wpf
{
    public class MenuItemInfo
    {
        /// <summary>
        /// 显示文字
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 判断当前菜单是否可用
        /// </summary>
        public Func<object, bool> CanExecute { get; set; }

        /// <summary>
        /// 菜单点击执行的命令
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public IEnumerable<MenuItemInfo> Children { get; set; }


        public MenuItemInfo()
        {
            Children = new List<MenuItemInfo>();
        }
    }
}