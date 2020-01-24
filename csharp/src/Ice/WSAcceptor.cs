//
// Copyright (c) ZeroC, Inc. All rights reserved.
//

namespace IceInternal
{
    internal class WSAcceptor : IAcceptor
    {
        public void Close() => _delegate.Close();

        public Endpoint Listen()
        {
            _endpoint = _endpoint.endpoint(_delegate.Listen());
            return _endpoint;
        }

        public bool StartAccept(AsyncCallback callback, object state) => _delegate.StartAccept(callback, state);

        public void FinishAccept() => _delegate.FinishAccept();

        public ITransceiver Accept() => new WSTransceiver(_instance, _delegate.Accept());

        public string Protocol() => _delegate.Protocol();

        public override string ToString() => _delegate.ToString();

        public string ToDetailedString() => _delegate.ToDetailedString();

        public IAcceptor getDelegate() => _delegate;

        internal WSAcceptor(WSEndpoint endpoint, ProtocolInstance instance, IAcceptor del)
        {
            _endpoint = endpoint;
            _instance = instance;
            _delegate = del;
        }

        private WSEndpoint _endpoint;
        private ProtocolInstance _instance;
        private IAcceptor _delegate;
    }
}
