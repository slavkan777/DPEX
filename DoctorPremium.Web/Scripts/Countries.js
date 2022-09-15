function CountryAndCityInit() {

	$("#CountryId").chosen({
		search_contains: true
	});

	$("#CityId").chosen({
		search_contains: true
	});

	$('#CountryId').on('change', function (evt, params) {
		var defOption = $('#CityId').find('[value=""]');
		$('#CityId').find('option:not(:first)').remove();
		$('#CityId').trigger('chosen:updated');
		if (params.selected != '') {
			SendAjax('/UserInfo/GetCityList/', { countryid: params.selected }, filiingCityList);
		}
	});

};

function filiingCityList(list) {
	$.each(list, function (i, item) {
		$("#CityId").append($('<option>', {
			value: item.Value,
			text: item.Text
		}));
	});
	$('#CityId').trigger('chosen:updated');
};