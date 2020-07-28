using System;
using DuckLib.Social.Commands;
using DuckLib.Social.Extensions;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social
{
    public class SocialService
    {
        private readonly ISocialPlatform _socialPlatform;

        protected SocialService(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public void ShowLeaderboardUi()
        {
            _socialPlatform
                .ContinueAfterAuthenticate(new ShowLeaderboardCommand(_socialPlatform).Execute());
        }

        public void ShowAchievementsUi()
        {
            _socialPlatform
                .ContinueAfterAuthenticate(new ShowAchievementsCommand(_socialPlatform).Execute());
        }

        public IObservable<bool> PostScore(long score, string leaderboardId)
        {
            return _socialPlatform
                .ContinueAfterAuthenticate(new PostScoreCommand(_socialPlatform).Execute(new PostScoreArgs
                {
                    Score = score,
                    LeaderboardId = leaderboardId
                }));
        }

        public IObservable<bool> ReportProgress(string achievementId, double progress = 100)
        {
            return _socialPlatform
                .ContinueAfterAuthenticate(new ReportProgressCommand(_socialPlatform)
                    .Execute(new ReportProgressArgs
                    {
                        Score = progress,
                        AchievementId = achievementId
                    }));
        }

        public static int ProgressProportion(int currentValue, int totalValue)
        {
            return 100 * currentValue / totalValue;
        }
    }
}