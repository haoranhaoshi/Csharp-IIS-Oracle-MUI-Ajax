var QueryBaseInfo = function(cardId, testDate) {
	var urlStr1 = serverAddress + "QueryBaseInfo";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardId: cardId,
			testDate: testDate
		},
		success: function(data) {
			var NurseDetail = getXmlDoc(data).getElementsByTagName("NurseDetail");
			var Patient = NurseDetail[0].getElementsByTagName("Patient");
			var dalysisState;
			switch(getNodeValue(NurseDetail[0].getElementsByTagName("DIALYSISSTATE")[0])) {
				case "0":
					dalysisState = "未检测";
					break;
				case "1":
					dalysisState = "准备上机";
					break;
				case "2":
					dalysisState = "透析中";
					break;
				case "3":
					dalysisState = "已下机";
					break;
				case "4":
					dalysisState = "未总审";
					break;
				case "5":
					dalysisState = "已总审";
					break;
			}
			mui("#basicInformation div")[0].innerHTML = "姓名：" + getNodeValue(Patient[0].getElementsByTagName("PATIENTNAME")[0]) + "<br>性别：" + getNodeValue(Patient[0].getElementsByTagName("PATIENTSEX")[0]) + "<br>班次：" + getNodeValue(NurseDetail[0].getElementsByTagName("CLASSES")[0]) + "<br>透析号：" + getNodeValue(NurseDetail[0].getElementsByTagName("CARDID")[2]);
			mui("#basicInformation div")[1].innerHTML = "<font color='red'>状态：" + dalysisState + "</font><br>科室：" + getNodeValue(Patient[0].getElementsByTagName("DEPARTMENTNAME")[0]) + "<br>区域：" + getNodeValue(NurseDetail[0].getElementsByTagName("AREANAME")[0]) + "<br>透析日期：" + getNodeValue(NurseDetail[0].getElementsByTagName("TESTDATE")[1]);
			dalysisState = getNodeValue(NurseDetail[0].getElementsByTagName("DIALYSISSTATE")[0]);
			setMachineButton(dalysisState);
			setAddDeleteButton(dalysisState);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryBaseInfo:Ex");
		}
	});
}
var setMachineButton = function(state) {
	$("#right").empty();
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
		switch(state) {
			case "1":
				$("#right").append("<input id='upButton' type='submit' value='上机' />");
				break;
			case "2":
				$("#right").append("<input id='upButton' type='submit' value='取消上机' /><input id='downButton' type='submit' value='下机' />");
				break;
			case "3":
				$("#right").append("<input id='upButton' type='submit' value='取消下机' /><input id='downButton' type='submit' value='确认' />");
				break;
		}
	}

}
var updateDalysisState = function(state) {	
	for(var i = 0; i < cardIdArray.length; i++) {
		if(CARDID == cardIdArray[i]) {
			document.getElementById("patientList").getElementsByTagName("li")[i].getElementsByClassName("listCircle")[0].style.backgroundColor = dialysisStateColor[parseInt(state)];
		}
	}
	
	if(state == "0" || state == "1"){
		if(document.getElementById("changeBedSubmit") == null){
			$(activeLi).append("<input id='changeBedSubmit' type='submit' value='换床' />");
		}	
	}
	if(state == "2" || state == "3" || state == "4" || state == "5"){
		if(document.getElementById("changeBedSubmit") != null){
			activeLi.removeChild(activeLi.lastChild);
		}		
	}
	jumpTab(state);
	
	dialysisStateArray[cardIdIndex] = state;
	QueryBaseInfo(CARDID, strDialysisDate);
}