namespace StarsReloaded.Client.ViewModel.Fragments
{
    using GalaSoft.MvvmLight.Command;

    using StarsReloaded.Client.ViewModel.Attributes;

    public class CollapsiblePanelViewModel : BaseViewModel
    {
        private bool isExpanded;

        private string header;

        public CollapsiblePanelViewModel()
        {
            this.ToggleExpansionCommand = new RelayCommand(this.ToggleExpansion);

            this.IsExpanded = true; ////TODO: save/read from user's settings

            if (this.IsInDesignMode)
            {
                this.Header = "Some Panel";
            }
        }

        public string Header
        {
            get { return this.header; }
            set { this.Set(() => this.Header, ref this.header, value); }
        }

        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set { this.Set(() => this.IsExpanded, ref this.isExpanded, value); }
        }

        [DependsUpon(nameof(IsExpanded))]
        public bool IsCollapsed => !this.IsExpanded;

        public RelayCommand ToggleExpansionCommand { get; private set; }

        private void ToggleExpansion()
        {
            this.IsExpanded = !this.IsExpanded;
        }
    }
}