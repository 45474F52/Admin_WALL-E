namespace Admin_WALL_E.Model.Messages
{
    internal sealed class ExecuteBotMessage
    {
        public ExecuteBotMessage(bool status)
        {
            Status = status;
            App.Messenger.Send(this);
        }

        public bool Status { get; private init; }
    }
}