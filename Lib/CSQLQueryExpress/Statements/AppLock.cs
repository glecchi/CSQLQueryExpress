using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CSQLQueryExpress
{
    public class AppLock
    {
        public static int Test(string databasePrinpal, Expression<Func<string>> resourceName, AppLockMode lockMode, AppLockOwner lockOwner)
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
