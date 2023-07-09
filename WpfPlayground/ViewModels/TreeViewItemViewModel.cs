using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace WpfPlayground.ViewModels
{
    public class TreeViewItemViewModel: BindableBase
    {
        private static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();
        
        private TreeViewItemViewModel(){}
        public TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren = false)
        {
            _parent = parent;
            Children = new ObservableCollection<TreeViewItemViewModel>();
            if (lazyLoadChildren)
            {
                Children.Add(DummyChild);
            }
        }

        private bool _isExpanded;
        private readonly TreeViewItemViewModel _parent;
        private bool _isSelected;

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                SetProperty(ref _isExpanded, value);
                
                // 如果当前节点展开，那么父节点也要展开
                if (_isExpanded && _parent != null)
                {
                    _parent.IsExpanded = true;
                }
                
                // 懒加载：如果当前节点是初始状态，那么加载子节点
                if (Children.Count == 1 && Children[0] == DummyChild)
                {
                    Children.Clear();
                    LoadChildren();
                }
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public TreeViewItemViewModel Parent
        {
            get => _parent;
        }
        
        /// <summary>
        /// 派生类重写此方法，可以在启用懒加载的时候动态加载子节点
        /// </summary>
        protected virtual void LoadChildren()
        {
            
        }

        public ObservableCollection<TreeViewItemViewModel> Children { get; set; }
    }
}