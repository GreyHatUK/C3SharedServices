var HomeIndexController = function(model, rootPath, bindTo) {
    self = this;
    self.rootPath = rootPath;
    self.searchName = ko.observable("");
    self.searchResults = {};
    self.searchResults.message = ko.observable("");
    self.searchResults.data = ko.observable();
    self.searchResults.success = ko.observable(false);
    self.returnedError = ko.observable(false);
    self.Search = function () {
        $.ajax({
            type: 'POST',
            url: self.rootPath + 'home/search',
            dataType: 'json',
            data: { searchName: self.searchName() },
            success: function (data) {
                ko.mapping.fromJS(data, {}, self.searchResults);
                self.returnedError(!data.success);
            }

        });
    }

    
    ko.applyBindings(self, document.getElementById(bindTo));
}