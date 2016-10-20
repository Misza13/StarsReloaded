namespace StarsReloaded.Client.ViewModel.Messages
{
    using GalaSoft.MvvmLight.Messaging;
    using StarsReloaded.Shared.Model;

    public class ShowMainWindowMessage : MessageBase
    {
        public Galaxy Galaxy { get; set; }

        public Race Race { get; set; }
    }
}