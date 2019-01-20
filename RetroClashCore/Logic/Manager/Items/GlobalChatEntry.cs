namespace RetroClashCore.Logic.Manager.Items
{
    public class GlobalChatEntry
    {
        public long SenderId { get; set; }

        public string SenderName { get; set; }

        public string Message { get; set; }

        public int SenderLeague { get; set; }

        public int SenderExpLevel { get; set; }
    }
}