using System;
using System.Collections.Generic;
using System.Linq;
using DuckLib.Social.Commands;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social
{
    public class SocialService
    {
        private readonly ISocialPlatform _socialPlatform;

        public bool IsAuthenticated => _socialPlatform.localUser.authenticated;
        public string SocialUserName => _socialPlatform.localUser.userName;
        public bool IsUserUnderAge => _socialPlatform.localUser.underage;
        public UserState UserOnlineState => _socialPlatform.localUser.state;
        public Texture2D UserImage => _socialPlatform.localUser.image;

        protected SocialService(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<AuthenticateResult> Authenticate()
        {
            return new AuthenticateCommand(_socialPlatform)
                .Execute();
        }

        public List<IUserProfile> GetFriends()
        {
            return _socialPlatform.localUser.friends.ToList();
        }

        public void ShowLeaderboardUi()
        {
            _socialPlatform.ShowLeaderboardUI();
        }

        public void ShowAchievementsUi()
        {
            _socialPlatform.ShowAchievementsUI();
        }

        public IObservable<IAchievement[]> GetAchievements()
        {
            return new LoadAchievementsCommand(_socialPlatform)
                .Execute();
        }

        public IObservable<IAchievementDescription[]> LoadAchievementDescriptions()
        {
            return new LoadAchievementDescriptionsCommand(_socialPlatform)
                .Execute();
        }

        public IObservable<IScore[]> LoadScores(string leaderboardId)
        {
            return new LoadScoresCommand(_socialPlatform)
                .Execute(leaderboardId);
        }

        public IObservable<IUserProfile[]> LoadUsers(string[] userIds)
        {
            return new LoadUsersCommand(_socialPlatform)
                .Execute(userIds);
        }

        public IObservable<bool> PostScore(long score, string leaderboardId)
        {
            return new PostScoreCommand(_socialPlatform)
                .Execute(new PostScoreArgs
                {
                    Score = score,
                    LeaderboardId = leaderboardId
                });
        }

        public IObservable<bool> ReportProgress(string achievementId, double progress = 100)
        {
            return new ReportProgressCommand(_socialPlatform)
                .Execute(new ReportProgressArgs
                {
                    Score = progress,
                    AchievementId = achievementId
                });
        }

        public static int ProgressProportion(int currentValue, int totalValue)
        {
            return 100 * currentValue / totalValue;
        }
    }
}