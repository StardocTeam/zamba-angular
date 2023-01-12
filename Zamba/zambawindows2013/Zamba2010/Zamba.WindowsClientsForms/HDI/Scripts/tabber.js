/*==================================================
  $Id: /scripts/tabber.js,v 1.9 2006/04/27 20:51:51 pat Exp $
  /scripts/tabber.js by Patrick Fitzgerald pat@barelyfitz.com

  Documentation can be found at the following URL:
  http://www.barelyfitz.com/projects/tabber/

  License (http://www.opensource.org/licenses/mit-license.php)

  Copyright (c) 2006 Patrick Fitzgerald

  Permission is hereby granted, free of charge, to any person
  obtaining a copy of this software and associated documentation files
  (the "Software"), to deal in the Software without restriction,
  including without limitation the rights to use, copy, modify, merge,
  publish, distribute, sublicense, and/or sell copies of the Software,
  and to permit persons to whom the Software is furnished to do so,
  subject to the following conditions:

  The above copyright notice and this permission notice shall be
  included in all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
  BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
  ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
  ==================================================*/

function tabberObj(argsObj)
{
  var arg; /* name of an argument to override */

  /* Element for the main tabber div. If you supply this in argsObj,
     then the init() method will be called.
  */
  this.div = null;

  /* Class of the main tabber div */
  this.classMain = "tabber";

  /* Rename classMain to classMainLive after tabifying
     (so a different style can be applied)
  */
  this.classMainLive = "tabberlive";

  /* Class of each DIV that contains a tab */
  this.classTab = "tabbertab";

  /* Class to indicate which tab should be active on startup */
  this.classTabDefault = "tabbertabdefault";

  /* Class for the navigation UL */
  this.classNav = "tabbernav";

  /* When a tab is to be hidden, instead of setting display='none', we
     set the class of the div to classTabHide. In your screen
     stylesheet you should set classTabHide to display:none.  In your
     print stylesheet you should set display:block to ensure that all
     the information is printed.
  */
  this.classTabHide = "tabbertabhide";

  /* Class to set the navigation LI when the tab is active, so you can
     use a different style on the active tab.
  */
  this.classNavActive = "tabberactive";

  /* Elements that might contain the title for the tab, only used if a
     title is not specified in the TITLE attribute of DIV classTab.
  */
  this.titleElements = ['h2','h3','h4','h5','h6'];

  /* Should we strip out the HTML from the innerHTML of the title elements?
     This should usually be true.
  */
  this.titleElementsStripHTML = true;

  /* If the user specified the tab names using a TITLE attribute on
     the DIV, then the browser will display a tooltip whenever the
     mouse is over the DIV. To prevent this tooltip, we can remove the
     TITLE attribute after getting the tab name.
  */
  this.removeTitle = true;

  /* If you want to add an id to each link set this to true */
  this.addLinkId = false;

  /* If addIds==true, then you can set a format for the ids.
     <tabberid> will be replaced with the id of the main tabber div.
     <tabnumberzero> will be replaced with the tab number
       (tab numbers starting at zero)
     <tabnumberone> will be replaced with the tab number
       (tab numbers starting at one)
     <tabtitle> will be replaced by the tab title
       (with all non-alphanumeric characters removed)
   */
  this.linkIdFormat = '<tabberid>nav<tabnumberone>';

  /* You can override the defaults listed above by passing in an object:
     var mytab = new tabber({property:value,property:value});
  */
  for (arg in argsObj) { this[arg] = argsObj[arg]; }

  /* Create regular expressions for the class names; Note: if you
     change the class names after a new object is created you must
     also change these regular expressions.
  */
  this.REclassMain = new RegExp('\\b' + this.classMain + '\\b', 'gi');
  this.REclassMainLive = new RegExp('\\b' + this.classMainLive + '\\b', 'gi');
  this.REclassTab = new RegExp('\\b' + this.classTab + '\\b', 'gi');
  this.REclassTabDefault = new RegExp('\\b' + this.classTabDefault + '\\b', 'gi');
  this.REclassTabHide = new RegExp('\\b' + this.classTabHide + '\\b', 'gi');

  /* Array of objects holding info about each tab */
  this.tabs = new Array();

  /* If the main tabber div was specified, call init() now */
  if (this.div) {

    this.init(this.div);

    /* We don't need the main div anymore, and to prevent a memory leak
       in IE, we must remove the circular reference between the div
       and the tabber object. */
    this.div = null;
  }
}


/*--------------------------------------------------
  Methods for tabberObj
  --------------------------------------------------*/


tabberObj.prototype.init = function(e)
{
  /* Set up the tabber interface.

     e = element (the main containing div)

     Example:
     init(document.getElementById('mytabberdiv'))
   */

  var
  childNodes, /* child nodes of the tabber div */
  i, i2, /* loop indices */
  t, /* object to store info about a single tab */
  defaultTab=0, /* which tab to select by default */
  DOM_ul, /* tabbernav list */
  DOM_li, /* tabbernav list item */
  DOM_a, /* tabbernav link */
  aId, /* A unique id for DOM_a */
  headingElement; /* searching for text to use in the tab */

  /* Verify that the browser supports DOM scripting */
  if (!document.getElementsByTagName) { return false; }

  /* If the main DIV has an ID then save it. */
  if (e.id) {
    this.id = e.id;
  }

  /* Clear the tabs array (but it should normally be empty) */
  this.tabs.length = 0;

  /* Loop through an array of all the child nodes within our tabber element. */
  childNodes = e.childNodes;
  for(i=0; i < childNodes.length; i++) {

    /* Find the nodes where class="tabbertab" */
    if(childNodes[i].className &&
       childNodes[i].className.match(this.REclassTab)) {
      
      /* Create a new object to save info about this tab */
      t = new Object();
      
      /* Save a pointer to the div for this tab */
      t.div = childNodes[i];
      
      /* Add the new object to the array of tabs */
      this.tabs[this.tabs.length] = t;

      /* If the class name contains classTabDefault,
	 then select this tab by default.
      */
      if (childNodes[i].className.match(this.REclassTabDefault)) {
	defaultTab = this.tabs.length-1;
      }
    }
  }

  /* Create a new UL list to hold the tab headings */
  DOM_ul = document.createElement("ul");
  DOM_ul.className = this.classNav;
  
  /* Loop through each tab we found */
  for (i=0; i < this.tabs.length; i++) {

    t = this.tabs[i];

    /* Get the label to use for this tab:
       From the title attribute on the DIV,
       Or from one of the this.titleElements[] elements,
       Or use an automatically generated number.
     */
    t.headingText = t.div.title;

    /* Remove the title attribute to prevent a tooltip from appearing */
    if (this.removeTitle) { t.div.title = ''; }

    if (!t.headingText) {

      /* Title was not defined in the title of the DIV,
	 So try to get the title from an element within the DIV.
	 Go through the list of elements in this.titleElements
	 (typically heading elements ['h2','h3','h4'])
      */
      for (i2=0; i2<this.titleElements.length; i2++) {
	headingElement = t.div.getElementsByTagName(this.titleElements[i2])[0];
	if (headingElement) {
	  t.headingText = headingElement.innerHTML;
	  if (this.titleElementsStripHTML) {
	    t.headingText.replace(/<br>/gi," ");
	    t.headingText = t.headingText.replace(/<[^>]+>/g,"");
	  }
	  break;
	}
      }
    }

    if (!t.headingText) {
      /* Title was not found (or is blank) so automatically generate a
         number for the tab.
      */
      t.headingText = i + 1;
    }

    /* Create a list element for the tab */
    DOM_li = document.createElement("li");

    /* Save a reference to this list item so we can later change it to
       the "active" class */
    t.li = DOM_li;

    /* Create a link to activate the tab */
    DOM_a = document.createElement("a");
    DOM_a.appendChild(document.createTextNode(t.headingText));
    DOM_a.href = "javascript:void(null);";
    DOM_a.title = t.headingText;
    DOM_a.onclick = this.navClick;

    /* Add some properties to the link so we can identify which tab
       was clicked. Later the navClick method will need this.
    */
    DOM_a.tabber = this;
    DOM_a.tabberIndex = i;

    /* Do we need to add an id to DOM_a? */
    if (this.addLinkId && this.linkIdFormat) {

      /* Determine the id name */
      aId = this.linkIdFormat;
      aId = aId.replace(/<tabberid>/gi, this.id);
      aId = aId.replace(/<tabnumberzero>/gi, i);
      aId = aId.replace(/<tabnumberone>/gi, i+1);
      aId = aId.replace(/<tabtitle>/gi, t.headingText.replace(/[^a-zA-Z0-9\-]/gi, ''));

      DOM_a.id = aId;
    }

    /* Add the link to the list element */
    DOM_li.appendChild(DOM_a);

    /* Add the list element to the list */
    DOM_ul.appendChild(DOM_li);
  }

  /* Add the UL list to the beginning of the tabber div */
  e.insertBefore(DOM_ul, e.firstChild);

  /* Make the tabber div "live" so different CSS can be applied */
  e.className = e.className.replace(this.REclassMain, this.classMainLive);

  /* Activate the default tab, and do not call the onclick handler */
  this.tabShow(defaultTab);

  /* If the user specified an onLoad function, call it now. */
  if (typeof this.onLoad == 'function') {
    this.onLoad({tabber:this});
  }

  return this;
};


tabberObj.prototype.navClick = function(event)
{
  /* This method should only be called by the onClick event of an <A>
     element, in which case we will determine which tab was clicked by
     examining a property that we previously attached to the <A>
     element.

     Since this was triggered from an onClick event, the variable
     "this" refers to the <A> element that triggered the onClick
     event (and not to the tabberObj).

     When tabberObj was initialized, we added some extra properties
     to the <A> element, for the purpose of retrieving them now. Get
     the tabberObj object, plus the tab number that was clicked.
  */

  var
  rVal, /* Return value from the user onclick function */
  a, /* element that triggered the onclick event */
  self, /* the tabber object */
  tabberIndex, /* index of the tab that triggered the event */
  onClickArgs; /* args to send the onclick function */

  a = this;
  if (!a.tabber) { return false; }

  self = a.tabber;
  tabberIndex = a.tabberIndex;

  /* Remove focus from the link because it looks ugly.
     I don't know if this is a good idea...
  */
  a.blur();

  /* If the user specified an onClick function, call it now.
     If the function returns false then do not continue.
  */
  if (typeof self.onClick == 'function') {

    onClickArgs = {'tabber':self, 'index':tabberIndex, 'event':event};

    /* IE uses a different way to access the event object */
    if (!event) { onClickArgs.event = window.event; }

    rVal = self.onClick(onClickArgs);
    if (rVal === false) { return false; }
  }

  self.tabShow(tabberIndex);

  return false;
};


tabberObj.prototype.tabHideAll = function()
{
  var i; /* counter */

  /* Hide all tabs and make all navigation links inactive */
  for (i = 0; i < this.tabs.length; i++) {
    this.tabHide(i);
  }
};


tabberObj.prototype.tabHide = function(tabberIndex)
{
  var div;

  if (!this.tabs[tabberIndex]) { return false; }

  /* Hide a single tab and make its navigation link inactive */
  div = this.tabs[tabberIndex].div;

  /* Hide the tab contents by adding classTabHide to the div */
  if (!div.className.match(this.REclassTabHide)) {
    div.className += ' ' + this.classTabHide;
  }
  this.navClearActive(tabberIndex);

  return this;
};


tabberObj.prototype.tabShow = function(tabberIndex)
{
  /* Show the tabberIndex tab and hide all the other tabs */

  var div;

  if (!this.tabs[tabberIndex]) { return false; }

  /* Hide all the tabs first */
  this.tabHideAll();

  /* Get the div that holds this tab */
  div = this.tabs[tabberIndex].div;

  /* Remove classTabHide from the div */
  div.className = div.className.replace(this.REclassTabHide, '');

  /* Mark this tab navigation link as "active" */
  this.navSetActive(tabberIndex);

  /* If the user specified an onTabDisplay function, call it now. */
  if (typeof this.onTabDisplay == 'function') {
    this.onTabDisplay({'tabber':this, 'index':tabberIndex});
  }

  return this;
};

tabberObj.prototype.navSetActive = function(tabberIndex)
{
  /* Note: this method does *not* enforce the rule
     that only one nav item can be active at a time.
  */

  /* Set classNavActive for the navigation list item */
  this.tabs[tabberIndex].li.className = this.classNavActive;

  return this;
};


tabberObj.prototype.navClearActive = function(tabberIndex)
{
  /* Note: this method does *not* enforce the rule
     that one nav should always be active.
  */

  /* Remove classNavActive from the navigation list item */
  this.tabs[tabberIndex].li.className = '';

  return this;
};


/*==================================================*/


function tabberAutomatic(tabberArgs)
{
  /* This function finds all DIV elements in the document where
     class=tabber.classMain, then converts them to use the tabber
     interface.

     tabberArgs = an object to send to "new tabber()"
  */
  var
    tempObj, /* Temporary tabber object */
    divs, /* Array of all divs on the page */
    i; /* Loop index */

  if (!tabberArgs) { tabberArgs = {}; }

  /* Create a tabber object so we can get the value of classMain */
  tempObj = new tabberObj(tabberArgs);

  /* Find all DIV elements in the document that have class=tabber */

  /* First get an array of all DIV elements and loop through them */
  divs = document.getElementsByTagName("div");
  for (i=0; i < divs.length; i++) {
    
    /* Is this DIV the correct class? */
    if (divs[i].className &&
	divs[i].className.match(tempObj.REclassMain)) {
      
      /* Now tabify the DIV */
      tabberArgs.div = divs[i];
      divs[i].tabber = new tabberObj(tabberArgs);
    }
  }
  
  return this;
}


/*==================================================*/


function tabberAutomaticOnLoad(tabberArgs)
{
  /* This function adds tabberAutomatic to the window.onload event,
     so it will run after the document has finished loading.
  */
  var oldOnLoad;

  if (!tabberArgs) { tabberArgs = {}; }

  /* Taken from: http://simon.incutio.com/archive/2004/05/26/addLoadEvent */

  oldOnLoad = window.onload;
  if (typeof window.onload != 'function') {
    window.onload = function() {
      tabberAutomatic(tabberArgs);
    };
  } else {
    window.onload = function() {
      oldOnLoad();
      tabberAutomatic(tabberArgs);
    };
  }
}


/*==================================================*/
var loadedTabs= [];
var lasttabselected;
var tabletorefresh;
var tables = [];

//Cada vez que se haga click un un tab, se comprobara si este fue cargado anteriormente. Si no lo hizo, se carga de la tabla que contiene y se modifica su flag (para evitar que se intente cargar la tabla cada vez que se clickie el tab). 
var tabberOptions = {
    'onClick': function (argsObj) {
	//alert('Tab click');
        try {
              var tables = [];
	var tab = argsObj.tabber;
        var index = argsObj.index;
        lasttabselected = tab;

        if (!loadedTabs[index]) {
            loadedTabs[index] = true;
            var table;
            for (var i = 0; i < tab.tabs[index].div.children.length; i++) {
                if (tab.tabs[index].div.children[i].nodeName == "TABLE") {
                    table = tab.tabs[index].div.children[i];
                    if (table.outerHTML.indexOf("zAjaxTable") > -1 || table.outerHTML.indexOf("ZAsociated") > -1) {
                        if ($.inArray(table, tables) == -1)
                        {
                            tables.push(table);
                            //  alert('table zAjaxTable Added');
                        }
					}
                }
            }
                  //  alert('call pat');

            ProcessAjaxTable(tables);
        }
        }
        catch (err)
        { }
    }
};

function RefreshTables() {
 if (tables != undefined)      
        ProcessAjaxTable(tables);
};


/* Run tabberAutomaticOnload() unless the "manualStartup" option was specified */

if (typeof tabberOptions == 'undefined') {

//alert('auto');
    tabberAutomaticOnLoad();

} else {

  if (!tabberOptions['manualStartup']) {
//alert('manual');
    tabberAutomaticOnLoad(tabberOptions);
  }

}



function ProcessAjaxTable(tables) {
//alert('ProcessAjaxTable');
    if (tables.length > 0) { 
        var table;
        for (j = 0; j < tables.length; j++) {
            table = tables[j];
                //Obtengo las propiedades de configuracion
            for (i = 0; i < table.attributes.length; i++) {
                switch(table.attributes[i].name) {
                    case 'source':
                        var source = table.attributes[i].value;
                        break;
                    case 'filterfield':
                        var filterFieldId = table.attributes[i].value;
                        break;
                    case 'showcolumns':
                        var showColumns = table.attributes[i].value;
                        break;
                    case 'editablecolumns':
                        var editablecolumns = table.attributes[i].value;
                        break;
                    case 'editablecolumnsattributes':
                        var editableColumnsAttributes = table.attributes[i].value;
                        break;
                    case 'additionalvalidationbutton':
                        var additionalValidationButton = table.attributes[i].value;
                        break;
                    case 'postajaxfuncion':
                        var postAjaxFuncion = table.attributes[i].value;
                        break;
                    case 'id':
                        var controlId = table.attributes[i].value;
                        break;
                    default:
                        break;
                }
            }
            var tbody = '';
		
            if (table.children[0] != undefined)
            {
                tbody = table.children[0].outerHTML;
            }
			if (source != "" && source != null){
                //Pregunto si debe filtrar o no, si no debe filtrar solo lleno el combo, si filtra ademas agrego el change
                if (filterFieldId != "" && filterFieldId != null) {
                    //Si hay mas de un id de filtro					
                    if (filterFieldId.indexOf('|') > -1) {
					
                        SetGetRowsMultipleFilters(controlId, source, filterFieldId, showColumns, editablecolumns, editableColumnsAttributes, additionalValidationButton, postAjaxFuncion, tbody);
                    }
                    else {						
                        $('#' + filterFieldId).change(function () {
                            GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, $(this).val(), additionalValidationButton, postAjaxFuncion, tbody);
                        });		
                        GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, $('#' + filterFieldId).val(), additionalValidationButton, postAjaxFuncion, tbody);
                    }
                }
                else
                    GetZvarRows(controlId, source, showColumns, filterFieldId, editableColumnsAttributes, editablecolumns, '', additionalValidationButton, postAjaxFuncion, tbody);
			} else if (controlId != "" && controlId != null && controlId.indexOf('ZAsociated') > -1) {
                ajaxGetAsociatedJson(controlId, tbody);
			}

        }
     
    }       
}

function ProcessAllzAutoAjaxTable() {
    //Por cada combo que se llene con zvar
    $('.zAutoAjaxTable').each(function () {
        //Obtengo las propiedades de configuracion           
        var source = $(this).attr("source");
        var filterFieldId = $(this).attr("filterField");
        var showColumns = $(this).attr("showColumns");
        var editablecolumns = $(this).attr("editablecolumns");
        var editableColumnsAttributes = $(this).attr("editableColumnsAttributes");
        var additionalValidationButton = $(this).attr("additionalValidationButton");
        var postAjaxFuncion = $(this).attr("postAjaxFuncion");
        var controlId = this.id;
        var tbody = '';

        if ($(this).children('tbody') != undefined) {
            var children = ($(this).children('tbody')[0]);
            tbody = $(children).clone().wrap('<p>').parent().html();
        }

		if (source != "" && source != null){
            //Pregunto si debe filtrar o no, si no debe filtrar solo lleno el combo, si filtra ademas agrego el change
            if (filterFieldId != "" && filterFieldId != null) {
                //Si hay mas de un id de filtro
                if (filterFieldId.indexOf('|') > -1) {
                    SetGetRowsMultipleFilters(controlId, source, filterFieldId, showColumns, editablecolumns, editableColumnsAttributes, additionalValidationButton, postAjaxFuncion, tbody);
                }
                else {
                    $('#' + filterFieldId).change(function () {
                        GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, $(this).val(), additionalValidationButton, postAjaxFuncion, tbody);
                    });
                    GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, $('#' + filterFieldId).val(), additionalValidationButton, postAjaxFuncion, tbody);
                }
            }
            else
                GetZvarRows(controlId, source, showColumns, filterFieldId, editableColumnsAttributes, editablecolumns, '', additionalValidationButton, postAjaxFuncion, tbody);
		}

    });
}


function SetGetRowsMultipleFilters(controlId, source, filterFieldId, showColumns, editablecolumns, editableColumnsAttributes, additionalValidationButton, postAjaxFuncion) {
    var filters = filterFieldId.split('|');
    var values = '';
    for (var i = 0; i < filters.length; i++) {
        if (filters[i] != null && filters[i] != '') {
            $('#' + filters[i]).change(function () {
                GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, GetPipedSeparateValues(filters), additionalValidationButton, postAjaxFuncion);
            });
        }
    }
    GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, GetPipedSeparateValues(filters), additionalValidationButton, postAjaxFuncion);
}

function GetZvarRows(controlId, source, showColumns, filterFieldId, editablecolumns, editableColumnsAttributes, filterValues, additionalValidationButton, postAjaxFuncion, tbody) {
    //getWSPath() viene de zamba.js
	//filterValues=256397;
	//alert(getWSPath());
	$.ajax({ 
        type: "POST",
        url: getWSPath()+"/GetZDynamicTable",
        data: "{ controlId: '" + controlId + "', dataSource: '" + source + "', showColumns: '" +
                    showColumns + "', filterFieldId: '" + filterFieldId + "', editableColumns: '" + editablecolumns +
                    "', editableColumnsAttributes: '" + editableColumnsAttributes + "',filterValues: '" +
                    filterValues + "', additionalValidationButton: '" + additionalValidationButton + "', postAjaxFuncion: '" + postAjaxFuncion + "', tbody: '" + tbody + "'}",
        
        contentType: "application/json; charset=utf-8",
        dataType: "json", 
        success: ZvarRowsComplete,
        cache: true,
        error: function (request, status, err) {
            alert("Error al obtener el cuerpo de tabla. \rRequest response: " + request.responseText + ".\rError: "+err); 
        } 
    });
}


function ZvarRowsComplete(msg) { 
	var controlId = "#" + msg.d.ControlId;
    if ($(controlId).html != msg.d.SelectOptions) {
        $(controlId).html(msg.d.SelectOptions);
        if (msg.d.AdditionalValidationButton != null && msg.d.AdditionalValidationButton != '') {
            SetValidationsAction(msg.d.AdditionalValidationButton);
        }
        else {           
            SetFieldsValidations();
        }

        if (msg.d.PostAjaxFunction != null && msg.d.PostAjaxFunction != '') {
            eval(msg.d.PostAjaxFunction + "();");
        }
//		alert(controlId);
	//	alert($(controlId));
		var entityId = $(controlId).attr('entityId');
		var documentId = $(controlId).attr('documentId');
//		alert(entityId);
	//	alert(documentId);

		if (typeof entityId !== typeof undefined && entityId !== false) {
		    if (typeof documentId !== typeof undefined && documentId !== false) {
		        OcultarColumnas(controlId, documentId, entityId);
		      
			}
		}
    }
    FixFocusError();
}
function OcultarColumnas(controlId,documentId,entityId){
var indexEntityId;	
var indexDocumentId;
	$(controlId +' tr th').each(function () {
		if (($(this).text()==entityId) || ($(this).text()==documentId)){
			if ($(this).text()==entityId){
				indexEntityId=$(this).parent().children().index(this);
			}else{
				indexDocumentId=$(this).parent().children().index(this);
			}
		}
	});
	$(controlId +' tr').each(function () {
		$(this).children('td:nth('+indexEntityId+')').hide();
		$(this).children('td:nth('+indexDocumentId+')').hide();
		//$(this).children('td:nth(0)').hide(); //Columna ejecutar -- o icono
	});
	$(controlId +' tr').children('th:nth('+indexEntityId+')').hide();
	$(controlId +' tr').children('th:nth('+indexDocumentId+')').hide();
//	$(controlId +' tr').children('th:nth(0)').hide(); //Columna ejecutar -- o icono
	CrearLinks(indexEntityId,indexDocumentId,controlId);
}
function CrearLinks(indexEntityId,indexDocumentId,controlId){
	var DocId;
	var DocTypeId;
	var htmlName;
	var newInput;
	var primerFila=true;
	$(controlId +' tr').each(function () {
		if (!primerFila){ //La primera fila esta vacia
			DocTypeId=$(this).children('td:nth('+indexEntityId+')').text();
			DocId=$(this).children('td:nth('+indexDocumentId+')').text();
			htmlName='zamba_asoc_'+DocId+'_'+DocTypeId;
//			newInput = '<button type=\"button\"  id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  ><span class=\"glyphicon glyphicon-paperclip\" aria-hidden=\"true\"></span></button>';
			newInput = '<img alt=\"\" id=' + htmlName + ' onclick=SetAsocId(this); value=\"Ver\" Name=\"' + htmlName + '\"  src=\"images/toolbars/page_text_32.png\" />';
			$(this).children('td:nth(0)').html(newInput);
		}else{
			primerFila=false;
		}		
	});
	//Si hace clic en cualquier parte de la fila, abre el link en de la columna ejecutar(que esta oculto)
	$(controlId +' td').click(function () {
		var inputHtml=$(this).parent().children('td:nth(0)').html();
		var botonId=inputHtml.substring(inputHtml.indexOf("id"),inputHtml.indexOf(" onclick")).replace("id=","");
		var boton= document.getElementById(botonId);
		boton.click(); 
	});
}