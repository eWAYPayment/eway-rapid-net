using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using eWAY.Rapid.Enums;
using eWAY.Rapid.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Internal = eWAY.Rapid.Internals.Models;
namespace eWAY.Rapid.Tests.IntegrationTests
{
    [TestClass]
    public class MultiThreadedTests : SdkTestBase
    {
        [TestMethod]
        public void Multithread_CreatingMultipleClientsInManyThreads_ReturnsValidData()
        {
            RunInMultipleThreads(20, () =>
            {

                IRapidClient client = CreateRapidApiClient();
                Transaction transaction = new Transaction()
                {
                    Customer = TestUtil.CreateCustomer(),
                    PaymentDetails = new PaymentDetails()
                    {
                        TotalAmount =  200
                    },
                    TransactionType = TransactionTypes.MOTO,
                    

                };
                CreateTransactionResponse response = 
                    client.Create(PaymentMethod.Direct, transaction);
                });

            
        }


        private void RunInMultipleThreads(int threadCount, Action action)
        {
            List<Thread> startedThreads = new List<Thread>();
            for (int i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(() => action());
                startedThreads.Add(thread);
                thread.Start();

            }
            foreach (Thread thread in startedThreads)
            {
                thread.Join();
            }
            
        }


    }
}
