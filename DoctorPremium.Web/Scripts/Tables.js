function renderPatienVisitTable(table, patientid, lang) {
	var t = table.DataTable({
	    paging: true,
	    searching: false,
	    processing: false,
	    serverSide: true,
	    ordering: false,
	    //order: [[1, "asc"]],
	    language: {
	    	url: lang == "ru" ? "/Scripts/dataTables.Russian.js" : ""
	    },
	    info: true,
	    columns: [
	     { data: 'PatientVisitId', orderable: false, width: "5%", className: "text-center", render: function (data, type, row) { return "<a href='/PatientVisit/Edit/" + data + "' class='btn btn-primary btn-line'><i class='icon-edit'></i></a>"; } },
	    {
	        data: 'VisitDate',
	        width: "9%",
	        orderable: false,
	        render: function(pDate) {
	            return renderDate(pDate, lang);
	        }
	    },
	    {
	        data: 'VisitStartTime',
	        orderable: false,
	        width: "6%",
	        render: function(pDate) {
	            return renderTime(pDate);
	        }
	    },
	    { data: 'PurposeOfVisit', orderable: false, width: "60%" },
	    { data: 'CostOfServices', orderable: false, width: "8%" },
	    { data: 'Paid', orderable: false, width: "8%" },
	   
	  ],
		ajax: {
			url: '/PatientVisit/ListPatientVisits',
			type: 'POST',
			data: function ( d ) {
				d.PatientId = patientid;
			},
			error: handleAjaxError
		}
	});
};

function handleAjaxError(xhr, textStatus, error) {
	if (textStatus === 'timeout') {
		alert('The server took too long to send the data.');
	}
	else {
		alert('An error occurred on the server. Please try again in a minute.');
	}
};