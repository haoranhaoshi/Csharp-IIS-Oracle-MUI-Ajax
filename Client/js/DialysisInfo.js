var DoctorDetail;
var SPRESSURE;
var DPRESSURE;
var PULSE;
var BREATHING;
var QueryDialysisInfo = function(testDate, cardId) {
	var urlStr1 = serverAddress + "QueryDialysisInfo";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			testDate: testDate,
			cardId: cardId,
		},
		success: function(data) {
			DoctorDetail = getXmlDoc(data).getElementsByTagName("DoctorDetail")[0];
			var lastTestDate = "无";
			if(getInfoItem("LASTTESTDATE") != null) {
				lastTestDate = insert_flg(insert_flg(getInfoItem("LASTTESTDATE"), "/", 4), "/", 7);
			}
			var danChao = getInfoItem("DANCHAOHOUR") + "小时" + getInfoItem("DANCHAOML") + "ml";
			if((danChao) == "小时ml") {
				danChao = "无";
			}
			var zuiJia = getInfoItem("SECOND") + getInfoItem("SECONDUNIT");
			if(zuiJia == "") {
				zuiJia = "无";
			}
			var total = getInfoItem("TOTAL") + getInfoItem("TOTALUNIT");
			if(total == "") {
				total = "无";
			}
			if(DoctorDetail != null) {
				SPRESSURE = getInfoItem("SPRESSURE");
				DPRESSURE = getInfoItem("DPRESSURE");
				PULSE = getInfoItem("PULSE");
				BREATHING = getInfoItem("BREATHING");
				document.getElementById("processAlert").innerText = "超滤总量：" + getInfoItem("FILTRATIONQUANTITY") + "ml 治疗时长：" + getInfoItem("TREATMENTDURATION") + "h 血流量：" + getInfoItem("BLOODFOLW") + "ml/min";
				document.getElementById("afterAlert").innerText = "超滤总量：" + getInfoItem("FILTRATIONQUANTITY") + "ml 治疗时长：" + getInfoItem("TREATMENTDURATION") + "h";				
				mui("#dialysisBeforeInfo div")[0].innerHTML = "血压：" + getInfoItem("SPRESSURE") + "/" + getInfoItem("DPRESSURE") + "mmHg<br>呼吸：" + getInfoItem("BREATHING") + "次/分<br>透前体重：<font id='beforeWeight'>" + getInfoItem("FRONTWEIGHT") + "</font>kg<br>前次透后体重：" + getInfoItem("FRONTWEIGHT")  + "kg<br>血流量：" + getInfoItem("BLOODFOLW") + "ml/min<br>目标（干）体重：" + getInfoItem("TARGETWEIGHT") + "kg<br>透析机类型：" + getInfoItem("MACHINETYPE") + "<br>血管通路：" + getInfoItem("VASCULARACCESS") + "<br>透析液类型：" + getInfoItem("DIALYSATETYPE") + "<br>透析液钠成分：" + getInfoItem("NA") + "mmol/L<br>透析液钙成分：" + getInfoItem("CA") + "mmol/L<br>碳酸氢根：" + getInfoItem("HCO2") + "mmol/L<br>单超：" + danChao;
				mui("#dialysisBeforeInfo div")[1].innerHTML = "心率：" + getInfoItem("PULSE") + "次/分<br>体温：" + getInfoItem("TEMPERATURE") + "℃<br>增加体重：" + getInfoItem("INCREASEWEIGHT") + "kg<br>前次透析日期：<br>" + lastTestDate +  "<br><font color='red'>治疗模式：" + getInfoItem("DIALYSISMODE") + "</font><br>治疗时长：<font id='TREATMENTDURATION'>" + getInfoItem("TREATMENTDURATION") + "</font>h<br>超滤总量：" + getInfoItem("FILTRATIONQUANTITY") + "ml<br>置换量：" + getInfoItem("CHANGEQUANTITY") + "ml<br>透析（滤）器：" + getInfoItem("DIALYZER") + "<br>流量：" + getInfoItem("FOLW") + "ml/min<br>抗凝剂：" + getInfoItem("ANTICOAGULANT") + "<br>总量：" + total + "<br>首剂：" + getInfoItem("FRIST") + getInfoItem("FRISTUNIT") + "<br>追加：" + zuiJia;
				//mui("#dialysisAfterInfo div")[0].innerHTML = "透后体重：" + getInfoItem("AFTERWEIGHT") + "kg";
				//mui("#dialysisAfterInfo div")[1].innerHTML = "实际超滤总量：" + getInfoItem("FACTSFILTRATIONQUANTITY") + "ml";
				if(getInfoItem("FRONTWEIGHT") == "卧") {
					var input = document.getElementsByClassName("afterItem")[0].getElementsByTagName("INPUT");					
					input[1].type = "text";
					input[2].type = "text";
					input[1].value = "-";
					input[2].value = "卧";
					document.getElementById("lessWeight").innerText = "-";
					input[1].readonly = "true";
					input[2].readonly = "true";
				}
			} else {
				clearInfo();
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryDialysisInfo:Ex");
		}
	});
}
var getInfoItem = function(item) {
	if(DoctorDetail != null) {
		return getNodeValue(DoctorDetail.getElementsByTagName(item)[0]);
	}
}
var insert_flg = function(str, flg, sn) {
	var newstr = "";
	var tmp = str.substring(0, sn);
	newstr = tmp + flg + str.substring(sn);
	return newstr;
}