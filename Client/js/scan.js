var serverAddress, scan;
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
				dealError();
			} else {
				var xmldoc = loadXML(result);
				var PersonModel = xmldoc.getElementsByTagName("PersonModel");
				var PERSONNAME = PersonModel[0].getElementsByTagName("PERSONNAME")[0].firstChild.nodeValue;
				var PERSONID = PersonModel[0].getElementsByTagName("PERSONID")[0].firstChild.nodeValue;
				var HOSPITALID = PersonModel[0].getElementsByTagName("HOSPITALID")[0].firstChild.nodeValue;
				var DEPTID = PersonModel[0].getElementsByTagName("DEPTID")[0].firstChild.nodeValue;
				scan.close();
				mui.fire(plus.webview.getWebviewById("login"), "pageflowrefresh");
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
mui.init();
mui.plusReady(function() {
	startRecognize();
});

function startRecognize() {
	try {
		var filter;
		//自定义的扫描控件样式  
		var styles = {
			frameColor: "#5ebafe",
			scanbarColor: "#5ebafe",
			background: ""
		}
		//扫描控件构造  
		scan = new plus.barcode.Barcode('bcid', filter, styles);
		scan.onmarked = onmarked;
		scan.onerror = onerror;
		scan.start();
		//打开关闭闪光灯处理  
		var flag = false;
		document.getElementById("turnTheLight").addEventListener('tap', function() {
			if(flag == false) {
				scan.setFlash(true);
				flag = true;
			} else {
				scan.setFlash(false);
				flag = false;
			}
		});
	} catch(e) {
		alert("出现错误啦:\n" + e);
	}
};

function onerror(e) {
	alert(e);
};

function onmarked(type, result) {
	var text = '';
	switch(type) {
		case plus.barcode.QR:
			text = 'QR: ';
			break;
		case plus.barcode.EAN13:
			text = 'EAN13: ';
			break;
		case plus.barcode.EAN8:
			text = 'EAN8: ';
			break;
	}
	if(result.split("|") == null || result.split("|").length != 2) {
		dealError();
	} else {
		if(localStorage.scanTarget == "Login") {
			var loginInfo = result.split("|");
			LoginMain(loginInfo[0], loginInfo[1]);
		}
		if(localStorage.scanTarget == "QueryPatient") {
			var loginInfo = result.split("|");
			QueryPatient(loginInfo[0], loginInfo[1]);
		}
	}
};

var dealError = function() {
	scan.cancel()
	alert("无效二维码！");
	scan.start();
};
var QueryPatient = function(cardId, testDate) {
	serverAddress = "http://" + localStorage.ip + ":" + localStorage.port + "/HdisService.asmx/";
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
			if(NurseDetail[0].childNodes.length != 0) {
				scan.close();
				localStorage.scanPatientID = cardId;
				localStorage.scanPatientDate = testDate;
				mui.fire(plus.webview.getWebviewById("index"), "pageflowrefresh");
				plus.webview.show("index");
			}
			else {
				dealError();
			}

		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryBaseInfo:Ex");
		}
	});
}

// 从相册中选择二维码图片   
function scanPicture() {
	plus.gallery.pick(function(path) {
		plus.barcode.scan(path, onmarked, function(error) {
			plus.nativeUI.alert("无法识别此图片");
		});
	}, function(err) {
		plus.nativeUI.alert("Failed: " + err.message);
	});
}

document.getElementById("backIcon").addEventListener('tap', function() {
	cancelScan();
});
document.getElementById("backText").addEventListener('tap', function() {
	cancelScan();
});
var cancelScan = function() {
	var btn = ["确定",
		"取消"
	];
	mui.confirm('确认关闭当前窗口？', '血透', btn, function(e) {
		if(e.index == 0) {
			scan.close();
			var main;
			if(localStorage.scanTarget == "Login") {
				main = plus.webview.getWebviewById("login");
			} else {
				main = plus.webview.getWebviewById("index");
			}
			mui.fire(main, "pageflowrefresh");
			localStorage.scanTarget = "Login";
			backFather();
		}
	});
}
var backFather = function() {
	if(localStorage.scanTarget == "Login") {
		mui.openWindow({
			url: 'login.html',
			id: 'login'
		});
	} else {
		mui.openWindow({
			url: 'index.html',
			id: 'index'
		});
	}
}