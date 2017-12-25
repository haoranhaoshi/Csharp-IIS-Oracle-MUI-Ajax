document.getElementById("dateSelect").addEventListener('tap', function() { //日历图标选择时间
	plus.nativeUI.pickDate(function(e) {
		var d = e.date;
		getDateAndPatients(d.getFullYear(), d.getMonth() + 1, d.getDate());
	}, function(e) {
		mui.toast('已取消');
	}, {
		title: "请选择日期",
		date: now
	});
});
document.getElementById("date").addEventListener('tap', function() { //日期选择时间
	plus.nativeUI.pickDate(function(e) {
		var d = e.date;
		getDateAndPatients(d.getFullYear(), d.getMonth() + 1, d.getDate());
	}, function(e) {
		mui.toast('已取消');
	}, {
		title: "请选择日期",
		date: now,
	});
});
document.getElementById("lastDay").addEventListener('tap', function() { //前一天
	var currentString = document.getElementById("date").innerText;
	var currentDate = new Date(currentString);
	var preDate = new Date(currentDate.getTime() - 24 * 60 * 60 * 1000);
	getDateAndPatients(preDate.getFullYear(), preDate.getMonth() + 1, preDate.getDate());
});
document.getElementById("nextDay").addEventListener('tap', function() { //后一天
	var currentString = document.getElementById("date").innerText;
	var currentDate = new Date(currentString);
	var nextDate = new Date(currentDate.getTime() + 24 * 60 * 60 * 1000);
	getDateAndPatients(nextDate.getFullYear(), nextDate.getMonth() + 1, nextDate.getDate());
});
var searchPatientIndex = new Array(); //搜索结果项在全部列表项中的索引
document.getElementById("search").addEventListener('input', function() { //搜索
	var selectCount = 0;
	searchPatientIndex.length = 0;
	if(this.value != "") {
		clearPatient();
	}
	for(var i = 0; i < cardIdArray.length; i++) {
		if(selectChar(this.value, cardIdArray[i]) || selectChar(this.value, patientNameArray[i])) {
			getSearchResult(i);
			selectCount++;
			searchPatientIndex.length++;
			searchPatientIndex[searchPatientIndex.length - 1] = i;
		}
	}
	setPateintCount(selectCount);
});
var selectChar = function(substr, str) {
	return new RegExp(substr).test(str);
}
var setPateintCount = function(count) {
	document.getElementById("patientCount").innerText = "(" + count + ")";
}
document.getElementById("returnIcon").addEventListener('tap', function() { //搜索区返回
	document.activeElement.blur();
	searchDiv.style.left = "calc(8px + 12px + 20px)";
	searchDiv.style.width = "calc(100% - 12px * 2 - 8px * 2 - 20px)";
	searchDiv.parentNode.style.width = "calc(100% - 16px)";
	searchDiv.parentNode.getElementsByTagName("span")[0].style.left = "calc(8px + 12px)";
	this.style.visibility = "hidden";
	document.getElementById("scanIcon").style.visibility = "visible";
	clearPatient();
	for(var i = 0; i < cardIdArray.length; i++) {
		$("#patientList").append("<li class='mui-table-view-cell'><div class='item'><div>" + bedNameArray[i] + "&nbsp;" + patientNameArray[i] + "</div><div>" + patientSexArray[i] + "&nbsp;&nbsp;" + patientAgeArray[i] + "</div></div><div class='listCircle' style='background-color:" + dialysisStateColor[parseInt(dialysisStateArray[i])] + ";'></div></li>");
	}
	setPateintCount(cardIdArray.length);
	document.getElementById("search").value = "";
});
var stateItems = document.getElementById("stateList").getElementsByTagName("li"); //患者签到状态切换
for(var i = 0; i < stateItems.length; i++) {
	stateItems[i].addEventListener('tap', function() {
		document.getElementById("state").innerText = this.innerText;
		switch(this.innerText) {
			case "已签到":
				strSigninState = "1";
				break;
			case "未签到":
				strSigninState = "0";
				break;
			case "全部":
				strSigninState = "ALL";
				break;
		}
		QueryDialysisPatientListByDate(strDialysisDate, strSigninState);
	})
}
var QueryDialysisState = function() {
	var urlStr1 = serverAddress + "QueryDialysisState";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {},
		success: function(data) {
			var DalysisState = getXmlDoc(data).getElementsByTagName("DalysisState");
			var index; //按透析状态0至5排列
			for(var i = 0; i < DalysisState.length; i++) {
				index = parseInt(getNodeValue(DalysisState[i].getElementsByTagName("DICID")[0]));
				dialysisStateColor[index] = colorRGB2Hex(getNodeValue(DalysisState[index].getElementsByTagName("MEMO3")[0]));
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryDialysisState:Ex");
		}
	});
}
var colorRGB2Hex = function(color) {
	var rgb = color.split(',');
	var r = parseInt(rgb[1]);
	var g = parseInt(rgb[2]);
	var b = parseInt(rgb[3]);
	var colorH = ((r << 16) + (g << 8) + b).toString(16);
	if(colorH.length < 6) {
		var length = colorH.length;
		for(var i = 0; i < (6 - length); i++) {
			colorH = "0" + colorH;
		}
	}
	var hex = "#" + colorH;
	return hex;
}
var setDate = function(year, month, date) {
	var Year = year;
	var Month = month;
	if(Month < 10) {
		Month = "0" + Month;
	}
	var Date = date;
	if(Date < 10) {
		Date = "0" + Date;
	}
	var strDialysisDate = Year + "-" + Month + "-" + Date;
	document.getElementById("date").innerText = Year + "/" + Month + "/" + Date;
}
var getDateAndPatients = function(year, month, date) {
	var Year = year;
	var Month = month;
	var Date = date;
	if(Month < 10) {
		Month = "0" + Month;
	}
	if(date < 10) {
		Date = "0" + Date;
	}
	document.getElementById("date").innerText = Year + "/" + Month + "/" + Date;
	strDialysisDate = Year + "-" + Month + "-" + Date;
	QueryDialysisPatientListByDate(strDialysisDate, strSigninState);
}
var getSearchResult = function(i) {
	$("#patientList").append("<li class='mui-table-view-cell'><div class='item'><div>" + bedNameArray[i] + "&nbsp;" + patientNameArray[i] + "</div><div>" + patientSexArray[i] + "&nbsp;&nbsp;" + patientAgeArray[i] + "</div></div><div class='listCircle' style='background-color:" + dialysisStateColor[parseInt(dialysisStateArray[i])] + ";'></div></li>");
}
var getToday = function() {
	var year = now.getFullYear();
	var month = now.getMonth() + 1;
	if(month < 10) {
		month = "0" + month;
	}
	var date = now.getDate();
	if(date < 10) {
		date = "0" + date;
	}
	var strDialysisDate = year + "-" + month + "-" + date;
	return strDialysisDate;
}
var QueryDialysisPatientListByDate = function(strDialysisDate, strSigninState) {
	var urlStr1 = serverAddress + "QueryDialysisPatientListByDate";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strDialysisDate: strDialysisDate,
			strSigninState: strSigninState
		},
		success: function(data) {
			var NurseDetail = getXmlDoc(data).getElementsByTagName("NurseDetail");
			clearAllTab();	
			$("#right").empty();
			clearArray(NurseDetail.length);
			setPateintCount(NurseDetail.length);
			document.getElementById("scrollPatientList").style.height = "calc(60px * " + NurseDetail.length + ")";
			document.getElementById("class").innerText = "所有班";
			document.getElementById("area").innerText = "所有区";
			var Patient;
			for(var i = 0; i < NurseDetail.length; i++) {
				bedNameArray[i] = getNodeValue(NurseDetail[i].getElementsByTagName("BEDNAME")[0]);
				Patient = NurseDetail[i].getElementsByTagName("Patient");
				patientNameArray[i] = getNodeValue(Patient[0].getElementsByTagName("PATIENTNAME")[0]);
				patientSexArray[i] = getNodeValue(Patient[0].getElementsByTagName("PATIENTSEX")[0]);
				patientAgeArray[i] = getNodeValue(Patient[0].getElementsByTagName("PATIENTAGE")[0]).replace(/[^0-9]/ig, ""); //患者年龄	
				dialysisStateArray[i] = getNodeValue(NurseDetail[i].getElementsByTagName("DIALYSISSTATE")[0]);
				$("#patientList").append("<li class='mui-table-view-cell'><div class='item'><div>" + bedNameArray[i] + "&nbsp;" + patientNameArray[i] + "</div><div>" + patientSexArray[i] + "&nbsp;&nbsp;" + patientAgeArray[i] + "</div></div><div class='listCircle' style='background-color:" + dialysisStateColor[parseInt(dialysisStateArray[i])] + ";'></div></li>");
				cardIdArray[i] = getNodeValue(NurseDetail[i].getElementsByTagName("CARDID")[2]);
				classArray[i] = getNodeValue(NurseDetail[i].getElementsByTagName("CLASSES")[0]);
				areaNameArray[i] = getNodeValue(NurseDetail[i].getElementsByTagName("AREANAME")[0]);
				departmentNameArray[i] = getNodeValue(Patient[0].getElementsByTagName("DEPARTMENTNAME")[0]);
				signStateArray[i] = getNodeValue(NurseDetail[i].getElementsByTagName("SIGNINSTATE")[0]);
				specialPatientType[i] = getNodeValue(Patient[0].getElementsByTagName("SPECIALPATIENTTYPE")[0]);
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryDialysisPatientListByDate：Ex");
		}
	});
}
var clearAllTab = function() {
	clearPatient();
	clearRecords();
	$("#executeContent").empty();
	clearAfterRecords();
}
var clearAfterRecords = function() {
	var result = "";
	setNurse("punctureNurse",result);
	setNurse("treatmentNurse",result);
	var input = document.getElementsByClassName("afterItem")[0].getElementsByTagName("INPUT");
	for(var i = 0; i < input.length; i++) {
		input[i].value = "";
	}
	document.getElementById("lessWeight").innerText = "";
	document.getElementsByClassName("afterItem")[0].getElementsByTagName("TEXTAREA")[0].value = "";
};
var getNodeValue = function(node) {
	if(node == null || node.firstChild == null) {
		return "";
	} else {
		return node.firstChild.nodeValue;
	}
}
var clearArray = function(i) {
	cardIdArray.length = i;
	bedNameArray.length = i;
	patientNameArray.length = i;
	patientSexArray.length = i;
	patientAgeArray.length = i;
	classArray.length = i;
	areaNameArray.length = i;
	departmentNameArray.length = i;
	signStateArray.length = i;
	dialysisStateArray.length = i;
	specialPatientType.length = i;
}
var clearPatient = function() {
	$("#patientList").empty();
	CARDID = "";
	mui("#basicInformation div")[0].innerHTML = "姓名：<br>性别：<br>班次：<br>透析号：";
	mui("#basicInformation div")[1].innerHTML = "<font color='red'>状态：</font><br>科室：<br>区域：<br>透析日期：";
	clearInfo();
}
var clearInfo = function() {
	mui("#dialysisBeforeInfo div")[0].innerHTML = "血压：/mmHg<br>呼吸：次/分<br>透前体重：<font id='beforeWeight'></font>kg<br>前次透后体重：kg<br><font color='red'>治疗模式：</font><br>血流量：ml/min<br>目标（干）体重：kg<br>透析机类型：<br>血管通路：<br>透析液类型：<br>透析液钠成分：mmol/L<br>透析液钙成分：mmol/L<br>碳酸氢根：mmol/L<br>单超：";
	mui("#dialysisBeforeInfo div")[1].innerHTML = "心率：次/分<br>体温：℃<br>增加体重：kg<br>前次透析日期：<br>治疗时长：<font id='TREATMENTDURATION'></font>h<br>超滤总量：ml<br>置换量：ml<br>透析（滤）器：<br>流量：ml/min<br>抗凝剂：<br>总量：<br>首剂：<br>追加：";
	//mui("#dialysisAfterInfo div")[0].innerHTML = "透后体重： kg";
	//mui("#dialysisAfterInfo div")[1].innerHTML = "实际超滤总量： ml";
}