using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisBizLogic.State
{
    public class StateLogic : ManagementBizlogic
    {

        /// <summary>
        /// 查询透析状态
        /// </summary>
        /// <param name="model">透析状态实体</param>
        /// <returns></returns>
        public List<HdisModels.State.DalysisState> QueryDialysisState()
        {
            List<HdisModels.State.DalysisState> list = new List<HdisModels.State.DalysisState>();
            HdisModels.State.DalysisState model = null;
            string sql = string.Empty;
            sql = @"select * from hdis_sys_dictionary a where a.typeid='DIALYSISSTATE'";
            try
            {
                if (this.ExecSQLQuery(sql) < 0)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    model = new HdisModels.State.DalysisState();
                    model.DICID = this.Reader["DICID"].ToString();
                    model.DICNAME = this.Reader["DICNAME"].ToString();
                    model.MEMO3 = this.Reader["MEMO3"].ToString();                    
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
            finally
            {
                this.oracleDb.CloseReader();
                this.oracleDb.CloseDB();
            }
        }
    }
}
