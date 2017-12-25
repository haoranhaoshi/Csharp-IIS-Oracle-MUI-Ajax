var recipeNoArray = new Array();
var recipeSeqArray = new Array();
var QueryExecuteOrder = function(cardid, testDate) {
	var urlStr1 = serverAddress + "QueryExecuteOrder";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardid: cardid,
			testDate: testDate
		},
		success: function(data) {
			var OrderModel = getXmlDoc(data).getElementsByTagName("OrderModel");
			$("#executeContent").empty();
			var orderArray = new Array();
			var confirmTimeArray = new Array();
			var confirmNurseArray = new Array();
			var orderStateArray = new Array();
			var orderNumArray = new Array();
			var orderDocArray = new Array();
			var orderDocTimeArray = new Array();
			var frequenArray = new Array();
			var useWayArray = new Array();
			var k = 0;
			var combNoSave = "empty";
			for(var i = 0; i < OrderModel.length; i++) {
				var confirmTime = getNodeValue(OrderModel[i].getElementsByTagName("CONFIRM_DATE")[0]);
				if(confirmTime == "0001-01-01T00:00:00") {
					confirmTime = "";
				} else {
					var time = confirmTime.split("T")[1].split(":");
					confirmTime = time[0] + ":" + time[1];
				}	
				var frequen = getNodeValue(OrderModel[i].getElementsByTagName("FREQUENCY_NAME")[0]);
				var useWay = getNodeValue(OrderModel[i].getElementsByTagName("USAGE_NAME")[0]);
				var orderDoc = getNodeValue(OrderModel[i].getElementsByTagName("DOCT_CODE")[0]);
				var orderDocTime = getNodeValue(OrderModel[i].getElementsByTagName("OPER_DATE")[0]).replace("T", " ");
				var orderState = getNodeValue(OrderModel[i].getElementsByTagName("STATUS")[0]);
				var confirmNurse = getNodeValue(OrderModel[i].getElementsByTagName("CONFIRM_NAME")[0]);
				var orderItem = "<font class='orderItem'>名称：<font style='color:#333399'> <input type='text' style='width:calc(100% - 120px); border:0px #dddddd solid;height:40px;margin:0px;'    readonly='readonly' value='" + getNodeValue(OrderModel[i].getElementsByTagName("ITEM_NAME")[0]) + "'/>" 
				+ "</font> <br>规格：" + getNodeValue(OrderModel[i].getElementsByTagName("SPECS")[0]) 
				+ "， 每次量：" + getNodeValue(OrderModel[i].getElementsByTagName("ONCE_DOSE")[0]) 
				+ "，  数量：" + getNodeValue(OrderModel[i].getElementsByTagName("QTY")[0]) 
				+ "， 类型：" + getNodeValue(OrderModel[i].getElementsByTagName("CLASS_CODE")[0]) 
				+ "， <br>备注：<input type='text' style='width:calc(100% - 120px);border:1px #dddddd solid;height:40px;margin:0px;' readonly='readonly' value='" + getNodeValue(OrderModel[i].getElementsByTagName("MEMO")[0]) 
				+ "'/><br></font>";
				recipeNoArray[i] = getNodeValue(OrderModel[i].getElementsByTagName("RECIPE_NO")[0]);
				recipeSeqArray[i] = getNodeValue(OrderModel[i].getElementsByTagName("RECIPE_SEQ")[0]);
				var COMB_NO = getNodeValue(OrderModel[i].getElementsByTagName("COMB_NO")[0]);
				if((COMB_NO == "") || (COMB_NO.split("|")[0] != combNoSave)) {
					orderArray[k] = orderItem;
					confirmTimeArray[k] = confirmTime;
					confirmNurseArray[k] = confirmNurse;
					orderStateArray[k] = orderState;
					frequenArray[k] = frequen;
					useWayArray[k] = useWay;
					orderDocArray[k] = orderDoc;
					orderDocTimeArray[k] = orderDocTime;
					orderNumArray[k] = 1;
					combNoSave = COMB_NO.split("|")[0];
					k++;
				} else {
					orderNumArray[k - 1]++;
					orderArray[k - 1] += orderItem;					
				}
			}
			var allHeight = 0;
			for(var j = 0; j < orderArray.length; j++) {
				var orderText = "医嘱<br><div class='leftRectangle'></div><div class='rightRectangle'>" + orderArray[j] 
				+ "频次：" + frequenArray[j] 
				+ "，用法：" + useWayArray[j] + "，<br>开立医生：" 
				+ orderDocArray[j] + "<br>开立时间：" + orderDocTimeArray[j] + "</div>";
				for(var i = 0;i < orderNumArray[j]*3 + 2 ;i++){
					orderText += "<br>";
				}
				orderText += "<span style='float:left;'>确认护士<font style='padding-left: 6em;font-size:22px'></font>" + confirmNurseArray[j] + "<br>";
				if(orderStateArray[j] == "1") {
					orderText += "状态<font style='padding-left: 8em;font-size:22px'></font><font style='color: red;'>未执行</font></span>";					
				}else{
					orderText += "状态<font style='padding-left: 8em;font-size:22px'></font><font style='color: red;'>已执行</font></span>";
				}
				var order = "<div class='executeOrder'><div class='rectangle'></div><div class='time'><p>" + confirmTimeArray[j] + "</p></div><div class='orderContent'><div class='orderText'>" + orderText + "</div>";
				if(orderStateArray[j] == "1") {
					order += "<button class='mui-btn mui-btn-primary executeButton'>执行</button>";				
				}				
				order += "</div><div class='circleOrder'></div><div class='lineOrder'></div></div>";
				$("#executeContent").append(order);	
				document.getElementsByClassName("executeOrder")[j].style.height = (418 + (orderNumArray[j] - 1)*3*40) + "px";
				allHeight += (23 + parseInt(document.getElementsByClassName("executeOrder")[j].style.height));				
				document.getElementsByClassName("orderContent")[j].style.height = (370 + (orderNumArray[j] - 1)*3*40) + "px";
				if(document.getElementsByClassName("executeButton")[j] != null){
					document.getElementsByClassName("executeButton")[j].style.top = (326 + (orderNumArray[j] - 1)*3*40) + "px";
				}	
				document.getElementsByClassName("leftRectangle")[j].style.height = (3*40*orderNumArray[j]) + "px";
				if(orderNumArray[j]=="1")
				{
				   document.getElementsByClassName("leftRectangle")[j].style.visibility = "hidden";
				}
				else
				{
					document.getElementsByClassName("leftRectangle")[j].style.visibility = "visible";
				}			
				document.getElementsByClassName("circleOrder")[j].style.marginTop = (398 + (orderNumArray[j] - 1)*3*40) + "px";
				document.getElementsByClassName("lineOrder")[j].style.marginTop = (408 + (orderNumArray[j] - 1)*3*40) + "px";
			}
			if(OrderModel.length > 0) {
				//$(document.getElementById("executeContent").getElementsByClassName("time")[0]).append("<div id='batchExecute'><a href='#executePopover'><span class='mui-icon mui-icon-compose'></span><span>批量执行</span></a></div>");
			}

			document.getElementById("executeContent").style.height = allHeight + "px";
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryUnExecuteOrder:Ex");
		}
	});
}
var executeCurrent;
var itemNum = 0;
var ExecuteOrder = function(strCardId, strRecipeNo, strRecipeSeq, strConfimCode, strConfimDept, testDate,successNum) {
	var urlStr1 = serverAddress + "ExecuteOrder";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			strCardId: strCardId,
			strRecipeNo: strRecipeNo,
			strRecipeSeq: strRecipeSeq,
			strConfimCode: strConfimCode,
			strConfimDept: strConfimDept
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			if(result == "成功执行！") {
				/*var date = new Date();
				executeCurrent.parentNode.parentNode.getElementsByTagName("p")[0].innerText = date.getHours() + ":" + date.getMinutes();
				executeCurrent.parentNode.getElementsByTagName("span")[0].innerText = PERSONNAME;
				executeCurrent.parentNode.getElementsByTagName("span")[1].getElementsByTagName("font")[0].innerText = "已执行";*/				
				if(itemNum == successNum){
					QueryExecuteOrder(CARDID, testDate);
					mui.toast("成功执行");
				}					
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("ExecuteOrder:Ex");
		}
	});
}
//执行
mui("#executeContent").on("tap", ".executeButton", function() {
	executeCurrent = this;
	itemNum = this.parentNode.getElementsByClassName("orderItem").length;
	var order = document.getElementsByClassName("orderItem");
	var firstGet;
	for(var i = 0; i < order.length; i++) {
		if(order[i] == this.parentNode.getElementsByClassName("orderItem")[0]) {
			firstGet = i;
			break;
		}
	}
	for(var i = 0; i < this.parentNode.getElementsByClassName("orderItem").length; i++) {
		ExecuteOrder(CARDID, recipeNoArray[firstGet + i], recipeSeqArray[firstGet + i], PERSONID, DEPTID, strDialysisDate.replace("-", "").replace("-", ""),i + 1);
	}	
});