using System;
using System.Linq.Expressions;

namespace CSQLQueryExpress
{
    public class AppLock
    {
        public static int Test(string databasePrinpal, string resourceName, AppLockMode lockMode, AppLockOwner lockOwner)
        {
            return default;
        }
    }

    public enum AppLockMode
    {
        Shared,
        Update,
        IntentShared,
        IntentExclusive,
        Exclusive
    }

    public enum AppLockOwner
    {
        Transaction,
        Session
    }
}
