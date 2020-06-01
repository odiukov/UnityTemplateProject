using DuckLib.Core;
using DuckLib.Core.Commands;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class LoadAchievementsCommand : CommandWithCallback<IAchievement[]>
    {
        private readonly ISocialPlatform _socialPlatform;

        public LoadAchievementsCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        protected override void OnStart()
        {
            new AuthenticateCommand(_socialPlatform)
                .AddCallback(new Responder(OnAuthenticateResult, OnAuthenticateFault))
                .Execute();
        }

        private void OnAuthenticateFault(string message)
        {
            FinishCommand(false);
        }

        private void OnAuthenticateResult(string message)
        {
            _socialPlatform.LoadAchievements(OnAchievementsLoaded);
        }

        private void OnAchievementsLoaded(IAchievement[] result)
        {
            FinishCommand(true, result);
        }
    }
}