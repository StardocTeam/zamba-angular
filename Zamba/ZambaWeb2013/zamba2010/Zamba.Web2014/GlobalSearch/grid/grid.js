ko.bindingHandlers.shortText = {
    update: function (element, valueAccessor) {
        var valueUnwrapped = ko.unwrap(valueAccessor());
        if (settings.maxCellTextLength > 0 && valueUnwrapped.length > settings.maxCellTextLength)
            $(element).html(valueUnwrapped.substring(0, settings.maxCellTextLength) + '...');
        else
            $(element).html(valueUnwrapped);
    }
};

function Settings(page, pagesCount, hideTaskData, hideIndexData, maxCellTextLength) {
    var self = this;

    self.page = page;
    self.pagesCount = pagesCount;
    self.pages = ko.computed(function () {
        var array = [self.pagesCount - 1];
        for (i = 0; i < self.pagesCount; i++)
            array[i] = i + 1;
        return array;
    }, this);

    self.hideTaskData = hideTaskData;
    self.hideIndexData = hideIndexData;
    self.maxCellTextLength = maxCellTextLength;
}
var settings = new Settings(1, 8, false, false, 30);

ResultViewModel = function () {
    var self = this;

    self.results = ko.observableArray([]);
    
    self.fill = function (data) {
        if (data == null)
            return;
        var underlyingArray = self.results();
        for (var i = 0, j = data.length; i < j; i++) {
            underlyingArray.push(data[i]);
        }
        self.results.valueHasMutated();
    };
};

var resultVM;
$(function () {
    resultVM = new ResultViewModel();
    ko.applyBindings(resultVM, document.getElementById("grid"));
      try{
          if (enableGlobalSearch != undefined && enableGlobalSearch) {
              $.ajax({
                  url: urlGlobalSearch + "Results",
                  type: 'POST',
                  async: false,
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      resultVM.fill(data);
                  },
                  error: function (xhr, ajaxOptions, thrownError) {
                      console.log("Error de servidor: " + (xhr.status) + " | " + (thrownError));
                  }
              });
          }
      }
    catch(ex){}
});
