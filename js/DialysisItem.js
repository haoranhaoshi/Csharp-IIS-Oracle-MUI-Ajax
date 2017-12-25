var recordHeight;
var processItemHeight;
var circleMarginTop;
var lineMarginTop;
var recordCount = 0;
var resultSeqArray = new Array();
var firstTime = "00:00";
var lastTime = "00:00";
var timeId = "RESULTTIME";
var QueryDialysisItem = function() {
	var urlStr1 = serverAddress + "QueryDialysisItem";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {},
		success: function(data) {
			var ItemModel = getXmlDoc(data).getElementsByTagName("ItemModel");
			mui("#dialysisItem").innerHTML = "";
			var j = 0;
			var itemId;
			for(var i = 0; i < ItemModel.length; i++) {
				itemId = getNodeValue(ItemModel[i].getElementsByTagName("ITEMID")[0]);
				if(itemId != timeId) {
					signIdArray[j] = itemId;
					signNameArray[j] = getNodeValue(ItemModel[i].getElementsByTagName("ITEMNAME")[0]);
					signUnitArray[j] = getNodeValue(ItemModel[i].getElementsByTagName("UNIT")[0]);
					j++;
				}
			}
			changeLayout(signIdArray.length);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryDialysisItem:Ex");
		}
	});
}
var AddDialysisRecord = function(strDialysisDate, strCardId, loginId, loginName) {
	var urlStr1 = serverAddress + "AddDialysisRecord";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strDialysisDate: strDialysisDate,
			strCardId: strCardId,
			loginId: loginId,
			loginName: loginName
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(!isNaN(Number(result))) {
				addRecord(result);
			} else {
				myToast(result);
			}

		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("AddDialysisRecord:Ex");
		}
	});
}
var addRecord = function(mr) {
	if(document.getElementById("processContent").getElementsByClassName("record").length == 0) {
		QueryUpMachineTime(CARDID, strDialysisDate, mr);
		return;
	}
	var record = document.getElementById("processContent").getElementsByClassName("record");
	lastTime = record[record.length - 1].getElementsByClassName("time")[0].getElementsByTagName("INPUT")[0].value;
	lastTime = getNextHour();
	var aa = lastTime.substring(0, 2);
	if(aa == "00"){
		var bb = parseInt(strDialysisDate.substring(8, 10)) + 1;
		strDialysisDate = strDialysisDate.substring(0, 8) + bb.toString();
	
//		var cc = parseInt(strDialysisDate.substring(8, 10)) - 1;
		if(parseInt(strDialysisDate.substring(8, 10)) > 10){
			strDialysisDate = strDialysisDate.substring(0, 8) + bb.toString();
		}else{
			strDialysisDate = strDialysisDate.substring(0, 8) + "0" + bb.toString();
		}		
		SaveDialysisResult(timeId, strDialysisDate + lastTime + ":59", mr);
		console.log(strDialysisDate + "1");
		console.log(lastTime +"1");
	}
	else{
		SaveDialysisResult(timeId, strDialysisDate + lastTime + ":59", mr);
		console.log(strDialysisDate + "2");
		console.log(lastTime +"2");
	}

	//addRecordUI();
//	SaveDialysisResult(timeId, strDialysisDate + lastTime + ":59", mr);
	QueryDialysisRecords(CARDID, strDialysisDate);
	myToast("成功增加！");
};
var addRecordUI = function() {
	var processItem = "";
	for(var i = 0; i < signIdArray.length; i++) {
		processItem += signNameArray[i] + "（" + signUnitArray[i] + "）：<input type='number' onfocus='textfocus(this)' onblur='textblur(this)' placeholder=''><br>";
	}
	$("#addButton").before("<div class='record'><div class='rectangle'></div><div class='time'><p><input type='text' onfocus='textfocus(this)' onblur='textblur(this)' value='" + lastTime + "'></p></div><div class='processItem'><div class='dialysisItem'>" + processItem + "</div><button class='mui-btn mui-btn-primary deleteButton'>删除</button></div><div class='circle'></div><div class='line'></div></div>");
	recordCount++;
	resizeRecord();
};
var getNextHour = function() {
	var hh = parseInt(lastTime.substring(0, 2));
	if(firstTime != "00:00") {
		var TREATMENTDURATION = parseFloat(document.getElementById("TREATMENTDURATION").innerText);
		if(parseInt(lastTime.substring(0, 2)) - parseInt(firstTime.substring(0, 2)) == Math.floor(parseInt(document.getElementById("TREATMENTDURATION").innerText))) {
			var mi = Math.floor((TREATMENTDURATION - Math.floor(TREATMENTDURATION)) * 60);
			if(mi != 0) {
				mi += parseInt(lastTime.substring(3, 5));
				if(mi > 60) {
					mi = mi - 60;
					hh++;
					hh = hh < 10 ? "0" + hh : hh;
				}
				mi = mi < 10 ? "0" + mi : mi;

				return hh + ":" + mi;
			}

		}
	}

	hh++;
	if(hh > 23){
		hh = "0";
	}
	hh = hh < 10 ? "0" + hh : hh;
	return hh + lastTime.substring(2, 5);
}
var QueryDialysisRecords = function(cardid, testdate) {
	var urlStr1 = serverAddress + "QueryDialysisRecords";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardid: cardid,
			testdate: testdate
		},
		success: function(data) {
			clearRecords();
			var ProcResult = getXmlDoc(data).getElementsByTagName("ProcResult");
			var processItem;
			resultSeqArray.length = 0;
			lastTime = "00:00";
			for(var j = 0; j < ProcResult.length; j++) {
				resultSeqArray[j] = getNodeValue(ProcResult[j].getElementsByTagName("RESULTSEQ")[0]);

				var processItem = "";
				for(var i = 0; i < signIdArray.length; i++) {
					if(signIdArray[i] == "ARTERIALPRESSURE" || signIdArray[i] == "SPO2"){
						processItem += signNameArray[i] + "（" + signUnitArray[i] + "）：<input type='number' style='background-color: transparent;' onfocus='textfocus(this)' onblur='textblur(this)' value='" + getNodeValue(ProcResult[j].getElementsByTagName(signIdArray[i])[0]) + "'><br>";
					}else{
						processItem += signNameArray[i] + "（" + signUnitArray[i] + "）：<input type='number' style='background-color: transparent;' onfocus='textfocus(this)' onblur='textblur(this)' placeholder='" + getNodeValue(ProcResult[j].getElementsByTagName(signIdArray[i])[0]) + "'><br>";
					}										
				}
				if(j == 0) {
					firstTime = getNodeValue(ProcResult[j].getElementsByTagName(timeId)[0]).substring(11, 16);
				}
				lastTime = getNodeValue(ProcResult[j].getElementsByTagName(timeId)[0]).substring(11, 16);
				$("#addButton").before("<div class='record'><div class='rectangle'></div><div class='time'><p><input type='time' style='background-color: transparent;' onfocus='textfocus(this)' onblur='textblur(this)' value='" + lastTime + "'></p></div><div class='processItem'><div class='dialysisItem'>" + processItem + "</div><button class='mui-btn mui-btn-primary deleteButton'>删除</button></div><div class='circle'></div><div class='line'></div></div>");
			}

			setAddDeleteButton(dialysisStateArray[cardIdIndex]);

			recordCount = ProcResult.length;
			resizeRecord();
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryDialysisRecords:Ex");
		}
	});
}
var setAddDeleteButton = function(state) {
	var deleteButton = document.getElementsByClassName("deleteButton");
	var addButton = document.getElementById("addButton");
	if(state != "2") {
		addButton.style.visibility = "hidden";
		for(var i = 0; i < deleteButton.length; i++) {
			deleteButton[i].style.visibility = "hidden";
		}
	} else if(state == "2") {
		addButton.style.visibility = "visible";
		for(var i = 0; i < deleteButton.length; i++) {
			deleteButton[i].style.visibility = "visible";
		}
	}
}
var DeleteDialysisRecord = function(strResultSeq) {
	var urlStr1 = serverAddress + "DeleteDialysisRecord";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strResultSeq: strResultSeq
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "成功删除！") {
				QueryDialysisRecords(CARDID, strDialysisDate);
				myToast("成功删除！");
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("DeleteDialysisRecord:Ex");
		}
	});
}
var itemInput;
var SaveDialysisResult = function(strItemId, strResult, strResultSeq) {
	var urlStr1 = serverAddress + "SaveDialysisResult";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strItemId: strItemId,
			strResult: strResult,
			strResultSeq: strResultSeq
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "成功保存！") {
				if(itemInput != null) {
					if(itemInput.type != "time") {
						if(strResult == "-") {
							itemInput.placeholder = strResult;
							itemInput.setAttribute('class', 'change');
						} else if(strItemId != "ARTERIALPRESSURE" && strItemId != "SPO2"){
							itemInput.placeholder = itemInput.value;
							itemInput.value = "";
							itemInput.setAttribute('class', 'change');
						}else{
							itemInput.style.color = "red";
						}

					} else {
						QueryDialysisRecords(CARDID, strDialysisDate);
					}
					itemInput = null;
				} else {
					QueryDialysisRecords(CARDID, strDialysisDate);
				}
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("SaveDialysisResult:Ex");
		}
	});
}
var ConfirmDialysisEnd = function(cardId, testDate, strDialysisSummary) {
	var urlStr1 = serverAddress + "ConfirmDialysisEnd";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardId: cardId,
			testDate: testDate,
			strDialysisSummary: strDialysisSummary
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "成功确认！") {
				updateDalysisState("5");
			}
			myToast(result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("ConfirmDialysisEnd:Ex");
		}
	});
}
var clearRecords = function() {
	var processContent = document.getElementById("processContent");
	var childs = processContent.childNodes;
	for(var i = childs.length - 1 - 2; i >= 0; i--) {
		processContent.removeChild(childs[i]);
	}
	recordCount = 0;
}
var changeLayout = function(signCount) {
	recordHeight = "calc(28px + 40px * " + signCount + " + 10px + 38px + 15px + 20px)";
	processItemHeight = "calc(40px * " + signCount + " + 10px + 38px + 15px)";
	circleMarginTop = "calc(40px * " + signCount + " + 10px + 38px + 15px + 28px - 10px)";
	lineMarginTop = "calc(40px * " + signCount + " + 10px + 38px + 15px + 28px)";
}
//增加
document.getElementById("addButton").addEventListener("tap", function() {
	if(CARDID == "") {
		mui.toast("请选择患者");
	} else {
		AddDialysisRecord(strDialysisDate, CARDID, PERSONID, PERSONNAME);
	}
});
//焦点离开
function textblur(o) {
	itemInput = o;
	var record = o.parentNode.parentNode.parentNode;
	if(o.parentNode.parentNode.className == "time") {
		SaveDialysisResult(timeId, strDialysisDate + o.value + ":59", resultSeqArray[$(record).index()]);
		return;
	}
	if(o.value == "") {
		//超滤率、超滤量可为"-"
		if(o.parentNode.className == "dialysisItem" && (signIdArray[($(o).index()) / 2] == "ULTRAFILTRATIONVOLUME" || signIdArray[($(o).index()) / 2] == "HEPARIN")) {
			SaveDialysisResult(signIdArray[($(o).index()) / 2], "-", resultSeqArray[$(record).index()]);
			return;
		}
		//置换液速度、血氧饱和度可为空
		if(o.parentNode.className == "dialysisItem" && (signIdArray[($(o).index()) / 2] == "ARTERIALPRESSURE" || signIdArray[($(o).index()) / 2] == "SPO2")) {
			SaveDialysisResult(signIdArray[($(o).index()) / 2], "", resultSeqArray[$(record).index()]);
			return;
		}		
	}
	if(o.value != o.placeholder) {
		if(o.value != "") {
			if(o.parentNode.className == "dialysisItem") {
				SaveDialysisResult(signIdArray[($(o).index()) / 2], o.value, resultSeqArray[$(record).index()]);
			}
		}
	} else {
		o.placeholder = o.value;
		o.value = "";
		if(o.className == "change") {
			o.setAttribute('class', 'change');
		} else {
			o.setAttribute('class', '');
		}
	}
}
var resizeRecord = function() {
	var record = document.getElementsByClassName("record");
	var processItem = document.getElementsByClassName("processItem");
	var circle = document.getElementsByClassName("circle");
	var line = document.getElementsByClassName("line");
	for(var i = 0; i < record.length; i++) {
		record[i].style.height = recordHeight;
		processItem[i].style.height = processItemHeight;
		circle[i].style.marginTop = circleMarginTop;
		line[i].style.marginTop = lineMarginTop;
	}
	document.getElementById("processContent").style.height = "calc((20px + 20px + 28px + 40px * " + signIdArray.length + " + 10px + 38px + 15px + 20px) * " + recordCount + " + 30px + 38px)";
}
//删除
mui("#processContent").on("tap", ".deleteButton", function() {
	var record = this.parentNode.parentNode;
	var deleteTime = record.getElementsByClassName("time")[0].getElementsByTagName("INPUT")[0].value;
	mui.confirm("是否要删除 " + patientNameArray[cardIdIndex] + " 透析时间为 " + deleteTime + " 的透析结果信息？", "提示", ['取消', '确定'], function(e) {
		if(e.index == 1) {
			DeleteDialysisRecord(resultSeqArray[$(record).index()]);
		} else {
			mui.toast('已取消');
		}
	}, 'div')
});
var QueryUpMachineTime = function(cardId, testDate, mr) {
	var urlStr1 = serverAddress + "QueryUpMachineTime";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardId: cardId,
			testDate: testDate
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			var time = result.split(" ")[1].split(":");
			lastTime = (parseInt(time[0]) < 10 ? "0" : "") + time[0] + ":" + time[1];
			SaveDialysisResult(timeId, strDialysisDate + lastTime + ":59", mr);
			SaveDialysisResult("SPRESSURE", SPRESSURE, mr);
			SaveDialysisResult("DPRESSURE", DPRESSURE, mr);
			SaveDialysisResult("PULSE", PULSE, mr);
			SaveDialysisResult("TRANSMEMBRANEPRESSURE", BREATHING, mr);
			myToast("成功增加！");
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryDialysisItem:Ex");
		}
	});
}