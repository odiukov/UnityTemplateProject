using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class ShowAchievementsCommand : AuthenticatedCommand
    {
        public ShowAchievementsCommand(ISocialPlatform socialPlatform) : base(socialPlatform)
        {
        }

        protected override void OnAuthenticateFault(string message)
        {
            FinishCommand();
        }

        protected override void OnAuthenticateResult(string message)
        {
            SocialPlatform.ShowAchievementsUI();
            FinishCommand();
        }
    }
}