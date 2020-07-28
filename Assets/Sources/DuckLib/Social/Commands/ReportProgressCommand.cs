using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class ReportProgressCommand : ICommand<ReportProgressArgs, bool>
    {
        private readonly ISocialPlatform _socialPlatform;

        public ReportProgressCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<bool> Execute(ReportProgressArgs args)
        {
            return Observable.Create<bool>(observer =>
            {
                _socialPlatform.ReportProgress(args.AchievementId, args.Score, result =>
                {
                    observer.OnNext(result);
                    observer.OnCompleted();
                });
                return Disposable.Empty;
            });
        }
    }

    public class ReportProgressArgs
    {
        public string AchievementId;
        public double Score;
    }
}