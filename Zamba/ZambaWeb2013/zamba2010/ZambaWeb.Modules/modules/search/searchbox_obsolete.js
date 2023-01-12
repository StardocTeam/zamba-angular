function filter(type, value1, value2) {
    var self = this;
    self.type = ko.observable(type);
    self.value1 = ko.observable(value1);
    self.value2 = ko.observable(value2);
};


function searchViewModel() {
    var self = this;
    self.entities = ko.observableArray([]);
    self.entity = function(id, name) {
        
        if (name) {
            this.name = ko.observable(name);
        } else {
            for (item in self.entities) {
                if (self.entities[item].id == id) {
                    this.name =  ko.observable(self.entities[item].name);
                }
            }
        }
        this.id = ko.observable(id);
        this.setSelected = function (index) {
            self.selectedEntity = index;
            self.entityButtonText = this.name;
        };
    };

    self.filterList = ko.observableArray([]);
    self.addFilter = function () {
        self.filterList.push(new filter(1, self.selectedItem.entity.name, self.selectedItem.Word));//TODO: logica para determinar cantidad y tipo de filtros a agregar
    }


    self.selectedEntity = ko.observable();
    self.entityButtonText = ko.observable("All");
    self.query;
    var userId = parseInt(GetUID());
    //initialize entities
    $.getJSON(ZambaWebRestApiURL + "/Search/Entities?UserId=" + userId, function (data) {
        try {
            self.entities.push(new self.entity(0, "All"));
            for (item in data)
            {
                self.entities.push(new self.entity(data[item].id, data[item].name))
            }
        } catch (e) {
            console.log('Error al cargar las entidades')
        }
    });

    var searchBox = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('WordId'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        prefetch: ZambaWebRestApiURL + "/Search/Suggestions",
        remote: ZambaWebRestApiURL + "/Search/Suggestions?text=%QUERY",
    });

    searchBox.initialize();


    $('#search').typeahead(null, {
        name: 'best-pictures',
        displayKey: 'Word',
        source: searchBox.ttAdapter(),
        templates: {
            suggestion: Handlebars.compile('<p><span class="label label-info">{{selectedItem.entity.name}}</span> {{Word}}</p>')
        }
    });

    $('#search').on('typeahead:selected', function (e, datum) {
        if (datum) {

            if (datum.DTID) {
                self.selectedEntity = new self.entity(datum.DTID, "");
            }
            self.selectedItem.Word = datum.Word;
            self.selectedItem.WordId = datum.WordId;
            self.selectedItem.IndexId = datum.IndexId;
        }
        else {

            self.selectedItem.Word = '';
            self.selectedItem.WordId = '';
            self.selectedItem.IndexId = '';
        }
        console.log(datum);
    });
};


$(function () {
    ko.applyBindings(new searchViewModel([]), document.getElementById("search-container"));
});

