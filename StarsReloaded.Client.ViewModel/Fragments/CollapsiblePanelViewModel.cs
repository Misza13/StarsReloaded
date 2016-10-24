namespace StarsReloaded.Client.ViewModel.Fragments
{
    using System.Windows;

    using GalaSoft.MvvmLight.Command;

    using StarsReloaded.Client.ViewModel.Attributes;

    public class CollapsiblePanelViewModel : BaseViewModel
    {
        private bool isExpanded;

        private string header;

        public CollapsiblePanelViewModel()
        {
            this.CollapseCommand = new RelayCommand(this.Collapse);
            this.ExpandCommand = new RelayCommand(this.Expand);

            this.IsExpanded = true; ////TODO: save/read from user's settings

            if (this.IsInDesignMode)
            {
                this.Header = "Some Panel";
            }
        }

        public string Header
        {
            get
            {
                return this.header;
            }

            set
            {
                this.Set(() => this.Header, ref this.header, value);
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }

            set
            {
                this.Set(() => this.IsExpanded, ref this.isExpanded, value);
            }
        }

        [DependsUpon(nameof(IsExpanded))]
        public Visibility ExpandButtonVisibility => this.IsExpanded ? Visibility.Hidden : Visibility.Visible;

        [DependsUpon(nameof(IsExpanded))]
        public Visibility CollapseButtonVisibility => this.IsExpanded ? Visibility.Visible : Visibility.Hidden;

        [DependsUpon(nameof(IsExpanded))]
        public Visibility ContentVisibility => this.IsExpanded ? Visibility.Visible : Visibility.Collapsed;

        public RelayCommand CollapseCommand { get; private set; }

        public RelayCommand ExpandCommand { get; private set; }

        private void Collapse()
        {
            this.IsExpanded = false;
        }

        private void Expand()
        {
            this.IsExpanded = true;
        }
    }
}