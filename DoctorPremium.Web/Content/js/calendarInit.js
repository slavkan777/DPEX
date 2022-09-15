function CalendarInit(lang) {
    "use strict";

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    var hdr = {};

    if ($(window).width() <= 767) {
        hdr = { left: 'title', center: '', right: 'prev,today,agendaDay,agendaWeek,month,next' };
    } else {
        hdr = { left: '', center: 'title', right: 'prev,today,agendaDay,agendaWeek,month,next' };
    }

    var initDrag = function (e) {
        // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
        // it doesn't need to have a start or end



        var eventObject = {
            title: $.trim(e.text()), // use the element's text as the event title
            description: $.trim(e.text()),
            className: $.trim(e.children('span').attr('class')) // use the element's children as the event class
        };
        // store the Event Object in the DOM element so we can get to it later
        e.data('eventObject', eventObject);

        // make the event draggable using jQuery UI
        e.draggable({
            zIndex: 999,
            revert: true, // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        });
    };

    var addEvent = function (title, description, priority) {
        title = title.length === 0 ? "Untitled Event" : title;
        description = description.length === 0 ? "Untitled Event" : description;
        priority = priority.length === 0 ? "label label-default" : priority;

        var html = $('<li class="external-event"><span class="' + priority + '">' + title + description + '</span></li>');

        jQuery('#external-events').append(html);
        initDrag(html);
    };

    /* initialize the external events
     -----------------------------------------------------------------*/

    $('#external-events li.external-event').each(function () {
        initDrag($(this));
    });

    $('#add-event').click(function () {
        var title = $('#title').val();
        var description = $('#description').val();
        var priority = $('input:radio[name=priority]:checked').val();

        addEvent(title, description, priority);
    });
    /* initialize the calendar
     -----------------------------------------------------------------*/

    $('#calendar').fullCalendar({
        height: 1300,
        header: hdr,
		//lang: lang,
        //buttonText: {
        //    prev: '<i class="icon-chevron-left"></i>',
        //    next: '<i class="icon-chevron-right"></i>'
        //},
        eventRender: function (event, element) {
	        if (event.finished == 0) {
		        element.prepend("<span class='finishOn' style='color: white; float: right; margin-right:15px;width:15px; font-size:15px' ><i class='icon-ok-sign' style=''></span>");
	        }
            element.prepend("<span class='closeon' style='color: white; float: right;width:22px; font-size:12px' ><i class='icon-remove' style='color: white;  font-size: 10px;width:15px; '></span>");
            element.find(".closeon").click(function () {
                DeleteDataCalendar(event._id);
                return false;
            });
            element.find(".finishOn").click(function () {
                FinishDataCalendar(event._id);
                return false;
            });
            DeleteFlag();
        },
        defaultView: 'agendaDay',
        events: "/Schedule/GetEvents/",
        droppable: true, // this allows things to be dropped onto the calendar !!!
        firstHour: 7,
        slotMinutes: 30,
        defaultEventMinutes: 60,
        axisFormat: 'HH:mm',
        timeFormat: {
            agenda: 'HH:mm{ - hh:mm}'
        },
        minTime: 0,
        maxTime: 24,
        firstDay: 1,
        ignoreTimezone: true,


        eventClick: function (calEvent, jsEvent, view) {
            GetEditForm(calEvent.id, calEvent.type,null, null);
        },
        editable: true,
        eventDrop: function (event, delta, revertFunc, jsEvent, ui, view) {
            SaveDataCalendar(event._id, event.start);
           
        },

        eventOverlap: false,
        dayClick: function (date, jsEvent, view) {
            GetModalForm((date.getHours() + ':' + date.getMinutes()), (((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear())));
            
        },
        windowResize: function (event, ui) {
            $('#calendar').fullCalendar('render');
        },
        monthNames: lang == "ru" ? ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'] :['January','February','March','April','May','June','July','August','September','October','November','December'],
        monthNamesShort: lang == "ru" ?  ['Янв.', 'Фев.', 'Март', 'Апр.', 'Май', 'Июнь', 'Июль', 'Авг.', 'Сент.', 'Окт.', 'Ноя.', 'Дек.'] : ['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'],
        dayNames: lang == "ru" ?  ["Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"] : ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'],
        dayNamesShort: lang == "ru" ?  ["ВС", "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ"] : ['Sun','Mon','Tue','Wed','Thu','Fri','Sat'],
    	buttonText: lang == "ru" ?  {
		    prev: "&nbsp;&#9668;&nbsp;",
		    next: "&nbsp;&#9658;&nbsp;",
		    prevYear: "&nbsp;&lt;&lt;&nbsp;",
		    nextYear: "&nbsp;&gt;&gt;&nbsp;",
		    today: "Сегодня",
		    month: "Месяц",
		    week: "Неделя",
		    day: "День"
    	} : {
    		prev: "<span class='fc-text-arrow'>&lsaquo;</span>",
    		next: "<span class='fc-text-arrow'>&rsaquo;</span>",
    		prevYear: "<span class='fc-text-arrow'>&laquo;</span>",
    		nextYear: "<span class='fc-text-arrow'>&raquo;</span>",
    		today: 'today',
    		month: 'month',
    		week: 'week',
    		day: 'day'
    	},
    });
}