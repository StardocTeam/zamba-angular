(function () {
    $.widget("custom.zTabs", $.ui.tabs, {
        remove: function (id) {
            var removeId = getSelectorId(id);

            $(removeId).remove();
            $('a[href*="' + removeId + '"]').parent().remove();
            this.refresh();
        },
        add: function (id, name) {
            var addId = getSelectorId(id);

            var ul = this.element.find("ul");
            $("<li><a href='#" + id + "'>" + name + "</a></li>").appendTo(ul);
            this.refresh();
        },
        select: function (id) {
            var tabIndex = -1;
            for (var i = 0; i < this.tabs.length; i++) {
                if ($(this.tabs[i]).html().indexOf(id) > -1) {
                    tabIndex = i;
                    break;
                }
            }

            this.option("active", tabIndex);
        },
        activeAnchor: function () {
            return this.active.find("a");
        },
        length: function () {
            return this.tabs.length;
        }
    });
})();

