
using Oracle.ManagedDataAccess.Client;
using System;

namespace ClearSubs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            Console.Write("\r\nInput Msisdn: ");
            //Console.Write(Console.ReadLine());
            OracleDB db = new OracleDB();
            db.ClearSubs(Console.ReadLine());
            return true;
        }
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
        public class OracleDB
        {

            public void ClearSubs(string msisdn)
            {
                try
                {
                    using (var con = new OracleConnection(CsHelper.ConnectionStringPreprod))
                    {
                        con.Open();
                        string sql = "UPDATE subscription SET STATUS = 'REMOVED' where msisdn =:msisdn and status = 'ACTIVE'";
                        using (OracleCommand command = new OracleCommand(sql, con))
                        {
                            OracleParameter[] parameters = new OracleParameter[]
                            {
                        new OracleParameter("msisdn",msisdn),
                            };
                            command.Parameters.AddRange(parameters);
                            var r = command.ExecuteNonQuery();
                            Console.WriteLine(CsHelper.CsMessage.RowUpdated, r);
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.ToUpper().Equals(CsHelper.CsMessage.RequestTimeOut.ToUpper()))
                        Console.Write(CsHelper.CsMessage.ErrorConnection, e.Message);
                    else
                        Console.WriteLine(e.Message);
                }
            }
        }
    }
}
