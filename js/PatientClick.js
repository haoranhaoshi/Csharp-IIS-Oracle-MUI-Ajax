var cardIdIndex, activeLi;
mui("#patientList").on("tap", "li .item", function() {
	var index = $(this.parentNode).index();
	if(this.parentNode.parentNode.getElementsByTagName("li").length != cardIdArray.length) {
		index = searchPatientIndex[index]; //搜索结果中点击
	}
	cardIdIndex = index;
	CARDID = cardIdArray[index];
	if(activeLi != null) {
		activeLi.getElementsByClassName("item")[0].style.color = "#333333";
		if(activeLi.lastChild.tagName == "INPUT") {
			activeLi.removeChild(activeLi.lastChild);
		}
	}
	activeLi = this.parentNode;
	this.style.color = "#5ebafe";
	var now = new Date();
	var month = now.getMonth() + 1;
	if(month < 10) {
		month = "0" + month;
	}
	var day = now.getDate();
	if(day < 10){
		day = "0" + day;
	}
	var today = now.getFullYear() + "-" + month + "-" + day;
	if(today == strDialysisDate) {
		if(dialysisStateArray[index] == "0" || dialysisStateArray[index] == "1") {
			$(this.parentNode).append("<input id='changeBedSubmit' type='submit' value='换床' />");
		}
	}

	QueryPuntureNurse(CARDID, strDialysisDate);// 穿刺护士查询
	QueryTreamentNurse(CARDID, strDialysisDate);//治疗护士查询
	QueryDialysisSummary(CARDID, strDialysisDate);//查询患者透后记录
	QueryBaseInfo(CARDID, strDialysisDate);//查询患者基本信息
	QueryDialysisInfo(strDialysisDate.replace("-", "").replace("-", ""), CARDID);//透前透后信息
	QueryDialysisRecords(CARDID, strDialysisDate);//查询透析过程记录
	//console.log(CARDID + "---" +strDialysisDate.replace("-", "").replace("-", ""));
	QueryExecuteOrder(CARDID, strDialysisDate.replace("-", "").replace("-", ""));//查询未执行与待执行医嘱	

});