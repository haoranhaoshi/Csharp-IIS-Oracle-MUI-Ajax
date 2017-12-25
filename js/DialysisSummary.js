var QueryDialysisSummary = function(cardId, testDate) {
	var urlStr1 = serverAddress + "QueryDialysisSummary";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardId: cardId,
			testDate: testDate
		},
		success: function(data) {			
			var input = document.getElementsByClassName("afterItem")[0].getElementsByTagName("INPUT");
			if(data.childNodes[0].textContent == "") {
				for(var i = 0; i < input.length; i++) {
					input[i].value = "";
				}
				document.getElementById("lessWeight").innerText = "";
				document.getElementsByClassName("afterItem")[0].getElementsByTagName("TEXTAREA")[0].value = "";
				return;
			}
			input[1].type = "number";
			input[2].type = "number";	
			input[1].readonly = "false";
			input[2].readonly = "false";
			var NurseDetail = getXmlDoc(data).getElementsByTagName("NurseDetail");
			input[0].value = getNodeValue(NurseDetail[0].getElementsByTagName("TREATMENTDURATION")[1]);
			var factsfi = getNodeValue(NurseDetail[0].getElementsByTagName("DoctorDetail")[0].getElementsByTagName("FACTSFILTRATIONQUANTITY")[0]);
			if(factsfi == "-"){
				input[1].type = "-";	
				input[1].readonly = "true";
			}
			input[1].value = factsfi;
			var afterw = getNodeValue(NurseDetail[0].getElementsByTagName("DoctorDetail")[0].getElementsByTagName("AFTERWEIGHT")[0])
			if(afterw == "卧"){
				input[2].type = "卧";	
				input[2].readonly = "true";
			}
			input[2].value = afterw;
			
			document.getElementById("lessWeight").innerText = getNodeValue(NurseDetail[0].getElementsByTagName("LESSENWEIGHT")[0]);
			input[3].value = getNodeValue(NurseDetail[0].getElementsByTagName("DoctorDetail")[0].getElementsByTagName("TEMPERATURE")[0]);
			input[4].value = getNodeValue(NurseDetail[0].getElementsByTagName("DoctorDetail")[0].getElementsByTagName("PULSE")[0]);
			input[5].value = getNodeValue(NurseDetail[0].getElementsByTagName("HEARTRATE")[0]);
			input[6].value = getNodeValue(NurseDetail[0].getElementsByTagName("DoctorDetail")[0].getElementsByTagName("BREATHING")[0]);
			input[7].value = getNodeValue(NurseDetail[0].getElementsByTagName("DoctorDetail")[0].getElementsByTagName("SPRESSURE")[0]);
			input[8].value = getNodeValue(NurseDetail[0].getElementsByTagName("DoctorDetail")[0].getElementsByTagName("DPRESSURE")[0]);
						
			document.getElementsByClassName("afterItem")[0].getElementsByTagName("TEXTAREA")[0].value = getNodeValue(NurseDetail[0].getElementsByTagName("DIALYSISSUMMARY")[0]);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}
document.getElementById("afterSaveButton").addEventListener('tap', function() {
	if(CARDID == "") {
		mui.toast("请选择患者");
	} else {		
		var PUNCTURENURSE = document.getElementById("punctureNurse").options[document.getElementById("punctureNurse").selectedIndex].text;
		var TREAMENTNURSE = document.getElementById("treatmentNurse").options[document.getElementById("treatmentNurse").selectedIndex].text;
		var input = document.getElementsByClassName("afterItem")[0].getElementsByTagName("INPUT");
		var result = "";
		var emptyNum = 0;
		for(var i = 0; i < input.length; i++) {
			result += input[i].value + '|';
			if(i == 2) {
				result += document.getElementById("lessWeight").innerText + '|';
			}

			if(input[i].value == "") {
				emptyNum++;
			}
		}
		var summaryText = document.getElementById("summaryText").value;
		if(PUNCTURENURSE == "" && TREAMENTNURSE == "" && emptyNum == input.length && summaryText == "") {
			mui.toast("请填写透后记录再保存");
			return;
		}
		saveNurseRole();
		result += summaryText;
		SaveDialysisSummary(CARDID, strDialysisDate, result);
	}

});

var SaveDialysisSummary = function(strCardId, strDialysisDate, strDialysisSummary) {
	var urlStr1 = serverAddress + "SaveDialysisSummary";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strCardId: strCardId,
			strDialysisDate: strDialysisDate,
			strDialysisSummary: strDialysisSummary
		},
		success: function(data) {
			myToast("其他信息" + data.childNodes[0].textContent);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert(errorThrown);
		}
	});
}
var getLessWeight = function(o) {
	if(CARDID != "") {
		if(document.getElementById("beforeWeight") != null) {
			var beforeWeight = document.getElementById("beforeWeight").innerText;
			if(beforeWeight != "") {
				document.getElementById("lessWeight").innerText = ((parseFloat(beforeWeight)*10000 - parseFloat(o.value)*10000)/10000);
			}
		}
	}
};
var factTimeBlur = function(o){
	if(document.getElementById("beforeWeight") != null && document.getElementById("beforeWeight").innerText == "卧"){
		document.getElementById("Temper").focus();
	}
}
