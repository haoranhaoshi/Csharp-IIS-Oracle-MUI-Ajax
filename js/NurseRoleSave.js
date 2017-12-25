var QueryNurseList = function() {
	var urlStr1 = serverAddress + "QueryNurseList";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {},
		success: function(data) {
			var PersonModel = getXmlDoc(data).getElementsByTagName("PersonModel");
			$("#punctureNurse").append("<option>" + "" + "</option>");
			$("#treatmentNurse").append("<option>" + "" + "</option>");
			for(var i = 0; i < PersonModel.length; i++) {
				var Name = PersonModel[i].getElementsByTagName("PERSONNAME")[0].firstChild.nodeValue;
				$("#punctureNurse").append("<option>" + Name + "</option>");
				$("#treatmentNurse").append("<option>" + Name + "</option>");
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryNurseList:Ex");
		}
	});
}
var QueryPuntureNurse = function(cardId, testDate) {
	var urlStr1 = serverAddress + "QueryPuntureNurse";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardId: cardId,
			testDate: testDate
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			setNurse("punctureNurse",result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryPuntureNurse:Ex");
		}
	});
}
var setNurse = function(nurse,result){
	for(var i = 0; i < document.getElementById(nurse).options.length; i++) {
				if(document.getElementById(nurse).options[i].text == result) {
					document.getElementById(nurse).options[i].selected = true;
				} else {
					document.getElementById(nurse).options[i].selected = false;
				}
			}
};
var QueryTreamentNurse = function(cardId, testDate) {
	var urlStr1 = serverAddress + "QueryTreamentNurse";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			cardId: cardId,
			testDate: testDate
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			setNurse("treatmentNurse",result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryTreamentNurse:Ex");
		}
	});
}
var saveNurseRole = function() {
	var PUNCTURENURSE = document.getElementById("punctureNurse").options[document.getElementById("punctureNurse").selectedIndex].text;
	var TREAMENTNURSE = document.getElementById("treatmentNurse").options[document.getElementById("treatmentNurse").selectedIndex].text;
	var now = new Date();
	var OPERATETIME = now.getFullYear() + "-" + ((now.getMonth() + 1) < 10 ? "0" : "") + (now.getMonth() + 1) + "-" + (now.getDate() < 10 ? "0" : "") + now.getDate();

	SavePuntureAndTreamentNurse(CARDID, strDialysisDate, PUNCTURENURSE, TREAMENTNURSE, OPERATETIME);
};
var SavePuntureAndTreamentNurse = function(CARDID, TESTDATE, PUNCTURENURSE, TREAMENTNURSE, OPERATETIME) {
	var urlStr1 = serverAddress + "SavePuntureAndTreamentNurse";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {
			CARDID: CARDID,
			TESTDATE: TESTDATE,
			PUNCTURENURSE: PUNCTURENURSE,
			TREAMENTNURSE: TREAMENTNURSE,
			OPERATETIME: OPERATETIME
		},
		success: function(data) {
			var result = data.childNodes[0].textContent;
			mui.toast("穿刺与治疗护士" + result);
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("SavePuntureAndTreamentNurse:Ex");
		}
	});
}