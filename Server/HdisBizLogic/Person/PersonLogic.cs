using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.Person
{
    /// <summary>
    /// [功能描述:登录人员数据操作]
    /// [创建者:yinbsh]
    /// [创建时间:2017-06-06]
    /// </summary>
    public class PersonLogic : ManagementBizlogic
    {


        public string  GetLoginPerson(string loginName, string password)
        {
            string strReturn = string.Empty;
            HdisModels.Person.PersonModel personModel = this.GetPersonByLoginName(loginName);
            if (personModel == null)
            {
                return "用户名错误";
            }
            else
            {
                if (personModel.ISSOURCEHIS == "0")
                {
                    if (personModel.LOGINPASS ==HdisCommon.Ciphertext.Encrypt(password.Trim()))
                    {
                        return HdisCommon.Fuction.PersonInfoForXml(personModel);
                    }
                    else
                    {
                        return "密码错误";
                    }
                }
                else
                {
                    
                }
            }
            return strReturn;
        }


        /// <summary>
        /// 通过登录账号查人员信息
        /// </summary>
        private HdisModels.Person.PersonModel GetPersonByLoginName(string LoginName)
        {
            HdisModels.Person.PersonModel model=null;
            string strSQL = @"select a.* from HDIS_ROL_PERSON a where a.loginname='{0}' and a.isenabled='1' ";
            strSQL = string.Format(strSQL, LoginName);
             try
            {
                if (this.ExecSQLQuery(strSQL) == -1)
                {
                    return null;
                }
                if (this.Reader.Read())
                {
                    model = new HdisModels.Person.PersonModel();
                    model.PERSONID = Reader["PERSONID"].ToString();
                    model.PERSONNAME = Reader["PERSONNAME"].ToString();
                    model.LOGINNAME = Reader["LOGINNAME"].ToString();
                    model.HOSPITALID = Reader["HOSPITALID"].ToString();
                    model.HOSPITALNAME = Reader["HOSPITALNAME"].ToString();
                    model.DEPTID = Reader["DEPTID"].ToString();
                    model.DEPTNAME = Reader["DEPTNAME"].ToString();
                    model.ROLEID = Reader["ROLEID"].ToString();
                    model.ROLENAME = Reader["ROLENAME"].ToString();
                    model.ISSOURCEHIS = Reader["ISSOURCEHIS"].ToString();
                    model.LOGINPASS = Reader["LOGINPASS"].ToString();
                }
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }

        /// <summary>
        /// 通过登录账号查人员信息
        /// </summary>
        public List<HdisModels.Person.PersonModel> QueryNurseList()
        {
            List < HdisModels.Person.PersonModel> list= new List<HdisModels.Person.PersonModel>();
            HdisModels.Person.PersonModel model = null;
            string strSQL = @"select a.* from HDIS_ROL_PERSON a where a.roleid='03' or a.roleid='04'";
            strSQL = string.Format(strSQL);
            try
            {
                if (this.ExecSQLQuery(strSQL) == -1)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    model = new HdisModels.Person.PersonModel();
                    model.PERSONID = Reader["PERSONID"].ToString();
                    model.PERSONNAME = Reader["PERSONNAME"].ToString();
                    model.LOGINNAME = Reader["LOGINNAME"].ToString();
                    model.HOSPITALID = Reader["HOSPITALID"].ToString();
                    model.HOSPITALNAME = Reader["HOSPITALNAME"].ToString();
                    model.DEPTID = Reader["DEPTID"].ToString();
                    model.DEPTNAME = Reader["DEPTNAME"].ToString();
                    model.ROLEID = Reader["ROLEID"].ToString();
                    model.ROLENAME = Reader["ROLENAME"].ToString();
                    model.ISSOURCEHIS = Reader["ISSOURCEHIS"].ToString();
                    model.LOGINPASS = Reader["LOGINPASS"].ToString();
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }

        public String QueryPuntureNurse(String CARDID, String TESTDATE)
        {
            String sql = String.Empty;
            sql = "select PUNCTURENURSE from HDIS_COL_NURSES where CARDID='{0}' and TESTDATE='{1}'";
            sql = string.Format(sql, CARDID, TESTDATE);
            try
            {
                if (this.ExecSQLQuery(sql) == -1)
                {
                    return null;
                }
                if (this.Reader.Read())
                {
                    return this.Reader["PUNCTURENURSE"].ToString();
                }
                return null;              
            }
            catch(Exception ex)
            {
                return null;
            }               
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }

        public String QueryTreamentNurse(string CARDID, string TESTDATE)
        {
            String sql = String.Empty;
            sql = "select TREAMENTNURSE from HDIS_COL_NURSES where CARDID='{0}' and TESTDATE='{1}'";
            sql = string.Format(sql, CARDID, TESTDATE);
            try
            {
                if (this.ExecSQLQuery(sql) == -1)
                {
                    return null;
                }
                if (this.Reader.Read())
                {
                    return this.Reader["TREAMENTNURSE"].ToString();
                }
                return null;                   
            }
            catch
            {
                return null;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }

        public int SavePuntureAndTreamentNurse(string CARDID, string TESTDATE, string PUNCTURENURSE, string TREAMENTNURSE, string OPERATETIME)
        {
            String sql = String.Empty;
            sql = sql = "insert into HDIS_COL_NURSES(CARDID,TESTDATE,PUNCTURENURSE,TREAMENTNURSE,OPERATETIME)VALUES('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, CARDID, TESTDATE, PUNCTURENURSE, TREAMENTNURSE, OPERATETIME);
            try
            {
                if (this.ExecSQL(sql) == -1)
                {
                    sql = sql = "update HDIS_COL_NURSES set PUNCTURENURSE='{2}',TREAMENTNURSE='{3}',OPERATETIME='{4}' where CARDID='{0}' and TESTDATE='{1}'";
                    sql = string.Format(sql, CARDID, TESTDATE, PUNCTURENURSE, TREAMENTNURSE, OPERATETIME);
                    if (this.ExecSQL(sql) == -1)
                    {
                        return -1;
                    }
                    return 1;
                }
                return 1;
            }
            catch
            {
                return -1;
            }
            finally
            {
                this.oracleDb.CloseDB();
            }
        }

    }
}
