var QueryClassesList = function() {
	var urlStr1 = serverAddress + "QueryClassesList";
	mui.ajax({
		type: "get",
		url: urlStr1,
		dataType: 'xml',
		data: {			
		},
		success: function(data) {
			var Object = getXmlDoc(data).getElementsByTagName("Object");
			var objectCount = Object.length;
			document.getElementById("classSelect").style.height = "calc(43px * " + (objectCount + 1)  + " + 7px + 7px)";
			for(var i = 0;i < objectCount;i++){
				$("#classList").append("<li class='mui-table-view-cell'>" + getNodeValue(Object[i].getElementsByTagName("Name")[0]) + "</li>");
				$("#classChange").append("<option>" + getNodeValue(Object[i].getElementsByTagName("Name")[0]) + "</option>");
			}
		},
		error: function(XMLHttpRequest, textStatus, errorThrown) {
			alert("QueryClassesList:Ex");
		}
	});
}
mui("#classSelect").on("tap", "li", function() {
	document.getElementById("class").innerText = this.innerText;
		getPatientWithClassArea(this.innerText, document.getElementById("area").innerText);
});

