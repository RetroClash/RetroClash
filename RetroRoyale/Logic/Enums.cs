namespace RetroRoyale.Logic
{
    public class Enums
    {
        public enum AllianceEvent
        {
            Kicked = 1,
            AcceptedJoinRequest = 2,
            JoinedClan = 3,
            Left = 4,
            Promoted = 5,
            Demoted = 6
        }

        public enum AllianceType
        {
            AnyoneCanJoin = 0,
            InviteOnly = 1,
            Closed = 2
        }

        public enum Gamefile
        {
            Achievements = 1,
        }

        public enum LogType
        {
            Info = 1,
            Warning = 2,
            Error = 3,
            Debug = 4
        }

        public enum Role
        {
            Member = 1,
            Leader = 2,
            Elder = 3,
            CoLeader = 4
        }

        public enum State
        {
            Login = 0,
            Home = 1,
            Npc = 2,
            Battle = 3,
            Replay = 4
        }
    }
}