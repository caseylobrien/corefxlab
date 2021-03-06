// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Buffers;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace System.IO.Pipelines.Samples
{
    public class RawSocketHttpClientSample : RawHttpClientSampleBase
    {
        private MemoryPool<byte> pool;

        public RawSocketHttpClientSample()
        {
            pool = new MemoryPool();
        }

        protected override Task<IDuplexPipe> GetConnection()
        {
            Socket s = new Socket(SocketType.Stream, ProtocolType.Tcp);
            s.Connect(new IPEndPoint(IPAddress.Loopback, 5000));

            var pipeConnection = new StreamDuplexPipe(new PipeOptions(pool), new NetworkStream(s));
            return Task.FromResult((IDuplexPipe)pipeConnection);
        }

        protected override MemoryPool<byte> GetBufferPool()
        {
            return pool;
        }
    }
}
