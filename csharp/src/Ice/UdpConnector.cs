//
// Copyright (c) ZeroC, Inc. All rights reserved.
//

using System.Net;

namespace IceInternal
{
    internal sealed class UdpConnector : IConnector
    {
        public ITransceiver Connect() =>
            new UdpTransceiver(_instance, _addr, _sourceAddr, _mcastInterface, _mcastTtl);

        public short Type() => _instance.Type;

        //
        // Only for use by UdpEndpointI
        //
        internal UdpConnector(ProtocolInstance instance, EndPoint addr, EndPoint? sourceAddr, string mcastInterface,
                              int mcastTtl, string connectionId)
        {
            _instance = instance;
            _addr = addr;
            _sourceAddr = sourceAddr;
            _mcastInterface = mcastInterface;
            _mcastTtl = mcastTtl;
            _connectionId = connectionId;

            _hashCode = 5381;
            HashUtil.HashAdd(ref _hashCode, _addr);
            if (sourceAddr != null)
            {
                HashUtil.HashAdd(ref _hashCode, _sourceAddr);
            }
            HashUtil.HashAdd(ref _hashCode, _mcastInterface);
            HashUtil.HashAdd(ref _hashCode, _mcastTtl);
            HashUtil.HashAdd(ref _hashCode, _connectionId);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UdpConnector))
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            var p = (UdpConnector)obj;
            if (!_connectionId.Equals(p._connectionId))
            {
                return false;
            }

            if (!_mcastInterface.Equals(p._mcastInterface))
            {
                return false;
            }

            if (_mcastTtl != p._mcastTtl)
            {
                return false;
            }

            if (!Equals(_sourceAddr, p._sourceAddr))
            {
                return false;
            }

            return _addr.Equals(p._addr);
        }

        public override string ToString() => Network.AddrToString(_addr);

        public override int GetHashCode() => _hashCode;

        private readonly ProtocolInstance _instance;
        private readonly EndPoint _addr;
        private readonly EndPoint _sourceAddr;
        private readonly string _mcastInterface;
        private readonly int _mcastTtl;
        private readonly string _connectionId;
        private readonly int _hashCode;
    }
}
