//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.Shared.Communication;
using Orders.Shared.Communication.Adapters;
using Orders.Shared.Communication.Messages;

namespace Orders.Shared.Tests
{
    [TestClass]
    public class ReceiveSendMessageLoopFixture
    {
        private ManualResetEvent waitHandle;

        [TestMethod]
        public void WHEN_exception_is_thrown_while_receiving_message_THEN_abandon_must_be_called_on_the_receiver()
        {
            waitHandle = new ManualResetEvent(false);

            var brokeredMessageMock = new Mock<IBrokeredMessageAdapter>();
            brokeredMessageMock.SetupGet(bm => bm.Properties).Returns(new Dictionary<string, object>());

            var receiverMock = new Mock<IMessageReceiverAdapter>();

            Given_the_receiver_receives_only_one_message(receiverMock, brokeredMessageMock);

            var receiverHandler = new ServiceBusReceiverHandler<NewOrderMessage>(receiverMock.Object)
                {
                    MessagePollingInterval = TimeSpan.FromMilliseconds(500)
                };
            receiverHandler.ProcessMessages(
                (message, queueDescription, token) =>
                {
                    var task = Task.Factory.StartNew(
                        this.ThrowException,
                        Task.Factory.CancellationToken,
                        TaskCreationOptions.AttachedToParent,
                        TaskScheduler.Default);
                    return task;
                }, CancellationToken.None);

            brokeredMessageMock.Setup(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>())).Callback(() => waitHandle.Set());
            waitHandle.WaitOne(15000);

            brokeredMessageMock.Verify(bm => bm.BeginComplete(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Never());
            brokeredMessageMock.Verify(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Once());
        }

        [TestMethod]
        public void WHEN_failing_to_receive_the_message_more_than_3_times_THEN_deadletter_must_be_called_on_the_receiver()
        {
            waitHandle = new ManualResetEvent(false);

            var brokeredMessageMock = new Mock<IBrokeredMessageAdapter>();
            brokeredMessageMock.SetupGet(bm => bm.Properties).Returns(new Dictionary<string, object>());

            var receiverMock = new Mock<IMessageReceiverAdapter>();

            Given_the_receiver_returns_same_message_3_times(receiverMock, brokeredMessageMock);

            var receiverHandler = new ServiceBusReceiverHandler<NewOrderMessage>(receiverMock.Object)
                {
                    MessagePollingInterval = TimeSpan.FromMilliseconds(500)
                };
            receiverHandler.ProcessMessages(
                (message, queueDescription, token) =>
                {
                    var task = Task.Factory.StartNew(
                        this.ThrowException,
                        Task.Factory.CancellationToken,
                        TaskCreationOptions.AttachedToParent,
                        TaskScheduler.Default);
                    return task;
                }, CancellationToken.None);

            brokeredMessageMock.Setup(bm => bm.BeginDeadLetter(It.IsAny<AsyncCallback>(), It.IsAny<object>())).Callback(() => waitHandle.Set());
            waitHandle.WaitOne(15000);

            brokeredMessageMock.Verify(bm => bm.BeginComplete(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Never());
            brokeredMessageMock.Verify(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Exactly(3));
            brokeredMessageMock.Verify(bm => bm.BeginDeadLetter(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Once());
        }

        [TestMethod]
        public void WHEN_exception_is_thrown_while_beginsending_message_THEN_abandon_must_be_called_on_the_receiver()
        {
            waitHandle = new ManualResetEvent(false);

            var brokeredMessageMock = new Mock<IBrokeredMessageAdapter>();
            brokeredMessageMock.SetupGet(bm => bm.Properties).Returns(new Dictionary<string, object>());

            var receiverMock = new Mock<IMessageReceiverAdapter>();
            var senderMock = new Mock<IMessageSenderAdapter>();

            Given_the_receiver_receives_only_one_message(receiverMock, brokeredMessageMock);
            Given_the_sender_fails_to_send_the_message_on_BeginSend(senderMock);

            var receiverHandler = new ServiceBusReceiverHandler<NewOrderMessage>(receiverMock.Object)
                {
                    MessagePollingInterval = TimeSpan.FromMilliseconds(500)
                };
            receiverHandler.ProcessMessages(
                (message, queueDescription, token) =>
                {
                    var task = Task.Factory.StartNew(
                        () => {
                                  var sbQueue = new ServiceBusQueue();
                                  sbQueue.Send(brokeredMessageMock.Object, senderMock.Object);
                        },
                        Task.Factory.CancellationToken,
                        TaskCreationOptions.AttachedToParent,
                        TaskScheduler.Default);
                    return task;
                }, CancellationToken.None);

            brokeredMessageMock.Setup(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>())).Callback(() => waitHandle.Set());
            waitHandle.WaitOne(15000);

            brokeredMessageMock.Verify(bm => bm.BeginComplete(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Never()); 
            brokeredMessageMock.Verify(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Once());
        }

        [TestMethod]
        public void WHEN_exception_is_thrown_while_endsending_message_THEN_abandon_must_be_called_on_the_receiver()
        {
            waitHandle = new ManualResetEvent(false);

            var brokeredMessageMock = new Mock<IBrokeredMessageAdapter>();
            brokeredMessageMock.SetupGet(bm => bm.Properties).Returns(new Dictionary<string, object>());

            var receiverMock = new Mock<IMessageReceiverAdapter>();
            var senderMock = new Mock<IMessageSenderAdapter>();

            Given_the_receiver_receives_only_one_message(receiverMock, brokeredMessageMock);
            Given_the_sender_fails_to_send_the_message_on_EndSend(senderMock);

            var receiverHandler = new ServiceBusReceiverHandler<NewOrderMessage>(receiverMock.Object)
                {
                    MessagePollingInterval = TimeSpan.FromMilliseconds(500)
                };
            receiverHandler.ProcessMessages(
                (message, queueDescription, token) =>
                {
                    var task = Task.Factory.StartNew(
                        () => {
                                  var sbQueue = new ServiceBusQueue();
                                  sbQueue.Send(brokeredMessageMock.Object, senderMock.Object);
                        },
                        Task.Factory.CancellationToken,
                        TaskCreationOptions.AttachedToParent,
                        TaskScheduler.Default);
                    return task;
                }, CancellationToken.None);

            brokeredMessageMock.Setup(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>())).Callback(() => waitHandle.Set());
            waitHandle.WaitOne(15000);

            brokeredMessageMock.Verify(bm => bm.BeginComplete(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Never());
            brokeredMessageMock.Verify(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Once());
        }

        [TestMethod]
        public void WHEN_the_message_is_sent_successfully_THEN_complete_must_be_called_on_the_receiver()
        {
            waitHandle = new ManualResetEvent(false);

            var brokeredMessageMock = new Mock<IBrokeredMessageAdapter>();
            brokeredMessageMock.SetupGet(bm => bm.Properties).Returns(new Dictionary<string, object>());

            var receiverMock = new Mock<IMessageReceiverAdapter>();
            var senderMock = new Mock<IMessageSenderAdapter>();

            Given_the_receiver_receives_only_one_message(receiverMock, brokeredMessageMock);
            Given_the_sender_sends_the_message_successfully(senderMock);

            var receiverHandler = new ServiceBusReceiverHandler<NewOrderMessage>(receiverMock.Object)
                {
                    MessagePollingInterval = TimeSpan.FromMilliseconds(500)
                };
            receiverHandler.ProcessMessages(
                (message, queueDescription, token) =>
                {
                    var task = Task.Factory.StartNew(
                        () =>
                        {
                            var sbQueue = new ServiceBusQueue();
                            sbQueue.Send(brokeredMessageMock.Object, senderMock.Object);
                        },
                        Task.Factory.CancellationToken,
                        TaskCreationOptions.AttachedToParent,
                        TaskScheduler.Default);
                    return task;
                }, CancellationToken.None);

            brokeredMessageMock.Setup(bm => bm.BeginComplete(It.IsAny<AsyncCallback>(), It.IsAny<object>())).Callback(() => waitHandle.Set());
            waitHandle.WaitOne(15000);

            brokeredMessageMock.Verify(bm => bm.BeginComplete(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Once());
            brokeredMessageMock.Verify(bm => bm.BeginAbandon(It.IsAny<AsyncCallback>(), It.IsAny<object>()), Times.Never());
        }

        private void Given_the_sender_sends_the_message_successfully(Mock<IMessageSenderAdapter> senderMock)
        {
            var asyncResultMock = new Mock<IAsyncResult>();
            asyncResultMock.SetupGet(ar => ar.IsCompleted).Returns(true);

            senderMock.Setup(r => r.BeginSend(It.IsAny<IBrokeredMessageAdapter>(), It.IsAny<AsyncCallback>(), It.IsAny<object>()))
                .Callback((IBrokeredMessageAdapter brokeredMessageAdapter, AsyncCallback callback, object state) => callback.BeginInvoke(asyncResultMock.Object, null, null));
        }

        private void Given_the_sender_fails_to_send_the_message_on_BeginSend(Mock<IMessageSenderAdapter> senderMock)
        {
            senderMock.Setup(r => r.BeginSend(It.IsAny<IBrokeredMessageAdapter>(), It.IsAny<AsyncCallback>(), It.IsAny<object>()))
                .Callback(this.ThrowException);
        }
        
        private void Given_the_sender_fails_to_send_the_message_on_EndSend(Mock<IMessageSenderAdapter> senderMock)
        {
            var asyncResultMock = new Mock<IAsyncResult>();
            asyncResultMock.SetupGet(ar => ar.IsCompleted).Returns(true);

            senderMock.Setup(r => r.BeginSend(It.IsAny<IBrokeredMessageAdapter>(), It.IsAny<AsyncCallback>(), It.IsAny<object>()))
                .Callback((IBrokeredMessageAdapter brokeredMessageAdapter, AsyncCallback callback, object state) => callback.BeginInvoke(asyncResultMock.Object, null, null));

            senderMock.Setup(r => r.EndSend(It.IsAny<IAsyncResult>()))
                .Callback(this.ThrowException);
        }

        private void Given_the_receiver_receives_only_one_message(Mock<IMessageReceiverAdapter> receiverMock, Mock<IBrokeredMessageAdapter> brokeredMessageMock)
        {
            var asyncResultMock = new Mock<IAsyncResult>();
            asyncResultMock.SetupGet(ar => ar.IsCompleted).Returns(true);

            receiverMock.Setup(r => r.BeginReceive(It.IsAny<TimeSpan>(), It.IsAny<AsyncCallback>(), It.IsAny<object>()))
                .Callback((TimeSpan serverWaitTime, AsyncCallback callback, object state) => callback.BeginInvoke(asyncResultMock.Object, null, null));


            bool[] firstCall = {true};
            receiverMock.Setup(r => r.EndReceive(It.IsAny<IAsyncResult>())).Returns(() =>
                {
                    if (firstCall[0])
                                                                                                return brokeredMessageMock.Object;
                    return null;
                }).Callback(() =>
                                                                                                        {
                                                                                                            firstCall[0] = false;
                                                                                                        });
        }

        private void Given_the_receiver_returns_same_message_3_times(Mock<IMessageReceiverAdapter> receiverMock, Mock<IBrokeredMessageAdapter> brokeredMessageMock)
        {
            var asyncResultMock = new Mock<IAsyncResult>();
            asyncResultMock.SetupGet(ar => ar.IsCompleted).Returns(true);

            receiverMock.Setup(r => r.BeginReceive(It.IsAny<TimeSpan>(), It.IsAny<AsyncCallback>(), It.IsAny<object>()))
                .Callback((TimeSpan serverWaitTime, AsyncCallback callback, object state) => callback.BeginInvoke(asyncResultMock.Object, null, null));

            int[] numCalls = { 0 };

            receiverMock.Setup(r => r.EndReceive(It.IsAny<IAsyncResult>())).Returns(() =>
            {
                if (numCalls[0] <= 3)
                {
                    brokeredMessageMock.SetupGet(bm => bm.DeliveryCount).Returns(numCalls[0] + 1);
                    return brokeredMessageMock.Object;
                }

                return null;

            }).Callback(() =>
            {
                if (numCalls[0] <= 3)
                {
                    numCalls[0] = numCalls[0] + 1;
                }
            });
        }

        private void ThrowException()
        {
            throw new NotImplementedException();
        }
    }
}
