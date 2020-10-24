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

        public IObservable<AuthenticateResult> Execute(Unit args = default)
        {
            return Observable.Create<AuthenticateResult>(observer =>
            {
                void OnAuthenticate(bool isAuthenticated, string message)
                {
                    observer.OnNext(new AuthenticateResult
                    {
                        Result = isAuthenticated,
                        Message = message
                    });
                    observer.OnCompleted();
                }

                _socialPlatform.localUser.Authenticate(OnAuthenticate);
                return Disposable.Empty;
            });
        }
    }

    public class AuthenticateResult
    {
        public bool Result = true;
        public string Message;
    }
}