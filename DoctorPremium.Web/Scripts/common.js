function renderDate(pDate, lang) {
	if (pDate != null && pDate != '') {
		var javascriptDate = new Date(parseInt(pDate.slice(6, -2)));
		//javascriptDate = javascriptDate.getDate() + "." + (javascriptDate.getMonth() + 1) + "." + javascriptDate.getFullYear();
		if (lang == 'ru') {
			return ("0" + javascriptDate.getDate()).slice(-2) + "." + ("0" + (javascriptDate.getMonth() + 1)).slice(-2) + "." + javascriptDate.getFullYear();
		}
		return ("0" + (javascriptDate.getMonth() + 1)).slice(-2) + "/" + ("0" + javascriptDate.getDate()).slice(-2) + "/" + javascriptDate.getFullYear();//javascriptDate.format('dd.MM.yyyy HH:mm:ss');
	}
	return '';
};

function renderTime(pDate) {
	if (pDate != null && pDate != '') {
		return pDate.Hours + ':' + pDate.Minutes;
	}
	return '';
};

function renderFullDate(pDate, lang) {
    if (pDate != null && pDate != '') {
        var hDate = new Date(parseInt(pDate.slice(6, -2)));
        return renderDate(pDate, lang) + ' ' + hDate.getHours() + ':' + hDate.getMinutes();
    }
    return '';
};

function calculateAge(birthday) {
	var ageDifMs = Date.now() - birthday.getTime();
	var ageDate = new Date(ageDifMs);
	return Math.abs(ageDate.getUTCFullYear() - 1970);
};

function SendAjax(url, data, success) {
	$.ajax({
		type: "Post",
		url: url,
		data: data,
		success: function (data) {
			if (data.success) {
				success(data.value);
			}
		}
	});
};

//$(function () {
//	$.validator.methods.date = function (value, element) {
//		if ($.browser.webkit) {
//			var d = new Date();
//			return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
//		}
//		else {
//			return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
//		}
//	};
//});

//$.validator.methods.range = function (value, element, param) {
//	var globalizedValue = value.replace(",", ".");
//	return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
//}

//$.validator.methods.number = function (value, element) {
//	return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
//}