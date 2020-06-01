using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class ShowLeaderboardCommand : AuthenticatedCommand
    {
        public ShowLeaderboardCommand(ISocialPlatform socialPlatform) : base(socialPlatform)
        {
        }

        protected override void OnAuthenticateFault(string message)
        {
            FinishCommand();
        }

        protected override void OnAuthenticateResult(string message)
        {
            SocialPlatform.ShowLeaderboardUI();
            FinishCommand();
        }
    }
}