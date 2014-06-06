using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSky.Common
{
    public static class MsTestExtensions
    {
        /// <summary>
        /// Checks to make sure that the input delegate throws a exception of type TException, for MSTests.
        /// </summary>
        /// <typeparam name="TException">The type of exception expected.</typeparam>
        /// <param name="methodToExecute">The method to execute to generate the exception.</param>
        public static TException AssertRaises<TException>(Action methodToExecute) where TException : System.Exception
        {
            try
            {
                methodToExecute();
            }
            catch (TException t)
            {
                return t;
            }
            catch (System.Exception ex)
            {
                Assert.Fail("Expected exception of type " + typeof(TException) + " but type of " + ex.GetType() + " was thrown instead.");
            }

            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");

            // The line above will raise an exception and so the code will never get here
            // However there is no way that the compilier will know what Asset.Fail is going to do
            return null;
        }
    }
}