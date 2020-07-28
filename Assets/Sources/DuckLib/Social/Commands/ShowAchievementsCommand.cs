using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class ShowAchievementsCommand : ICommand<bool>
    {
        private readonly ISocialPlatform _socialPlatform;

        public ShowAchievementsCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<bool> Execute()
        {
            return Observable.Create<bool>(observer =>
            {
                _socialPlatform.ShowAchievementsUI();
                observer.OnNext(true);
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}