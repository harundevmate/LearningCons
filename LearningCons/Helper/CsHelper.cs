using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCons.Helper
{
    public static class CsHelper
    {
        public const string ConnectionStringPreprod = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.34.152.26)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=OTNGSSP.OFFICE.CORP.INDOSAT.COM)));USER ID=SSP; Password=pPr0dssp;Connection Timeout=5;";
        public const string ConnectionStringUAT = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.147.114.11)(PORT=1521))(CONNECT_DATA=(SID=NGSSPUAT)));USER ID=ssp; Password=uats5pNg;Connection Timeout=5;";

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
