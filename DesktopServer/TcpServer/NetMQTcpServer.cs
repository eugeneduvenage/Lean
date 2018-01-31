﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using QuantConnect.Logging;
using QuantConnect.Orders;
using QuantConnect.Packets;

namespace QuantConnect.DesktopServer.TcpServer
{
    public class NetMQTcpServer
    {
        private CancellationTokenSource _cancellationToken;
        private Task socketTask;

        public void Start(string port, ILeanMessageHandler handler)
        {
            _cancellationToken = new CancellationTokenSource();
            Action<object> RunSocket = (object obj) =>
            {
                try
                {                
                    CancellationToken token = (CancellationToken)obj;
                    token.ThrowIfCancellationRequested();

                    //Allow proper decoding of orders.
                    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                    {
                        Converters = { new OrderJsonConverter() }
                    };

                    using (var pullSocket = new PullSocket(">tcp://localhost:" + port))
                    {
                        while (!token.IsCancellationRequested)
                        {
                            Console.WriteLine(token.IsCancellationRequested);
                            var message = pullSocket.ReceiveMultipartMessage();

                            // There should only be 1 part messages
                            if (message.FrameCount != 1) continue;

                            var payload = message[0].ConvertToString();

                            var packet = JsonConvert.DeserializeObject<Packet>(payload);

                            switch (packet.Type)
                            {
                                case PacketType.BacktestNode:
                                    var backtestJobModel = JsonConvert.DeserializeObject<BacktestNodePacket>(payload);
                                    handler.Initialize(backtestJobModel);
                                    break;
                                case PacketType.LiveNode:
                                    var liveJobModel = JsonConvert.DeserializeObject<LiveNodePacket>(payload);
                                    handler.Initialize(liveJobModel);
                                    break;
                                case PacketType.Debug:
                                    var debugEventModel = JsonConvert.DeserializeObject<DebugPacket>(payload);
                                    handler.HandleDebugPacket(debugEventModel);
                                    break;
                                case PacketType.HandledError:
                                    var handleErrorEventModel = JsonConvert.DeserializeObject<HandledErrorPacket>(payload);
                                    handler.HandleErrorPacket(handleErrorEventModel);
                                    break;
                                case PacketType.BacktestResult:
                                    var backtestResultEventModel = JsonConvert.DeserializeObject<BacktestResultPacket>(payload);
                                    handler.HandleBacktestResultsPacket(backtestResultEventModel);
                                    break;
                                case PacketType.RuntimeError:
                                    var runtimeErrorEventModel = JsonConvert.DeserializeObject<RuntimeErrorPacket>(payload);
                                    handler.HandleRuntimeErrorPacket(runtimeErrorEventModel);
                                    break;
                                case PacketType.Log:
                                    var logEventModel = JsonConvert.DeserializeObject<LogPacket>(payload);
                                    handler.HandleLogPacket(logEventModel);
                                    break;
                            }
                        }
                    }
                }
                catch (OperationCanceledException oex)
                {
                    Console.WriteLine("LOOP EXITING - Cancel");
                    Console.WriteLine(oex.Message);
                }
                catch (ThreadAbortException tex)
                {
                    Console.WriteLine("LOOP EXITING - Abort");
                    Console.WriteLine(tex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                    Log.Error("Exception occurred in DesktopLeanServer NetMQ Socker loop", ex);
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    
                }
            };
            this.socketTask = Task.Factory.StartNew(RunSocket, _cancellationToken.Token);
        }

        public void Stop()
        {
            Trace.WriteLine("Cancelling");
            this._cancellationToken.Cancel();
            if (!this.socketTask.IsCompleted)
            {   
                Console.WriteLine("Waiting");
                //this.socketTask.Wait();
            }
            this._cancellationToken.Dispose();
        }
    }
}