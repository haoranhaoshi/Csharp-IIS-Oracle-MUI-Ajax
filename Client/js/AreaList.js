/*区信息*/
var QueryAreaList = function(strHospitalId, strDeptId) {
	var urlStr1 = serverAddress + "QueryAreaList";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strHospitalId: strHospitalId,
			strDeptId: strDeptId,
		},
		success: function(data) {
			var AreaModel = getXmlDoc(data).getElementsByTagName("AreaModel");
			var showAreaCount = AreaModel.length;
			if(showAreaCount > 9) {
				showAreaCount = 9;
			}
			document.getElementById("areaSelect").style.height = "calc(43px * " + showAreaCount + " + 7px + 7px)";
			for(var i = 0; i < AreaModel.length; i++) {
				var Name = AreaModel[i].getElementsByTagName("Name")[0].firstChild.nodeValue; //区名
				if(Name == "全部") {
					Name = "所有区";
				}
				$("#areaChange").append("<option>" + Name + "</option>");
				$("#areaList").append("<li class='mui-table-view-cell'>" + Name + "</li>");
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryAreaList:Ex");
		}
	});
}
mui("#areaList").on("tap", "li", function() {
	document.getElementById("area").innerText = this.innerText;
	getPatientWithClassArea(document.getElementById("class").innerText, this.innerText);
});
var getPatientWithClassArea = function(classType, areaName) {
	clearPatient();
	if(classType == "所有班" & areaName == "所有区") {
		QueryDialysisPatientListByDate(strDialysisDate, strSigninState);
	}
	var selectPatientCount = 0;
	if(classType == "所有班" & areaName != "所有区") {
		for(var i = 0; i < areaNameArray.length; i++) {
			if(areaName == areaNameArray[i]) {
				getSearchResult(i);
				selectPatientCount++;
			}
		}
	}
	if(classType != "所有班" & areaName == "所有区") {
		for(var i = 0; i < classArray.length; i++) {
			if(classType == classArray[i]) {
				getSearchResult(i);
				selectPatientCount++;
			}
		}
	}
	if(classType != "所有班" & areaName != "所有区") {
		for(var i = 0; i < classArray.length; i++) {
			if(classType == classArray[i]) {
				if(areaName == areaNameArray[i]) {
					getSearchResult(i);
					selectPatientCount++;
				}
			}
		}
	}
	setPateintCount(selectPatientCount);
}