// clase cronometro

// constructor
function Cronometro(id, style, expired, upwards, causePostBack, imgPath, duracion, year, month, day, hour, minute, second){
	this.id = id;
	this.style = style;
	this.expired = expired;
	this.upwards = upwards;
	this.causePostBack = causePostBack;
	this.imgPath = imgPath;
	this.timeSpan = this.GetTimeSpan(duracion);
	this.serverInitTime = this.GetInitialServerTime();
	this.clientInitTime = new Date().getTime();
	this.serverInitLoadTime = new Date(year, month, day, hour, minute, second);
	
	this.ShowRow(false);
	var thisObj = this;
	window.setTimeout(function(){ thisObj.Tick(); }, 500);
}

// fija los métodos del objeto
Cronometro.prototype.Tick = m_Tick;
Cronometro.prototype.ShowRow = m_ShowRow;
Cronometro.prototype.DisplayTime = m_DisplayTime;
Cronometro.prototype.GetInitialServerTime = m_GetInitialServerTime;
Cronometro.prototype.GetTimeSpan = m_GetTimeSpan;

// código de los métodos

function m_Tick(){
	var finished = false;
	var dif = (new Date().getTime() - this.clientInitTime);

	var serverEndTime = new Date(this.serverInitTime);
	serverEndTime.setHours(serverEndTime.getHours() + this.timeSpan.Hours);
	serverEndTime.setMinutes(serverEndTime.getMinutes() + this.timeSpan.Minutes);
	serverEndTime.setSeconds(serverEndTime.getSeconds() + this.timeSpan.Seconds);
	
	var serverCurrentTime = new Date(this.serverInitLoadTime);
	serverCurrentTime.setMilliseconds(serverCurrentTime.getMilliseconds() + dif);

	if (this.upwards){
		dif = serverCurrentTime.getTime() - this.serverInitTime.getTime();
		finished = serverCurrentTime.getTime() > serverEndTime.getTime();
	} else {
		dif = serverEndTime.getTime() - serverCurrentTime.getTime();
		finished = dif <= 0;
	}
	
	if (!finished){
		this.DisplayTime(dif);
		
		var thisObj = this;
		window.setTimeout(function(){ thisObj.Tick(); }, 500);
	} else {
		if (this.upwards){
			this.DisplayTime((((this.timeSpan.Hours)*60 + this.timeSpan.Minutes)*60 + this.timeSpan.Seconds)*1000);
		} else {
			this.DisplayTime(0);
		}
		this.ShowRow(true);
		
		if (this.causePostBack && (!this.expired)){
			window.setTimeout(this.id + '_postBack()', 3000);
		}
	}
}

function m_ShowRow(visible){
	var rowId = document.getElementById(this.id + "_text");
	if (visible){
		rowId.style.display = '';
	} else {
		rowId.style.display = 'none';
	}
}

function m_DisplayTime(cnt){
	var cronoId = document.getElementById(this.id);
	var rutaImg = this.imgPath + this.style;
	var msecondsPerSecond = 1000;
	var msecondsPerMinute = 1000*60;
	var msecondsPerHour = msecondsPerMinute*60;
	var hours = Math.floor(cnt/msecondsPerHour);
	cnt = cnt - (hours*msecondsPerHour);
	var minutes = Math.floor(cnt/msecondsPerMinute);
	cnt = cnt - (minutes*msecondsPerMinute);
	var seconds = Math.floor(cnt/msecondsPerSecond);

	//document.images[cronoId.id + '_hora1'].src = rutaImg + (Math.floor(hours/10)) + '.gif';
	//document.images[cronoId.id + '_hora0'].src = rutaImg + (Math.floor(hours%10)) + '.gif';
	//document.images[cronoId.id + '_min1'].src = rutaImg + (Math.floor(minutes/10)) + '.gif';
	//document.images[cronoId.id + '_min0'].src = rutaImg + (Math.floor(minutes%10)) + '.gif';
	//document.images[cronoId.id + '_seg1'].src = rutaImg + (Math.floor(seconds/10)) + '.gif';
	//document.images[cronoId.id + '_seg0'].src = rutaImg + (Math.floor(seconds%10)) + '.gif';
}

function m_GetInitialServerTime(){
	var cronoTime = document.getElementById(this.id + '_initialTime').value;
	var dInit = new Date();
	dInit.setYear(cronoTime.substring(0, 4));
	dInit.setMonth(Number(cronoTime.substring(4, 6)) - 1);
	dInit.setDate(cronoTime.substring(6, 8));
	dInit.setHours(cronoTime.substring(9, 11));
	dInit.setMinutes(cronoTime.substring(11, 13));
	dInit.setSeconds(cronoTime.substring(13, 15));

	return dInit;
}

function m_GetTimeSpan(timeSpan){
	var duration = new Object();
	duration.Hours = Number(timeSpan.substring(0, 2));
	duration.Minutes = Number(timeSpan.substring(3, 5));
	duration.Seconds = Number(timeSpan.substring(6, 8));

	return duration;
}
