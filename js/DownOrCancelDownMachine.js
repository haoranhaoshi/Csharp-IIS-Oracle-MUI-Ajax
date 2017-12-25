mui("#right").on("tap", "#downButton", function() {
	if(CARDID == "") {
		CARDID = localStorage.scanPatientID;
	}
	var buttonValue = this.value;
	var btnArray = ['取消', buttonValue];
	var now = new Date();
	var year = now.getFullYear(); //年   
	var month = now.getMonth() + 1; //月   
	var day = now.getDate(); //日
	var hour = now.getHours(); //时
	var minute = now.getMinutes() % 60;
	if(minute < 10) minute = '0' + minute;
	mui.confirm('当前时间：' + year + '/' + month + '/' + day + ' ' + hour + ':' + minute, '您确定要' + buttonValue + '吗？', btnArray, function(e) {
		if(e.index == 1) {
			if(buttonValue == "下机") {
				if(CARDID == "") {
					mui.toast("请选择患者");
				} else {
					DownMachine(strDialysisDate, CARDID, loginName, PERSONNAME);
				}
			}
			if(buttonValue == "确认") {
				if(CARDID == "") {
					mui.toast("请选择患者");
				} else {
					QueryDialysisSummary(CARDID, strDialysisDate);
					QueryPuntureNurse(CARDID, strDialysisDate);
					QueryTreamentNurse(CARDID, strDialysisDate);
					var input = document.getElementsByClassName("afterItem")[0].getElementsByTagName("INPUT");
					var result = "";
					for(var i = 0; i < input.length; i++) {
						result += input[i].value + '|';
						if(i == 2) {
							result += document.getElementById("lessWeight").innerText + '|';
						}
					}

					var summaryText = document.getElementById("summaryText").value;
					result += summaryText;
					ConfirmDialysisEnd(CARDID, strDialysisDate, result);
				}
			}
		} else {
			mui.toast('已取消');
		}
	}, 'div')
});
var jumpTab = function(state) {
	if(state == "3") {
		var tab = document.getElementById("tab1");
		tab.setAttribute("class", "mui-control-item");
		tab = document.getElementById("tab2");
		tab.setAttribute("class", "mui-control-item");
		tab = document.getElementById("tab3");
		tab.setAttribute("class", "mui-control-item");
		tab = document.getElementById("tab4");
		tab.setAttribute("class", "mui-control-item mui-active");

		tab = document.getElementById("hd");
		tab.setAttribute("class", "mui-control-content");
		tab = document.getElementById("process");
		tab.setAttribute("class", "mui-control-content");
		tab = document.getElementById("advice");
		tab.setAttribute("class", "mui-control-content");
		tab = document.getElementById("after");
		tab.setAttribute("class", "mui-control-content mui-active");
		return;
	}
	if(state == "2") {
		var tab = document.getElementById("tab1");
		tab.setAttribute("class", "mui-control-item");
		tab = document.getElementById("tab2");
		tab.setAttribute("class", "mui-control-item mui-active");
		tab = document.getElementById("tab3");
		tab.setAttribute("class", "mui-control-item");
		tab = document.getElementById("tab4");
		tab.setAttribute("class", "mui-control-item");

		tab = document.getElementById("hd");
		tab.setAttribute("class", "mui-control-content");
		tab = document.getElementById("process");
		tab.setAttribute("class", "mui-control-content mui-active");
		tab = document.getElementById("advice");
		tab.setAttribute("class", "mui-control-content");
		tab = document.getElementById("after");
		tab.setAttribute("class", "mui-control-content");
	}

}
var DownMachine = function(strDialysisDate, strCardId, strDownMachineNurseId, strDownMachineNurseName) {
	var urlStr1 = serverAddress + "DownMachine";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strDialysisDate: strDialysisDate,
			strCardId: strCardId,
			strDownMachineNurseId: strDownMachineNurseId,
			strDownMachineNurseName: strDownMachineNurseName
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "下机成功！") {
				updateDalysisState("3");
			}
			myToast(result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("DownMachine:Ex");
		}
	});
}
var CancelDownMachine = function(strDialysisDate, strCardId) {
	var urlStr1 = serverAddress + "CancelDownMachine";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strDialysisDate: strDialysisDate,
			strCardId: strCardId,
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "取消下机成功！") {
				updateDalysisState("2");
			}
			myToast(result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			//console.log(XMLHttpRequest);
			alert("CancelDownMachine:Ex");
		}
	});
}