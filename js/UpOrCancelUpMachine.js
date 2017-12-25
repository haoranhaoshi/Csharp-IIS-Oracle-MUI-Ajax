mui("#right").on("tap","#upButton",function(){
	if(CARDID == ""){
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
			if(buttonValue == "上机") {
				if(CARDID == ""){
					mui.toast("请选择患者");
				}else{
					UpMachine(strDialysisDate, CARDID, loginName, PERSONNAME);
				}				
			}
			if(buttonValue == "取消上机") {
				if(CARDID == ""){
					mui.toast("请选择患者");
				}else{
					CancelUpMachine(strDialysisDate, CARDID);
				}				
			}
			if(buttonValue == "取消下机") {
				if(CARDID == ""){
					mui.toast("请选择患者");
				}else{
					CancelDownMachine(strDialysisDate, CARDID);
				}				
			}

		} else {
			mui.toast('已取消');
		}
	}, 'div')
});
var jumpTab = function() {
	var tab = document.getElementById("tab1");
	tab.setAttribute("class", "mui-control-item");
	tab = document.getElementById("tab2");
	tab.setAttribute("class", "mui-control-item mui-active");
	tab = document.getElementById("tab3");
	tab.setAttribute("class", "mui-control-item");
	tab = document.getElementById("hd");
	tab.setAttribute("class", "mui-control-content");
	tab = document.getElementById("process");
	tab.setAttribute("class", "mui-control-content mui-active");
	tab = document.getElementById("advice");
	tab.setAttribute("class", "mui-control-content");
}
var UpMachine = function(strDialysisDate, strCardId, strUpMachineNurseId, strUpMachineNurseName) {
	var urlStr1 = serverAddress + "UpMachine";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strDialysisDate: strDialysisDate,
			strCardId: strCardId,
			strUpMachineNurseId: strUpMachineNurseId,
			strUpMachineNurseName: strUpMachineNurseName
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "上机成功！") {
				updateDalysisState("2");
			}
			myToast(result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("UpMachine:Ex");
		}
	});
}
var CancelUpMachine = function(strDialysisDate, strCardId) {
	var urlStr1 = serverAddress + "CancelUpMachine";
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
			if(result == "取消上机成功！") {
				updateDalysisState("1");
			}
			myToast(result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("CancelUpMachine:Ex");
		}
	});
}
var myToast = function(content){
	var time = 1;
	for(var i = 0;i <  time;i++){
		mui.toast(content);
	}	
}
