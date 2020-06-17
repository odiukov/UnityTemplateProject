using DuckLib.Social.Commands;
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
            new ShowLeaderboardCommand(_socialPlatform).Execute();
        }

        public void ShowAchievementsUi()
        {
            new ShowAchievementsCommand(_socialPlatform).Execute();
        }

        public void PostScore(long score, string leaderboardId)
        {
            new PostScoreCommand(_socialPlatform)
                .SetArgs(new PostScoreArgs
                {
                    Score = score,
                    LeaderboardId = leaderboardId
                }).Execute();
        }

        public void ReportProgress(string achievementId, double progress = 100)
        {
            new ReportProgressCommand(_socialPlatform)
                .SetArgs(new ReportProgressArgs
                {
                    Score = progress,
                    AchievementId = achievementId
                }).Execute();
        }

        public static int ProgressProportion(int currentValue, int totalValue)
        {
            return 100 * currentValue / totalValue;
        }
    }
}