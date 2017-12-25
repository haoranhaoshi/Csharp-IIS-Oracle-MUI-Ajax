var serverAddress;
var strDialysisDate; //透析日期
var strSigninState; //签到状态
var now = new Date(); //当前日期
var loginName; //护士账号
var PERSONID; //护士ID
var PERSONNAME; //护士姓名
var HOSPITALID; //院区ID
var DEPTID; //科室ID
var CARDID;
var bedNameArray = new Array(); //床名
var patientNameArray = new Array(); //患者姓名 
var patientSexArray = new Array(); //患者性别
var patientAgeArray = new Array(); //患者年龄
var cardIdArray = new Array(); //患者透析号
var classArray = new Array(); //患者班号
var areaNameArray = new Array(); //患者区号
var specialPatientType = new Array(); //患者类型
var departmentNameArray = new Array(); //患者科室名
var signStateArray = new Array(); //患者签到状态
var dialysisStateArray = new Array(); //患者透析状态
var signIdArray = new Array(); //透析体征项目ID
var signNameArray = new Array(); //透析体征项目名称
var signUnitArray = new Array(); //透析体征项目单位
var dialysisStateColor = new Array(); //透析状态对应颜色
mui.plusReady(function() {
	var jumpInfo = plus.webview.currentWebview();
	serverAddress = jumpInfo.serverAddress;
	PERSONNAME = jumpInfo.PERSONNAME;
	loginName = jumpInfo.loginName;
	PERSONID = jumpInfo.PERSONID;
	HOSPITALID = jumpInfo.HOSPITALID;
	DEPTID = jumpInfo.DEPTID;
	document.getElementById("offCanvasSide").getElementsByTagName("p")[0].innerText = PERSONNAME + "护士";
	/*if(localStorage.versionNumber == null) {
		localStorage.versionNumber = "1.0";
		document.getElementById("versionNumber").innerText = "1.0";
	} else {
		QueryVersionNumber();
	}*/
	QueryVersionNumber();
	QueryDialysisState();
	QueryDialysisItem();
	QueryClassesList();
	QueryNurseList();
	QueryAreaList(HOSPITALID, DEPTID); //显示所有的区
	if(localStorage.scanTarget != null && localStorage.scanTarget == "QueryPatient") {
		strDialysisDate = localStorage.scanPatientDate;
		strSigninState = "ALL";
		document.getElementById("state").innerText = "全部";
		QueryDialysisPatientListByDate(strDialysisDate, strSigninState);
		CARDID = localStorage.scanPatientID;
		QueryBaseInfo(CARDID, strDialysisDate);
		QueryDialysisInfo(strDialysisDate.replace("-", "").replace("-", ""), CARDID);
		QueryDialysisRecords(CARDID, strDialysisDate);
		QueryUnExecuteOrder(CARDID);
		document.getElementById("date").innerText = strDialysisDate.replace("-","/").replace("-","/");
	} else {
		setDate(now.getFullYear(), now.getMonth() + 1, now.getDate()); //显示当天			
		strDialysisDate = getToday(); //显示当天已签到的患者列表
		strSigninState = "1";
		QueryDialysisPatientListByDate(strDialysisDate, strSigninState);
	}
	plus.screen.lockOrientation("landscape-primary"); //强制横屏	
});