using System;
using DuckLib.Core.Commands;
using UniRx;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class AuthenticateCommand : ICommand<AuthenticateResult>
    {
        private readonly ISocialPlatform _socialPlatform;

        public AuthenticateCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        public IObservable<AuthenticateResult> Execute()
        {
            return Observable.Create<AuthenticateResult>(observer =>
            {
                if (!_socialPlatform.localUser.authenticated)
                {
                    _socialPlatform.localUser.Authenticate((result, message) =>
                    {
                        observer.OnNext(new AuthenticateResult()
                        {
                            Result = result,
                            Message = message
                        });
                        observer.OnCompleted();
                    });
                }
                else
                {
                    observer.OnNext(new AuthenticateResult());
                    observer.OnCompleted();
                }
                return this;
            });
        }

        public void Dispose()
        {
        }
    }

    public class AuthenticateResult
    {
        public bool Result = true;
        public string Message;
    }
}