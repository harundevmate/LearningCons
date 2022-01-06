using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCons.Helper
{
    public static class CsHelper
    {
        public const string ConnectionStringPreprod ="";
        public const string ConnectionStringUAT = "";
        public static class CsMessage
        {
            public const string ErrorConnection = "{0}: Check your connection";
            public const string NotFound = "Data NotFound";
            public const string RowUpdated = "{0} rows updated";
            public const string RowInserted = "{0} rows inserted";

            public const string RequestTimeOut = "Connection request timed out";
        }
    }
}
