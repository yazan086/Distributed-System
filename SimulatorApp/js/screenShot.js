﻿/*
@author Yazan Alsahhar.
A code which is responsible for taking  screenshot of the simulated diagram. 
*/
function screenShot() {
	html2canvas(document.getElementById("SDCanvas"), {
		onrendered: function (canvas) {
			var link = document.createElement("a");
			link.href = SDCanvas.toDataURL('image/jpg');
			link.download = 'screenshot.jpg';
			link.click();
		}
	});
}