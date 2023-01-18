/*
HTML Input for jQuery
Copyright (c) 2008-2009 Anthony Johnston
http://www.antix.co.uk    
        
version 1.1.0

$Revision: 42 $

13 Jul 2009 
Now requires jquery.ui.core.js

Use and distibution http://www.gnu.org/licenses/gpl.html
*/

/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.3.1/jquery.js" />
/// <reference path="http://jquery-ui.googlecode.com/svn/tags/1.6rc5/ui/ui.core.js" />
/// <reference path="http://jquery-clean.googlecode.com/svn/tags/v1/jquery.htmlClean.js" />

(function($) {
    var frames = 0; // counter incremented as frames added for identification

    $.widget("ui.htmlInput", {
        _init: function() {
            var self = this;

            // set up the toolbar and editor
            this.toolbar = new Toolbar("<div class='ui-widget-header'></div>", function(e) {
                var $command = $(this);
                switch ($command.attr("name")) {
                    case "link":
                        var link = findFirst(self.selectedElement, ["a"]);
                        if (link && !$.browser.msie) { self.setSelected(link); }
                        self.options.editLink(e, self, link, self._setLink);

                        break;
                    case "image":
                        self.options.editImage(e, self, findFirst(self.selectedElement, ["img"]), self._setImage);

                        break;
                    default:
                        self._applyCommand($command.attr("name"), $command.attr("value"));
                        break;
                }
            });
            this.toolbar.addButtons(this.options);
            if ($.browser.safari) this.toolbar.element.css("height", "21px"); 

            // create the container, add the toolbar, and the container to the document
            this.container =
                $("<div class='ui-htmlInput ui-widget' style='position:relative;overflow:hidden;'></div>")
                .addClass(this.element.attr("class"))
                .attr("id", this.element.attr("id").concat("_container"))
                .append(this.toolbar.element);
            this.element.after(this.container);

            // work out toolbar and iframe heights
            var toolbarHeight = this.toolbar.element.outerHeight();
            if (!this.options.showToolbar) {
                this.toolbar.element.css("display", "none");
                toolbarHeight = 0;
            }
            var frameHeight = this.element.outerHeight() - toolbarHeight - 3;

            frames++;
            this._frameId = "htmlInput_".concat(frames);
            this._frame = $("<iframe frameborder='no' border='0' marginWidth='0' marginHeight='0' leftMargin='0' topMargin='0' width='100%' height='".concat(frameHeight, "px' allowTransparency='true' scroll='yes'>"))
                .attr("id", this._frameId)
                .attr("src", $.browser.msie ? "javascript:false;" : "javascript:;");

            if (!this.options.debug) {
                this.element.css("display", "none");
            }

            // add the iframe to the container
            this.container.append($("<div class='ui-widget-content'/>").append(this._frame));

            // set the content
            this._setContent(this.element.val());

            // size new elements
            this.container
                .height(this.element.outerHeight())
                .width(this.options.widthAuto ? "auto" : this.element.width());
            this._frame
                .height(frameHeight)
                .css({ "width": "100%" });
            var frameHeight = this._frame.height();

            // bind events
            $(this._document)
                .bind("keydown.htmlInput keypress.htmlInput keyup.htmlInput mouseup.htmlInput focus.htmlInput blur.htmlInput", function(e) { self._eventBubble(e) });

            if (this.element[0].form) $(this.element[0].form).bind("submit.htmlInput", function() { self.clean() });

            if ($.browser.msie) {
                this._frame
                    .bind("focus.htmlInput", function(e) { self._eventBubble(e) })
                    .bind("blur.htmlInput", function(e) { self._eventBubble(e) });
                this._document.body.onpaste = function() { setTimeout(self.clean, 0) };
                this._document.onbeforedeactivate = function() {
                    self.bookmark();
                };
                this._document.onactivate = function() {
                    // if a book mark has been saved restore its selection, ie only
                    self.restoreBookmark();
                };
            }

            if ($.browser.mozilla || $.browser.safari) {
                // moz and safari go blank when the frame is moved, this detects the move and redisplays the value
                this._frame.parents().bind("DOMNodeInserted.htmlInput", function(e) {
                    if ($(e.target).find("#".concat(self._frameId)).length == 1) {
                        setTimeout(self._resetContent, 0);
                        e.stopPropagation();
                    }
                });
                if ($.browser.mozilla) {
                    try {
                        // catch pastish event for moz
                        $(self._document.body)
                            .bind("input.htmlInput", function() { self.clean() });
                    } catch (ex) { }
                }
            }

            // Selection functions
            if (window.getSelection) {
                this.bookmark = function() {
                    self._bookmark = self._window.getSelection();
                }
                this.restoreBookmark = function() {
                    if (self._bookmark) {
                        var selection = self._window.getSelection();
                        selection.removeAllRanges();
                        selection.addRange(self._bookmark);
                        self._bookmark = null;
                    }
                }
                this.getSelected = function() {
                    var selection = self._window.getSelection();
                    if (selection.rangeCount > 0) {
                        var range = selection.getRangeAt(0);
                        var element = range.collapsed || range.startContainer.childNodes.length == 0
                            ? selection.focusNode
                            : range.startContainer.childNodes[range.startOffset];
                        return element.tagName == undefined ? element.parentNode : element;
                    }
                }
                this.getSelectedHTML = function() {
                    var selection = self._window.getSelection();
                    if (selection.rangeCount > 0) {
                        var range = selection.getRangeAt(0);
                        var el = document.createElement("div");
                        el.appendChild(range.cloneContents());
                        return el.innerHTML;
                    }
                }
                this.setSelected = function(item) {
                    var selection = self._window.getSelection();
                    selection.removeAllRanges();
                    if (item) {
                        var range = self._document.createRange();
                        range.selectNodeContents(item);
                        selection.addRange(range);
                    }
                }
                this.insertHtml = function(html) {
                    self._document.execCommand("insertHTML", null, html);
                    self.update();
                }
            } else { // ie
                this.bookmark = function() {
                    try {
                        self._bookmark = self._document.selection.createRange();
                    } catch (ex) { }
                }
                this.getSelected = function() {
                    self._window.focus();
                    var range = self._document.selection.createRange();
                    return range.item
                        ? range.item(0)
                        : range.parentElement();
                }
                this.getSelectedHTML = function() {
                    self._window.focus();
                    var range = self._document.selection.createRange();
                    return range.htmlText ? range.htmlText : range.item ? range.item(0).outerHTML : "";
                }
                this.setSelected = function(item) {
                    try {
                        self._window.focus();
                        var range = self._document.selection.createRange();
                        range = self._document.body.createTextRange();
                        range.moveToElementText(item);
                        range.select();
                    } catch (e) { }
                }
                this.insertHtml = function(html) {
                    var range = this._document.selection.createRange();
                    if (range.item) {
                        range.item(0).outerHTML = html;
                    } else {
                        range.pasteHTML(html);
                    }
                    self.update();
                }
                this.restoreBookmark = function() {
                    if (self._bookmark) {
                        self._bookmark.select();
                        self._bookmark = null;
                    }
                }
            }

            this._setLink = function(hRef, target, type) {
                if (hRef == "http:/".concat("/") || hRef == "") { hRef == null; }

                // set focus so that execCommand works
                self._window.focus();

                // check for a current link
                var link = findFirst(self.selectedElement, ["a"]);
                if (hRef == null) {
                    if (link != null) {
                        // remove link
                        self._document.execCommand("unLink", false, null);
                    }
                    return;
                } else if (link == null) {
                    // add a new link
                    var content = self.selectedHTML,
                        after = "",
                        afterIndex = $.htmlClean.trimEndIndex(content);

                    if (afterIndex > -1) {
                        after = content.substring(afterIndex);
                        content = content.substring(0, afterIndex);
                    }
                    self.insertHtml("<a href='".concat(hRef, "'>", content, "</a>", after));
                    link = findFirst(self.getSelected(), ["a"]);
                }

                // set properties
                link = $(link);
                link.attr("href", hRef);
                target != null && target.length > 0
                    ? link.attr("target", target)
                    : link.removeAttr("target");
                type != null && type.length > 0
                    ? link.attr("type", type)
                    : link.removeAttr("type");

                self.update();
            }

            this._setImage = function(src) {
                // set focus so that execCommand works
                self._window.focus();

                // check for a current image
                var image = findFirst(self.selectedElement, ["img"]);
                if (image == null && src != null) {
                    // add a new image
                    self._document.execCommand("insertimage", false, src);
                    self.selectedElement = self.getSelected();
                    image = findFirst(self.selectedElement, ["img"]);
                } else if (image != null && src == null) {
                    // do nothing
                    return;
                }

                // set properties
                image = $(image);
                image.attr("src", src);

                self.update();
            }
        },

        value: function(html) {
            /// <summary>Set the html in the editor</summary>
            if (arguments.length) {
                this._document.body.innerHTML = html;
                this.clean();
            }

            return this._document.body.innerHTML;
        },

        clean: function() {
            /// <summary> cleans the html from the editor and in the original element</summary>
            var html = this._htmlClean(this.value());
            if (html != this.element.val()) {
                this.element.val(html);
                this._change();
            }
        },

        destroy: function() {
            $.widget.prototype.destroy.apply(this, arguments);
            this._document
                .add(this._document.body)
                .add(this._frame)
                .unbind(".htmlInput");
        },

        focus: function() {
            this._window.focus();
        },

        update: function() {
            if (this.getSelected().tagName == "BODY"
                    || ($.browser.safari && this.getSelected().tagName == "DIV")) {
                this._document.execCommand("formatblock", false, "<p>");
            }

            // get the currently selected element
            this.selectedElement = this.getSelected();
            this.selectedHTML = this.getSelectedHTML();
            this._showStatus();

            // show button statuses
            for (var name in this.toolbar.tools) {
                var tool = this.toolbar.tools[name];
                var selected = false;
                var enabled = true;
                switch (name) {
                    case Tools.bulletList.command:
                    case Tools.numberList.command:
                        var listItem = findFirstTagName(this.selectedElement, ["ul", "ol"]);
                        selected = (name == Tools.bulletList.command && listItem == "ul")
                                || (name == Tools.numberList.command && listItem == "ol")
                        break;
                    case Tools.bold.command:
                        selected = findFirst(this.selectedElement, ["strong", "b", ["span", /weight:\s*bold/i]]);
                        break;
                    case Tools.italic.command:
                        selected = findFirst(this.selectedElement, ["em", "i", ["span", /style:\s*italic/i]]);
                        break;
                    case Tools.superscript.command:
                        selected = findFirst(this.selectedElement, ["sup", ["span", /-align:\s*super/i]]);
                        break;
                    case Tools.subscript.command:
                        selected = findFirst(this.selectedElement, ["sub", ["span", /-align:\s*sub/i]]);
                        break;
                    case Tools.block.command:
                        tool.command.val(findFirstTagName(this.selectedElement, ["p", "h1", "h2", "h3", "h4", "h5", "h6", "pre"]));
                        continue;
                    case Tools.link.command:
                        selected = findFirst(this.selectedElement, ["a"]);
                        enabled = selected
                            || (this.selectedHTML && this.selectedHTML.length > 0)
                            || findFirst(this.selectedElement, ["img"]);
                        break;
                    case Tools.image.command:
                        selected = findFirst(this.selectedElement, ["img"]);
                        break;
                    case Tools.leftAlign.command:
                        selected = $(findFirst(this.selectedElement, this.options.canAlign)).hasClass("left");
                        break;
                    case Tools.middleAlign.command:
                        selected = $(findFirst(this.selectedElement, this.options.canAlign)).hasClass("middle");
                        break;
                    case Tools.rightAlign.command:
                        selected = $(findFirst(this.selectedElement, this.options.canAlign)).hasClass("right");
                        break;
                }

                tool.selected(selected);
                tool.enabled(enabled);
            }
        },

        _showStatus: function() {
            if (this.options.debug) {
                window.status = "selected html: '".concat(
                this.selectedHTML, "' element: ",
                (this.selectedElement ? this.selectedElement.tagName == undefined ? this.selectedElement.toString() : this.selectedElement.tagName : ""));
            }
        },

        _htmlClean: function(html, replace) {
            /// <summary>Clean the passed html, checks for htmlClean plug-in, if not present nothing is done</summary>
            return $.htmlClean
                ? $.htmlClean(html, {
                    allowedClasses: this.options.allowedClasses,
                    replace: replace
                })
                : html;
        },

        _change: function(e) {
            // raise change event on the element
            this.element.trigger("change", e, {
                value: this.element.val()
            });
        },

        _eventBubble: function(e) {
            if (e) {
                if (e.type == "blur") this.clean();
                switch (e.type) {
                    case "mouseup":
                    case "keyup": this.update();
                    default:
                        this.element.triggerHandler(e.type, e); break;
                }
            }
        },

        _setContent: function(html) {

            this._window = this._frame.attr("contentWindow")
                ? this._frame.attr("contentWindow")
                : window.frames[this._frameId].window;
            this._document = this._window.document;

            html = "<html><head>".concat(
                    "<link href='", this.options.styleUrl, "' rel='stylesheet' type='text/css' />",
                    this.options.baseUrl ? "<base href='".concat(this.options.baseUrl, "' />") : "",
                    "<style type='text/css'>body{overflow:auto;}</style></head><body class='editor'>", this._htmlClean(html, [
                            [["strong", "big", /span.*?weight:\s*bold/i], "b"],
                            [["em", /span.*?style:\s*italic/i], "i"],
                            [[/span.*?-align:\s*super/i], "sup"],
                            [[/span.*?-align:\s*sub/i], "sub"]
                        ]
                    ), "</body></html>");
            try {
                this._document.designMode = "on";
                this._document.open();
                this._document.write(html);
                this._document.close();
            } catch (e) { }

            try {
                // stop moz using inline styles, can't do anything about webkit at the mo
                this._document.execCommand("useCSS", false, true); // off (true=off!)            
                this._document.execCommand("styleWithCSS", false, false); // new implementation of the same thing
            } catch (ex) { }
        },

        _resetContent: function() {

            this._setContent(this.value());
        },

        _applyCommand: function(command, value) {
            this.focus();
            switch (command) {
                default:
                    this._document.execCommand(command, false, null);

                    break;
                case Tools.block.command:
                    this._document.execCommand(command, false, "<" + value + ">");

                    break;
                case Tools.leftAlign.command:
                    $(findFirst(this.selectedElement, this.options.canAlign))
                        .removeClass(Tools.middleAlign.command)
                        .removeClass(Tools.rightAlign.command)
                        .toggleClass(command);

                    break;
                case Tools.middleAlign.command:
                    $(findFirst(this.selectedElement, this.options.canAlign))
                        .removeClass(Tools.leftAlign.command)
                        .removeClass(Tools.rightAlign.command)
                        .toggleClass(command);

                    break;
                case Tools.rightAlign.command:
                    $(findFirst(this.selectedElement, this.options.canAlign))
                        .removeClass(Tools.leftAlign.command)
                        .removeClass(Tools.middleAlign.command)
                        .toggleClass(command);

                    break;
                case Tools.bulletList.command:
                case Tools.numberList.command:
                    this._document.execCommand(command, false, null);

                    break;
            }
            this.update();

            this.clean();
        }
    });

    /* Helpers */
    function findFirstTagName(element, tags) {
        /// <summary>Find the first matching element up the heirachy starting from, and including, the element passed</summary>
        /// <param name="element">Element to start search from</param>
        /// <param name="tags">A string array of tags to look for</param>
        /// <returns>Element tagName found, or null if not found</returns>

        element = findFirst(element, tags);
        return element == null ? "" : element.tagName.toLowerCase();
    }

    function findFirst(element, tags) {
        /// <summary>Find the first matching element up the heirachy starting from, and including, the element passed</summary>
        /// <param name="element">Element to start search from</param>
        /// <param name="tags">A string array of tags to look for</param>
        /// <returns>Element found, or null if not found</returns>

        while (element != null
                && (findFirstMatch(element, tags) == -1)) {
            element = element.parentNode;
        }

        return element;
    }
    function findFirstMatch(element, tags) {
        if (element.tagName) {
            var tag = element.tagName.toLowerCase();
            for (var i = 0; i < tags.length; i++) {
                if (tags[i] == tag
                    || (tags[i].constructor == Array && tags[i][0] == tag
                        && tags[i][1].test(element.getAttribute("style")))) { return i; }
            }
        }
        return -1;
    }
    function findHRef(element) {
        element = findFirst(element, ["a"]);
        return element == null ? "" : element.href;
    }

    var popup;

    function editPopup(e, control, element, urlProperty, showRemove, callBack) {
        /// <summary>Shows a popup in the toolbar requesting a Url for an image or link</summary>

        if (!popup) {
            popup = $("<div id='Popup' style='position:relative;zoom:1;overflow:auto'><label style='float:left;width:35px;padding:3px 5px 0 0;text-align:right'>url</label></div>");
            popup.input = $("<input style='float:left;height:100%' />");
            popup.ok = $("<span class='ui-tool ui-state-default'><a style='width:55px'>ok</a></span>");
            popup.cancel = $("<span class='ui-tool ui-state-default'><a style='width:55px'>cancel</a></span>");

            popup
                .append(popup.input)
                .append(popup.ok)
                .append(popup.cancel)
                .bind("click.htmlInput", function(e) { e.stopPropagation(); });
        }

        var toolbarWidth = control.toolbar.element.width(),
            toolbarHeight = control.toolbar.element.outerHeight();
        control.toolbar.element.append(popup);
        popup.input.width(toolbarWidth > 500 ? 335 : toolbarWidth - 175);
        popup.width(toolbarWidth - 2);
        if (element) {
            if (showRemove) { popup.cancel.contents("a").text("remove"); }
            popup.input.val(element[urlProperty]);
        } else {
            popup.input.val("");
        }

        control.toolbar.element.contents("span")
            .animate({ marginTop: -toolbarHeight }, {
                duration: 500,
                complete: function() {
                    popup.input.focus();
                }
            });

        popup.buttons = popup.ok.add(popup.cancel);
        popup.buttons
                .bind("mouseover.htmlInput", function() { $(this).addClass("ui-state-hover"); })
                .bind("mouseout.htmlInput", function() { $(this).removeClass("ui-state-hover"); })
                .bind("click.htmlInput", function(e) {
                    callBack($(this).contents("a").text() == "ok" ? popup.input.val() : null);
                    control.toolbar.element.contents("span").animate({ marginTop: 0 }, {
                        duration: 250,
                        complete: function() {
                            $(document)
                                .add(control.document)
                                .add(popup.buttons)
                                .add(popup.input)
                                .add(popup)
                                .unbind(".htmlInput");
                            popup.remove();
                        }
                    });
                    e.preventDefault();
                });
        $(document)
            .add(control.document)
            .bind("click.htmlInput", function() { popup.cancel.click() });
        popup.input.keydown(function(e) {
            switch (e.which) {
                case $.ui.keyCode.ENTER: popup.ok.click(); break;
                case $.ui.keyCode.ESCAPE: popup.cancel.click(); break;
            }
        });
    }

    function editLink(e, control, anchor, callBack) {
        editPopup(e, control, anchor, "href", true, callBack);
    }

    function editImage(e, control, image, callBack) {
        editPopup(e, control, image, "src", false, callBack);
    }

    // objects
    function Toolbar(element, onCommand) {
        this.element = $(element);
        this.tools = {};

        this.element
            .bind("click.htmlInput", function(e) { e.stopPropagation(); })
            .addClass("ui-toolbar");

        this.add = function(name, command) {
            var $tool = $("<span class='ui-tool ".concat(name, "'></span>"));

            this.element.append($tool);
            if (command) {
                $tool.append(command);

                command.attr("name", name);
                $tool.command = command;
            }
            $tool.selected = function(value) {
                if (value) { this.addClass("ui-state-active"); }
                else { this.removeClass("ui-state-active"); }
            }
            $tool.enabled = function(value) {
                if (!value) { this.addClass("ui-state-disabled"); }
                else { this.removeClass("ui-state-disabled"); }
            }

            this.tools[name] = $tool;
            return $tool;
        }

        this.addButton = function(tool) {
            var command = $("<a title='".concat(tool.tooltip, "' tabindex='1000'>", tool.content, "</a>"));

            var $tool = this.add(tool.command, command)
                .addClass("ui-state-default");

            command.bind("click.htmlInput", onCommand);
            $tool.mouseover(function() { $(this).addClass("ui-state-hover"); });
            $tool.mouseout(function() { $(this).removeClass("ui-state-hover"); });
        }

        this.addList = function(tool) {
            var command = $("<select title='".concat(tool.tooltip, "' tabindex='1000' style='height:100%'></select>"));

            var $tool = this.add(tool.command, command)
                .addClass("ui-state-default");

            command.bind("change.htmlInput", onCommand);
            for (var value in tool.content) {
                var item = "<option value='".concat(value, "'>", tool.content[value], "</option>");
                command.append(item);
            }
        }

        this.addSeparator = function(tool) {
            this.add(tool.command, null);
        }

        this.addButtons = function(options) {
            for (var name in options.tools) {
                var tool = options.tools[name];
                if (tool.add) { this["add" + tool.type](tool); }
            }
        }
    }

    // Tool object
    function Tool(type, command, content, tooltip) {
        this.type = type;
        this.command = command;
        this.content = content;
        this.add = true;
        this.tooltip = tooltip;
    }

    // types of tools
    var ToolTypes = {
        Button: "Button",
        List: "List",
        Separator: "Separator"
    };

    // Tools
    var Tools = {
        bold: new Tool(ToolTypes.Button, "bold", "<strong>B</strong>", "selection bold"),
        italic: new Tool(ToolTypes.Button, "italic", "<em>I</em>", "selection itallic"),
        superscript: new Tool(ToolTypes.Button, "superscript", "X<sup>2</sup>", "selection superscript"),
        subscript: new Tool(ToolTypes.Button, "subscript", "X<sub>2</sub>", "selection subscript"),
        subSeparator: new Tool(ToolTypes.Separator),
        removeFormat: new Tool(ToolTypes.Button, "removeformat", "~", "remove selection formatting"),
        removeFormatSeparator: new Tool(ToolTypes.Separator),
        block: new Tool(ToolTypes.List, "formatBlock", {
            "p": "Normal",
            "h1": "Heading 1",
            "h2": "Heading 2",
            "h3": "Heading 3",
            "h4": "Heading 4",
            "h5": "Heading 5",
            "h6": "Heading 6",
            "pre": "Preformatted"
        }, "Format selection as..."),
        blockSeparator: new Tool(ToolTypes.Separator),
        leftAlign: new Tool(ToolTypes.Button, "left", "&lArr;", "left align"),
        middleAlign: new Tool(ToolTypes.Button, "middle", "&hArr;", "middle align"),
        rightAlign: new Tool(ToolTypes.Button, "right", "&rArr;", "right align"),
        rightAlignSeparator: new Tool(ToolTypes.Separator),
        bulletList: new Tool(ToolTypes.Button, "insertUnorderedList", "&bull;&equiv;", "selection as a numbered list"),
        numberList: new Tool(ToolTypes.Button, "insertOrderedList", "1&equiv;", "selection as a bulleted list"),
        increaseIndent: new Tool(ToolTypes.Button, "indent", "&gt;&equiv;", "increase indent"),
        decreaseIndent: new Tool(ToolTypes.Button, "outdent", "&lt;&equiv;", "decrease indent"),
        decreaseIndentSeparator: new Tool(ToolTypes.Separator),
        link: new Tool(ToolTypes.Button, "link", "<strong>&infin;</strong>", "add/edit link"),
        image: new Tool(ToolTypes.Button, "image", "&#10065;", "add/edit image")
    };

    $.extend($.ui.htmlInput, {
        getter: "value",
        version: "1.1.0",
        eventPrefix: "htmlInput",
        defaults: {
            debug: false,
            styleUrl: "/content/editor.css",
            editLink: editLink,
            editImage: editImage,
            showToolbar: true,
            tools: Tools,
            widthAuto: false,
            baseUrl: false,
            allowedClasses: ["left", "middle", "right"],
            canAlign: ["img", "p", "h1", "h2", "h3", "h4", "h5", "h6", "th", "td", "li", "dt"]
        }
    });

})(jQuery);