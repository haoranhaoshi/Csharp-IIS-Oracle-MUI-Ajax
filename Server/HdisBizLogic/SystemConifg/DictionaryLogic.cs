using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.SystemConifg
{
    public class DictionaryLogic : ManagementBizlogic
    {

        /// <summary>
        /// 获取数据字典(不包括停用项)
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<HdisModels.Object> GetDictionary(string typeId)
        {
            List<HdisModels.Object> infoList = new List<HdisModels.Object>();
            HdisModels.Object obj = null;
            string sql = string.Empty;
            try
            {
                sql = @"select * from hdis_sys_dictionary a where a.typeid='{0}' and a.isshow='1' order by a.showorder";
                sql = string.Format(sql, typeId);
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    obj = new HdisModels.Object();
                    obj.Id = Reader["DICID"].ToString();
                    obj.Name = Reader["DICNAME"].ToString();
                    obj.strMemo1 = Reader["MEMO1"].ToString();
                    obj.strMemo2 = Reader["MEMO2"].ToString();
                    obj.strMemo3 = Reader["MEMO3"].ToString();
                    infoList.Add(obj);
                }
                return infoList;
            }
            catch
            {
                return infoList;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }

        }
    }
}