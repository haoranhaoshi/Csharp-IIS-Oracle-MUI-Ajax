mui.init({
	gestureConfig: {
		//swipeup: true,
		swipedown: true,
		//dragstart: true,
		//drag: true,
		//dragend: true
	}
});

document.getElementById("wrapperPatientList").addEventListener("swipedown", function() {
	$("#listRefresh").css("display", "");
	setTimeout(function() {
		QueryDialysisPatientListByDate(strDialysisDate, strSigninState);
		$("#listRefresh").css("display", "none");
		mui.toast("已刷新");
	}, 1500);
});

window.addEventListener("pageflowrefresh", function(e) {
	location.reload();
});

//侧滑容器父节点
var offCanvasWrapper = mui('#offCanvasWrapper');
//主界面容器
var offCanvasInner = offCanvasWrapper[0].querySelector('.mui-inner-wrap');
//菜单容器
var offCanvasSide = document.getElementById("offCanvasSide");
/*if(!mui.os.android) {
	document.getElementById("move-togger").classList.remove('mui-hidden');
	var spans = document.querySelectorAll('.android-only');
	for(var i = 0, len = spans.length; i < len; i++) {
		spans[i].style.display = "none";
	}
}*/

//移动效果是否为整体移动
var moveTogether = false;
//侧滑容器的class列表，增加.mui-slide-in即可实现菜单移动、主界面不动的效果；
var classList = offCanvasWrapper[0].classList;
//变换侧滑动画移动效果；
mui('.mui-input-group').on('change', 'input', function() {
		if(this.checked) {
			offCanvasSide.classList.remove('mui-transitioning');
			offCanvasSide.setAttribute('style', '');
			classList.remove('mui-slide-in');
			classList.remove('mui-scalable');
			switch(this.value) {
				case 'main-move':
					if(moveTogether) {
						//仅主内容滑动时，侧滑菜单在off-canvas-wrap内，和主界面并列
						offCanvasWrapper[0].insertBefore(offCanvasSide, offCanvasWrapper[0].firstElementChild);
					}
					break;

			}
			offCanvasWrapper.offCanvas().refresh();
		}
	}

);
//主界面和侧滑菜单界面均支持区域滚动；
mui('#offCanvasSideScroll').scroll();
mui('#offCanvasContentScroll').scroll();
//实现ios平台原生侧滑关闭页面；
if(mui.os.plus && mui.os.ios) {
	mui.plusReady(function() {
		plus.screen.lockOrientation("landscape-primary");
		//5+ iOS暂时无法屏蔽popGesture时传递touch事件，故该demo直接屏蔽popGesture功能
		plus.webview.currentWebview().setStyle({
			'popGesture': 'none'
		});

	});
}
var searchDiv = document.getElementById("searchDiv");

function mySearch(o) { //搜索区获得焦点
	searchDiv.style.left = "calc(8px + 12px + 20px + 50px)";
	searchDiv.style.width = "calc(100% - 12px * 2 - 8px * 2 - 20px - 50px)";
	searchDiv.parentNode.style.width = "calc(100% - 16px - 50px)";
	searchDiv.parentNode.getElementsByTagName("span")[0].style.left = "calc(8px + 12px + 50px)";
	document.getElementById("returnIcon").style.visibility = "visible";
	document.getElementById("scanIcon").style.visibility = "hidden";
}

function away(o) {
	document.getElementById("scanIcon").style.visibility = "visible";
}
//患者信息可滚动
(function($) {
	$('#information .mui-scroll-wrapper').scroll({
		indicators: false //是否显示滚动条
	});
	$('#process .mui-scroll-wrapper').scroll({
		indicators: false //是否显示滚动条
	});
	$('#advice .mui-scroll-wrapper').scroll({
		indicators: false //是否显示滚动条
	});
	$('#after .mui-scroll-wrapper').scroll({
		indicators: false //是否显示滚动条
	});
	$('#classSelect .mui-scroll-wrapper').scroll({
		indicators: false //是否显示滚动条
	});
	$('#areaSelect .mui-scroll-wrapper').scroll({
		indicators: false //是否显示滚动条
	});
	$('#list .mui-scroll-wrapper').scroll({
		indicators: false //是否显示滚动条
	});
})(mui);
//扫描				
document.getElementById('scanIcon').addEventListener('tap', function() {
		plus.screen.unlockOrientation();
		plus.screen.lockOrientation("portrait-primary");
		localStorage.scanTarget = "QueryPatient";
		plus.webview.hide("index");
		plus.webview.open("scan.html");
	}

);
//退出登录
document.getElementById('exit').addEventListener('tap', function() {
	offCanvasWrapper.offCanvas('close');
	mui.fire(plus.webview.getWebviewById("login"), "pageflowrefresh"); //出发去父页的pageflowrefresh方法
	mui.openWindow({
		url: 'login.html',
		id: 'login',
	});
});
//批量执行-取消
document.getElementById('executeCancel').addEventListener('tap', function() {

		mui.toast('已取消');
		document.getElementById('executePopover').style.display = "none";
	}

);
//批量执行-执行
document.getElementById('execute').addEventListener('tap', function() {
	mui.toast('已执行');
	document.getElementById('executePopover').style.display = "none";
});
//批量执行-全选
document.getElementById("allChoose").addEventListener('tap', function() {
	var checkBoxs = document.getElementById("executePopover").getElementsByTagName("input");
	for(var i = 1; i < checkBoxs.length; i++) {
		checkBoxs[i].checked = !(this.checked);
	}
});

//编辑记录
function getElementTop(element) {　　　　
	var actualTop = element.offsetTop;　　　　
	var current = element.offsetParent;
	while(current !== null) {　　　　　　
		actualTop += current.offsetTop;　　　　　　
		current = current.offsetParent;　　　　
	}　　　
	return actualTop;　　
}
//点击文本框失去焦点屏幕失去
/*document.getElementById("body").addEventListener('tap', function() {
	if(document.activeElement.tagName == "INPUT" & document.activeElement.id != "search") {
		document.activeElement.blur();
	}
});*/
document.getElementById("information").addEventListener('tap', function() {
	if(document.activeElement.tagName == "INPUT") {
		document.activeElement.blur();
	}
	if(document.activeElement.tagName == "TEXTAREA") {
		document.activeElement.blur();
	}
});
document.getElementById("list").addEventListener('tap', function() {
	if(document.activeElement.tagName == "INPUT") {
		document.activeElement.blur();
	}
});
//编辑项滚动
function textfocus(o) {
	var top = getElementTop(o);
	var scrollheight = top - 58 - 58 - 40;
	mui('#scrollProcess').scroll().scrollTo(0, -scrollheight, 1000); 
};
$("#after input").focus(function(){
	var top = getElementTop(this);
	var scrollheight = top - 58 - 58 - 40;
	mui('#after .mui-scroll-wrapper').scroll().scrollTo(0, -scrollheight, 1000); 
});
//保存
/*var saveButton = document.getElementsByClassName("saveButton");
for(var i = 0; i < saveButton.length; i++) {
	saveButton[i].addEventListener("tap", function() {
		var input = this.parentNode.getElementsByTagName("div")[0].getElementsByTagName("input");
		for(var i = 0; i < input.length; i++) {
			input[i].setAttribute('class', '');;
		}
	})
}*/
//执行
var executeButton = document.getElementsByClassName("executeButton");
for(var i = 0; i < executeButton.length; i++) {
	executeButton[i].addEventListener("tap", function() {
		var nurseSign = this.parentNode.getElementsByTagName("div")[0].getElementsByTagName("span")[1].getElementsByTagName("font")[0];
		if(nurseSign.innerText == "未签字") {
			nurseSign.innerText = "已签字";
			nurseSign.style.color = "#333333";
			executeButton.enabled = false;
		}
	})
}

/*mui("#processContent").on("input", "input", function(e) {
	console.log("ddf");
	e.preventDefault();
});*/