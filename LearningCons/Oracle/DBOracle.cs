using LearningCons.Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningCons.Oracle
{
    public class DBOracle
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

        public void CopySubsToUat(string msisdn)
        {

            try
            {
                string queryUatInsert = "";
                List<OracleParameter[]> paramInsert = new List<OracleParameter[]>();
                using (var con = new OracleConnection(CsHelper.ConnectionStringPreprod))
                {
                    con.Open();
                    string sql = "select * from subscription WHERE msisdn = :msisdn and status = :status";
                    using (OracleCommand command = new OracleCommand(sql, con))
                    {
                        OracleParameter[] parameters = new OracleParameter[]
                        {
                            new OracleParameter("msisdn",msisdn),
                            new OracleParameter("status","ACTIVE")
                        };
                        command.Parameters.AddRange(parameters);

                        using (OracleDataReader row = command.ExecuteReader())
                        {
                            while (row.Read())
                            {
                                var paramIn = new OracleParameter[row.FieldCount];
                                string column = "";
                                string colParam = "";
                                for (int i = 0; i < row.FieldCount; i++)
                                {
                                    column = column + string.Format("{0},",row.GetName(i));
                                    colParam = colParam + string.Format(":{0},", row.GetName(i));
                                    paramIn[i] = new OracleParameter(":"+ row.GetName(i), row.GetValue(i));
                                }
                                column =  column.Remove(column.Length - 1,1);
                                colParam = colParam.Remove(colParam.Length - 1,1);
                                queryUatInsert = string.Format("insert into subscription ({0}) values({1})", column, colParam);
                                paramInsert.Add(paramIn);
                            }
                        }
                    }
                }
                if(paramInsert.Count > 0)
                {
                    int totalInserted = 0;
                    foreach (var item in paramInsert)
                    {
                        var subsid = item.Where(x => x.ParameterName.ToLower() == ":subscription_id").FirstOrDefault().Value;
                        using (var con = new OracleConnection(CsHelper.ConnectionStringUAT))
                        {
                            con.Open();
                            string sql = "delete from subscription WHERE msisdn = :msisdn and subscription_id = :subsid";
                            using (var command = new OracleCommand(sql,con))
                            {
                                OracleParameter[] paramDel = new OracleParameter[]
                                    {
                                        new OracleParameter("msisdn",msisdn),
                                        new OracleParameter("subsid",subsid)
                                    };
                                command.Parameters.AddRange(paramDel);
                                command.ExecuteNonQuery();
                            }
                        }
                        using (var con = new OracleConnection(CsHelper.ConnectionStringUAT))
                        {
                            con.Open();
                            using (OracleCommand command = new OracleCommand(queryUatInsert, con))
                            {
                                command.Parameters.AddRange(item);
                                var result = command.ExecuteNonQuery();
                                if(result != 0)
                                    totalInserted += 1;
                            }
                        }
                    }
                    var msg = string.Format(CsHelper.CsMessage.RowInserted, totalInserted);
                    Console.WriteLine(msg);
                }
                else
                {
                    Console.Write(CsHelper.CsMessage.NotFound);
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