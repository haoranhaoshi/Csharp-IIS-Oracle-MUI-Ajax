using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HdisModels.Doctor
{
    public class OrderModel
    {
        public OrderModel()
        {
            CARDID = "";
            TESTDATE = "";
            SEQID = "";
            ITEM_CODE = "";
            ITEM_NAME = "";
            SPECS = "";
            DRUG_FLAG = "";
            CLASS_CODE = "";
            FEE_CODE = "";
            UNIT_PRICE = 0;
            QTY = 0;
            PACK_QTY = 0;
            ITEM_UNIT = "";
            TOT_COST = 0;
            OWN_COST = 0;
            PAY_COST = 0;
            PUB_COST = 0;
            BASE_DOSE = 0;
            ONCE_DOSE = 0;
            ONCE_UNIT = "";
            DOSE_MODEL_CODE = "";
            FREQUENCY_CODE = "";
            FREQUENCY_NAME = "";
            USAGE_CODE = "";
            USAGE_NAME = "";
            EXEC_DPCD = "";
            EXEC_DPNM = "";
            MAIN_DRUG = "";
            COMB_NO = "";
            HYPOTEST = "";
            REMARK = "";
            DOCT_CODE = "";
            DOCT_NAME = "";
            DOCT_DPCD = "";
            OPER_DATE = new DateTime();
            STATUS = "";
            CANCEL_USERID = "";
            CANCEL_DATE = new DateTime();
            EMC_FLAG = "";
            LAB_TYPE = "";
            CHECK_BODY = "";
            APPLY_NO = "";
            SUBTBL_FLAG = "";
            NEED_CONFIRM = "";
            CONFIRM_CODE = "";
            CONFIRM_NAME = "";
            CONFIRM_DEPT = "";
            CONFIRM_DATE = new DateTime();
            CHARGE_FLAG = "";
            CHARGE_CODE = "";
            CHARGE_DATE = new DateTime();
            RECIPE_NO = "";
            RECIPE_SEQ = 0;
            PHAMARCY_CODE = "";
            SORT_ID = 0;
            CLINICDIAG_CODE = "";
            CLINICDIAG_NAME = "";
            CLINICOTHERDIAG1_CODE = "";
            CLINICOTHERDIAG1_NAME = "";
            CLINICOTHERDIAG2_CODE = "";
            CLINICOTHERDIAG2_NAME = "";
            MO_ORADER = "";
            MEMO = "";
        }

        /// <summary>
        ///透析号
        /// </summary>
        public string CARDID
        {
            get;
            set;
        }


        /// <summary>
        ///透析日期
        /// </summary>
        public string TESTDATE
        {
            get;
            set;
        }


        /// <summary>
        ///透析流水号
        /// </summary>
        public string SEQID
        {
            get;
            set;
        }


        /// <summary>
        ///项目代码
        /// </summary>
        public string ITEM_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///项目名称
        /// </summary>
        public string ITEM_NAME
        {
            get;
            set;
        }


        /// <summary>
        ///规格
        /// </summary>
        public string SPECS
        {
            get;
            set;
        }


        /// <summary>
        ///1药品，2非药品
        /// </summary>
        public string DRUG_FLAG
        {
            get;
            set;
        }


        /// <summary>
        ///系统类别
        /// </summary>
        public string CLASS_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///费用类别
        /// </summary>
        public string FEE_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///单价
        /// </summary>
        public int UNIT_PRICE
        {
            get;
            set;
        }


        /// <summary>
        ///开立数量
        /// </summary>
        public int QTY
        {
            get;
            set;
        }


        /// <summary>
        ///包装数量
        /// </summary>
        public int PACK_QTY
        {
            get;
            set;
        }


        /// <summary>
        ///计价单位
        /// </summary>
        public string ITEM_UNIT
        {
            get;
            set;
        }


        /// <summary>
        ///总金额
        /// </summary>
        public int TOT_COST
        {
            get;
            set;
        }


        /// <summary>
        ///自费金额（预留接入医保更新）
        /// </summary>
        public int OWN_COST
        {
            get;
            set;
        }


        /// <summary>
        ///自负金额（预留接入医保更新）
        /// </summary>
        public int PAY_COST
        {
            get;
            set;
        }


        /// <summary>
        ///报销金额（预留接入医保更新）
        /// </summary>
        public int PUB_COST
        {
            get;
            set;
        }


        /// <summary>
        ///基本剂量
        /// </summary>
        public int BASE_DOSE
        {
            get;
            set;
        }


        /// <summary>
        ///每次用量
        /// </summary>
        public Decimal ONCE_DOSE
        {
            get;
            set;
        }


        /// <summary>
        ///每次用量单位
        /// </summary>
        public string ONCE_UNIT
        {
            get;
            set;
        }


        /// <summary>
        ///剂型代码
        /// </summary>
        public string DOSE_MODEL_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///频次
        /// </summary>
        public string FREQUENCY_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///频次名称
        /// </summary>
        public string FREQUENCY_NAME
        {
            get;
            set;
        }


        /// <summary>
        ///使用方法
        /// </summary>
        public string USAGE_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///用法名称
        /// </summary>
        public string USAGE_NAME
        {
            get;
            set;
        }


        /// <summary>
        ///执行科室代码
        /// </summary>
        public string EXEC_DPCD
        {
            get;
            set;
        }


        /// <summary>
        ///执行科室名称
        /// </summary>
        public string EXEC_DPNM
        {
            get;
            set;
        }


        /// <summary>
        ///主药标记
        /// </summary>
        public string MAIN_DRUG
        {
            get;
            set;
        }


        /// <summary>
        ///组合号（相同组合号为一组）
        /// </summary>
        public string COMB_NO
        {
            get;
            set;
        }


        /// <summary>
        ///1不需要皮试/2需要皮试，未做/3皮试阳/4皮试阴
        /// </summary>
        public string HYPOTEST
        {
            get;
            set;
        }


        /// <summary>
        ///院内注射次数
        /// </summary>
        public string REMARK
        {
            get;
            set;
        }


        /// <summary>
        ///开立医生
        /// </summary>
        public string DOCT_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///开立医生名称
        /// </summary>
        public string DOCT_NAME
        {
            get;
            set;
        }


        /// <summary>
        ///医生科室
        /// </summary>
        public string DOCT_DPCD
        {
            get;
            set;
        }


        /// <summary>
        ///开立时间
        /// </summary>
        public DateTime OPER_DATE
        {
            get;
            set;
        }


        /// <summary>
        ///处方状态,0开立，1收费，2确认，3作废
        /// </summary>
        public string STATUS
        {
            get;
            set;
        }


        /// <summary>
        ///作废人
        /// </summary>
        public string CANCEL_USERID
        {
            get;
            set;
        }


        /// <summary>
        ///作废时间
        /// </summary>
        public DateTime CANCEL_DATE
        {
            get;
            set;
        }


        /// <summary>
        ///加急标记0普通/1加急
        /// </summary>
        public string EMC_FLAG
        {
            get;
            set;
        }


        /// <summary>
        ///样本类型
        /// </summary>
        public string LAB_TYPE
        {
            get;
            set;
        }


        /// <summary>
        ///检体部位
        /// </summary>
        public string CHECK_BODY
        {
            get;
            set;
        }


        /// <summary>
        ///申请单号
        /// </summary>
        public string APPLY_NO
        {
            get;
            set;
        }


        /// <summary>
        ///0没有附材/1带附材/2是附材
        /// </summary>
        public string SUBTBL_FLAG
        {
            get;
            set;
        }


        /// <summary>
        ///是否需要确认，1需要，0不需要
        /// </summary>
        public string NEED_CONFIRM
        {
            get;
            set;
        }


        /// <summary>
        ///确认人ID
        /// </summary>
        public string CONFIRM_CODE
        {
            get;
            set;
        }
        
        /// <summary>
        ///确认人姓名
        /// </summary>
        public string CONFIRM_NAME
        {
            get;
            set;
        }

        /// <summary>
        ///确认科室
        /// </summary>
        public string CONFIRM_DEPT
        {
            get;
            set;
        }


        /// <summary>
        ///确认时间
        /// </summary>
        public DateTime CONFIRM_DATE
        {
            get;
            set;
        }


        /// <summary>
        ///0未收费/1收费
        /// </summary>
        public string CHARGE_FLAG
        {
            get;
            set;
        }


        /// <summary>
        ///收费员
        /// </summary>
        public string CHARGE_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///收费时间
        /// </summary>
        public DateTime CHARGE_DATE
        {
            get;
            set;
        }


        /// <summary>
        ///处方号
        /// </summary>
        public string RECIPE_NO
        {
            get;
            set;
        }


        /// <summary>
        ///处方内流水号
        /// </summary>
        public int RECIPE_SEQ
        {
            get;
            set;
        }


        /// <summary>
        ///发药药房
        /// </summary>
        public string PHAMARCY_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///排列序号，按排列序号由大到小顺序显示医嘱
        /// </summary>
        public int SORT_ID
        {
            get;
            set;
        }


        /// <summary>
        ///门诊诊断编码
        /// </summary>
        public string CLINICDIAG_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///门诊诊断名称
        /// </summary>
        public string CLINICDIAG_NAME
        {
            get;
            set;
        }


        /// <summary>
        ///门诊其它诊断1编码
        /// </summary>
        public string CLINICOTHERDIAG1_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///门诊其它诊断1名称
        /// </summary>
        public string CLINICOTHERDIAG1_NAME
        {
            get;
            set;
        }


        /// <summary>
        ///门诊其它诊断2编码
        /// </summary>
        public string CLINICOTHERDIAG2_CODE
        {
            get;
            set;
        }


        /// <summary>
        ///门诊其它诊断2名称
        /// </summary>
        public string CLINICOTHERDIAG2_NAME
        {
            get;
            set;
        }


        /// <summary>
        ///HIS医嘱流水号
        /// </summary>
        public string MO_ORADER
        {
            get;
            set;
        }

        /// <summary>
        ///医嘱备注
        /// </summary>
        public string MEMO
        {
            get;
            set;
        }

    }
}
