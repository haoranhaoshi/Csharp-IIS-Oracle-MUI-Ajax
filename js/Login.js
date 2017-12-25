mui.init();
mui.plusReady(function() {
	plus.screen.unlockOrientation();
	plus.screen.lockOrientation("landscape-primary");
});
window.addEventListener("pageflowrefresh", function(e) {
	location.reload();
});
document.getElementById('myScan').addEventListener('tap', function() {
	plus.screen.unlockOrientation();
	plus.screen.lockOrientation("portrait-primary");
	localStorage.scanTarget = "Login";
	plus.webview.open("scan.html");
});
document.getElementById("settingIcon").addEventListener('tap', function() {
	document.getElementById("menban").style.visibility = "visible";
	document.getElementById("settingDiv").style.visibility = "visible";
	document.getElementById("portInput").value = localStorage.port;
	if(localStorage.ip != null) {
		var ipPart = localStorage.ip.split(".");
		for(var i = 0; i < ipPart.length; i++) {
			document.getElementById("ipDiv").getElementsByTagName("input")[i].value = ipPart[i];
		}
	}
});
document.getElementById('menban').addEventListener('tap', function() {
	changeSettingLeave();
});
var changeSettingLeave = function() {
	document.getElementById("menban").style.visibility = "hidden";
	document.getElementById("settingDiv").style.visibility = "hidden";
	document.getElementById("alertSetting").innerText = "";
}
document.getElementById('saveCancel').addEventListener('tap', function() {
	mui.toast('已取消');
	changeSettingLeave();
});
document.getElementById('saveExecute').addEventListener('tap', function() {
	var ip = "";
	for(var i = 0; i < 4; i++) {
		ip += document.getElementById("ipDiv").getElementsByTagName("input")[i].value;
		if(i != 3) {
			ip += ".";
		}
	}
	var port = document.getElementById("portInput").value;
	var alertSetting = "";
	if(/^([0-9]|[1-9]\d{1,3}|[1-5]\d{4}|6[0-5]{2}[0-3][0-5])$/.test(port)) {
		localStorage.port = port;
	} else {
		alertSetting = "端口";
	}
	if(/^(\d|[1-9]\d|1\d{2}|2[0-5][0-5])\.(\d|[1-9]\d|1\d{2}|2[0-5][0-5])\.(\d|[1-9]\d|1\d{2}|2[0-5][0-5])\.(\d|[1-9]\d|1\d{2}|2[0-5][0-5])$/.test(ip)) {
		localStorage.ip = ip;
	} else {
		if(alertSetting != "") {
			alertSetting += "、"
		}
		alertSetting += "IP"
	}
	if(alertSetting != "") {
		alertSetting += "未保存，请输入正确格式！"
	} else {
		changeSettingLeave();
		mui.toast("已保存！");
	}
	document.getElementById("alertSetting").innerText = alertSetting;
});
document.getElementById("myLogin").addEventListener('tap', function() {
	var loginName = document.getElementById("loginName").value;
	var password = document.getElementById("password").value;
	if(loginName == "" & password == "") {
		document.getElementById("alertLogin").style.visibility = "visible";
		document.getElementById("alertLogin").innerText = "用户名和密码不能为空";
	} else {
		if(loginName == "") {
			document.getElementById("alertLogin").style.visibility = "visible";
			document.getElementById("alertLogin").innerText = "用户名不能为空";
		} else {
			if(password == "") {
				document.getElementById("alertLogin").style.visibility = "visible";
				document.getElementById("alertLogin").innerText = "密码不能为空";
			} else {
				LoginMain(loginName, password);
			}
		}
	}
});
var serverAddress;
var LoginMain = function(loginName, password) {
	serverAddress = "http://" + localStorage.ip + ":" + localStorage.port + "/HdisService.asmx/";
	var urlStr1 = serverAddress + "LoginMain";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		timeout: 2000,
		data: {
			loginName: loginName,
			password: password
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "用户名错误" || result == "密码错误") {
				document.getElementById("alertLogin").style.visibility = "visible";
			} else {
				var xmldoc = loadXML(result);
				var PersonModel = xmldoc.getElementsByTagName("PersonModel");
				var PERSONNAME = PersonModel[0].getElementsByTagName("PERSONNAME")[0].firstChild.nodeValue;
				var PERSONID = PersonModel[0].getElementsByTagName("PERSONID")[0].firstChild.nodeValue;
				var HOSPITALID = PersonModel[0].getElementsByTagName("HOSPITALID")[0].firstChild.nodeValue;
				var DEPTID = PersonModel[0].getElementsByTagName("DEPTID")[0].firstChild.nodeValue;
				localStorage.scanTarget = "Login";
				mui.openWindow({
					url: 'index.html',
					id: 'index',
					extras: {
						PERSONNAME: PERSONNAME,
						loginName: loginName,
						PERSONID: PERSONID,
						HOSPITALID: HOSPITALID,
						DEPTID: DEPTID,
						serverAddress: serverAddress
					},
				});
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("请连接服务器！");
		}
	});
}