using System;
using System.Collections.Generic;
using UnityEngine;

namespace DuckLib.Core
{
    public class Callback<T>
    {
        private readonly List<Responder<T>> _responders;

        private bool _lockResponders;

        public Callback()
        {
            _responders = new List<Responder<T>>();
        }

        public void AddResponder(Responder<T> responder)
        {
            if (!_lockResponders)
            {
                _responders.Add(responder);
            }
        }

        public void RemoveResponder(Responder<T> responder)
        {
            if (!_lockResponders)
            {
                _responders.Remove(responder);
            }
        }

        internal void FireResponse(T response)
        {
            _lockResponders = true;

            foreach (var responder in _responders)
            {
                try
                {
                    responder.Result(response);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            _lockResponders = false;
        }

        internal void FireFault(T response)
        {
            _lockResponders = true;

            foreach (var responder in _responders)
            {
                responder.Fault?.Invoke(response);
            }

            _lockResponders = false;
        }

        public void Clear()
        {
            _responders.Clear();
        }
    }
}