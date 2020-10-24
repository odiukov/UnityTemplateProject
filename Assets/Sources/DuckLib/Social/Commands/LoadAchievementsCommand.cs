using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class LoadAchievementsCommand : ICommand<IAchievement[]>
    {
        private readonly ISocialPlatform _socialPlatform;

        public LoadAchievementsCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<IAchievement[]> Execute(Unit args = default)
        {
            return Observable.Create<IAchievement[]>(
                observer =>
                {
                    void OnLoadedAchievements(IAchievement[] achievements)
                    {
                        observer.OnNext(achievements);
                        observer.OnCompleted();
                    }

                    _socialPlatform.LoadAchievements(OnLoadedAchievements);
                    return Disposable.Empty;
                });
        }
    }
}