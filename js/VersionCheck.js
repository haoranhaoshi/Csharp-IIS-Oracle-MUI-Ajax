var newVersionNumber;
var QueryVersionNumber = function() {
	var urlStr1 = serverAddress + "QueryVersionNumber";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {},
		success: function(data) {
			plus.runtime.getProperty(plus.runtime.appid, function(wgtinfo) {
				document.getElementById("versionNumber").innerText = wgtinfo.version;
				var result = data.childNodes[0].textContent;
				if(compareVersionNumber(wgtinfo.version, result)) {
					document.getElementById("alertRedCircle").style.visibility = "visible";
				}
			});
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryVersionNumber:Ex");
		}
	});
}
var compareVersionNumber = function(localVM, webVM) {
	var LocalVM = localVM.split(".");
	var WebVM = webVM.split(".");
	var canUpdate = false;
	for(var i = 0; i < LocalVM.length; i++) {
		if(parseInt(WebVM[i]) > parseInt(LocalVM[i])) {
			canUpdate = true;
			break;
		}
	}
	return canUpdate;
}
document.getElementById("versionDiv").addEventListener('tap', function() {
	if(document.getElementById("alertRedCircle").style.visibility == "visible") {
		downWgt();
	}
})
// 下载wgt文件
var wgtUrl = "http://" + localStorage.ip + ":" + localStorage.port + "/new.wgt";

function downWgt() {
	plus.nativeUI.showWaiting("下载更新文件...");
	plus.downloader.createDownload(wgtUrl, {
		filename: "_doc/update/"
	}, function(d, status) {
		if(status == 200) {
			console.log("下载更新文件成功：" + d.filename);
			installWgt(d.filename); // 安装wgt包
		} else {
			console.log("下载更新文件失败！");
			plus.nativeUI.alert("下载更新文件失败！");
		}
		plus.nativeUI.closeWaiting();
	}).start();
}
// 更新应用资源
function installWgt(path) {
	plus.nativeUI.showWaiting("安装更新文件...");
	plus.runtime.install(path, {}, function() {
		plus.nativeUI.closeWaiting();
		console.log("安装更新文件成功！");
		plus.nativeUI.alert("应用资源更新完成！", function() {
			localStorage.versionNumber = newVersionNumber;
			plus.runtime.restart();
		});
	}, function(e) {
		plus.nativeUI.closeWaiting();
		console.log("安装更新文件失败[" + e.code + "]：" + e.message);
		plus.nativeUI.alert("安装更新文件失败[" + e.code + "]：" + e.message);
	});
}