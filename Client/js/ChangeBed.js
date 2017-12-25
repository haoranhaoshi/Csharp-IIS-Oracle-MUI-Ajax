mui("#patientList").on("tap", "li input", function() {
	document.getElementById("menban").style.visibility = "visible";
	document.getElementById("changeBed").style.visibility = "visible";

	QueryEmptyBedList(HOSPITALID, DEPTID, "ALL", strDialysisDate, classArray[cardIdIndex], specialPatientType[cardIdIndex]);
});
document.getElementById('menban').addEventListener('tap', function() {
	changeBedLeave();
});
var changeBedLeave = function() {
	document.getElementById("menban").style.visibility = "hidden";
	document.getElementById("changeBed").style.visibility = "hidden";
}

function selectChanged() {
	var className = document.getElementById("classChange").options[document.getElementById("classChange").selectedIndex].text;
	var areaId = document.getElementById("areaChange").options[document.getElementById("areaChange").selectedIndex].text;
	if(areaId == "所有区") {
		areaId = "ALL";
	} else {
		areaId = areaId.replace("区", "");
	}
	QueryEmptyBedList(HOSPITALID, DEPTID, areaId, strDialysisDate, className, specialPatientType[cardIdIndex]);
}
var changeBedIdArray = new Array();
var changeTr, changeAreaName, changeBedId, changeBedName, changeType;
mui("#bedChoices").on("tap", "tr", function() {
	if(changeTr != null) {
		if($(changeTr).index() != 0) {
			$(changeTr).prev().css('borderBottom','1px solid #D9D9D9');
		} else {
			document.getElementById("changeBed").getElementsByTagName("thead")[0].style.borderBottom = "1px solid #D9D9D9";
		}
		changeTr.style.borderTop = "none";
		changeTr.style.borderLeft = "none";
		changeTr.style.borderRight = "none";
		changeTr.style.borderBottom = "1px solid #D9D9D9";
	}
	changeTr = this;
	if($(changeTr).index() != 0) {
		$(changeTr).prev().css('borderBottom','none');
	} else {
		document.getElementById("changeBed").getElementsByTagName("thead")[0].style.borderBottom = "none";
	}
	changeTr.style.border = "2px solid #5ebafe";
	var td = changeTr.getElementsByTagName("td");
	changeAreaName = td[1].innerText;
	changeBedId = changeBedIdArray[$(this).index()];
	changeBedName = td[2].innerText;
});
document.getElementById('saveCancel').addEventListener('tap', function() {
	mui.toast('已取消');
	changeBedLeave();
});
document.getElementById('saveExecute').addEventListener('tap', function() {
	var className = document.getElementById("classChange").options[document.getElementById("classChange").selectedIndex].text;
	ChangePatientBed(strDialysisDate, CARDID, className, changeAreaName.replace("区",""), changeAreaName, changeBedId, changeBedName);
});
var ChangePatientBed = function(testDate, cardId, classes, areaId, areaName, bedId, bedName) {
	var urlStr1 = serverAddress + "ChangePatientBed";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			testDate: testDate,
			cardId: cardId,
			classes: classes,
			areaId: areaId,
			areaName: areaName,
			bedId: bedId,
			bedName: bedName
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "成功保存！") {
				mui.toast('已保存');
				changeBedLeave();
				QueryDialysisPatientListByDate(strDialysisDate, strSigninState);
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryClassesList:Ex");
		}
	});
}
var QueryEmptyBedList = function(strHospitalId, strDeptId, strAreaIid, strTestDate, strClasses, patientType) {
	var urlStr1 = serverAddress + "QueryEmptyBedList";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strHospitalId: strHospitalId,
			strDeptId: strDeptId,
			strAreaIid: strAreaIid,
			strTestDate: strTestDate,
			strClasses: strClasses,
			patientType: patientType
		},
		success: function(data) {
			changeTr = null;
			$("#bedChoices").empty();
			changeBedIdArray.length = 0;
			var BedAndMachineModel = getXmlDoc(data).getElementsByTagName("BedAndMachineModel");
			for(var i = 0; i < BedAndMachineModel.length; i++) {
				$("#bedChoices").append("<tr><td style='width: 20px;'>" + (i + 1) + "</td><td style='width: calc((100% - 15px) / 5);'>" + getNodeValue(BedAndMachineModel[i].getElementsByTagName("AREANAME")[0]) + "</td><td style='width: calc((100% - 15px) / 5);'>" + getNodeValue(BedAndMachineModel[i].getElementsByTagName("BedName")[0]) + "</td><td style='width: calc((100% - 15px) / 5);'>" + getNodeValue(BedAndMachineModel[i].getElementsByTagName("BEDTYPE")[0]) + "</td><td style='width: calc((100% - 15px) / 5);'>" + getNodeValue(BedAndMachineModel[i].getElementsByTagName("DIALYSISMODE")[0]) + "</td><td style='width: calc((100% - 15px) / 5);'>" + getNodeValue(BedAndMachineModel[i].getElementsByTagName("DIALYZER")[0]) + "</td></tr>")
				changeBedIdArray[i] = getNodeValue(BedAndMachineModel[i].getElementsByTagName("BedID")[0]);
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryClassesList:Ex");
		}
	});
}