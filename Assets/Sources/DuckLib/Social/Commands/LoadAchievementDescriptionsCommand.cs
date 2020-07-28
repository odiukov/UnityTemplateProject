using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class LoadAchievementDescriptionsCommand : ICommand<IAchievementDescription[]>
    {
        private readonly ISocialPlatform _socialPlatform;

        public LoadAchievementDescriptionsCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<IAchievementDescription[]> Execute()
        {
            return Observable.Create<IAchievementDescription[]>(
                observer =>
                {
                    void OnLoadedAchievements(IAchievementDescription[] achievements)
                    {
                        observer.OnNext(achievements);
                        observer.OnCompleted();
                    }

                    _socialPlatform.LoadAchievementDescriptions(OnLoadedAchievements);
                    return Disposable.Empty;
                });
        }
    }
}