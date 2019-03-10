namespace RetroClash.Logic
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
            AllianceBadges = 2,
            AlliancePortal = 3,
            BillingPackages = 4,
            BuildingClasses = 5,
            Buildings = 6,
            Characters = 7,
            Decos = 8,
            Effects = 9,
            ExperienceLevels = 10,
            Faq = 11,
            Globals = 12,
            Heroes = 13,
            Hints = 14,
            Leagues = 15,
            Locales = 16,
            Missions = 17,
            News = 18,
            Npcs = 19,
            Obstacles = 20,
            ParticleEmitters = 21,
            Projectiles = 22,
            ResourcePacks = 23,
            Resources = 24,
            Shields = 25,
            Spells = 26,
            Texts = 27,
            Traps = 28
        }

        public enum LogType
        {
            Info = 1,
            Warning = 2,
            Error = 3,
            Debug = 4
        }

        public enum Resource
        {
            Gold = 3000001,
            Elixir = 3000002,
            DarkElixir = 3000003
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
            Visiting = 4,
            Replay = 5
        }
    }
}