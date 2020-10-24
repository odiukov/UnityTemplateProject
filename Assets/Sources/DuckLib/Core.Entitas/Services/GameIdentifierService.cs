using System.Collections.Generic;

namespace DuckLib.Core.Services
{
    public class GameIdentifierService : IIdentifierService
    {
        private readonly Dictionary<Identity, int> _identifiers = new Dictionary<Identity, int>();

        public int Next(Identity identity)
        {
            var last = _identifiers.ContainsKey(identity) ? _identifiers[identity] : 1;
            var next = ++last;

            _identifiers[identity] = next;

            return next;
        }
    }
}