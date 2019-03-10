using System;
using System.Collections.Generic;
using RetroClash.Protocol.Messages.Client;

namespace RetroClash.Protocol
{
    public class LogicMagicMessageFactory
    {
        public static Dictionary<int, Type> Messages;

        static LogicMagicMessageFactory()
        {
            Messages = new Dictionary<int, Type>
            {
                {10101, typeof(LoginMessage)},
                {10108, typeof(KeepAliveMessage)},
                //{10112, typeof(AuthenticationCheckMessage)},
                {10113, typeof(SetDeviceTokenMessage)},
                //{10116, typeof(ResetAccountMessage)},
                //{10117, typeof(ReportUserMessage)},
                {10118, typeof(AccountSwitchedMessage)},
                //{10150, typeof(AppleBillingRequestMessage)},
                //{10151, typeof(GoogleBillingRequestMessage)},
                {10212, typeof(ChangeAvatarNameMessage)},
                //{10503, typeof(AskForAddableFriendsMessage)},
                //{10512, typeof(AskForPlayingGamecenterFriendsMessage)},
                //{10513, typeof(AskForPlayingFacebookFriendsMessage)},
                {14101, typeof(GoHomeMessage)},
                {14102, typeof(EndClientTurnMessage)},
                //{14104, typeof(AskForTargetHomeListMessage)},
                //{14106, typeof(AttackHomeMessage)},
                //{14108, typeof(ChangeHomeNameMessage)},
                {14113, typeof(VisitHomeMessage)},
                {14114, typeof(HomeBattleReplayMessage)},
                //{14123, typeof(AttackMatchedHomeMessage)},
                {14134, typeof(AttackNpcMessage)},
                {14201, typeof(BindFacebookAccountMessage)},
                {14211, typeof(UnbindFacebookAccountMessage)},
                {14262, typeof(BindGoogleServiceAccountMessage)},
                {14301, typeof(CreateAllianceMessage)},
                {14302, typeof(AskForAllianceDataMessage)},
                {14303, typeof(AskForJoinableAlliancesListMessage)},
                {14305, typeof(JoinAllianceMessage)},
                //{14306, typeof(ChangeAllianceMemberRoleMessage)},
                //{14308, typeof(LeaveAllianceMessage)},
                //{14309, typeof(AskForAllianceUnitDonationsMessage)},
                //{14310, typeof(DonateAllianceUnitMessage)},
                {14315, typeof(ChatToAllianceStreamMessage)},
                //{14316, typeof(ChangeAllianceSettingsMessage)},
                //{14317, typeof(RequestJoinAllianceMessage)},
                //{14321, typeof(RespondToAllianceJoinRequestMessage)},
                //{14322, typeof(SendAllianceInvitationMessage)},
                //{14323, typeof(JoinAllianceUsingInvitationMessage)},
                //{14324, typeof(SearchAlliancesMessage)},
                {14325, typeof(AskForAvatarProfileMessage)},
                {14401, typeof(AskForAllianceRankingListMessage)},
                {14403, typeof(AskForAvatarRankingListMessage)},
                {14404, typeof(AskForAvatarLocalRankingListMessage)},
                //{14405, typeof(AskForAvatarStreamMessage)},
                //{14503, typeof(AskForLeagueMemberListMessage)},
                {14715, typeof(SendGlobalChatLineMessage)}
            };
        }
    }
}