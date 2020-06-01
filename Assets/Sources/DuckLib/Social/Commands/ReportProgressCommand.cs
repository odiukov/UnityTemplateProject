using DuckLib.Core;
using DuckLib.Core.Commands;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class ReportProgressCommand : CommandWithArgs<ReportProgressArgs>
    {
        private readonly ISocialPlatform _socialPlatform;

        public ReportProgressCommand(ISocialPlatform socialPlatform)
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
            FinishCommand();
        }

        private void OnAuthenticateResult(string message)
        {
            _socialPlatform.ReportProgress(Args.AchievementId, Args.Score, OnLeaderboardUpdated);
        }

        private void OnLeaderboardUpdated(bool result)
        {
            FinishCommand();
        }
    }

    public class ReportProgressArgs
    {
        public string AchievementId;
        public double Score;
    }
}