using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HdisWebService
{
    /// <summary>
    /// HdisService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class HdisService : System.Web.Services.WebService
    {
        #region 属性
        /// <summary>
        /// 护士透析主表数据操作
        /// </summary>
        HdisBizLogic.Nurse.NurseDetailLogic NurseDetailLogic = new HdisBizLogic.Nurse.NurseDetailLogic();

        /// <summary>
        /// 护士透析主表数据操作
        /// </summary>
        HdisBizLogic.Doctor.DoctorDetailLogic DoctorDetailLogic = new HdisBizLogic.Doctor.DoctorDetailLogic();

        /// <summary>
        /// 人员信息数据操作
        /// </summary>
        HdisBizLogic.Person.PersonLogic PersonLogic = new HdisBizLogic.Person.PersonLogic();

        /// <summary>
        /// 过程记录数据操作
        /// </summary>
        HdisBizLogic.Proc.ProcResultLogic ProcResultLogic = new HdisBizLogic.Proc.ProcResultLogic();

        /// <summary>
        /// 工作区数据操作
        /// </summary>
        HdisBizLogic.SystemConifg.AreaLogic AreaLogic = new HdisBizLogic.SystemConifg.AreaLogic();

        /// <summary>
        /// 数据字典数据操作
        /// </summary>
        HdisBizLogic.SystemConifg.DictionaryLogic DictionaryLogic = new HdisBizLogic.SystemConifg.DictionaryLogic();

        /// <summary>
        /// 透析项目数据操作
        /// </summary>
        HdisBizLogic.SystemConifg.ItemLogic ItemLogic = new HdisBizLogic.SystemConifg.ItemLogic();

        /// <summary>
        /// 医嘱执行数据操作
        /// </summary>
        HdisBizLogic.Doctor.OrderLogic OrderLogic = new HdisBizLogic.Doctor.OrderLogic();

        /// <summary>
        /// 打印数据库操作
        /// </summary>
        HdisBizLogic.Print.PrintPatAfterInfo PrintPatAfterInfo = new HdisBizLogic.Print.PrintPatAfterInfo();

        /// <summary>
        /// 透析状态数据操作
        /// </summary>
        HdisBizLogic.State.StateLogic StateLogic = new HdisBizLogic.State.StateLogic();
        #endregion

        /// <summary>
        /// 护士登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [WebMethod(Description = "护士登录")]
        public string LoginMain(string loginName, string password)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                strReturn = this.PersonLogic.GetLoginPerson(loginName, password);
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "LoginMain", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 根据院区编码和科室编码查询工作区列表
        /// </summary>
        /// <param name="strHospitalId">院区编码</param>
        /// <param name="strDeptId">科室编码</param>
        /// <returns></returns>
        [WebMethod(Description = "查询工作区列表")]
        public string QueryAreaList(string strHospitalId, string strDeptId)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.SystemConfig.AreaModel> list = this.AreaLogic.QueryAreaList(strHospitalId, strDeptId);
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.AreaListForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryAreaList", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 班次列表
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "查询班次列表")]
        public string QueryClassesList()
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.Object> list = this.DictionaryLogic.GetDictionary("shift");
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.ClassesListForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryClassesList", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 根据透析日期查询透析患者列表
        /// </summary>
        /// <param name="strDialysisDate">透析日期、当日</param>
        /// <param name="strSigninState">签到状态</param>
        /// <returns></returns>
        [WebMethod(Description = "查询透析患者列表")]
        public string QueryDialysisPatientListByDate(string strDialysisDate, string strSigninState)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.Nurse.NurseDetail> list = this.NurseDetailLogic.QueryPatientListByTestDate(strDialysisDate, strSigninState);
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.PatientListForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryDialysisPatientListByDate", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 上机操作
        /// </summary>
        /// <param name="strDialysisDate">透析日期</param>
        /// <param name="strCardId">透析号</param>
        /// <param name="strUpMachineNurseId">上机护士编码</param>
        /// <param name="strUpMachineNurseName">上机护士名称</param>
        /// <returns></returns>
        [WebMethod(Description = "上级操作")]
        public string UpMachine(string strDialysisDate, string strCardId, string strUpMachineNurseId, string strUpMachineNurseName)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(strDialysisDate, strCardId);
                if (model.DIALYSISSTATE == "0")
                {
                    return "该患者还未进行透前检测，不允许上机透析！";
                }
                else if (model.DIALYSISSTATE == "2")
                {
                    return "该患者正在透析中，不允许再次上机操作！";
                }
                else if (model.DIALYSISSTATE == "3" || model.DIALYSISSTATE == "4" || model.DIALYSISSTATE == "5")
                {
                    return "该患者已完成透析，不允许再次上机操作！";
                }

                model.DIALYSISSTATE = "2";
                model.UPMACHINESTATE = "1";
                if (this.NurseDetailLogic.UpMachine(model, strUpMachineNurseId, strUpMachineNurseName) < 0)
                {
                    return "上机失败！";
                }
                else
                {
                    this.NurseDetailLogic.UpdateDialysisAppointplan(model);
                    strReturn = "上机成功！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "UpMachine", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 取消上机操作
        /// </summary>
        /// <param name="strDialysisDate">透析日期</param>
        /// <param name="strCardId">透析号</param>
        /// <returns></returns>
        [WebMethod(Description = "取消上机操作")]
        public string CancelUpMachine(string strDialysisDate, string strCardId)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(strDialysisDate, strCardId);
                if (model.DIALYSISSTATE == "0" || model.DIALYSISSTATE == "1")
                {
                    return "该患者还未进行上机操作！";
                }
                else if (model.DIALYSISSTATE == "3" || model.DIALYSISSTATE == "4" || model.DIALYSISSTATE == "5")
                {
                    return "该患者已完成透析，不允许再进行取消上机操作！";
                }

                model.DIALYSISSTATE = "1";
                if (this.NurseDetailLogic.CancelUpMachine(model) < 0)
                {
                    return "取消上机失败！";
                }
                else
                {
                    this.NurseDetailLogic.UpdateDialysisAppointplan(model);
                    strReturn = "取消上机成功！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "CancelUpMachine", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 下机操作
        /// </summary>
        /// <param name="strDialysisDate">透析日期</param>
        /// <param name="strCardId">透析号</param>
        /// <param name="strUpMachineNurseId">上机护士编码</param>
        /// <param name="strUpMachineNurseName">上机护士名称</param>
        /// <returns></returns>
        [WebMethod(Description = "下机操作")]
        public string DownMachine(string strDialysisDate, string strCardId, string strDownMachineNurseId, string strDownMachineNurseName)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(strDialysisDate, strCardId);
                if (model.DIALYSISSTATE == "0" || model.DIALYSISSTATE == "1")
                {
                    return "该患者还未进行上机操作！";
                }
                else if (model.DIALYSISSTATE == "3")
                {
                    return "该患者已进行过下机操作！";
                }
                else if (model.DIALYSISSTATE == "4" || model.DIALYSISSTATE == "5")
                {
                    return "该患者已完成透析，不允许再进行下机操作！";
                }

                model.DIALYSISSTATE = "3";
                model.DOWNMACHINESTATE = "1";
                if (this.NurseDetailLogic.DownMachine(model, strDownMachineNurseId, strDownMachineNurseName) < 0)
                {
                    return "患者下机失败！";
                }
                else
                {
                    this.NurseDetailLogic.UpdateDialysisAppointplan(model);
                    strReturn = "下机成功！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "DownMachine", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 取消下机操作
        /// </summary>
        /// <param name="strDialysisDate">透析日期</param>
        /// <param name="strCardId">透析号</param>
        /// <returns></returns>
        [WebMethod(Description = "取消下机操作")]
        public string CancelDownMachine(string strDialysisDate, string strCardId)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(strDialysisDate, strCardId);
                if (model.DIALYSISSTATE == "0" || model.DIALYSISSTATE == "1")
                {
                    return "该患者还未进行上机操作！";
                }
                else if (model.DIALYSISSTATE == "5")
                {
                    return "该患者已总审，不允许再进行取消下机操作！";
                }

                model.DIALYSISSTATE = "2";
                model.DOWNMACHINESTATE = "0";
                model.DOWNMACHINENURSEID = "";
                model.DOWNMACHINENURSENAME = "";
                if (this.NurseDetailLogic.DownMachine(model, model.DOWNMACHINENURSEID, model.DOWNMACHINENURSENAME) < 0)
                {
                    return "患者取消下机失败！";
                }
                else
                {
                    this.NurseDetailLogic.UpdateDialysisAppointplan(model);
                    strReturn = "取消下机成功！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "CancelDownMachine", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 保存透析小结
        /// </summary>
        /// <param name="strDialysisDate">透析日期</param>
        /// <param name="strCardId">透析号</param>
        /// <param name="strDialysisSummary">透析小结</param>
        /// <returns></returns>
        [WebMethod(Description = "保存透析小结")]
        public string SaveDialysisSummary(string strDialysisDate, string strCardId, string strDialysisSummary)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(strDialysisDate, strCardId);
                if (model.DIALYSISSTATE == "0" || model.DIALYSISSTATE == "1")
                {
                    strReturn = "保存失败，该患者还未进行透析操作！";
                    return strReturn;
                }
                if (model.DIALYSISSTATE == "2")
                {
                    strReturn = "保存失败，该患者还未完成透析操作！";
                    return strReturn;
                }
                if (model.DIALYSISSTATE == "4")
                {
                    strReturn = "保存失败，该患者已被护士确认！";
                    return strReturn;
                }
                else if (model.DIALYSISSTATE == "5")
                {
                    strReturn = "保存失败，该患者已总审，不允许再填写透后记录！";
                    return strReturn;
                }
                string[] itemInfo = strDialysisSummary.Split('|');
                model.TREATMENTDURATION = itemInfo[0];
                model.FACTSFILTRATIONQUANTITY = itemInfo[1];
                model.AFTERWEIGHT = itemInfo[2];
                model.LESSENWEIGHT = itemInfo[3];
                model.DoctorDetail.TEMPERATURE = itemInfo[4];
                model.DoctorDetail.PULSE = itemInfo[5];
                model.HEARTRATE = itemInfo[6];
                model.DoctorDetail.BREATHING = itemInfo[7];
                model.DoctorDetail.SPRESSURE = itemInfo[8];
                model.DoctorDetail.DPRESSURE = itemInfo[9];
                model.DIALYSISSUMMARY = itemInfo[10];
                if (this.NurseDetailLogic.SaveDialysisSummary(model) < 0)
                {
                    strReturn = "保存失败！";
                }
                else
                {
                    strReturn = "保存成功！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "SaveDialysisSummary", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询患者透后记录
        /// </summary>
        /// <param name="strDialysisDate">透析日期</param>
        /// <param name="strCardId">透析号</param>
        [WebMethod(Description = "查询患者透后记录")]
        public string QueryDialysisSummary(string testDate, string cardId)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail nurseDatail = new HdisModels.Nurse.NurseDetail();
                nurseDatail = this.NurseDetailLogic.GetDialysisSummary(testDate, cardId);
                if (nurseDatail != null)
                {
                    strReturn = HdisCommon.Fuction.DialysisSummaryForXml(nurseDatail);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryDialysisSummary", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 透前透后信息
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "查询透前透后记录")]
        public string QueryDialysisInfo(string testDate, string cardId)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Doctor.DoctorDetail doctorDatail = new HdisModels.Doctor.DoctorDetail();
                doctorDatail = this.DoctorDetailLogic.QueryDoctorDetail(testDate, cardId);
                if (doctorDatail != null)
                {
                    strReturn = HdisCommon.Fuction.DialysisInfoForXml(doctorDatail);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryDialysisInfo", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 透析项目列表
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "查询透析项目列表")]
        public string QueryDialysisItem()
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.SystemConfig.ItemModel> list = this.ItemLogic.QueryDialysisItemList();
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.DialysisItemListForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryDialysisItem", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询护士列表
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "查询护士列表")]
        public string QueryNurseList()
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.Person.PersonModel> list = this.PersonLogic.QueryNurseList();
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.NurseForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryDialysisItem", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 增加透析过程记录
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "增加透析过程记录")]
        public string AddDialysisRecord(string strDialysisDate, string strCardId, string loginId, string loginName)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail NurseDetailModel = new HdisModels.Nurse.NurseDetail();
                NurseDetailModel = this.NurseDetailLogic.QueryCurrentPatientInfo(strDialysisDate, strCardId);
                switch (NurseDetailModel.DIALYSISSTATE)
                {
                    case "0":
                    case "1":
                        return "该患者还未开始上机透析，不允许添加透析结果！";
                    case "2":
                        {
                            HdisModels.Proc.ProcResult ProcResult = new HdisModels.Proc.ProcResult();
                            ProcResult.CARDID = NurseDetailModel.CARDID;
                            ProcResult.SEQID = NurseDetailModel.SEQID;
                            ProcResult.TESTDATE = NurseDetailModel.TESTDATE;
                            ProcResult.RESULTSEQ = ProcResultLogic.GetResultSeq();
                            ProcResult.BEDID = NurseDetailModel.BEDID;
                            ProcResult.MACHINEID = NurseDetailModel.MACHINEID;
                            ProcResult.NURSEID = loginId;
                            ProcResult.NURSENAME = loginName;
                            this.ProcResultLogic.SaveDialysisResult(ProcResult);
                            strReturn = ProcResult.RESULTSEQ;
                            break;
                        }
                    case "3":
                    case "4":
                    case "5":
                        return "该患者已透析，不允许添加透析结果！";
                }

                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "AddDialysisRecord", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 保存透析结果
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "保存透析结果")]
        public string SaveDialysisResult(string strItemId, string strResult, string strResultSeq)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                if (this.ProcResultLogic.UpdateDialysisResultByResultSeq(strItemId, strResult, strResultSeq) != -1)
                {
                    strReturn = "成功保存！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "SaveDialysisResult", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 删除透析记录
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "删除透析记录")]
        public string DeleteDialysisRecord(string strResultSeq)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                if (this.ProcResultLogic.DeleteDialysisResult(strResultSeq) != -1)
                {
                    strReturn = "成功删除！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "DeleteDialysisRecord", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询透析过程记录
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "查询透析记录过程")]
        public string QueryDialysisRecords(string cardid, string testdate)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.Proc.ProcResult> list = this.ProcResultLogic.QueryDialysisResultList(cardid, testdate);
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.DialysisRecordForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryDialysisRecords", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询医嘱执行
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "查询医嘱执行")]
        public string QueryOrderExecute(string cardid)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.Doctor.OrderModel> list = this.OrderLogic.QueryOrder(cardid);
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.OrderExecuteForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryOrderExecute", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询未执行与待执行医嘱
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "查询未执行与待执行医嘱")]
        public string QueryExecuteOrder(string cardid, string testDate)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.Doctor.OrderModel> list = this.OrderLogic.QueryExecuteOrder(cardid, testDate);
                if (list.Count > 0)
                {
                    strReturn = HdisCommon.Fuction.UnExecuteOrderForXml(list);
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryUnExecuteOrder", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 执行患者医嘱
        /// </summary>
        /// <param name="strCardId">透析号</param>
        /// <param name="strRecipeNo">处方号</param>
        /// <param name="strRecipeSeq">处方内流水号</param>
        /// <param name="strConfimCode">确认人</param>
        /// <param name="strConfimDept">确认科室</param>
        /// <returns></returns>
        /// <returns></returns>
        [WebMethod(Description = "执行患者医嘱")]
        public string ExecuteOrder(string strCardId, string strRecipeNo, string strRecipeSeq, string strConfimCode, string strConfimDept)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                if (this.OrderLogic.ExecuteOrder(strCardId, strRecipeNo, strRecipeSeq, strConfimCode, strConfimDept) == 1)
                {
                    strReturn = "成功执行！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "ExecuteOrder", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 护士确认透析结束
        /// </summary>
        [WebMethod(Description = "护士确认透析结束")]
        public string ConfirmDialysisEnd(string cardId, string testDate, string strDialysisSummary)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(testDate, cardId);
                if (model.DIALYSISSTATE != "3")
                {
                    return "该患者非透析后患者无法确认！";
                }

                HdisModels.Nurse.NurseDetail nurseDetail = new HdisModels.Nurse.NurseDetail();
                nurseDetail = this.NurseDetailLogic.GetDialysisSummary(testDate, cardId);
                if (nurseDetail != null)
                {
                    string[] afterInfo = { nurseDetail.DIALYSISSUMMARY, nurseDetail.TREATMENTDURATION, nurseDetail.DoctorDetail.FACTSFILTRATIONQUANTITY, nurseDetail.DoctorDetail.AFTERWEIGHT, nurseDetail.LESSENWEIGHT, nurseDetail.DoctorDetail.TEMPERATURE, nurseDetail.DoctorDetail.PULSE, nurseDetail.DoctorDetail.SPRESSURE, nurseDetail.DoctorDetail.DPRESSURE, nurseDetail.DoctorDetail.BREATHING, nurseDetail.HEARTRATE };
                    for (int i = 0; i < afterInfo.Length; i++)
                    {
                        if (afterInfo[i] == "")
                        {
                            return "请填写完整透后记录并保存";
                        }
                    }
                }

                if (QueryPuntureNurse(cardId, testDate) == "")
                {
                    return "请填写完整透后记录并保存";
                }

                if (QueryTreamentNurse(cardId, testDate) == "")
                {
                    return "请填写完整透后记录并保存";
                }

                this.NurseDetailLogic.EditNurConfirmState(cardId, testDate);
                this.NurseDetailLogic.EditAppointConfirmState(cardId, testDate);
                this.DoctorDetailLogic.EditDocConfirmState(cardId, testDate);

                string[] itemInfo = strDialysisSummary.Split('|');
                model.UPMACHINENURSENAME = NurseDetailLogic.QueryUpMachineNurseName(cardId, testDate);
                model.TREATMENTDURATION = itemInfo[0];
                model.FACTSFILTRATIONQUANTITY = itemInfo[1];
                model.AFTERWEIGHT = itemInfo[2];
                model.LESSENWEIGHT = itemInfo[3];
                model.DoctorDetail.TEMPERATURE = itemInfo[4];
                model.DoctorDetail.PULSE = itemInfo[5];
                model.HEARTRATE = itemInfo[6];
                model.DoctorDetail.BREATHING = itemInfo[7];
                model.DoctorDetail.SPRESSURE = itemInfo[8];
                model.DoctorDetail.DPRESSURE = itemInfo[9];
                model.DIALYSISSUMMARY = itemInfo[10];
                this.PrintPatAfterInfo.SavePatResult(model);

                strReturn = "成功确认！";
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "confirmDialysisEnd", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询患者基本信息
        /// </summary>
        [WebMethod(Description = "查询患者基本信息")]
        public string QueryBaseInfo(string cardId, string testDate)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(testDate, cardId);
                strReturn = HdisCommon.Fuction.BaseInfoForXml(model);
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryBaseInfo", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询透析状态
        /// </summary>
        [WebMethod(Description = "查询透析状态")]
        public string QueryDialysisState()
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.State.DalysisState> list = this.StateLogic.QueryDialysisState();
                strReturn = HdisCommon.Fuction.DalysisStateForXml(list);
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryDialysisState", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 查询空床
        /// </summary>
        [WebMethod(Description = "查询空床信息")]
        public string QueryEmptyBedList(string strHospitalId, string strDeptId, string strAreaIid, string strTestDate, string strClasses, string patientType)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                List<HdisModels.SystemConfig.BedAndMachineModel> list = this.NurseDetailLogic.QueryEmptyBedList(strHospitalId, strDeptId, strAreaIid, strTestDate, strClasses, patientType);
                strReturn = HdisCommon.Fuction.EmptyBedForXml(list);
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryEmptyBedList", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 透析患者换床
        /// </summary>
        [WebMethod(Description = "透析患者换床")]
        public string ChangePatientBed(string testDate, string cardId, string classes, string areaId, string areaName, string bedId, string bedName)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                if (this.NurseDetailLogic.ChangePatientBed(testDate, cardId, classes, areaId, areaName, bedId, bedName) == 1)
                {
                    strReturn = "成功保存！";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "ChangePatientBed", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 上机时间查询
        /// </summary>
        [WebMethod(Description = "")]
        public string QueryUpMachineTime(string cardId, string testDate)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                strReturn = NurseDetailLogic.QueryUpMachineTime(cardId, testDate).ToString();
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryUpMachineTime", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 版本号查询
        /// </summary>
        [WebMethod(Description = "版本号查询")]
        public string QueryVersionNumber()
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                if (!HdisCommon.InitConfig.isReadConfig)
                {
                    HdisCommon.InitConfig ic = new HdisCommon.InitConfig();
                    ic.GetIniConfig();
                }
                strReturn = HdisCommon.InitConfig.VersionNumber;
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "GetVersionNumber", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 穿刺护士查询
        /// </summary>
        [WebMethod(Description = "穿刺护士查询")]
        public string QueryPuntureNurse(string cardId, string testDate)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                strReturn = PersonLogic.QueryPuntureNurse(cardId, testDate);
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryPuntureNurse", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

        /// <summary>
        /// 治疗护士查询
        /// </summary>
        [WebMethod(Description = "治疗护士查询")]
        public string QueryTreamentNurse(string cardId, string testDate)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                strReturn = PersonLogic.QueryTreamentNurse(cardId, testDate);
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "QueryTreamentNurse", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }


        /// <summary>
        /// 穿刺护士与治疗护士保存
        /// </summary>
        [WebMethod(Description = "穿刺护士与治疗护士保存")]
        public string SavePuntureAndTreamentNurse(string CARDID, string TESTDATE, string PUNCTURENURSE, string TREAMENTNURSE, string OPERATETIME)
        {
            lock (typeof(HdisService))
            {
                string strReturn = string.Empty;
                HdisModels.Nurse.NurseDetail model = new HdisModels.Nurse.NurseDetail();
                model = this.NurseDetailLogic.QueryCurrentPatientInfo(TESTDATE, CARDID);

                if (model.DIALYSISSTATE == "4")
                {
                    strReturn = "保存失败，该患者已被护士确认！";
                    return strReturn;
                }
                else if (model.DIALYSISSTATE == "5")
                {
                    strReturn = "保存失败，该患者已总审！";
                    return strReturn;
                }
                if (PersonLogic.SavePuntureAndTreamentNurse(CARDID, TESTDATE, PUNCTURENURSE, TREAMENTNURSE, OPERATETIME) == -1)
                {
                    strReturn = "保存失败";
                }
                else
                {
                    strReturn = "保存成功";
                }
                HdisCommon.Log.Logging("{0}.{1} ：{2}", "HdisService", "SavePuntureAndTreamentNurse", "strReturn[" + strReturn + "]");
                return strReturn;
            }
        }

    }
}
