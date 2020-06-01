using DuckLib.Core.Commands;
using UnityEngine.SocialPlatforms;

namespace DuckLib.Social.Commands
{
    public class AuthenticateCommand: CommandWithCallback<string>
    {
        private readonly ISocialPlatform _socialPlatform;

        public AuthenticateCommand(ISocialPlatform socialPlatform)
        {
            _socialPlatform = socialPlatform;
        }

        protected override void OnStart()
        {
            if (!_socialPlatform.localUser.authenticated)
                _socialPlatform.localUser.Authenticate(OnAuthenticateResult);
            else
                FinishCommand(true);
        }

        private void OnAuthenticateResult(bool result, string message)
        {
            FinishCommand(_socialPlatform.localUser.authenticated, message);
        }
    }
}