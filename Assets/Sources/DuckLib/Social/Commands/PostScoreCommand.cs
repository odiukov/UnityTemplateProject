using DuckLib.Core;
using DuckLib.Core.Commands;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class PostScoreCommand : CommandWithArgs<PostScoreArgs>
    {
        private readonly ISocialPlatform _socialPlatform;

        public PostScoreCommand(ISocialPlatform socialPlatform)
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
            _socialPlatform.ReportScore(Args.Score, Args.LeaderboardId, OnLeaderboardUpdated);
        }

        private void OnLeaderboardUpdated(bool result)
        {
            FinishCommand();
        }
    }

    public class PostScoreArgs
    {
        public string LeaderboardId;
        public long Score;
    }
}