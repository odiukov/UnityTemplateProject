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

        public IObservable<IAchievement[]> Execute()
        {
            return new AuthenticateCommand(_socialPlatform).Execute()
                .ContinueWith(Observable.Create<IAchievement[]>(
                    observer =>
                    {
                        _socialPlatform.LoadAchievements(achievements =>
                        {
                            observer.OnNext(achievements);
                            observer.OnCompleted();
                        });
                        return Disposable.Empty;
                    }));
        }
    }
}