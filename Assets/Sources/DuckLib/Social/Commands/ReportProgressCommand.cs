using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class ReportProgressCommand : ICommand<bool, ReportProgressArgs>
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
                void OnProgressReported(bool success)
                {
                    observer.OnNext(success);
                    observer.OnCompleted();
                }

                _socialPlatform.ReportProgress(args.AchievementId, args.Score, OnProgressReported);
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