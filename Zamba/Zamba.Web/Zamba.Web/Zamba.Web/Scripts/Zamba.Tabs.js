(function () {
    if ($.ui != undefined) {
        $.widget("custom.zTabs", $.ui.tabs, {
            remove: function (id) {
                var removeId = getSelectorId(id);
                var nextTabToSel = $(removeId).parent().find(id).index() - 1;
                $(removeId).remove();
                $('a[href*="' + removeId + '"]').parent().remove();
                this.refresh();
                //Coloco color a tab seleccionado
                if (nextTabToSel > 0)
                    SelTaskMenu($($("#TasksDivUL").children("li")[nextTabToSel - 1]));
                else if (nextTabToSel == 0 && $("#TasksDivUL").children("li").length > 1) {
                    SelTaskMenu($($("#TasksDivUL").children("li")[0]));
                }

            },
            add: function (id, name) {
                var addId = getSelectorId(id);

                var ul = this.element.find("ul");
                $("<li><a href='#" + id + "'>" + name + "</a></li>").appendTo(ul);
                this.refresh();
                //Coloco color a tab seleccionado
                SelTaskMenu(ul.children(":last"));
            },
            select: function (id) {
                var tabIndex = -1;
                for (var i = 0; i < this.tabs.length; i++) {
                    if ($(this.tabs[i]).html().indexOf(id) > -1) {
                        tabIndex = i;
                        break;
                    }
                }

                //Coloco color a tab seleccionado
                SelHeaderMenu($(this.tabs[i]));
                SelTaskMenu($(this.tabs[i]));

                this.option("active", tabIndex);
            },
            activeAnchor: function () {
                return this.active.find("a");
            },
            length: function () {
                return this.tabs.length;
            }
        });
    }
})();

