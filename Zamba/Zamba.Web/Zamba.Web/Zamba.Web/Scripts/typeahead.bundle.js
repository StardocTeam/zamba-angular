﻿///*!
// * typeahead.js 0.10.5
// * https://github.com/twitter/typeahead.js
// * Copyright 2013-2014 Twitter, Inc. and other contributors; Licensed MIT
// */

//(function($) {
//    var _ = function() {
//        "use strict";
//        return {
//            isMsie: function() {
//                return /(msie|trident)/i.test(navigator.userAgent) ? navigator.userAgent.match(/(msie |rv:)(\d+(.\d+)?)/i)[2] : false;
//            },
//            isBlankString: function(str) {
//                return !str || /^\s*$/.test(str);
//            },
//            escapeRegExChars: function(str) {
//                return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
//            },
//            isString: function(obj) {
//                return typeof obj === "string";
//            },
//            isNumber: function(obj) {
//                return typeof obj === "number";
//            },
//            isArray: $.isArray,
//            isFunction: $.isFunction,
//            isObject: $.isPlainObject,
//            isUndefined: function(obj) {
//                return typeof obj === "undefined";
//            },
//            toStr: function toStr(s) {
//                return _.isUndefined(s) || s === null ? "" : s + "";
//            },
//            bind: $.proxy,
//            each: function(collection, cb) {
//                $.each(collection, reverseArgs);
//                function reverseArgs(index, value) {
//                    return cb(value, index);
//                }
//            },
//            map: $.map,
//            filter: $.grep,
//            every: function(obj, test) {
//                var result = true;
//                if (!obj) {
//                    return result;
//                }
//                $.each(obj, function(key, val) {
//                    if (!(result = test.call(null, val, key, obj))) {
//                        return false;
//                    }
//                });
//                return !!result;
//            },
//            some: function(obj, test) {
//                var result = false;
//                if (!obj) {
//                    return result;
//                }
//                $.each(obj, function(key, val) {
//                    if (result = test.call(null, val, key, obj)) {
//                        return false;
//                    }
//                });
//                return !!result;
//            },
//            mixin: $.extend,
//            getUniqueId: function() {
//                var counter = 0;
//                return function() {
//                    return counter++;
//                };
//            }(),
//            templatify: function templatify(obj) {
//                return $.isFunction(obj) ? obj : template;
//                function template() {
//                    return String(obj);
//                }
//            },
//            defer: function(fn) {
//                setTimeout(fn, 0);
//            },
//            debounce: function(func, wait, immediate) {
//                var timeout, result;
//                return function() {
//                    var context = this, args = arguments, later, callNow;
//                    later = function() {
//                        timeout = null;
//                        if (!immediate) {
//                            result = func.apply(context, args);
//                        }
//                    };
//                    callNow = immediate && !timeout;
//                    clearTimeout(timeout);
//                    timeout = setTimeout(later, wait);
//                    if (callNow) {
//                        result = func.apply(context, args);
//                    }
//                    return result;
//                };
//            },
//            throttle: function(func, wait) {
//                var context, args, timeout, result, previous, later;
//                previous = 0;
//                later = function() {
//                    previous = new Date();
//                    timeout = null;
//                    result = func.apply(context, args);
//                };
//                return function() {
//                    var now = new Date(), remaining = wait - (now - previous);
//                    context = this;
//                    args = arguments;
//                    if (remaining <= 0) {
//                        clearTimeout(timeout);
//                        timeout = null;
//                        previous = now;
//                        result = func.apply(context, args);
//                    } else if (!timeout) {
//                        timeout = setTimeout(later, remaining);
//                    }
//                    return result;
//                };
//            },
//            noop: function() {}
//        };
//    }();
//    var VERSION = "0.10.5";
//    var tokenizers = function() {
//        "use strict";
//        return {
//            nonword: nonword,
//            whitespace: whitespace,
//            obj: {
//                nonword: getObjTokenizer(nonword),
//                whitespace: getObjTokenizer(whitespace)
//            }
//        };
//        function whitespace(str) {
//            str = _.toStr(str);
//            return str ? str.split(/\s+/) : [];
//        }
//        function nonword(str) {
//            str = _.toStr(str);
//            return str ? str.split(/\W+/) : [];
//        }
//        function getObjTokenizer(tokenizer) {
//            return function setKey() {
//                var args = [].slice.call(arguments, 0);
//                return function tokenize(o) {
//                    var tokens = [];
//                    _.each(args, function(k) {
//                        tokens = tokens.concat(tokenizer(_.toStr(o[k])));
//                    });
//                    return tokens;
//                };
//            };
//        }
//    }();
//    var LruCache = function() {
//        "use strict";
//        function LruCache(maxSize) {
//            this.maxSize = _.isNumber(maxSize) ? maxSize : 100;
//            this.reset();
//            if (this.maxSize <= 0) {
//                this.set = this.get = $.noop;
//            }
//        }
//        _.mixin(LruCache.prototype, {
//            set: function set(key, val) {
//                var tailItem = this.list.tail, node;
//                if (this.size >= this.maxSize) {
//                    this.list.remove(tailItem);
//                    delete this.hash[tailItem.key];
//                }
//                if (node = this.hash[key]) {
//                    node.val = val;
//                    this.list.moveToFront(node);
//                } else {
//                    node = new Node(key, val);
//                    this.list.add(node);
//                    this.hash[key] = node;
//                    this.size++;
//                }
//            },
//            get: function get(key) {
//                var node = this.hash[key];
//                if (node) {
//                    this.list.moveToFront(node);
//                    return node.val;
//                }
//            },
//            reset: function reset() {
//                this.size = 0;
//                this.hash = {};
//                this.list = new List();
//            }
//        });
//        function List() {
//            this.head = this.tail = null;
//        }
//        _.mixin(List.prototype, {
//            add: function add(node) {
//                if (this.head) {
//                    node.next = this.head;
//                    this.head.prev = node;
//                }
//                this.head = node;
//                this.tail = this.tail || node;
//            },
//            remove: function remove(node) {
//                node.prev ? node.prev.next = node.next : this.head = node.next;
//                node.next ? node.next.prev = node.prev : this.tail = node.prev;
//            },
//            moveToFront: function(node) {
//                this.remove(node);
//                this.add(node);
//            }
//        });
//        function Node(key, val) {
//            this.key = key;
//            this.val = val;
//            this.prev = this.next = null;
//        }
//        return LruCache;
//    }();
//    var PersistentStorage = function() {
//        "use strict";
//        var ls, methods;
//        try {
//            ls = window.localStorage;
//            ls.setItem("~~~", "!");
//            ls.removeItem("~~~");
//        } catch (err) {
//            ls = null;
//        }
//        function PersistentStorage(namespace) {
//            this.prefix = [ "__", namespace, "__" ].join("");
//            this.ttlKey = "__ttl__";
//            this.keyMatcher = new RegExp("^" + _.escapeRegExChars(this.prefix));
//        }
//        if (ls && window.JSON) {
//            methods = {
//                _prefix: function(key) {
//                    return this.prefix + key;
//                },
//                _ttlKey: function(key) {
//                    return this._prefix(key) + this.ttlKey;
//                },
//                get: function(key) {
//                    if (this.isExpired(key)) {
//                        this.remove(key);
//                    }
//                    return decode(ls.getItem(this._prefix(key)));
//                },
//                set: function(key, val, ttl) {
//                    if (_.isNumber(ttl)) {
//                        ls.setItem(this._ttlKey(key), encode(now() + ttl));
//                    } else {
//                        ls.removeItem(this._ttlKey(key));
//                    }
//                    return ls.setItem(this._prefix(key), encode(val));
//                },
//                remove: function(key) {
//                    ls.removeItem(this._ttlKey(key));
//                    ls.removeItem(this._prefix(key));
//                    return this;
//                },
//                clear: function() {
//                    var i, key, keys = [], len = ls.length;
//                    for (i = 0; i < len; i++) {
//                        if ((key = ls.key(i)).match(this.keyMatcher)) {
//                            keys.push(key.replace(this.keyMatcher, ""));
//                        }
//                    }
//                    for (i = keys.length; i--; ) {
//                        this.remove(keys[i]);
//                    }
//                    return this;
//                },
//                isExpired: function(key) {
//                    var ttl = decode(ls.getItem(this._ttlKey(key)));
//                    return _.isNumber(ttl) && now() > ttl ? true : false;
//                }
//            };
//        } else {
//            methods = {
//                get: _.noop,
//                set: _.noop,
//                remove: _.noop,
//                clear: _.noop,
//                isExpired: _.noop
//            };
//        }
//        _.mixin(PersistentStorage.prototype, methods);
//        return PersistentStorage;
//        function now() {
//            return new Date().getTime();
//        }
//        function encode(val) {
//            return JSON.stringify(_.isUndefined(val) ? null : val);
//        }
//        function decode(val) {
//            return JSON.parse(val);
//        }
//    }();
//    var Transport = function() {
//        "use strict";
//        var pendingRequestsCount = 0, pendingRequests = {}, maxPendingRequests = 6, sharedCache = new LruCache(10);
//        function Transport(o) {
//            o = o || {};
//            this.cancelled = false;
//            this.lastUrl = null;
//            this._send = o.transport ? callbackToDeferred(o.transport) : $.ajax;
//            this._get = o.rateLimiter ? o.rateLimiter(this._get) : this._get;
//            this._cache = o.cache === false ? new LruCache(0) : sharedCache;
//        }
//        Transport.setMaxPendingRequests = function setMaxPendingRequests(num) {
//            maxPendingRequests = num;
//        };
//        Transport.resetCache = function resetCache() {
//            sharedCache.reset();
//        };
//        _.mixin(Transport.prototype, {
//            _get: function(url, o, cb) {
//                var that = this, jqXhr;
//                if (this.cancelled || url !== this.lastUrl) {
//                    return;
//                }
//                if (jqXhr = pendingRequests[url]) {
//                    jqXhr.done(done).fail(fail);
//                } else if (pendingRequestsCount < maxPendingRequests) {
//                    pendingRequestsCount++;
//                    pendingRequests[url] = this._send(url, o).done(done).fail(fail).always(always);
//                } else {
//                    this.onDeckRequestArgs = [].slice.call(arguments, 0);
//                }
//                function done(resp) {
//                    cb && cb(null, resp);
//                    that._cache.set(url, resp);
//                }
//                function fail() {
//                    cb && cb(true);
//                }
//                function always() {
//                    pendingRequestsCount--;
//                    delete pendingRequests[url];
//                    if (that.onDeckRequestArgs) {
//                        that._get.apply(that, that.onDeckRequestArgs);
//                        that.onDeckRequestArgs = null;
//                    }
//                }
//            },
//            get: function(url, o, cb) {
//                var resp;
//                if (_.isFunction(o)) {
//                    cb = o;
//                    o = {};
//                }
//                this.cancelled = false;
//                this.lastUrl = url;
//                if (resp = this._cache.get(url)) {
//                    _.defer(function() {
//                        cb && cb(null, resp);
//                    });
//                } else {
//                    this._get(url, o, cb);
//                }
//                return !!resp;
//            },
//            cancel: function() {
//                this.cancelled = true;
//            }
//        });
//        return Transport;
//        function callbackToDeferred(fn) {
//            return function customSendWrapper(url, o) {
//                var deferred = $.Deferred();
//                fn(url, o, onSuccess, onError);
//                return deferred;
//                function onSuccess(resp) {
//                    _.defer(function() {
//                        deferred.resolve(resp);
//                    });
//                }
//                function onError(err) {
//                    _.defer(function() {
//                        deferred.reject(err);
//                    });
//                }
//            };
//        }
//    }();
//    var SearchIndex = function() {
//        "use strict";
//        function SearchIndex(o) {
//            o = o || {};
//            if (!o.datumTokenizer || !o.queryTokenizer) {
//                $.error("datumTokenizer and queryTokenizer are both required");
//            }
//            this.datumTokenizer = o.datumTokenizer;
//            this.queryTokenizer = o.queryTokenizer;
//            this.reset();
//        }
//        _.mixin(SearchIndex.prototype, {
//            bootstrap: function bootstrap(o) {
//                this.datums = o.datums;
//                this.trie = o.trie;
//            },
//            add: function(data) {
//                var that = this;
//                data = _.isArray(data) ? data : [ data ];
//                _.each(data, function(datum) {
//                    var id, tokens;
//                    id = that.datums.push(datum) - 1;
//                    tokens = normalizeTokens(that.datumTokenizer(datum));
//                    _.each(tokens, function(token) {
//                        var node, chars, ch;
//                        node = that.trie;
//                        chars = token.split("");
//                        while (ch = chars.shift()) {
//                            node = node.children[ch] || (node.children[ch] = newNode());
//                            node.ids.push(id);
//                        }
//                    });
//                });
//            },
//            get: function get(query) {
//                var that = this, tokens, matches;
//                tokens = normalizeTokens(this.queryTokenizer(query));
//                _.each(tokens, function(token) {
//                    var node, chars, ch, ids;
//                    if (matches && matches.length === 0) {
//                        return false;
//                    }
//                    node = that.trie;
//                    chars = token.split("");
//                    while (node && (ch = chars.shift())) {
//                        node = node.children[ch];
//                    }
//                    if (node && chars.length === 0) {
//                        ids = node.ids.slice(0);
//                        matches = matches ? getIntersection(matches, ids) : ids;
//                    } else {
//                        matches = [];
//                        return false;
//                    }
//                });
//                return matches ? _.map(unique(matches), function(id) {
//                    return that.datums[id];
//                }) : [];
//            },
//            reset: function reset() {
//                this.datums = [];
//                this.trie = newNode();
//            },
//            serialize: function serialize() {
//                return {
//                    datums: this.datums,
//                    trie: this.trie
//                };
//            }
//        });
//        return SearchIndex;
//        function normalizeTokens(tokens) {
//            tokens = _.filter(tokens, function(token) {
//                return !!token;
//            });
//            tokens = _.map(tokens, function(token) {
//                return token.toLowerCase();
//            });
//            return tokens;
//        }
//        function newNode() {
//            return {
//                ids: [],
//                children: {}
//            };
//        }
//        function unique(array) {
//            var seen = {}, uniques = [];
//            for (var i = 0, len = array.length; i < len; i++) {
//                if (!seen[array[i]]) {
//                    seen[array[i]] = true;
//                    uniques.push(array[i]);
//                }
//            }
//            return uniques;
//        }
//        function getIntersection(arrayA, arrayB) {
//            var ai = 0, bi = 0, intersection = [];
//            arrayA = arrayA.sort(compare);
//            arrayB = arrayB.sort(compare);
//            var lenArrayA = arrayA.length, lenArrayB = arrayB.length;
//            while (ai < lenArrayA && bi < lenArrayB) {
//                if (arrayA[ai] < arrayB[bi]) {
//                    ai++;
//                } else if (arrayA[ai] > arrayB[bi]) {
//                    bi++;
//                } else {
//                    intersection.push(arrayA[ai]);
//                    ai++;
//                    bi++;
//                }
//            }
//            return intersection;
//            function compare(a, b) {
//                return a - b;
//            }
//        }
//    }();
//    var oParser = function() {
//        "use strict";
//        return {
//            local: getLocal,
//            prefetch: getPrefetch,
//            remote: getRemote
//        };
//        function getLocal(o) {
//            return o.local || null;
//        }
//        function getPrefetch(o) {
//            var prefetch, defaults;
//            defaults = {
//                url: null,
//                thumbprint: "",
//                ttl: 24 * 60 * 60 * 1e3,
//                filter: null,
//                ajax: {}
//            };
//            if (prefetch = o.prefetch || null) {
//                prefetch = _.isString(prefetch) ? {
//                    url: prefetch
//                } : prefetch;
//                prefetch = _.mixin(defaults, prefetch);
//                prefetch.thumbprint = VERSION + prefetch.thumbprint;
//                prefetch.ajax.type = prefetch.ajax.type || "GET";
//                prefetch.ajax.dataType = prefetch.ajax.dataType || "json";
//                !prefetch.url && $.error("prefetch requires url to be set");
//            }
//            return prefetch;
//        }
//        function getRemote(o) {
//            var remote, defaults;
//            defaults = {
//                url: null,
//                cache: true,
//                wildcard: "%QUERY",
//                replace: null,
//                rateLimitBy: "debounce",
//                rateLimitWait: 300,
//                send: null,
//                filter: null,
//                ajax: {}
//            };
//            if (remote = o.remote || null) {
//                remote = _.isString(remote) ? {
//                    url: remote
//                } : remote;
//                remote = _.mixin(defaults, remote);
//                remote.rateLimiter = /^throttle$/i.test(remote.rateLimitBy) ? byThrottle(remote.rateLimitWait) : byDebounce(remote.rateLimitWait);
//                remote.ajax.type = remote.ajax.type || "GET";
//                remote.ajax.dataType = remote.ajax.dataType || "json";
//                delete remote.rateLimitBy;
//                delete remote.rateLimitWait;
//                !remote.url && $.error("remote requires url to be set");
//            }
//            return remote;
//            function byDebounce(wait) {
//                return function(fn) {
//                    return _.debounce(fn, wait);
//                };
//            }
//            function byThrottle(wait) {
//                return function(fn) {
//                    return _.throttle(fn, wait);
//                };
//            }
//        }
//    }();
//    (function(root) {
//        "use strict";
//        var old, keys;
//        old = root.Bloodhound;
//        keys = {
//            data: "data",
//            protocol: "protocol",
//            thumbprint: "thumbprint"
//        };
//        root.Bloodhound = Bloodhound;
//        function Bloodhound(o) {
//            if (!o || !o.local && !o.prefetch && !o.remote) {
//                $.error("one of local, prefetch, or remote is required");
//            }
//            this.limit = o.limit || 5;
//            this.sorter = getSorter(o.sorter);
//            this.dupDetector = o.dupDetector || ignoreDuplicates;
//            this.local = oParser.local(o);
//            this.prefetch = oParser.prefetch(o);
//            this.remote = oParser.remote(o);
//            this.cacheKey = this.prefetch ? this.prefetch.cacheKey || this.prefetch.url : null;
//            this.index = new SearchIndex({
//                datumTokenizer: o.datumTokenizer,
//                queryTokenizer: o.queryTokenizer
//            });
//            this.storage = this.cacheKey ? new PersistentStorage(this.cacheKey) : null;
//        }
//        Bloodhound.noConflict = function noConflict() {
//            root.Bloodhound = old;
//            return Bloodhound;
//        };
//        Bloodhound.tokenizers = tokenizers;
//        _.mixin(Bloodhound.prototype, {
//            _loadPrefetch: function loadPrefetch(o) {
//                var that = this, serialized, deferred;
//                if (serialized = this._readFromStorage(o.thumbprint)) {
//                    this.index.bootstrap(serialized);
//                    deferred = $.Deferred().resolve();
//                } else {
//                    deferred = $.ajax(o.url, o.ajax).done(handlePrefetchResponse);
//                }
//                return deferred;
//                function handlePrefetchResponse(resp) {
//                    that.clear();
//                    that.add(o.filter ? o.filter(resp) : resp);
//                    that._saveToStorage(that.index.serialize(), o.thumbprint, o.ttl);
//                }
//            },
//            _getFromRemote: function getFromRemote(query, cb) {
//                var that = this, url, uriEncodedQuery;
//                if (!this.transport) {
//                    return;
//                }
//                query = query || "";
//                uriEncodedQuery = encodeURIComponent(query);
//                url = this.remote.replace ? this.remote.replace(this.remote.url, query) : this.remote.url.replace(this.remote.wildcard, uriEncodedQuery);
//                return this.transport.get(url, this.remote.ajax, handleRemoteResponse);
//                function handleRemoteResponse(err, resp) {
//                    err ? cb([]) : cb(that.remote.filter ? that.remote.filter(resp) : resp);
//                }
//            },
//            _cancelLastRemoteRequest: function cancelLastRemoteRequest() {
//                this.transport && this.transport.cancel();
//            },
//            _saveToStorage: function saveToStorage(data, thumbprint, ttl) {
//                if (this.storage) {
//                    this.storage.set(keys.data, data, ttl);
//                    this.storage.set(keys.protocol, location.protocol, ttl);
//                    this.storage.set(keys.thumbprint, thumbprint, ttl);
//                }
//            },
//            _readFromStorage: function readFromStorage(thumbprint) {
//                var stored = {}, isExpired;
//                if (this.storage) {
//                    stored.data = this.storage.get(keys.data);
//                    stored.protocol = this.storage.get(keys.protocol);
//                    stored.thumbprint = this.storage.get(keys.thumbprint);
//                }
//                isExpired = stored.thumbprint !== thumbprint || stored.protocol !== location.protocol;
//                return stored.data && !isExpired ? stored.data : null;
//            },
//            _initialize: function initialize() {
//                var that = this, local = this.local, deferred;
//                deferred = this.prefetch ? this._loadPrefetch(this.prefetch) : $.Deferred().resolve();
//                local && deferred.done(addLocalToIndex);
//                this.transport = this.remote ? new Transport(this.remote) : null;
//                return this.initPromise = deferred.promise();
//                function addLocalToIndex() {
//                    that.add(_.isFunction(local) ? local() : local);
//                }
//            },
//            initialize: function initialize(force) {
//                return !this.initPromise || force ? this._initialize() : this.initPromise;
//            },
//            add: function add(data) {
//                this.index.add(data);
//            },
//            get: function get(query, cb) {
//                var that = this, matches = [], cacheHit = false;
//                matches = this.index.get(query);
//                matches = this.sorter(matches).slice(0, this.limit);
//                matches.length < this.limit ? cacheHit = this._getFromRemote(query, returnRemoteMatches) : this._cancelLastRemoteRequest();
//                if (!cacheHit) {
//                    (matches.length > 0 || !this.transport) && cb && cb(matches);
//                }
//                function returnRemoteMatches(remoteMatches) {
//                    var matchesWithBackfill = matches.slice(0);
//                    _.each(remoteMatches, function(remoteMatch) {
//                        var isDuplicate;
//                        isDuplicate = _.some(matchesWithBackfill, function(match) {
//                            return that.dupDetector(remoteMatch, match);
//                        });
//                        !isDuplicate && matchesWithBackfill.push(remoteMatch);
//                        return matchesWithBackfill.length < that.limit;
//                    });
//                    cb && cb(that.sorter(matchesWithBackfill));
//                }
//            },
//            clear: function clear() {
//                this.index.reset();
//            },
//            clearPrefetchCache: function clearPrefetchCache() {
//                this.storage && this.storage.clear();
//            },
//            clearRemoteCache: function clearRemoteCache() {
//                this.transport && Transport.resetCache();
//            },
//            ttAdapter: function ttAdapter() {
//                return _.bind(this.get, this);
//            }
//        });
//        return Bloodhound;
//        function getSorter(sortFn) {
//            return _.isFunction(sortFn) ? sort : noSort;
//            function sort(array) {
//                return array.sort(sortFn);
//            }
//            function noSort(array) {
//                return array;
//            }
//        }
//        function ignoreDuplicates() {
//            return false;
//        }
//    })(this);
//    var html = function() {
//        return {
//            wrapper: '<span class="twitter-typeahead"></span>',
//            dropdown: '<span class="tt-dropdown-menu"></span>',
//            dataset: '<div class="tt-dataset-%CLASS%"></div>',
//            suggestions: '<span class="tt-suggestions"></span>',
//            suggestion: '<div class="tt-suggestion"></div>'
//        };
//    }();
//    var css = function() {
//        "use strict";
//        var css = {
//            wrapper: {
//                position: "relative",
//                display: "inline-block"
//            },
//            hint: {
//                position: "absolute",
//                top: "0",
//                left: "0",
//                borderColor: "transparent",
//                boxShadow: "none",
//                opacity: "1"
//            },
//            input: {
//                position: "relative",
//                verticalAlign: "top",
//                backgroundColor: "transparent"
//            },
//            inputWithNoHint: {
//                position: "relative",
//                verticalAlign: "top"
//            },
//            dropdown: {
//                position: "absolute",
//                top: "100%",
//                left: "0",
//                zIndex: "100",
//                display: "none"
//            },
//            suggestions: {
//                display: "block"
//            },
//            suggestion: {
//                whiteSpace: "nowrap",
//                cursor: "pointer"
//            },
//            suggestionChild: {
//                whiteSpace: "normal"
//            },
//            ltr: {
//                left: "0",
//                right: "auto"
//            },
//            rtl: {
//                left: "auto",
//                right: " 0"
//            }
//        };
//        if (_.isMsie()) {
//            _.mixin(css.input, {
//                backgroundImage: "url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7)"
//            });
//        }
//        if (_.isMsie() && _.isMsie() <= 7) {
//            _.mixin(css.input, {
//                marginTop: "-1px"
//            });
//        }
//        return css;
//    }();
//    var EventBus = function() {
//        "use strict";
//        var namespace = "typeahead:";
//        function EventBus(o) {
//            if (!o || !o.el) {
//                $.error("EventBus initialized without el");
//            }
//            this.$el = $(o.el);
//        }
//        _.mixin(EventBus.prototype, {
//            trigger: function(type) {
//                var args = [].slice.call(arguments, 1);
//                this.$el.trigger(namespace + type, args);
//            }
//        });
//        return EventBus;
//    }();
//    var EventEmitter = function() {
//        "use strict";
//        var splitter = /\s+/, nextTick = getNextTick();
//        return {
//            onSync: onSync,
//            onAsync: onAsync,
//            off: off,
//            trigger: trigger
//        };
//        function on(method, types, cb, context) {
//            var type;
//            if (!cb) {
//                return this;
//            }
//            types = types.split(splitter);
//            cb = context ? bindContext(cb, context) : cb;
//            this._callbacks = this._callbacks || {};
//            while (type = types.shift()) {
//                this._callbacks[type] = this._callbacks[type] || {
//                    sync: [],
//                    async: []
//                };
//                this._callbacks[type][method].push(cb);
//            }
//            return this;
//        }
//        function onAsync(types, cb, context) {
//            return on.call(this, "async", types, cb, context);
//        }
//        function onSync(types, cb, context) {
//            return on.call(this, "sync", types, cb, context);
//        }
//        function off(types) {
//            var type;
//            if (!this._callbacks) {
//                return this;
//            }
//            types = types.split(splitter);
//            while (type = types.shift()) {
//                delete this._callbacks[type];
//            }
//            return this;
//        }
//        function trigger(types) {
//            var type, callbacks, args, syncFlush, asyncFlush;
//            if (!this._callbacks) {
//                return this;
//            }
//            types = types.split(splitter);
//            args = [].slice.call(arguments, 1);
//            while ((type = types.shift()) && (callbacks = this._callbacks[type])) {
//                syncFlush = getFlush(callbacks.sync, this, [ type ].concat(args));
//                asyncFlush = getFlush(callbacks.async, this, [ type ].concat(args));
//                syncFlush() && nextTick(asyncFlush);
//            }
//            return this;
//        }
//        function getFlush(callbacks, context, args) {
//            return flush;
//            function flush() {
//                var cancelled;
//                for (var i = 0, len = callbacks.length; !cancelled && i < len; i += 1) {
//                    cancelled = callbacks[i].apply(context, args) === false;
//                }
//                return !cancelled;
//            }
//        }
//        function getNextTick() {
//            var nextTickFn;
//            if (window.setImmediate) {
//                nextTickFn = function nextTickSetImmediate(fn) {
//                    setImmediate(function() {
//                        fn();
//                    });
//                };
//            } else {
//                nextTickFn = function nextTickSetTimeout(fn) {
//                    setTimeout(function() {
//                        fn();
//                    }, 0);
//                };
//            }
//            return nextTickFn;
//        }
//        function bindContext(fn, context) {
//            return fn.bind ? fn.bind(context) : function() {
//                fn.apply(context, [].slice.call(arguments, 0));
//            };
//        }
//    }();
//    var highlight = function(doc) {
//        "use strict";
//        var defaults = {
//            node: null,
//            pattern: null,
//            tagName: "strong",
//            className: null,
//            wordsOnly: false,
//            caseSensitive: false
//        };
//        return function hightlight(o) {
//            var regex;
//            o = _.mixin({}, defaults, o);
//            if (!o.node || !o.pattern) {
//                return;
//            }
//            o.pattern = _.isArray(o.pattern) ? o.pattern : [ o.pattern ];
//            regex = getRegex(o.pattern, o.caseSensitive, o.wordsOnly);
//            traverse(o.node, hightlightTextNode);
//            function hightlightTextNode(textNode) {
//                var match, patternNode, wrapperNode;
//                if (match = regex.exec(textNode.data)) {
//                    wrapperNode = doc.createElement(o.tagName);
//                    o.className && (wrapperNode.className = o.className);
//                    patternNode = textNode.splitText(match.index);
//                    patternNode.splitText(match[0].length);
//                    wrapperNode.appendChild(patternNode.cloneNode(true));
//                    textNode.parentNode.replaceChild(wrapperNode, patternNode);
//                }
//                return !!match;
//            }
//            function traverse(el, hightlightTextNode) {
//                var childNode, TEXT_NODE_TYPE = 3;
//                for (var i = 0; i < el.childNodes.length; i++) {
//                    childNode = el.childNodes[i];
//                    if (childNode.nodeType === TEXT_NODE_TYPE) {
//                        i += hightlightTextNode(childNode) ? 1 : 0;
//                    } else {
//                        traverse(childNode, hightlightTextNode);
//                    }
//                }
//            }
//        };
//        function getRegex(patterns, caseSensitive, wordsOnly) {
//            var escapedPatterns = [], regexStr;
//            for (var i = 0, len = patterns.length; i < len; i++) {
//                escapedPatterns.push(_.escapeRegExChars(patterns[i]));
//            }
//            regexStr = wordsOnly ? "\\b(" + escapedPatterns.join("|") + ")\\b" : "(" + escapedPatterns.join("|") + ")";
//            return caseSensitive ? new RegExp(regexStr) : new RegExp(regexStr, "i");
//        }
//    }(window.document);
//    var Input = function() {
//        "use strict";
//        var specialKeyCodeMap;
//        specialKeyCodeMap = {
//            9: "tab",
//            27: "esc",
//            37: "left",
//            39: "right",
//            13: "enter",
//            38: "up",
//            40: "down"
//        };
//        function Input(o) {
//            var that = this, onBlur, onFocus, onKeydown, onInput;
//            o = o || {};
//            if (!o.input) {
//                $.error("input is missing");
//            }
//            onBlur = _.bind(this._onBlur, this);
//            onFocus = _.bind(this._onFocus, this);
//            onKeydown = _.bind(this._onKeydown, this);
//            onInput = _.bind(this._onInput, this);
//            this.$hint = $(o.hint);
//            this.$input = $(o.input).on("blur.tt", onBlur).on("focus.tt", onFocus).on("keydown.tt", onKeydown);
//            if (this.$hint.length === 0) {
//                this.setHint = this.getHint = this.clearHint = this.clearHintIfInvalid = _.noop;
//            }
//            if (!_.isMsie()) {
//                this.$input.on("input.tt", onInput);
//            } else {
//                this.$input.on("keydown.tt keypress.tt cut.tt paste.tt", function($e) {
//                    if (specialKeyCodeMap[$e.which || $e.keyCode]) {
//                        return;
//                    }
//                    _.defer(_.bind(that._onInput, that, $e));
//                });
//            }
//            this.query = this.$input.val();
//            this.$overflowHelper = buildOverflowHelper(this.$input);
//        }
//        Input.normalizeQuery = function(str) {
//            return (str || "").replace(/^\s*/g, "").replace(/\s{2,}/g, " ");
//        };
//        _.mixin(Input.prototype, EventEmitter, {
//            _onBlur: function onBlur() {
//                this.resetInputValue();
//                this.trigger("blurred");
//            },
//            _onFocus: function onFocus() {
//                this.trigger("focused");
//            },
//            _onKeydown: function onKeydown($e) {
//                var keyName = specialKeyCodeMap[$e.which || $e.keyCode];
//                this._managePreventDefault(keyName, $e);
//                if (keyName && this._shouldTrigger(keyName, $e)) {
//                    this.trigger(keyName + "Keyed", $e);
//                }
//            },
//            _onInput: function onInput() {
//                this._checkInputValue();
//            },
//            _managePreventDefault: function managePreventDefault(keyName, $e) {
//                var preventDefault, hintValue, inputValue;
//                switch (keyName) {
//                  case "tab":
//                    hintValue = this.getHint();
//                    inputValue = this.getInputValue();
//                    preventDefault = hintValue && hintValue !== inputValue && !withModifier($e);
//                    break;

//                  case "up":
//                  case "down":
//                    preventDefault = !withModifier($e);
//                    break;

//                  default:
//                    preventDefault = false;
//                }
//                preventDefault && $e.preventDefault();
//            },
//            _shouldTrigger: function shouldTrigger(keyName, $e) {
//                var trigger;
//                switch (keyName) {
//                  case "tab":
//                    trigger = !withModifier($e);
//                    break;

//                  default:
//                    trigger = true;
//                }
//                return trigger;
//            },
//            _checkInputValue: function checkInputValue() {
//                var inputValue, areEquivalent, hasDifferentWhitespace;
//                inputValue = this.getInputValue();
//                areEquivalent = areQueriesEquivalent(inputValue, this.query);
//                hasDifferentWhitespace = areEquivalent ? this.query.length !== inputValue.length : false;
//                this.query = inputValue;
//                if (!areEquivalent) {
//                    this.trigger("queryChanged", this.query);
//                } else if (hasDifferentWhitespace) {
//                    this.trigger("whitespaceChanged", this.query);
//                }
//            },
//            focus: function focus() {
//                this.$input.focus();
//            },
//            blur: function blur() {
//                this.$input.blur();
//            },
//            getQuery: function getQuery() {
//                return this.query;
//            },
//            setQuery: function setQuery(query) {
//                this.query = query;
//            },
//            getInputValue: function getInputValue() {
//                return this.$input.val();
//            },
//            setInputValue: function setInputValue(value, silent) {
//                this.$input.val(value);
//                silent ? this.clearHint() : this._checkInputValue();
//            },
//            resetInputValue: function resetInputValue() {
//                this.setInputValue(this.query, true);
//            },
//            getHint: function getHint() {
//                return this.$hint.val();
//            },
//            setHint: function setHint(value) {
//                this.$hint.val(value);
//            },
//            clearHint: function clearHint() {
//                this.setHint("");
//            },
//            clearHintIfInvalid: function clearHintIfInvalid() {
//                var val, hint, valIsPrefixOfHint, isValid;
//                val = this.getInputValue();
//                hint = this.getHint();
//                valIsPrefixOfHint = val !== hint && hint.indexOf(val) === 0;
//                isValid = val !== "" && valIsPrefixOfHint && !this.hasOverflow();
//                !isValid && this.clearHint();
//            },
//            getLanguageDirection: function getLanguageDirection() {
//                return (this.$input.css("direction") || "ltr").toLowerCase();
//            },
//            hasOverflow: function hasOverflow() {
//                var constraint = this.$input.width() - 2;
//                this.$overflowHelper.text(this.getInputValue());
//                return this.$overflowHelper.width() >= constraint;
//            },
//            isCursorAtEnd: function() {
//                var valueLength, selectionStart, range;
//                valueLength = this.$input.val().length;
//                selectionStart = this.$input[0].selectionStart;
//                if (_.isNumber(selectionStart)) {
//                    return selectionStart === valueLength;
//                } else if (document.selection) {
//                    range = document.selection.createRange();
//                    range.moveStart("character", -valueLength);
//                    return valueLength === range.text.length;
//                }
//                return true;
//            },
//            destroy: function destroy() {
//                this.$hint.off(".tt");
//                this.$input.off(".tt");
//                this.$hint = this.$input = this.$overflowHelper = null;
//            }
//        });
//        return Input;
//        function buildOverflowHelper($input) {
//            return $('<pre aria-hidden="true"></pre>').css({
//                position: "absolute",
//                visibility: "hidden",
//                whiteSpace: "pre",
//                fontFamily: $input.css("font-family"),
//                fontSize: $input.css("font-size"),
//                fontStyle: $input.css("font-style"),
//                fontVariant: $input.css("font-variant"),
//                fontWeight: $input.css("font-weight"),
//                wordSpacing: $input.css("word-spacing"),
//                letterSpacing: $input.css("letter-spacing"),
//                textIndent: $input.css("text-indent"),
//                textRendering: $input.css("text-rendering"),
//                textTransform: $input.css("text-transform")
//            }).insertAfter($input);
//        }
//        function areQueriesEquivalent(a, b) {
//            return Input.normalizeQuery(a) === Input.normalizeQuery(b);
//        }
//        function withModifier($e) {
//            return $e.altKey || $e.ctrlKey || $e.metaKey || $e.shiftKey;
//        }
//    }();
//    var Dataset = function() {
//        "use strict";
//        var datasetKey = "ttDataset", valueKey = "ttValue", datumKey = "ttDatum";
//        function Dataset(o) {
//            o = o || {};
//            o.templates = o.templates || {};
//            if (!o.source) {
//                $.error("missing source");
//            }
//            if (o.name && !isValidName(o.name)) {
//                $.error("invalid dataset name: " + o.name);
//            }
//            this.query = null;
//            this.highlight = !!o.highlight;
//            this.name = o.name || _.getUniqueId();
//            this.source = o.source;
//            this.displayFn = getDisplayFn(o.display || o.displayKey);
//            this.templates = getTemplates(o.templates, this.displayFn);
//            this.$el = $(html.dataset.replace("%CLASS%", this.name));
//        }
//        Dataset.extractDatasetName = function extractDatasetName(el) {
//            return $(el).data(datasetKey);
//        };
//        Dataset.extractValue = function extractDatum(el) {
//            return $(el).data(valueKey);
//        };
//        Dataset.extractDatum = function extractDatum(el) {
//            return $(el).data(datumKey);
//        };
//        _.mixin(Dataset.prototype, EventEmitter, {
//            _render: function render(query, suggestions) {
//                if (!this.$el) {
//                    return;
//                }
//                var that = this, hasSuggestions;
//                this.$el.empty();
//                hasSuggestions = suggestions && suggestions.length;
//                if (!hasSuggestions && this.templates.empty) {
//                    this.$el.html(getEmptyHtml()).prepend(that.templates.header ? getHeaderHtml() : null).append(that.templates.footer ? getFooterHtml() : null);
//                } else if (hasSuggestions) {
//                    this.$el.html(getSuggestionsHtml()).prepend(that.templates.header ? getHeaderHtml() : null).append(that.templates.footer ? getFooterHtml() : null);
//                }
//                this.trigger("rendered");
//                function getEmptyHtml() {
//                    return that.templates.empty({
//                        query: query,
//                        isEmpty: true
//                    });
//                }
//                function getSuggestionsHtml() {
//                    var $suggestions, nodes;
//                    $suggestions = $(html.suggestions).css(css.suggestions);
//                    nodes = _.map(suggestions, getSuggestionNode);
//                    $suggestions.append.apply($suggestions, nodes);
//                    that.highlight && highlight({
//                        className: "tt-highlight",
//                        node: $suggestions[0],
//                        pattern: query
//                    });
//                    return $suggestions;
//                    function getSuggestionNode(suggestion) {
//                        var $el;
//                        $el = $(html.suggestion).append(that.templates.suggestion(suggestion)).data(datasetKey, that.name).data(valueKey, that.displayFn(suggestion)).data(datumKey, suggestion);
//                        $el.children().each(function() {
//                            $(this).css(css.suggestionChild);
//                        });
//                        return $el;
//                    }
//                }
//                function getHeaderHtml() {
//                    return that.templates.header({
//                        query: query,
//                        isEmpty: !hasSuggestions
//                    });
//                }
//                function getFooterHtml() {
//                    return that.templates.footer({
//                        query: query,
//                        isEmpty: !hasSuggestions
//                    });
//                }
//            },
//            getRoot: function getRoot() {
//                return this.$el;
//            },
//            update: function update(query) {
//                var that = this;
//                this.query = query;
//                this.canceled = false;
//                this.source(query, render);
//                function render(suggestions) {
//                    if (!that.canceled && query === that.query) {
//                        that._render(query, suggestions);
//                    }
//                }
//            },
//            cancel: function cancel() {
//                this.canceled = true;
//            },
//            clear: function clear() {
//                this.cancel();
//                this.$el.empty();
//                this.trigger("rendered");
//            },
//            isEmpty: function isEmpty() {
//                return this.$el.is(":empty");
//            },
//            destroy: function destroy() {
//                this.$el = null;
//            }
//        });
//        return Dataset;
//        function getDisplayFn(display) {
//            display = display || "value";
//            return _.isFunction(display) ? display : displayFn;
//            function displayFn(obj) {
//                return obj[display];
//            }
//        }
//        function getTemplates(templates, displayFn) {
//            return {
//                empty: templates.empty && _.templatify(templates.empty),
//                header: templates.header && _.templatify(templates.header),
//                footer: templates.footer && _.templatify(templates.footer),
//                suggestion: templates.suggestion || suggestionTemplate
//            };
//            function suggestionTemplate(context) {
//                return "<p>" + displayFn(context) + "</p>";
//            }
//        }
//        function isValidName(str) {
//            return /^[_a-zA-Z0-9-]+$/.test(str);
//        }
//    }();
//    var Dropdown = function() {
//        "use strict";
//        function Dropdown(o) {
//            var that = this, onSuggestionClick, onSuggestionMouseEnter, onSuggestionMouseLeave;
//            o = o || {};
//            if (!o.menu) {
//                $.error("menu is required");
//            }
//            this.isOpen = false;
//            this.isEmpty = true;
//            this.datasets = _.map(o.datasets, initializeDataset);
//            onSuggestionClick = _.bind(this._onSuggestionClick, this);
//            onSuggestionMouseEnter = _.bind(this._onSuggestionMouseEnter, this);
//            onSuggestionMouseLeave = _.bind(this._onSuggestionMouseLeave, this);
//            this.$menu = $(o.menu).on("click.tt", ".tt-suggestion", onSuggestionClick).on("mouseenter.tt", ".tt-suggestion", onSuggestionMouseEnter).on("mouseleave.tt", ".tt-suggestion", onSuggestionMouseLeave);
//            _.each(this.datasets, function(dataset) {
//                that.$menu.append(dataset.getRoot());
//                dataset.onSync("rendered", that._onRendered, that);
//            });
//        }
//        _.mixin(Dropdown.prototype, EventEmitter, {
//            _onSuggestionClick: function onSuggestionClick($e) {
//                this.trigger("suggestionClicked", $($e.currentTarget));
//            },
//            _onSuggestionMouseEnter: function onSuggestionMouseEnter($e) {
//                this._removeCursor();
//                this._setCursor($($e.currentTarget), true);
//            },
//            _onSuggestionMouseLeave: function onSuggestionMouseLeave() {
//                this._removeCursor();
//            },
//            _onRendered: function onRendered() {
//                this.isEmpty = _.every(this.datasets, isDatasetEmpty);
//                this.isEmpty ? this._hide() : this.isOpen && this._show();
//                this.trigger("datasetRendered");
//                function isDatasetEmpty(dataset) {
//                    return dataset.isEmpty();
//                }
//            },
//            _hide: function() {
//                this.$menu.hide();
//            },
//            _show: function() {
//                this.$menu.css("display", "block");
//            },
//            _getSuggestions: function getSuggestions() {
//                return this.$menu.find(".tt-suggestion");
//            },
//            _getCursor: function getCursor() {
//                return this.$menu.find(".tt-cursor").first();
//            },
//            _setCursor: function setCursor($el, silent) {
//                $el.first().addClass("tt-cursor");
//                !silent && this.trigger("cursorMoved");
//            },
//            _removeCursor: function removeCursor() {
//                this._getCursor().removeClass("tt-cursor");
//            },
//            _moveCursor: function moveCursor(increment) {
//                var $suggestions, $oldCursor, newCursorIndex, $newCursor;
//                if (!this.isOpen) {
//                    return;
//                }
//                $oldCursor = this._getCursor();
//                $suggestions = this._getSuggestions();
//                this._removeCursor();
//                newCursorIndex = $suggestions.index($oldCursor) + increment;
//                newCursorIndex = (newCursorIndex + 1) % ($suggestions.length + 1) - 1;
//                if (newCursorIndex === -1) {
//                    this.trigger("cursorRemoved");
//                    return;
//                } else if (newCursorIndex < -1) {
//                    newCursorIndex = $suggestions.length - 1;
//                }
//                this._setCursor($newCursor = $suggestions.eq(newCursorIndex));
//                this._ensureVisible($newCursor);
//            },
//            _ensureVisible: function ensureVisible($el) {
//                var elTop, elBottom, menuScrollTop, menuHeight;
//                elTop = $el.position().top;
//                elBottom = elTop + $el.outerHeight(true);
//                menuScrollTop = this.$menu.scrollTop();
//                menuHeight = this.$menu.height() + parseInt(this.$menu.css("paddingTop"), 10) + parseInt(this.$menu.css("paddingBottom"), 10);
//                if (elTop < 0) {
//                    this.$menu.scrollTop(menuScrollTop + elTop);
//                } else if (menuHeight < elBottom) {
//                    this.$menu.scrollTop(menuScrollTop + (elBottom - menuHeight));
//                }
//            },
//            close: function close() {
//                if (this.isOpen) {
//                    this.isOpen = false;
//                    this._removeCursor();
//                    this._hide();
//                    this.trigger("closed");
//                }
//            },
//            open: function open() {
//                if (!this.isOpen) {
//                    this.isOpen = true;
//                    !this.isEmpty && this._show();
//                    this.trigger("opened");
//                }
//            },
//            setLanguageDirection: function setLanguageDirection(dir) {
//                this.$menu.css(dir === "ltr" ? css.ltr : css.rtl);
//            },
//            moveCursorUp: function moveCursorUp() {
//                this._moveCursor(-1);
//            },
//            moveCursorDown: function moveCursorDown() {
//                this._moveCursor(+1);
//            },
//            getDatumForSuggestion: function getDatumForSuggestion($el) {
//                var datum = null;
//                if ($el.length) {
//                    datum = {
//                        raw: Dataset.extractDatum($el),
//                        value: Dataset.extractValue($el),
//                        datasetName: Dataset.extractDatasetName($el)
//                    };
//                }
//                return datum;
//            },
//            getDatumForCursor: function getDatumForCursor() {
//                return this.getDatumForSuggestion(this._getCursor().first());
//            },
//            getDatumForTopSuggestion: function getDatumForTopSuggestion() {
//                return this.getDatumForSuggestion(this._getSuggestions().first());
//            },
//            update: function update(query) {
//                _.each(this.datasets, updateDataset);
//                function updateDataset(dataset) {
//                    dataset.update(query);
//                }
//            },
//            empty: function empty() {
//                _.each(this.datasets, clearDataset);
//                this.isEmpty = true;
//                function clearDataset(dataset) {
//                    dataset.clear();
//                }
//            },
//            isVisible: function isVisible() {
//                return this.isOpen && !this.isEmpty;
//            },
//            destroy: function destroy() {
//                this.$menu.off(".tt");
//                this.$menu = null;
//                _.each(this.datasets, destroyDataset);
//                function destroyDataset(dataset) {
//                    dataset.destroy();
//                }
//            }
//        });
//        return Dropdown;
//        function initializeDataset(oDataset) {
//            return new Dataset(oDataset);
//        }
//    }();
//    var Typeahead = function() {
//        "use strict";
//        var attrsKey = "ttAttrs";
//        function Typeahead(o) {
//            var $menu, $input, $hint;
//            o = o || {};
//            if (!o.input) {
//                $.error("missing input");
//            }
//            this.isActivated = false;
//            this.autoselect = !!o.autoselect;
//            this.minLength = _.isNumber(o.minLength) ? o.minLength : 1;
//            this.$node = buildDom(o.input, o.withHint);
//            $menu = this.$node.find(".tt-dropdown-menu");
//            $input = this.$node.find(".tt-input");
//            $hint = this.$node.find(".tt-hint");
//            $input.on("blur.tt", function($e) {
//                var active, isActive, hasActive;
//                active = document.activeElement;
//                isActive = $menu.is(active);
//                hasActive = $menu.has(active).length > 0;
//                if (_.isMsie() && (isActive || hasActive)) {
//                    $e.preventDefault();
//                    $e.stopImmediatePropagation();
//                    _.defer(function() {
//                        $input.focus();
//                    });
//                }
//            });
//            $menu.on("mousedown.tt", function($e) {
//                $e.preventDefault();
//            });
//            this.eventBus = o.eventBus || new EventBus({
//                el: $input
//            });
//            this.dropdown = new Dropdown({
//                menu: $menu,
//                datasets: o.datasets
//            }).onSync("suggestionClicked", this._onSuggestionClicked, this).onSync("cursorMoved", this._onCursorMoved, this).onSync("cursorRemoved", this._onCursorRemoved, this).onSync("opened", this._onOpened, this).onSync("closed", this._onClosed, this).onAsync("datasetRendered", this._onDatasetRendered, this);
//            this.input = new Input({
//                input: $input,
//                hint: $hint
//            }).onSync("focused", this._onFocused, this).onSync("blurred", this._onBlurred, this).onSync("enterKeyed", this._onEnterKeyed, this).onSync("tabKeyed", this._onTabKeyed, this).onSync("escKeyed", this._onEscKeyed, this).onSync("upKeyed", this._onUpKeyed, this).onSync("downKeyed", this._onDownKeyed, this).onSync("leftKeyed", this._onLeftKeyed, this).onSync("rightKeyed", this._onRightKeyed, this).onSync("queryChanged", this._onQueryChanged, this).onSync("whitespaceChanged", this._onWhitespaceChanged, this);
//            this._setLanguageDirection();
//        }
//        _.mixin(Typeahead.prototype, {
//            _onSuggestionClicked: function onSuggestionClicked(type, $el) {
//                var datum;
//                if (datum = this.dropdown.getDatumForSuggestion($el)) {
//                    this._select(datum);
//                }
//            },
//            _onCursorMoved: function onCursorMoved() {
//                var datum = this.dropdown.getDatumForCursor();
//                this.input.setInputValue(datum.value, true);
//                this.eventBus.trigger("cursorchanged", datum.raw, datum.datasetName);
//            },
//            _onCursorRemoved: function onCursorRemoved() {
//                this.input.resetInputValue();
//                this._updateHint();
//            },
//            _onDatasetRendered: function onDatasetRendered() {
//                this._updateHint();
//            },
//            _onOpened: function onOpened() {
//                this._updateHint();
//                this.eventBus.trigger("opened");
//            },
//            _onClosed: function onClosed() {
//                this.input.clearHint();
//                this.eventBus.trigger("closed");
//            },
//            _onFocused: function onFocused() {
//                this.isActivated = true;
//                this.dropdown.open();
//            },
//            _onBlurred: function onBlurred() {
//                this.isActivated = false;
//                this.dropdown.empty();
//                this.dropdown.close();
//            },
//            _onEnterKeyed: function onEnterKeyed(type, $e) {
//                var cursorDatum, topSuggestionDatum;
//                cursorDatum = this.dropdown.getDatumForCursor();
//                topSuggestionDatum = this.dropdown.getDatumForTopSuggestion();
//                if (cursorDatum) {
//                    this._select(cursorDatum);
//                    $e.preventDefault();
//                } else if (this.autoselect && topSuggestionDatum) {
//                    this._select(topSuggestionDatum);
//                    $e.preventDefault();
//                }
//            },
//            _onTabKeyed: function onTabKeyed(type, $e) {
//                var datum;
//                if (datum = this.dropdown.getDatumForCursor()) {
//                    this._select(datum);
//                    $e.preventDefault();
//                } else {
//                    this._autocomplete(true);
//                }
//            },
//            _onEscKeyed: function onEscKeyed() {
//                this.dropdown.close();
//                this.input.resetInputValue();
//            },
//            _onUpKeyed: function onUpKeyed() {
//                var query = this.input.getQuery();
//                this.dropdown.isEmpty && query.length >= this.minLength ? this.dropdown.update(query) : this.dropdown.moveCursorUp();
//                this.dropdown.open();
//            },
//            _onDownKeyed: function onDownKeyed() {
//                var query = this.input.getQuery();
//                this.dropdown.isEmpty && query.length >= this.minLength ? this.dropdown.update(query) : this.dropdown.moveCursorDown();
//                this.dropdown.open();
//            },
//            _onLeftKeyed: function onLeftKeyed() {
//                this.dir === "rtl" && this._autocomplete();
//            },
//            _onRightKeyed: function onRightKeyed() {
//                this.dir === "ltr" && this._autocomplete();
//            },
//            _onQueryChanged: function onQueryChanged(e, query) {
//                this.input.clearHintIfInvalid();
//                query.length >= this.minLength ? this.dropdown.update(query) : this.dropdown.empty();
//                this.dropdown.open();
//                this._setLanguageDirection();
//            },
//            _onWhitespaceChanged: function onWhitespaceChanged() {
//                this._updateHint();
//                this.dropdown.open();
//            },
//            _setLanguageDirection: function setLanguageDirection() {
//                var dir;
//                if (this.dir !== (dir = this.input.getLanguageDirection())) {
//                    this.dir = dir;
//                    this.$node.css("direction", dir);
//                    this.dropdown.setLanguageDirection(dir);
//                }
//            },
//            _updateHint: function updateHint() {
//                var datum, val, query, escapedQuery, frontMatchRegEx, match;
//                datum = this.dropdown.getDatumForTopSuggestion();
//                if (datum && this.dropdown.isVisible() && !this.input.hasOverflow()) {
//                    val = this.input.getInputValue();
//                    query = Input.normalizeQuery(val);
//                    escapedQuery = _.escapeRegExChars(query);
//                    frontMatchRegEx = new RegExp("^(?:" + escapedQuery + ")(.+$)", "i");
//                    match = frontMatchRegEx.exec(datum.value);
//                    match ? this.input.setHint(val + match[1]) : this.input.clearHint();
//                } else {
//                    this.input.clearHint();
//                }
//            },
//            _autocomplete: function autocomplete(laxCursor) {
//                var hint, query, isCursorAtEnd, datum;
//                hint = this.input.getHint();
//                query = this.input.getQuery();
//                isCursorAtEnd = laxCursor || this.input.isCursorAtEnd();
//                if (hint && query !== hint && isCursorAtEnd) {
//                    datum = this.dropdown.getDatumForTopSuggestion();
//                    datum && this.input.setInputValue(datum.value);
//                    this.eventBus.trigger("autocompleted", datum.raw, datum.datasetName);
//                }
//            },
//            _select: function select(datum) {
//                this.input.setQuery(datum.value);
//                this.input.setInputValue(datum.value, true);
//                this._setLanguageDirection();
//                this.eventBus.trigger("selected", datum.raw, datum.datasetName);
//                this.dropdown.close();
//                _.defer(_.bind(this.dropdown.empty, this.dropdown));
//            },
//            open: function open() {
//                this.dropdown.open();
//            },
//            close: function close() {
//                this.dropdown.close();
//            },
//            setVal: function setVal(val) {
//                val = _.toStr(val);
//                if (this.isActivated) {
//                    this.input.setInputValue(val);
//                } else {
//                    this.input.setQuery(val);
//                    this.input.setInputValue(val, true);
//                }
//                this._setLanguageDirection();
//            },
//            getVal: function getVal() {
//                return this.input.getQuery();
//            },
//            destroy: function destroy() {
//                this.input.destroy();
//                this.dropdown.destroy();
//                destroyDomStructure(this.$node);
//                this.$node = null;
//            }
//        });
//        return Typeahead;
//        function buildDom(input, withHint) {
//            var $input, $wrapper, $dropdown, $hint;
//            $input = $(input);
//            $wrapper = $(html.wrapper).css(css.wrapper);
//            $dropdown = $(html.dropdown).css(css.dropdown);
//            $hint = $input.clone().css(css.hint).css(getBackgroundStyles($input));
//            $hint.val("").removeData().addClass("tt-hint").removeAttr("id name placeholder required").prop("readonly", true).attr({
//                autocomplete: "off",
//                spellcheck: "false",
//                tabindex: -1
//            });
//            $input.data(attrsKey, {
//                dir: $input.attr("dir"),
//                autocomplete: $input.attr("autocomplete"),
//                spellcheck: $input.attr("spellcheck"),
//                style: $input.attr("style")
//            });
//            $input.addClass("tt-input").attr({
//                autocomplete: "off",
//                spellcheck: false
//            }).css(withHint ? css.input : css.inputWithNoHint);
//            try {
//                !$input.attr("dir") && $input.attr("dir", "auto");
//            } catch (e) {}
//            return $input.wrap($wrapper).parent().prepend(withHint ? $hint : null).append($dropdown);
//        }
//        function getBackgroundStyles($el) {
//            return {
//                backgroundAttachment: $el.css("background-attachment"),
//                backgroundClip: $el.css("background-clip"),
//                backgroundColor: $el.css("background-color"),
//                backgroundImage: $el.css("background-image"),
//                backgroundOrigin: $el.css("background-origin"),
//                backgroundPosition: $el.css("background-position"),
//                backgroundRepeat: $el.css("background-repeat"),
//                backgroundSize: $el.css("background-size")
//            };
//        }
//        function destroyDomStructure($node) {
//            var $input = $node.find(".tt-input");
//            _.each($input.data(attrsKey), function(val, key) {
//                _.isUndefined(val) ? $input.removeAttr(key) : $input.attr(key, val);
//            });
//            $input.detach().removeData(attrsKey).removeClass("tt-input").insertAfter($node);
//            $node.remove();
//        }
//    }();
//    (function() {
//        "use strict";
//        var old, typeaheadKey, methods;
//        old = $.fn.typeahead;
//        typeaheadKey = "ttTypeahead";
//        methods = {
//            initialize: function initialize(o, datasets) {
//                datasets = _.isArray(datasets) ? datasets : [].slice.call(arguments, 1);
//                o = o || {};
//                return this.each(attach);
//                function attach() {
//                    var $input = $(this), eventBus, typeahead;
//                    _.each(datasets, function(d) {
//                        d.highlight = !!o.highlight;
//                    });
//                    typeahead = new Typeahead({
//                        input: $input,
//                        eventBus: eventBus = new EventBus({
//                            el: $input
//                        }),
//                        withHint: _.isUndefined(o.hint) ? true : !!o.hint,
//                        minLength: o.minLength,
//                        autoselect: o.autoselect,
//                        datasets: datasets
//                    });
//                    $input.data(typeaheadKey, typeahead);
//                }
//            },
//            open: function open() {
//                return this.each(openTypeahead);
//                function openTypeahead() {
//                    var $input = $(this), typeahead;
//                    if (typeahead = $input.data(typeaheadKey)) {
//                        typeahead.open();
//                    }
//                }
//            },
//            close: function close() {
//                return this.each(closeTypeahead);
//                function closeTypeahead() {
//                    var $input = $(this), typeahead;
//                    if (typeahead = $input.data(typeaheadKey)) {
//                        typeahead.close();
//                    }
//                }
//            },
//            val: function val(newVal) {
//                return !arguments.length ? getVal(this.first()) : this.each(setVal);
//                function setVal() {
//                    var $input = $(this), typeahead;
//                    if (typeahead = $input.data(typeaheadKey)) {
//                        typeahead.setVal(newVal);
//                    }
//                }
//                function getVal($input) {
//                    var typeahead, query;
//                    if (typeahead = $input.data(typeaheadKey)) {
//                        query = typeahead.getVal();
//                    }
//                    return query;
//                }
//            },
//            destroy: function destroy() {
//                return this.each(unattach);
//                function unattach() {
//                    var $input = $(this), typeahead;
//                    if (typeahead = $input.data(typeaheadKey)) {
//                        typeahead.destroy();
//                        $input.removeData(typeaheadKey);
//                    }
//                }
//            }
//        };
//        $.fn.typeahead = function(method) {
//            var tts;
//            if (methods[method] && method !== "initialize") {
//                tts = this.filter(function() {
//                    return !!$(this).data(typeaheadKey);
//                });
//                return methods[method].apply(tts, [].slice.call(arguments, 1));
//            } else {
//                return methods.initialize.apply(this, arguments);
//            }
//        };
//        $.fn.typeahead.noConflict = function noConflict() {
//            $.fn.typeahead = old;
//            return this;
//        };
//    })();
//})(window.jQuery);

/*!
 * typeahead.js 0.11.1
 * https://github.com/twitter/typeahead.js
 * Copyright 2013-2015 Twitter, Inc. and other contributors; Licensed MIT
 */

(function (root, factory) {
    if (typeof define === "function" && define.amd) {
        define("bloodhound", ["jquery"], function (a0) {
            return root["Bloodhound"] = factory(a0);
        });
    } else if (typeof exports === "object") {
        module.exports = factory(require("jquery"));
    } else {
        root["Bloodhound"] = factory(jQuery);
    }
})(this, function ($) {
    var _ = function () {
        "use strict";
        return {
            isMsie: function () {
                return /(msie|trident)/i.test(navigator.userAgent) ? navigator.userAgent.match(/(msie |rv:)(\d+(.\d+)?)/i)[2] : false;
            },
            isBlankString: function (str) {
                return !str || /^\s*$/.test(str);
            },
            escapeRegExChars: function (str) {
                return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
            },
            isString: function (obj) {
                return typeof obj === "string";
            },
            isNumber: function (obj) {
                return typeof obj === "number";
            },
            isArray: $.isArray,
            isFunction: $.isFunction,
            isObject: $.isPlainObject,
            isUndefined: function (obj) {
                return typeof obj === "undefined";
            },
            isElement: function (obj) {
                return !!(obj && obj.nodeType === 1);
            },
            isJQuery: function (obj) {
                return obj instanceof $;
            },
            toStr: function toStr(s) {
                return _.isUndefined(s) || s === null ? "" : s + "";
            },
            bind: $.proxy,
            each: function (collection, cb) {
                $.each(collection, reverseArgs);
                function reverseArgs(index, value) {
                    return cb(value, index);
                }
            },
            map: $.map,
            filter: $.grep,
            every: function (obj, test) {
                var result = true;
                if (!obj) {
                    return result;
                }
                $.each(obj, function (key, val) {
                    if (!(result = test.call(null, val, key, obj))) {
                        return false;
                    }
                });
                return !!result;
            },
            some: function (obj, test) {
                var result = false;
                if (!obj) {
                    return result;
                }
                $.each(obj, function (key, val) {
                    if (result = test.call(null, val, key, obj)) {
                        return false;
                    }
                });
                return !!result;
            },
            mixin: $.extend,
            identity: function (x) {
                return x;
            },
            clone: function (obj) {
                return $.extend(true, {}, obj);
            },
            getIdGenerator: function () {
                var counter = 0;
                return function () {
                    return counter++;
                };
            },
            templatify: function templatify(obj) {
                return $.isFunction(obj) ? obj : template;
                function template() {
                    return String(obj);
                }
            },
            defer: function (fn) {
                setTimeout(fn, 0);
            },
            debounce: function (func, wait, immediate) {
                var timeout, result;
                return function () {
                    var context = this, args = arguments, later, callNow;
                    later = function () {
                        timeout = null;
                        if (!immediate) {
                            result = func.apply(context, args);
                        }
                    };
                    callNow = immediate && !timeout;
                    clearTimeout(timeout);
                    timeout = setTimeout(later, wait);
                    if (callNow) {
                        result = func.apply(context, args);
                    }
                    return result;
                };
            },
            throttle: function (func, wait) {
                var context, args, timeout, result, previous, later;
                previous = 0;
                later = function () {
                    previous = new Date();
                    timeout = null;
                    result = func.apply(context, args);
                };
                return function () {
                    var now = new Date(), remaining = wait - (now - previous);
                    context = this;
                    args = arguments;
                    if (remaining <= 0) {
                        clearTimeout(timeout);
                        timeout = null;
                        previous = now;
                        result = func.apply(context, args);
                    } else if (!timeout) {
                        timeout = setTimeout(later, remaining);
                    }
                    return result;
                };
            },
            stringify: function (val) {
                return _.isString(val) ? val : JSON.stringify(val);
            },
            noop: function () { }
        };
    }();
    var VERSION = "0.11.1";
    var tokenizers = function () {
        "use strict";
        return {
            nonword: nonword,
            whitespace: whitespace,
            obj: {
                nonword: getObjTokenizer(nonword),
                whitespace: getObjTokenizer(whitespace)
            }
        };
        function whitespace(str) {
            str = _.toStr(str);
            return str ? str.split(/\s+/) : [];
        }
        function nonword(str) {
            str = _.toStr(str);
            return str ? str.split(/\W+/) : [];
        }
        function getObjTokenizer(tokenizer) {
            return function setKey(keys) {
                keys = _.isArray(keys) ? keys : [].slice.call(arguments, 0);
                return function tokenize(o) {
                    var tokens = [];
                    _.each(keys, function (k) {
                        tokens = tokens.concat(tokenizer(_.toStr(o[k])));
                    });
                    return tokens;
                };
            };
        }
    }();
    var LruCache = function () {
        "use strict";
        function LruCache(maxSize) {
            this.maxSize = _.isNumber(maxSize) ? maxSize : 100;
            this.reset();
            if (this.maxSize <= 0) {
                this.set = this.get = $.noop;
            }
        }
        _.mixin(LruCache.prototype, {
            set: function set(key, val) {
                var tailItem = this.list.tail, node;
                if (this.size >= this.maxSize) {
                    this.list.remove(tailItem);
                    delete this.hash[tailItem.key];
                    this.size--;
                }
                if (node = this.hash[key]) {
                    node.val = val;
                    this.list.moveToFront(node);
                } else {
                    node = new Node(key, val);
                    this.list.add(node);
                    this.hash[key] = node;
                    this.size++;
                }
            },
            get: function get(key) {
                var node = this.hash[key];
                if (node) {
                    this.list.moveToFront(node);
                    return node.val;
                }
            },
            reset: function reset() {
                this.size = 0;
                this.hash = {};
                this.list = new List();
            }
        });
        function List() {
            this.head = this.tail = null;
        }
        _.mixin(List.prototype, {
            add: function add(node) {
                if (this.head) {
                    node.next = this.head;
                    this.head.prev = node;
                }
                this.head = node;
                this.tail = this.tail || node;
            },
            remove: function remove(node) {
                node.prev ? node.prev.next = node.next : this.head = node.next;
                node.next ? node.next.prev = node.prev : this.tail = node.prev;
            },
            moveToFront: function (node) {
                this.remove(node);
                this.add(node);
            }
        });
        function Node(key, val) {
            this.key = key;
            this.val = val;
            this.prev = this.next = null;
        }
        return LruCache;
    }();
    var PersistentStorage = function () {
        "use strict";
        var LOCAL_STORAGE;
        try {
            LOCAL_STORAGE = window.localStorage;
            LOCAL_STORAGE.setItem("~~~", "!");
            LOCAL_STORAGE.removeItem("~~~");
        } catch (err) {
            LOCAL_STORAGE = null;
        }
        function PersistentStorage(namespace, override) {
            this.prefix = ["__", namespace, "__"].join("");
            this.ttlKey = "__ttl__";
            this.keyMatcher = new RegExp("^" + _.escapeRegExChars(this.prefix));
            this.ls = override || LOCAL_STORAGE;
            !this.ls && this._noop();
        }
        _.mixin(PersistentStorage.prototype, {
            _prefix: function (key) {
                return this.prefix + key;
            },
            _ttlKey: function (key) {
                return this._prefix(key) + this.ttlKey;
            },
            _noop: function () {
                this.get = this.set = this.remove = this.clear = this.isExpired = _.noop;
            },
            _safeSet: function (key, val) {
                try {
                    this.ls.setItem(key, val);
                } catch (err) {
                    if (err.name === "QuotaExceededError") {
                        this.clear();
                        this._noop();
                    }
                }
            },
            get: function (key) {
                if (this.isExpired(key)) {
                    this.remove(key);
                }
                return decode(this.ls.getItem(this._prefix(key)));
            },
            set: function (key, val, ttl) {
                if (_.isNumber(ttl)) {
                    this._safeSet(this._ttlKey(key), encode(now() + ttl));
                } else {
                    this.ls.removeItem(this._ttlKey(key));
                }
                return this._safeSet(this._prefix(key), encode(val));
            },
            remove: function (key) {
                this.ls.removeItem(this._ttlKey(key));
                this.ls.removeItem(this._prefix(key));
                return this;
            },
            clear: function () {
                var i, keys = gatherMatchingKeys(this.keyMatcher);
                for (i = keys.length; i--;) {
                    this.remove(keys[i]);
                }
                return this;
            },
            isExpired: function (key) {
                var ttl = decode(this.ls.getItem(this._ttlKey(key)));
                return _.isNumber(ttl) && now() > ttl ? true : false;
            }
        });
        return PersistentStorage;
        function now() {
            return new Date().getTime();
        }
        function encode(val) {
            return JSON.stringify(_.isUndefined(val) ? null : val);
        }
        function decode(val) {
            return $.parseJSON(val);
        }
        function gatherMatchingKeys(keyMatcher) {
            var i, key, keys = [], len = LOCAL_STORAGE.length;
            for (i = 0; i < len; i++) {
                if ((key = LOCAL_STORAGE.key(i)).match(keyMatcher)) {
                    keys.push(key.replace(keyMatcher, ""));
                }
            }
            return keys;
        }
    }();
    var Transport = function () {
        "use strict";
        var pendingRequestsCount = 0, pendingRequests = {}, maxPendingRequests = 6, sharedCache = new LruCache(10);
        function Transport(o) {
            o = o || {};
            this.cancelled = false;
            this.lastReq = null;
            this._send = o.transport;
            this._get = o.limiter ? o.limiter(this._get) : this._get;
            this._cache = o.cache === false ? new LruCache(0) : sharedCache;
        }
        Transport.setMaxPendingRequests = function setMaxPendingRequests(num) {
            maxPendingRequests = num;
        };
        Transport.resetCache = function resetCache() {
            sharedCache.reset();
        };
        _.mixin(Transport.prototype, {
            _fingerprint: function fingerprint(o) {
                o = o || {};
                return o.url + o.type + $.param(o.data || {});
            },
            _get: function (o, cb) {
                var that = this, fingerprint, jqXhr;
                fingerprint = this._fingerprint(o);
                if (this.cancelled || fingerprint !== this.lastReq) {
                    return;
                }
                if (jqXhr = pendingRequests[fingerprint]) {
                    jqXhr.done(done).fail(fail);
                } else if (pendingRequestsCount < maxPendingRequests) {
                    pendingRequestsCount++;
                    pendingRequests[fingerprint] = this._send(o).done(done).fail(fail).always(always);
                } else {
                    this.onDeckRequestArgs = [].slice.call(arguments, 0);
                }
                function done(resp) {
                    cb(null, resp);
                    that._cache.set(fingerprint, resp);
                }
                function fail() {
                    cb(true);
                }
                function always() {
                    pendingRequestsCount--;
                    delete pendingRequests[fingerprint];
                    if (that.onDeckRequestArgs) {
                        that._get.apply(that, that.onDeckRequestArgs);
                        that.onDeckRequestArgs = null;
                    }
                }
            },
            get: function (o, cb) {
                var resp, fingerprint;
                cb = cb || $.noop;
                o = _.isString(o) ? {
                    url: o
                } : o || {};
                fingerprint = this._fingerprint(o);
                this.cancelled = false;
                this.lastReq = fingerprint;
                if (resp = this._cache.get(fingerprint)) {
                    cb(null, resp);
                } else {
                    this._get(o, cb);
                }
            },
            cancel: function () {
                this.cancelled = true;
            }
        });
        return Transport;
    }();
    var SearchIndex = window.SearchIndex = function () {
        "use strict";
        var CHILDREN = "c", IDS = "i";
        function SearchIndex(o) {
            o = o || {};
            if (!o.datumTokenizer || !o.queryTokenizer) {
                $.error("datumTokenizer and queryTokenizer are both required");
            }
            this.identify = o.identify || _.stringify;
            this.datumTokenizer = o.datumTokenizer;
            this.queryTokenizer = o.queryTokenizer;
            this.reset();
        }
        _.mixin(SearchIndex.prototype, {
            bootstrap: function bootstrap(o) {
                this.datums = o.datums;
                this.trie = o.trie;
            },
            add: function (data) {
                var that = this;
                data = _.isArray(data) ? data : [data];
                _.each(data, function (datum) {
                    var id, tokens;
                    that.datums[id = that.identify(datum)] = datum;
                    tokens = normalizeTokens(that.datumTokenizer(datum));
                    _.each(tokens, function (token) {
                        var node, chars, ch;
                        node = that.trie;
                        chars = token.split("");
                        while (ch = chars.shift()) {
                            node = node[CHILDREN][ch] || (node[CHILDREN][ch] = newNode());
                            node[IDS].push(id);
                        }
                    });
                });
            },
            get: function get(ids) {
                var that = this;
                return _.map(ids, function (id) {
                    return that.datums[id];
                });
            },
            search: function search(query) {
                var that = this, tokens, matches;
                tokens = normalizeTokens(this.queryTokenizer(query));
                _.each(tokens, function (token) {
                    var node, chars, ch, ids;
                    if (matches && matches.length === 0) {
                        return false;
                    }
                    node = that.trie;
                    chars = token.split("");
                    while (node && (ch = chars.shift())) {
                        node = node[CHILDREN][ch];
                    }
                    if (node && chars.length === 0) {
                        ids = node[IDS].slice(0);
                        matches = matches ? getIntersection(matches, ids) : ids;
                    } else {
                        matches = [];
                        return false;
                    }
                });
                return matches ? _.map(unique(matches), function (id) {
                    return that.datums[id];
                }) : [];
            },
            all: function all() {
                var values = [];
                for (var key in this.datums) {
                    values.push(this.datums[key]);
                }
                return values;
            },
            reset: function reset() {
                this.datums = {};
                this.trie = newNode();
            },
            serialize: function serialize() {
                return {
                    datums: this.datums,
                    trie: this.trie
                };
            }
        });
        return SearchIndex;
        function normalizeTokens(tokens) {
            tokens = _.filter(tokens, function (token) {
                return !!token;
            });
            tokens = _.map(tokens, function (token) {
                return token.toLowerCase();
            });
            return tokens;
        }
        function newNode() {
            var node = {};
            node[IDS] = [];
            node[CHILDREN] = {};
            return node;
        }
        function unique(array) {
            var seen = {}, uniques = [];
            for (var i = 0, len = array.length; i < len; i++) {
                if (!seen[array[i]]) {
                    seen[array[i]] = true;
                    uniques.push(array[i]);
                }
            }
            return uniques;
        }
        function getIntersection(arrayA, arrayB) {
            var ai = 0, bi = 0, intersection = [];
            arrayA = arrayA.sort();
            arrayB = arrayB.sort();
            var lenArrayA = arrayA.length, lenArrayB = arrayB.length;
            while (ai < lenArrayA && bi < lenArrayB) {
                if (arrayA[ai] < arrayB[bi]) {
                    ai++;
                } else if (arrayA[ai] > arrayB[bi]) {
                    bi++;
                } else {
                    intersection.push(arrayA[ai]);
                    ai++;
                    bi++;
                }
            }
            return intersection;
        }
    }();
    var Prefetch = function () {
        "use strict";
        var keys;
        keys = {
            data: "data",
            protocol: "protocol",
            thumbprint: "thumbprint"
        };
        function Prefetch(o) {
            this.url = o.url;
            this.ttl = o.ttl;
            this.cache = o.cache;
            this.prepare = o.prepare;
            this.transform = o.transform;
            this.transport = o.transport;
            this.thumbprint = o.thumbprint;
            this.storage = new PersistentStorage(o.cacheKey);
        }
        _.mixin(Prefetch.prototype, {
            _settings: function settings() {
                return {
                    url: this.url,
                    type: "GET",
                    dataType: "json"
                };
            },
            store: function store(data) {
                if (!this.cache) {
                    return;
                }
                this.storage.set(keys.data, data, this.ttl);
                this.storage.set(keys.protocol, location.protocol, this.ttl);
                this.storage.set(keys.thumbprint, this.thumbprint, this.ttl);
            },
            fromCache: function fromCache() {
                var stored = {}, isExpired;
                if (!this.cache) {
                    return null;
                }
                stored.data = this.storage.get(keys.data);
                stored.protocol = this.storage.get(keys.protocol);
                stored.thumbprint = this.storage.get(keys.thumbprint);
                isExpired = stored.thumbprint !== this.thumbprint || stored.protocol !== location.protocol;
                return stored.data && !isExpired ? stored.data : null;
            },
            fromNetwork: function (cb) {
                var that = this, settings;
                if (!cb) {
                    return;
                }
                settings = this.prepare(this._settings());
                this.transport(settings).fail(onError).done(onResponse);
                function onError() {
                    cb(true);
                }
                function onResponse(resp) {
                    cb(null, that.transform(resp));
                }
            },
            clear: function clear() {
                this.storage.clear();
                return this;
            }
        });
        return Prefetch;
    }();
    var Remote = function () {
        "use strict";
        function Remote(o) {
            this.url = o.url;
            this.prepare = o.prepare;
            this.transform = o.transform;
            this.transport = new Transport({
                cache: o.cache,
                limiter: o.limiter,
                transport: o.transport
            });
        }
        _.mixin(Remote.prototype, {
            _settings: function settings() {
                return {
                    url: this.url,
                    type: "GET",
                    dataType: "json"
                };
            },
            get: function get(query, cb) {
                var that = this, settings;
                if (!cb) {
                    return;
                }
                query = query || "";
                settings = this.prepare(query, this._settings());
                return this.transport.get(settings, onResponse);
                function onResponse(err, resp) {
                    err ? cb([]) : cb(that.transform(resp));
                }
            },
            cancelLastRequest: function cancelLastRequest() {
                this.transport.cancel();
            }
        });
        return Remote;
    }();
    var oParser = function () {
        "use strict";
        return function parse(o) {
            var defaults, sorter;
            defaults = {
                initialize: true,
                identify: _.stringify,
                datumTokenizer: null,
                queryTokenizer: null,
                sufficient: 5,
                sorter: null,
                local: [],
                prefetch: null,
                remote: null
            };
            o = _.mixin(defaults, o || {});
            !o.datumTokenizer && $.error("datumTokenizer is required");
            !o.queryTokenizer && $.error("queryTokenizer is required");
            sorter = o.sorter;
            o.sorter = sorter ? function (x) {
                return x.sort(sorter);
            } : _.identity;
            o.local = _.isFunction(o.local) ? o.local() : o.local;
            o.prefetch = parsePrefetch(o.prefetch);
            o.remote = parseRemote(o.remote);
            return o;
        };
        function parsePrefetch(o) {
            var defaults;
            if (!o) {
                return null;
            }
            defaults = {
                url: null,
                ttl: 24 * 60 * 60 * 1e3,
                cache: true,
                cacheKey: null,
                thumbprint: "",
                prepare: _.identity,
                transform: _.identity,
                transport: null
            };
            o = _.isString(o) ? {
                url: o
            } : o;
            o = _.mixin(defaults, o);
            !o.url && $.error("prefetch requires url to be set");
            o.transform = o.filter || o.transform;
            o.cacheKey = o.cacheKey || o.url;
            o.thumbprint = VERSION + o.thumbprint;
            o.transport = o.transport ? callbackToDeferred(o.transport) : $.ajax;
            return o;
        }
        function parseRemote(o) {
            var defaults;
            if (!o) {
                return;
            }
            defaults = {
                url: null,
                cache: true,
                prepare: null,
                replace: null,
                wildcard: null,
                limiter: null,
                rateLimitBy: "debounce",
                rateLimitWait: 300,
                transform: _.identity,
                transport: null
            };
            o = _.isString(o) ? {
                url: o
            } : o;
            o = _.mixin(defaults, o);
            !o.url && $.error("remote requires url to be set");
            o.transform = o.filter || o.transform;
            o.prepare = toRemotePrepare(o);
            o.limiter = toLimiter(o);
            o.transport = o.transport ? callbackToDeferred(o.transport) : $.ajax;
            delete o.replace;
            delete o.wildcard;
            delete o.rateLimitBy;
            delete o.rateLimitWait;
            return o;
        }
        function toRemotePrepare(o) {
            var prepare, replace, wildcard;
            prepare = o.prepare;
            replace = o.replace;
            wildcard = o.wildcard;
            if (prepare) {
                return prepare;
            }
            if (replace) {
                prepare = prepareByReplace;
            } else if (o.wildcard) {
                prepare = prepareByWildcard;
            } else {
                prepare = idenityPrepare;
            }
            return prepare;
            function prepareByReplace(query, settings) {
                settings.url = replace(settings.url, query);
                return settings;
            }
            function prepareByWildcard(query, settings) {
                settings.url = settings.url.replace(wildcard, encodeURIComponent(query));
                return settings;
            }
            function idenityPrepare(query, settings) {
                return settings;
            }
        }
        function toLimiter(o) {
            var limiter, method, wait;
            limiter = o.limiter;
            method = o.rateLimitBy;
            wait = o.rateLimitWait;
            if (!limiter) {
                limiter = /^throttle$/i.test(method) ? throttle(wait) : debounce(wait);
            }
            return limiter;
            function debounce(wait) {
                return function debounce(fn) {
                    return _.debounce(fn, wait);
                };
            }
            function throttle(wait) {
                return function throttle(fn) {
                    return _.throttle(fn, wait);
                };
            }
        }
        function callbackToDeferred(fn) {
            return function wrapper(o) {
                var deferred = $.Deferred();
                fn(o, onSuccess, onError);
                return deferred;
                function onSuccess(resp) {
                    _.defer(function () {
                        deferred.resolve(resp);
                    });
                }
                function onError(err) {
                    _.defer(function () {
                        deferred.reject(err);
                    });
                }
            };
        }
    }();
    var Bloodhound = function () {
        "use strict";
        var old;
        old = window && window.Bloodhound;
        function Bloodhound(o) {
            o = oParser(o);
            this.sorter = o.sorter;
            this.identify = o.identify;
            this.sufficient = o.sufficient;
            this.local = o.local;
            this.remote = o.remote ? new Remote(o.remote) : null;
            this.prefetch = o.prefetch ? new Prefetch(o.prefetch) : null;
            this.index = new SearchIndex({
                identify: this.identify,
                datumTokenizer: o.datumTokenizer,
                queryTokenizer: o.queryTokenizer
            });
            o.initialize !== false && this.initialize();
        }
        Bloodhound.noConflict = function noConflict() {
            window && (window.Bloodhound = old);
            return Bloodhound;
        };
        Bloodhound.tokenizers = tokenizers;
        _.mixin(Bloodhound.prototype, {
            __ttAdapter: function ttAdapter() {
                var that = this;
                return this.remote ? withAsync : withoutAsync;
                function withAsync(query, sync, async) {
                    return that.search(query, sync, async);
                }
                function withoutAsync(query, sync) {
                    return that.search(query, sync);
                }
            },
            _loadPrefetch: function loadPrefetch() {
                var that = this, deferred, serialized;
                deferred = $.Deferred();
                if (!this.prefetch) {
                    deferred.resolve();
                } else if (serialized = this.prefetch.fromCache()) {
                    this.index.bootstrap(serialized);
                    deferred.resolve();
                } else {
                    this.prefetch.fromNetwork(done);
                }
                return deferred.promise();
                function done(err, data) {
                    if (err) {
                        return deferred.reject();
                    }
                    that.add(data);
                    that.prefetch.store(that.index.serialize());
                    deferred.resolve();
                }
            },
            _initialize: function initialize() {
                var that = this, deferred;
                this.clear();
                (this.initPromise = this._loadPrefetch()).done(addLocalToIndex);
                return this.initPromise;
                function addLocalToIndex() {
                    that.add(that.local);
                }
            },
            initialize: function initialize(force) {
                return !this.initPromise || force ? this._initialize() : this.initPromise;
            },
            add: function add(data) {
                this.index.add(data);
                return this;
            },
            get: function get(ids) {
                ids = _.isArray(ids) ? ids : [].slice.call(arguments);
                return this.index.get(ids);
            },
            search: function search(query, sync, async) {
                var that = this, local;
                local = this.sorter(this.index.search(query));
                sync(this.remote ? local.slice() : local);
                if (this.remote && local.length < this.sufficient) {
                    this.remote.get(query, processRemote);
                } else if (this.remote) {
                    this.remote.cancelLastRequest();
                }
                return this;
                function processRemote(remote) {
                    var nonDuplicates = [];
                    _.each(remote, function (r) {
                        !_.some(local, function (l) {
                            return that.identify(r) === that.identify(l);
                        }) && nonDuplicates.push(r);
                    });
                    async && async(nonDuplicates);
                }
            },
            all: function all() {
                return this.index.all();
            },
            clear: function clear() {
                this.index.reset();
                return this;
            },
            clearPrefetchCache: function clearPrefetchCache() {
                this.prefetch && this.prefetch.clear();
                return this;
            },
            clearRemoteCache: function clearRemoteCache() {
                Transport.resetCache();
                return this;
            },
            ttAdapter: function ttAdapter() {
                return this.__ttAdapter();
            }
        });
        return Bloodhound;
    }();
    return Bloodhound;
});

(function (root, factory) {
    if (typeof define === "function" && define.amd) {
        define("typeahead.js", ["jquery"], function (a0) {
            return factory(a0);
        });
    } else if (typeof exports === "object") {
        module.exports = factory(require("jquery"));
    } else {
        factory(jQuery);
    }
})(this, function ($) {
    var _ = function () {
        "use strict";
        return {
            isMsie: function () {
                return /(msie|trident)/i.test(navigator.userAgent) ? navigator.userAgent.match(/(msie |rv:)(\d+(.\d+)?)/i)[2] : false;
            },
            isBlankString: function (str) {
                return !str || /^\s*$/.test(str);
            },
            escapeRegExChars: function (str) {
                return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
            },
            isString: function (obj) {
                return typeof obj === "string";
            },
            isNumber: function (obj) {
                return typeof obj === "number";
            },
            isArray: $.isArray,
            isFunction: $.isFunction,
            isObject: $.isPlainObject,
            isUndefined: function (obj) {
                return typeof obj === "undefined";
            },
            isElement: function (obj) {
                return !!(obj && obj.nodeType === 1);
            },
            isJQuery: function (obj) {
                return obj instanceof $;
            },
            toStr: function toStr(s) {
                return _.isUndefined(s) || s === null ? "" : s + "";
            },
            bind: $.proxy,
            each: function (collection, cb) {
                $.each(collection, reverseArgs);
                function reverseArgs(index, value) {
                    return cb(value, index);
                }
            },
            map: $.map,
            filter: $.grep,
            every: function (obj, test) {
                var result = true;
                if (!obj) {
                    return result;
                }
                $.each(obj, function (key, val) {
                    if (!(result = test.call(null, val, key, obj))) {
                        return false;
                    }
                });
                return !!result;
            },
            some: function (obj, test) {
                var result = false;
                if (!obj) {
                    return result;
                }
                $.each(obj, function (key, val) {
                    if (result = test.call(null, val, key, obj)) {
                        return false;
                    }
                });
                return !!result;
            },
            mixin: $.extend,
            identity: function (x) {
                return x;
            },
            clone: function (obj) {
                return $.extend(true, {}, obj);
            },
            getIdGenerator: function () {
                var counter = 0;
                return function () {
                    return counter++;
                };
            },
            templatify: function templatify(obj) {
                return $.isFunction(obj) ? obj : template;
                function template() {
                    return String(obj);
                }
            },
            defer: function (fn) {
                setTimeout(fn, 0);
            },
            debounce: function (func, wait, immediate) {
                var timeout, result;
                return function () {
                    var context = this, args = arguments, later, callNow;
                    later = function () {
                        timeout = null;
                        if (!immediate) {
                            result = func.apply(context, args);
                        }
                    };
                    callNow = immediate && !timeout;
                    clearTimeout(timeout);
                    timeout = setTimeout(later, wait);
                    if (callNow) {
                        result = func.apply(context, args);
                    }
                    return result;
                };
            },
            throttle: function (func, wait) {
                var context, args, timeout, result, previous, later;
                previous = 0;
                later = function () {
                    previous = new Date();
                    timeout = null;
                    result = func.apply(context, args);
                };
                return function () {
                    var now = new Date(), remaining = wait - (now - previous);
                    context = this;
                    args = arguments;
                    if (remaining <= 0) {
                        clearTimeout(timeout);
                        timeout = null;
                        previous = now;
                        result = func.apply(context, args);
                    } else if (!timeout) {
                        timeout = setTimeout(later, remaining);
                    }
                    return result;
                };
            },
            stringify: function (val) {
                return _.isString(val) ? val : JSON.stringify(val);
            },
            noop: function () { }
        };
    }();
    var WWW = function () {
        "use strict";
        var defaultClassNames = {
            wrapper: "twitter-typeahead",
            input: "tt-input",
            hint: "tt-hint",
            menu: "tt-menu",
            dataset: "tt-dataset",
            suggestion: "tt-suggestion",
            selectable: "tt-selectable",
            empty: "tt-empty",
            open: "tt-open",
            cursor: "tt-cursor",
            highlight: "tt-highlight"
        };
        return build;
        function build(o) {
            var www, classes;
            classes = _.mixin({}, defaultClassNames, o);
            www = {
                css: buildCss(),
                classes: classes,
                html: buildHtml(classes),
                selectors: buildSelectors(classes)
            };
            return {
                css: www.css,
                html: www.html,
                classes: www.classes,
                selectors: www.selectors,
                mixin: function (o) {
                    _.mixin(o, www);
                }
            };
        }
        function buildHtml(c) {
            return {
                wrapper: '<span class="' + c.wrapper + '"></span>',
                menu: '<div class="' + c.menu + '"></div>'
            };
        }
        function buildSelectors(classes) {
            var selectors = {};
            _.each(classes, function (v, k) {
                selectors[k] = "." + v;
            });
            return selectors;
        }
        function buildCss() {
            var css = {
                wrapper: {
                    position: "relative",
                    display: "inline-block"
                },
                hint: {
                    position: "absolute",
                    top: "0",
                    left: "0",
                    borderColor: "transparent",
                    boxShadow: "none",
                    opacity: "1"
                },
                input: {
                    position: "relative",
                    verticalAlign: "top",
                    backgroundColor: "transparent"
                },
                inputWithNoHint: {
                    position: "relative",
                    verticalAlign: "top"
                },
                menu: {
                    position: "absolute",
                    top: "100%",
                    left: "0",
                    zIndex: "100",
                    display: "none"
                },
                ltr: {
                    left: "0",
                    right: "auto"
                },
                rtl: {
                    left: "auto",
                    right: " 0"
                }
            };
            if (_.isMsie()) {
                _.mixin(css.input, {
                    backgroundImage: "url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7)"
                });
            }
            return css;
        }
    }();
    var EventBus = function () {
        "use strict";
        var namespace, deprecationMap;
        namespace = "typeahead:";
        deprecationMap = {
            render: "rendered",
            cursorchange: "cursorchanged",
            select: "selected",
            autocomplete: "autocompleted"
        };
        function EventBus(o) {
            if (!o || !o.el) {
                $.error("EventBus initialized without el");
            }
            this.$el = $(o.el);
        }
        _.mixin(EventBus.prototype, {
            _trigger: function (type, args) {
                var $e;
                $e = $.Event(namespace + type);
                (args = args || []).unshift($e);
                this.$el.trigger.apply(this.$el, args);
                return $e;
            },
            before: function (type) {
                var args, $e;
                args = [].slice.call(arguments, 1);
                $e = this._trigger("before" + type, args);
                return $e.isDefaultPrevented();
            },
            trigger: function (type) {
                var deprecatedType;
                this._trigger(type, [].slice.call(arguments, 1));
                if (deprecatedType = deprecationMap[type]) {
                    this._trigger(deprecatedType, [].slice.call(arguments, 1));
                }
            }
        });
        return EventBus;
    }();
    var EventEmitter = function () {
        "use strict";
        var splitter = /\s+/, nextTick = getNextTick();
        return {
            onSync: onSync,
            onAsync: onAsync,
            off: off,
            trigger: trigger
        };
        function on(method, types, cb, context) {
            var type;
            if (!cb) {
                return this;
            }
            types = types.split(splitter);
            cb = context ? bindContext(cb, context) : cb;
            this._callbacks = this._callbacks || {};
            while (type = types.shift()) {
                this._callbacks[type] = this._callbacks[type] || {
                    sync: [],
                    async: []
                };
                this._callbacks[type][method].push(cb);
            }
            return this;
        }
        function onAsync(types, cb, context) {
            return on.call(this, "async", types, cb, context);
        }
        function onSync(types, cb, context) {
            return on.call(this, "sync", types, cb, context);
        }
        function off(types) {
            var type;
            if (!this._callbacks) {
                return this;
            }
            types = types.split(splitter);
            while (type = types.shift()) {
                delete this._callbacks[type];
            }
            return this;
        }
        function trigger(types) {
            var type, callbacks, args, syncFlush, asyncFlush;
            if (!this._callbacks) {
                return this;
            }
            types = types.split(splitter);
            args = [].slice.call(arguments, 1);
            while ((type = types.shift()) && (callbacks = this._callbacks[type])) {
                syncFlush = getFlush(callbacks.sync, this, [type].concat(args));
                asyncFlush = getFlush(callbacks.async, this, [type].concat(args));
                syncFlush() && nextTick(asyncFlush);
            }
            return this;
        }
        function getFlush(callbacks, context, args) {
            return flush;
            function flush() {
                var cancelled;
                for (var i = 0, len = callbacks.length; !cancelled && i < len; i += 1) {
                    cancelled = callbacks[i].apply(context, args) === false;
                }
                return !cancelled;
            }
        }
        function getNextTick() {
            var nextTickFn;
            if (window.setImmediate) {
                nextTickFn = function nextTickSetImmediate(fn) {
                    setImmediate(function () {
                        fn();
                    });
                };
            } else {
                nextTickFn = function nextTickSetTimeout(fn) {
                    setTimeout(function () {
                        fn();
                    }, 0);
                };
            }
            return nextTickFn;
        }
        function bindContext(fn, context) {
            return fn.bind ? fn.bind(context) : function () {
                fn.apply(context, [].slice.call(arguments, 0));
            };
        }
    }();
    var highlight = function (doc) {
        "use strict";
        var defaults = {
            node: null,
            pattern: null,
            tagName: "strong",
            className: null,
            wordsOnly: false,
            caseSensitive: false
        };
        return function hightlight(o) {
            var regex;
            o = _.mixin({}, defaults, o);
            if (!o.node || !o.pattern) {
                return;
            }
            o.pattern = _.isArray(o.pattern) ? o.pattern : [o.pattern];
            regex = getRegex(o.pattern, o.caseSensitive, o.wordsOnly);
            traverse(o.node, hightlightTextNode);
            function hightlightTextNode(textNode) {
                var match, patternNode, wrapperNode;
                if (match = regex.exec(textNode.data)) {
                    wrapperNode = doc.createElement(o.tagName);
                    o.className && (wrapperNode.className = o.className);
                    patternNode = textNode.splitText(match.index);
                    patternNode.splitText(match[0].length);
                    wrapperNode.appendChild(patternNode.cloneNode(true));
                    textNode.parentNode.replaceChild(wrapperNode, patternNode);
                }
                return !!match;
            }
            function traverse(el, hightlightTextNode) {
                var childNode, TEXT_NODE_TYPE = 3;
                for (var i = 0; i < el.childNodes.length; i++) {
                    childNode = el.childNodes[i];
                    if (childNode.nodeType === TEXT_NODE_TYPE) {
                        i += hightlightTextNode(childNode) ? 1 : 0;
                    } else {
                        traverse(childNode, hightlightTextNode);
                    }
                }
            }
        };
        function getRegex(patterns, caseSensitive, wordsOnly) {
            var escapedPatterns = [], regexStr;
            for (var i = 0, len = patterns.length; i < len; i++) {
                escapedPatterns.push(_.escapeRegExChars(patterns[i]));
            }
            regexStr = wordsOnly ? "\\b(" + escapedPatterns.join("|") + ")\\b" : "(" + escapedPatterns.join("|") + ")";
            return caseSensitive ? new RegExp(regexStr) : new RegExp(regexStr, "i");
        }
    }(window.document);
    var Input = function () {
        "use strict";
        var specialKeyCodeMap;
        specialKeyCodeMap = {
            9: "tab",
            27: "esc",
            37: "left",
            39: "right",
            13: "enter",
            38: "up",
            40: "down"
        };
        function Input(o, www) {
            o = o || {};
            if (!o.input) {
                $.error("input is missing");
            }
            www.mixin(this);
            this.$hint = $(o.hint);
            this.$input = $(o.input);
            this.query = this.$input.val();
            this.queryWhenFocused = this.hasFocus() ? this.query : null;
            this.$overflowHelper = buildOverflowHelper(this.$input);
            this._checkLanguageDirection();
            if (this.$hint.length === 0) {
                this.setHint = this.getHint = this.clearHint = this.clearHintIfInvalid = _.noop;
            }
        }
        Input.normalizeQuery = function (str) {
            return _.toStr(str).replace(/^\s*/g, "").replace(/\s{2,}/g, " ");
        };
        _.mixin(Input.prototype, EventEmitter, {
            _onBlur: function onBlur() {
                this.resetInputValue();
                this.trigger("blurred");
            },
            _onFocus: function onFocus() {
                this.queryWhenFocused = this.query;
                this.trigger("focused");
            },
            _onKeydown: function onKeydown($e) {
                var keyName = specialKeyCodeMap[$e.which || $e.keyCode];
                this._managePreventDefault(keyName, $e);
                if (keyName && this._shouldTrigger(keyName, $e)) {
                    this.trigger(keyName + "Keyed", $e);
                }
            },
            _onInput: function onInput() {
                this._setQuery(this.getInputValue());
                this.clearHintIfInvalid();
                this._checkLanguageDirection();
            },
            _managePreventDefault: function managePreventDefault(keyName, $e) {
                var preventDefault;
                switch (keyName) {
                    case "up":
                    case "down":
                        preventDefault = !withModifier($e);
                        break;

                    default:
                        preventDefault = false;
                }
                preventDefault && $e.preventDefault();
            },
            _shouldTrigger: function shouldTrigger(keyName, $e) {
                var trigger;
                switch (keyName) {
                    case "tab":
                        trigger = !withModifier($e);
                        break;

                    default:
                        trigger = true;
                }
                return trigger;
            },
            _checkLanguageDirection: function checkLanguageDirection() {
                var dir = (this.$input.css("direction") || "ltr").toLowerCase();
                if (this.dir !== dir) {
                    this.dir = dir;
                    this.$hint.attr("dir", dir);
                    this.trigger("langDirChanged", dir);
                }
            },
            _setQuery: function setQuery(val, silent) {
                var areEquivalent, hasDifferentWhitespace;
                areEquivalent = areQueriesEquivalent(val, this.query);
                hasDifferentWhitespace = areEquivalent ? this.query.length !== val.length : false;
                this.query = val;
                if (!silent && !areEquivalent) {
                    this.trigger("queryChanged", this.query);
                } else if (!silent && hasDifferentWhitespace) {
                    this.trigger("whitespaceChanged", this.query);
                }
            },
            bind: function () {
                var that = this, onBlur, onFocus, onKeydown, onInput;
                onBlur = _.bind(this._onBlur, this);
                onFocus = _.bind(this._onFocus, this);
                onKeydown = _.bind(this._onKeydown, this);
                onInput = _.bind(this._onInput, this);
                this.$input.on("blur.tt", onBlur).on("focus.tt", onFocus).on("keydown.tt", onKeydown);
                if (!_.isMsie() || _.isMsie() > 9) {
                    this.$input.on("input.tt", onInput);
                } else {
                    this.$input.on("keydown.tt keypress.tt cut.tt paste.tt", function ($e) {
                        if (specialKeyCodeMap[$e.which || $e.keyCode]) {
                            return;
                        }
                        _.defer(_.bind(that._onInput, that, $e));
                    });
                }
                return this;
            },
            focus: function focus() {
                this.$input.focus();
            },
            blur: function blur() {
                this.$input.blur();
            },
            getLangDir: function getLangDir() {
                return this.dir;
            },
            getQuery: function getQuery() {
                return this.query || "";
            },
            setQuery: function setQuery(val, silent) {
                this.setInputValue(val);
                this._setQuery(val, silent);
            },
            hasQueryChangedSinceLastFocus: function hasQueryChangedSinceLastFocus() {
                return this.query !== this.queryWhenFocused;
            },
            getInputValue: function getInputValue() {
                return this.$input.val();
            },
            setInputValue: function setInputValue(value) {
                this.$input.val(value);
                this.clearHintIfInvalid();
                this._checkLanguageDirection();
            },
            resetInputValue: function resetInputValue() {
                this.setInputValue(this.query);
            },
            getHint: function getHint() {
                return this.$hint.val();
            },
            setHint: function setHint(value) {
                this.$hint.val(value);
            },
            clearHint: function clearHint() {
                this.setHint("");
            },
            clearHintIfInvalid: function clearHintIfInvalid() {
                var val, hint, valIsPrefixOfHint, isValid;
                val = this.getInputValue();
                hint = this.getHint();
                valIsPrefixOfHint = val !== hint && hint.indexOf(val) === 0;
                isValid = val !== "" && valIsPrefixOfHint && !this.hasOverflow();
                !isValid && this.clearHint();
            },
            hasFocus: function hasFocus() {
                return this.$input.is(":focus");
            },
            hasOverflow: function hasOverflow() {
                var constraint = this.$input.width() - 2;
                this.$overflowHelper.text(this.getInputValue());
                return this.$overflowHelper.width() >= constraint;
            },
            isCursorAtEnd: function () {
                var valueLength, selectionStart, range;
                valueLength = this.$input.val().length;
                selectionStart = this.$input[0].selectionStart;
                if (_.isNumber(selectionStart)) {
                    return selectionStart === valueLength;
                } else if (document.selection) {
                    range = document.selection.createRange();
                    range.moveStart("character", -valueLength);
                    return valueLength === range.text.length;
                }
                return true;
            },
            destroy: function destroy() {
                this.$hint.off(".tt");
                this.$input.off(".tt");
                this.$overflowHelper.remove();
                this.$hint = this.$input = this.$overflowHelper = $("<div>");
            }
        });
        return Input;
        function buildOverflowHelper($input) {
            return $('<pre aria-hidden="true"></pre>').css({
                position: "absolute",
                visibility: "hidden",
                whiteSpace: "pre",
                fontFamily: $input.css("font-family"),
                fontSize: $input.css("font-size"),
                fontStyle: $input.css("font-style"),
                fontVariant: $input.css("font-variant"),
                fontWeight: $input.css("font-weight"),
                wordSpacing: $input.css("word-spacing"),
                letterSpacing: $input.css("letter-spacing"),
                textIndent: $input.css("text-indent"),
                textRendering: $input.css("text-rendering"),
                textTransform: $input.css("text-transform")
            }).insertAfter($input);
        }
        function areQueriesEquivalent(a, b) {
            return Input.normalizeQuery(a) === Input.normalizeQuery(b);
        }
        function withModifier($e) {
            return $e.altKey || $e.ctrlKey || $e.metaKey || $e.shiftKey;
        }
    }();
    var Dataset = function () {
        "use strict";
        var keys, nameGenerator;
        keys = {
            val: "tt-selectable-display",
            obj: "tt-selectable-object"
        };
        nameGenerator = _.getIdGenerator();
        function Dataset(o, www) {
            o = o || {};
            o.templates = o.templates || {};
            o.templates.notFound = o.templates.notFound || o.templates.empty;
            if (!o.source) {
                $.error("missing source");
            }
            if (!o.node) {
                $.error("missing node");
            }
            if (o.name && !isValidName(o.name)) {
                $.error("invalid dataset name: " + o.name);
            }
            www.mixin(this);
            this.highlight = !!o.highlight;
            this.name = o.name || nameGenerator();
            this.limit = o.limit || 5;
            this.displayFn = getDisplayFn(o.display || o.displayKey);
            this.templates = getTemplates(o.templates, this.displayFn);
            this.source = o.source.__ttAdapter ? o.source.__ttAdapter() : o.source;
            this.async = _.isUndefined(o.async) ? this.source.length > 2 : !!o.async;
            this._resetLastSuggestion();
            this.$el = $(o.node).addClass(this.classes.dataset).addClass(this.classes.dataset + "-" + this.name);
        }
        Dataset.extractData = function extractData(el) {
            var $el = $(el);
            if ($el.data(keys.obj)) {
                return {
                    val: $el.data(keys.val) || "",
                    obj: $el.data(keys.obj) || null
                };
            }
            return null;
        };
        _.mixin(Dataset.prototype, EventEmitter, {
            _overwrite: function overwrite(query, suggestions) {
                suggestions = suggestions || [];
                if (suggestions.length) {
                    this._renderSuggestions(query, suggestions);
                } else if (this.async && this.templates.pending) {
                    this._renderPending(query);
                } else if (!this.async && this.templates.notFound) {
                    this._renderNotFound(query);
                } else {
                    this._empty();
                }
                this.trigger("rendered", this.name, suggestions, false);
            },
            _append: function append(query, suggestions) {
                suggestions = suggestions || [];
                if (suggestions.length && this.$lastSuggestion.length) {
                    this._appendSuggestions(query, suggestions);
                } else if (suggestions.length) {
                    this._renderSuggestions(query, suggestions);
                } else if (!this.$lastSuggestion.length && this.templates.notFound) {
                    this._renderNotFound(query);
                }
                this.trigger("rendered", this.name, suggestions, true);
            },
            _renderSuggestions: function renderSuggestions(query, suggestions) {
                var $fragment;
                $fragment = this._getSuggestionsFragment(query, suggestions);
                this.$lastSuggestion = $fragment.children().last();
                this.$el.html($fragment).prepend(this._getHeader(query, suggestions)).append(this._getFooter(query, suggestions));
            },
            _appendSuggestions: function appendSuggestions(query, suggestions) {
                var $fragment, $lastSuggestion;
                $fragment = this._getSuggestionsFragment(query, suggestions);
                $lastSuggestion = $fragment.children().last();
                this.$lastSuggestion.after($fragment);
                this.$lastSuggestion = $lastSuggestion;
            },
            _renderPending: function renderPending(query) {
                var template = this.templates.pending;
                this._resetLastSuggestion();
                template && this.$el.html(template({
                    query: query,
                    dataset: this.name
                }));
            },
            _renderNotFound: function renderNotFound(query) {
                var template = this.templates.notFound;
                this._resetLastSuggestion();
                template && this.$el.html(template({
                    query: query,
                    dataset: this.name
                }));
            },
            _empty: function empty() {
                this.$el.empty();
                this._resetLastSuggestion();
            },
            _getSuggestionsFragment: function getSuggestionsFragment(query, suggestions) {
                var that = this, fragment;
                fragment = document.createDocumentFragment();
                _.each(suggestions, function getSuggestionNode(suggestion) {
                    var $el, context;
                    context = that._injectQuery(query, suggestion);
                    $el = $(that.templates.suggestion(context)).data(keys.obj, suggestion).data(keys.val, that.displayFn(suggestion)).addClass(that.classes.suggestion + " " + that.classes.selectable);
                    fragment.appendChild($el[0]);
                });
                this.highlight && highlight({
                    className: this.classes.highlight,
                    node: fragment,
                    pattern: query
                });
                return $(fragment);
            },
            _getFooter: function getFooter(query, suggestions) {
                return this.templates.footer ? this.templates.footer({
                    query: query,
                    suggestions: suggestions,
                    dataset: this.name
                }) : null;
            },
            _getHeader: function getHeader(query, suggestions) {
                return this.templates.header ? this.templates.header({
                    query: query,
                    suggestions: suggestions,
                    dataset: this.name
                }) : null;
            },
            _resetLastSuggestion: function resetLastSuggestion() {
                this.$lastSuggestion = $();
            },
            _injectQuery: function injectQuery(query, obj) {
                return _.isObject(obj) ? _.mixin({
                    _query: query
                }, obj) : obj;
            },
            update: function update(query) {
                var that = this, canceled = false, syncCalled = false, rendered = 0;
                this.cancel();
                this.cancel = function cancel() {
                    canceled = true;
                    that.cancel = $.noop;
                    that.async && that.trigger("asyncCanceled", query);
                };
                this.source(query, sync, async);
                !syncCalled && sync([]);
                function sync(suggestions) {
                    if (syncCalled) {
                        return;
                    }
                    syncCalled = true;
                    suggestions = (suggestions || []).slice(0, that.limit);
                    rendered = suggestions.length;
                    that._overwrite(query, suggestions);
                    if (rendered < that.limit && that.async) {
                        that.trigger("asyncRequested", query);
                    }
                }
                function async(suggestions) {
                    suggestions = suggestions || [];
                    if (!canceled && rendered < that.limit) {
                        that.cancel = $.noop;
                        rendered += suggestions.length;
                        that._append(query, suggestions.slice(0, that.limit - rendered));
                        that.async && that.trigger("asyncReceived", query);
                    }
                }
            },
            cancel: $.noop,
            clear: function clear() {
                this._empty();
                this.cancel();
                this.trigger("cleared");
            },
            isEmpty: function isEmpty() {
                return this.$el.is(":empty");
            },
            destroy: function destroy() {
                this.$el = $("<div>");
            }
        });
        return Dataset;
        function getDisplayFn(display) {
            display = display || _.stringify;
            return _.isFunction(display) ? display : displayFn;
            function displayFn(obj) {
                return obj[display];
            }
        }
        function getTemplates(templates, displayFn) {
            return {
                notFound: templates.notFound && _.templatify(templates.notFound),
                pending: templates.pending && _.templatify(templates.pending),
                header: templates.header && _.templatify(templates.header),
                footer: templates.footer && _.templatify(templates.footer),
                suggestion: templates.suggestion || suggestionTemplate
            };
            function suggestionTemplate(context) {
                return $("<div>").text(displayFn(context));
            }
        }
        function isValidName(str) {
            return /^[_a-zA-Z0-9-]+$/.test(str);
        }
    }();
    var Menu = function () {
        "use strict";
        function Menu(o, www) {
            var that = this;
            o = o || {};
            if (!o.node) {
                $.error("node is required");
            }
            www.mixin(this);
            this.$node = $(o.node);
            this.query = null;
            this.datasets = _.map(o.datasets, initializeDataset);
            function initializeDataset(oDataset) {
                var node = that.$node.find(oDataset.node).first();
                oDataset.node = node.length ? node : $("<div>").appendTo(that.$node);
                return new Dataset(oDataset, www);
            }
        }
        _.mixin(Menu.prototype, EventEmitter, {
            _onSelectableClick: function onSelectableClick($e) {
                this.trigger("selectableClicked", $($e.currentTarget));
            },
            _onRendered: function onRendered(type, dataset, suggestions, async) {
                this.$node.toggleClass(this.classes.empty, this._allDatasetsEmpty());
                this.trigger("datasetRendered", dataset, suggestions, async);
            },
            _onCleared: function onCleared() {
                this.$node.toggleClass(this.classes.empty, this._allDatasetsEmpty());
                this.trigger("datasetCleared");
            },
            _propagate: function propagate() {
                this.trigger.apply(this, arguments);
            },
            _allDatasetsEmpty: function allDatasetsEmpty() {
                return _.every(this.datasets, isDatasetEmpty);
                function isDatasetEmpty(dataset) {
                    return dataset.isEmpty();
                }
            },
            _getSelectables: function getSelectables() {
                return this.$node.find(this.selectors.selectable);
            },
            _removeCursor: function _removeCursor() {
                var $selectable = this.getActiveSelectable();
                $selectable && $selectable.removeClass(this.classes.cursor);
            },
            _ensureVisible: function ensureVisible($el) {
                var elTop, elBottom, nodeScrollTop, nodeHeight;
                elTop = $el.position().top;
                elBottom = elTop + $el.outerHeight(true);
                nodeScrollTop = this.$node.scrollTop();
                nodeHeight = this.$node.height() + parseInt(this.$node.css("paddingTop"), 10) + parseInt(this.$node.css("paddingBottom"), 10);
                if (elTop < 0) {
                    this.$node.scrollTop(nodeScrollTop + elTop);
                } else if (nodeHeight < elBottom) {
                    this.$node.scrollTop(nodeScrollTop + (elBottom - nodeHeight));
                }
            },
            bind: function () {
                var that = this, onSelectableClick;
                onSelectableClick = _.bind(this._onSelectableClick, this);
                this.$node.on("click.tt", this.selectors.selectable, onSelectableClick);
                _.each(this.datasets, function (dataset) {
                    dataset.onSync("asyncRequested", that._propagate, that).onSync("asyncCanceled", that._propagate, that).onSync("asyncReceived", that._propagate, that).onSync("rendered", that._onRendered, that).onSync("cleared", that._onCleared, that);
                });
                return this;
            },
            isOpen: function isOpen() {
                return this.$node.hasClass(this.classes.open);
            },
            open: function open() {
                this.$node.addClass(this.classes.open);
            },
            close: function close() {
                this.$node.removeClass(this.classes.open);
                this._removeCursor();
            },
            setLanguageDirection: function setLanguageDirection(dir) {
                this.$node.attr("dir", dir);
            },
            selectableRelativeToCursor: function selectableRelativeToCursor(delta) {
                var $selectables, $oldCursor, oldIndex, newIndex;
                $oldCursor = this.getActiveSelectable();
                $selectables = this._getSelectables();
                oldIndex = $oldCursor ? $selectables.index($oldCursor) : -1;
                newIndex = oldIndex + delta;
                newIndex = (newIndex + 1) % ($selectables.length + 1) - 1;
                newIndex = newIndex < -1 ? $selectables.length - 1 : newIndex;
                return newIndex === -1 ? null : $selectables.eq(newIndex);
            },
            setCursor: function setCursor($selectable) {
                this._removeCursor();
                if ($selectable = $selectable && $selectable.first()) {
                    $selectable.addClass(this.classes.cursor);
                    this._ensureVisible($selectable);
                }
            },
            getSelectableData: function getSelectableData($el) {
                return $el && $el.length ? Dataset.extractData($el) : null;
            },
            getActiveSelectable: function getActiveSelectable() {
                var $selectable = this._getSelectables().filter(this.selectors.cursor).first();
                return $selectable.length ? $selectable : null;
            },
            getTopSelectable: function getTopSelectable() {
                var $selectable = this._getSelectables().first();
                return $selectable.length ? $selectable : null;
            },
            update: function update(query) {
                var isValidUpdate = query !== this.query;
                if (isValidUpdate) {
                    this.query = query;
                    _.each(this.datasets, updateDataset);
                }
                return isValidUpdate;
                function updateDataset(dataset) {
                    dataset.update(query);
                }
            },
            empty: function empty() {
                _.each(this.datasets, clearDataset);
                this.query = null;
                this.$node.addClass(this.classes.empty);
                function clearDataset(dataset) {
                    dataset.clear();
                }
            },
            destroy: function destroy() {
                this.$node.off(".tt");
                this.$node = $("<div>");
                _.each(this.datasets, destroyDataset);
                function destroyDataset(dataset) {
                    dataset.destroy();
                }
            }
        });
        return Menu;
    }();
    var DefaultMenu = function () {
        "use strict";
        var s = Menu.prototype;
        function DefaultMenu() {
            Menu.apply(this, [].slice.call(arguments, 0));
        }
        _.mixin(DefaultMenu.prototype, Menu.prototype, {
            open: function open() {
                !this._allDatasetsEmpty() && this._show();
                return s.open.apply(this, [].slice.call(arguments, 0));
            },
            close: function close() {
                this._hide();
                return s.close.apply(this, [].slice.call(arguments, 0));
            },
            _onRendered: function onRendered() {
                if (this._allDatasetsEmpty()) {
                    this._hide();
                } else {
                    this.isOpen() && this._show();
                }
                return s._onRendered.apply(this, [].slice.call(arguments, 0));
            },
            _onCleared: function onCleared() {
                if (this._allDatasetsEmpty()) {
                    this._hide();
                } else {
                    this.isOpen() && this._show();
                }
                return s._onCleared.apply(this, [].slice.call(arguments, 0));
            },
            setLanguageDirection: function setLanguageDirection(dir) {
                this.$node.css(dir === "ltr" ? this.css.ltr : this.css.rtl);
                return s.setLanguageDirection.apply(this, [].slice.call(arguments, 0));
            },
            _hide: function hide() {
                this.$node.hide();
            },
            _show: function show() {
                this.$node.css("display", "block");
            }
        });
        return DefaultMenu;
    }();
    var Typeahead = function () {
        "use strict";
        function Typeahead(o, www) {
            var onFocused, onBlurred, onEnterKeyed, onTabKeyed, onEscKeyed, onUpKeyed, onDownKeyed, onLeftKeyed, onRightKeyed, onQueryChanged, onWhitespaceChanged;
            o = o || {};
            if (!o.input) {
                $.error("missing input");
            }
            if (!o.menu) {
                $.error("missing menu");
            }
            if (!o.eventBus) {
                $.error("missing event bus");
            }
            www.mixin(this);
            this.eventBus = o.eventBus;
            this.minLength = _.isNumber(o.minLength) ? o.minLength : 1;
            this.input = o.input;
            this.menu = o.menu;
            this.enabled = true;
            this.active = false;
            this.input.hasFocus() && this.activate();
            this.dir = this.input.getLangDir();
            this._hacks();
            this.menu.bind().onSync("selectableClicked", this._onSelectableClicked, this).onSync("asyncRequested", this._onAsyncRequested, this).onSync("asyncCanceled", this._onAsyncCanceled, this).onSync("asyncReceived", this._onAsyncReceived, this).onSync("datasetRendered", this._onDatasetRendered, this).onSync("datasetCleared", this._onDatasetCleared, this);
            onFocused = c(this, "activate", "open", "_onFocused");
            onBlurred = c(this, "deactivate", "_onBlurred");
            onEnterKeyed = c(this, "isActive", "isOpen", "_onEnterKeyed");
            onTabKeyed = c(this, "isActive", "isOpen", "_onTabKeyed");
            onEscKeyed = c(this, "isActive", "_onEscKeyed");
            onUpKeyed = c(this, "isActive", "open", "_onUpKeyed");
            onDownKeyed = c(this, "isActive", "open", "_onDownKeyed");
            onLeftKeyed = c(this, "isActive", "isOpen", "_onLeftKeyed");
            onRightKeyed = c(this, "isActive", "isOpen", "_onRightKeyed");
            onQueryChanged = c(this, "_openIfActive", "_onQueryChanged");
            onWhitespaceChanged = c(this, "_openIfActive", "_onWhitespaceChanged");
            this.input.bind().onSync("focused", onFocused, this).onSync("blurred", onBlurred, this).onSync("enterKeyed", onEnterKeyed, this).onSync("tabKeyed", onTabKeyed, this).onSync("escKeyed", onEscKeyed, this).onSync("upKeyed", onUpKeyed, this).onSync("downKeyed", onDownKeyed, this).onSync("leftKeyed", onLeftKeyed, this).onSync("rightKeyed", onRightKeyed, this).onSync("queryChanged", onQueryChanged, this).onSync("whitespaceChanged", onWhitespaceChanged, this).onSync("langDirChanged", this._onLangDirChanged, this);
        }
        _.mixin(Typeahead.prototype, {
            _hacks: function hacks() {
                var $input, $menu;
                $input = this.input.$input || $("<div>");
                $menu = this.menu.$node || $("<div>");
                $input.on("blur.tt", function ($e) {
                    var active, isActive, hasActive;
                    active = document.activeElement;
                    isActive = $menu.is(active);
                    hasActive = $menu.has(active).length > 0;
                    if (_.isMsie() && (isActive || hasActive)) {
                        $e.preventDefault();
                        $e.stopImmediatePropagation();
                        _.defer(function () {
                            $input.focus();
                        });
                    }
                });
                $menu.on("mousedown.tt", function ($e) {
                    $e.preventDefault();
                });
            },
            _onSelectableClicked: function onSelectableClicked(type, $el) {
                this.select($el);
            },
            _onDatasetCleared: function onDatasetCleared() {
                this._updateHint();
            },
            _onDatasetRendered: function onDatasetRendered(type, dataset, suggestions, async) {
                this._updateHint();
                this.eventBus.trigger("render", suggestions, async, dataset);
            },
            _onAsyncRequested: function onAsyncRequested(type, dataset, query) {
                this.eventBus.trigger("asyncrequest", query, dataset);
            },
            _onAsyncCanceled: function onAsyncCanceled(type, dataset, query) {
                this.eventBus.trigger("asynccancel", query, dataset);
            },
            _onAsyncReceived: function onAsyncReceived(type, dataset, query) {
                this.eventBus.trigger("asyncreceive", query, dataset);
            },
            _onFocused: function onFocused() {
                this._minLengthMet() && this.menu.update(this.input.getQuery());
            },
            _onBlurred: function onBlurred() {
                if (this.input.hasQueryChangedSinceLastFocus()) {
                    this.eventBus.trigger("change", this.input.getQuery());
                }
            },
            _onEnterKeyed: function onEnterKeyed(type, $e) {
                var $selectable;
                if ($selectable = this.menu.getActiveSelectable()) {
                    this.select($selectable) && $e.preventDefault();
                }
            },
            _onTabKeyed: function onTabKeyed(type, $e) {
                var $selectable;
                if ($selectable = this.menu.getActiveSelectable()) {
                    this.select($selectable) && $e.preventDefault();
                } else if ($selectable = this.menu.getTopSelectable()) {
                    this.autocomplete($selectable) && $e.preventDefault();
                }
            },
            _onEscKeyed: function onEscKeyed() {
                this.close();
            },
            _onUpKeyed: function onUpKeyed() {
                this.moveCursor(-1);
            },
            _onDownKeyed: function onDownKeyed() {
                this.moveCursor(+1);
            },
            _onLeftKeyed: function onLeftKeyed() {
                if (this.dir === "rtl" && this.input.isCursorAtEnd()) {
                    this.autocomplete(this.menu.getTopSelectable());
                }
            },
            _onRightKeyed: function onRightKeyed() {
                if (this.dir === "ltr" && this.input.isCursorAtEnd()) {
                    this.autocomplete(this.menu.getTopSelectable());
                }
            },
            _onQueryChanged: function onQueryChanged(e, query) {
                this._minLengthMet(query) ? this.menu.update(query) : this.menu.empty();
            },
            _onWhitespaceChanged: function onWhitespaceChanged() {
                this._updateHint();
            },
            _onLangDirChanged: function onLangDirChanged(e, dir) {
                if (this.dir !== dir) {
                    this.dir = dir;
                    this.menu.setLanguageDirection(dir);
                }
            },
            _openIfActive: function openIfActive() {
                this.isActive() && this.open();
            },
            _minLengthMet: function minLengthMet(query) {
                query = _.isString(query) ? query : this.input.getQuery() || "";
                return query.length >= this.minLength;
            },
            _updateHint: function updateHint() {
                var $selectable, data, val, query, escapedQuery, frontMatchRegEx, match;
                $selectable = this.menu.getTopSelectable();
                data = this.menu.getSelectableData($selectable);
                val = this.input.getInputValue();
                if (data && !_.isBlankString(val) && !this.input.hasOverflow()) {
                    query = Input.normalizeQuery(val);
                    escapedQuery = _.escapeRegExChars(query);
                    frontMatchRegEx = new RegExp("^(?:" + escapedQuery + ")(.+$)", "i");
                    match = frontMatchRegEx.exec(data.val);
                    match && this.input.setHint(val + match[1]);
                } else {
                    this.input.clearHint();
                }
            },
            isEnabled: function isEnabled() {
                return this.enabled;
            },
            enable: function enable() {
                this.enabled = true;
            },
            disable: function disable() {
                this.enabled = false;
            },
            isActive: function isActive() {
                return this.active;
            },
            activate: function activate() {
                if (this.isActive()) {
                    return true;
                } else if (!this.isEnabled() || this.eventBus.before("active")) {
                    return false;
                } else {
                    this.active = true;
                    this.eventBus.trigger("active");
                    return true;
                }
            },
            deactivate: function deactivate() {
                if (!this.isActive()) {
                    return true;
                } else if (this.eventBus.before("idle")) {
                    return false;
                } else {
                    this.active = false;
                    this.close();
                    this.eventBus.trigger("idle");
                    return true;
                }
            },
            isOpen: function isOpen() {
                return this.menu.isOpen();
            },
            open: function open() {
                if (!this.isOpen() && !this.eventBus.before("open")) {
                    this.menu.open();
                    this._updateHint();
                    this.eventBus.trigger("open");
                }
                return this.isOpen();
            },
            close: function close() {
                if (this.isOpen() && !this.eventBus.before("close")) {
                    this.menu.close();
                    this.input.clearHint();
                    this.input.resetInputValue();
                    this.eventBus.trigger("close");
                }
                return !this.isOpen();
            },
            setVal: function setVal(val) {
                this.input.setQuery(_.toStr(val));
            },
            getVal: function getVal() {
                return this.input.getQuery();
            },
            select: function select($selectable) {
                var data = this.menu.getSelectableData($selectable);
                if (data && !this.eventBus.before("select", data.obj)) {
                    this.input.setQuery(data.val, true);
                    this.eventBus.trigger("select", data.obj);
                    this.close();
                    return true;
                }
                return false;
            },
            autocomplete: function autocomplete($selectable) {
                var query, data, isValid;
                query = this.input.getQuery();
                data = this.menu.getSelectableData($selectable);
                isValid = data && query !== data.val;
                if (isValid && !this.eventBus.before("autocomplete", data.obj)) {
                    this.input.setQuery(data.val);
                    this.eventBus.trigger("autocomplete", data.obj);
                    return true;
                }
                return false;
            },
            moveCursor: function moveCursor(delta) {
                var query, $candidate, data, payload, cancelMove;
                query = this.input.getQuery();
                $candidate = this.menu.selectableRelativeToCursor(delta);
                data = this.menu.getSelectableData($candidate);
                payload = data ? data.obj : null;
                cancelMove = this._minLengthMet() && this.menu.update(query);
                if (!cancelMove && !this.eventBus.before("cursorchange", payload)) {
                    this.menu.setCursor($candidate);
                    if (data) {
                        this.input.setInputValue(data.val);
                    } else {
                        this.input.resetInputValue();
                        this._updateHint();
                    }
                    this.eventBus.trigger("cursorchange", payload);
                    return true;
                }
                return false;
            },
            destroy: function destroy() {
                this.input.destroy();
                this.menu.destroy();
            }
        });
        return Typeahead;
        function c(ctx) {
            var methods = [].slice.call(arguments, 1);
            return function () {
                var args = [].slice.call(arguments);
                _.each(methods, function (method) {
                    return ctx[method].apply(ctx, args);
                });
            };
        }
    }();
    (function () {
        "use strict";
        var old, keys, methods;
        old = $.fn.typeahead;
        keys = {
            www: "tt-www",
            attrs: "tt-attrs",
            typeahead: "tt-typeahead"
        };
        methods = {
            initialize: function initialize(o, datasets) {
                var www;
                datasets = _.isArray(datasets) ? datasets : [].slice.call(arguments, 1);
                o = o || {};
                www = WWW(o.classNames);
                return this.each(attach);
                function attach() {
                    var $input, $wrapper, $hint, $menu, defaultHint, defaultMenu, eventBus, input, menu, typeahead, MenuConstructor;
                    _.each(datasets, function (d) {
                        d.highlight = !!o.highlight;
                    });
                    $input = $(this);
                    $wrapper = $(www.html.wrapper);
                    $hint = $elOrNull(o.hint);
                    $menu = $elOrNull(o.menu);
                    defaultHint = o.hint !== false && !$hint;
                    defaultMenu = o.menu !== false && !$menu;
                    defaultHint && ($hint = buildHintFromInput($input, www));
                    defaultMenu && ($menu = $(www.html.menu).css(www.css.menu));
                    $hint && $hint.val("");
                    $input = prepInput($input, www);
                    if (defaultHint || defaultMenu) {
                        $wrapper.css(www.css.wrapper);
                        $input.css(defaultHint ? www.css.input : www.css.inputWithNoHint);
                        $input.wrap($wrapper).parent().prepend(defaultHint ? $hint : null).append(defaultMenu ? $menu : null);
                    }
                    MenuConstructor = defaultMenu ? DefaultMenu : Menu;
                    eventBus = new EventBus({
                        el: $input
                    });
                    input = new Input({
                        hint: $hint,
                        input: $input
                    }, www);
                    menu = new MenuConstructor({
                        node: $menu,
                        datasets: datasets
                    }, www);
                    typeahead = new Typeahead({
                        input: input,
                        menu: menu,
                        eventBus: eventBus,
                        minLength: o.minLength
                    }, www);
                    $input.data(keys.www, www);
                    $input.data(keys.typeahead, typeahead);
                }
            },
            isEnabled: function isEnabled() {
                var enabled;
                ttEach(this.first(), function (t) {
                    enabled = t.isEnabled();
                });
                return enabled;
            },
            enable: function enable() {
                ttEach(this, function (t) {
                    t.enable();
                });
                return this;
            },
            disable: function disable() {
                ttEach(this, function (t) {
                    t.disable();
                });
                return this;
            },
            isActive: function isActive() {
                var active;
                ttEach(this.first(), function (t) {
                    active = t.isActive();
                });
                return active;
            },
            activate: function activate() {
                ttEach(this, function (t) {
                    t.activate();
                });
                return this;
            },
            deactivate: function deactivate() {
                ttEach(this, function (t) {
                    t.deactivate();
                });
                return this;
            },
            isOpen: function isOpen() {
                var open;
                ttEach(this.first(), function (t) {
                    open = t.isOpen();
                });
                return open;
            },
            open: function open() {
                ttEach(this, function (t) {
                    t.open();
                });
                return this;
            },
            close: function close() {
                ttEach(this, function (t) {
                    t.close();
                });
                return this;
            },
            select: function select(el) {
                var success = false, $el = $(el);
                ttEach(this.first(), function (t) {
                    success = t.select($el);
                });
                return success;
            },
            autocomplete: function autocomplete(el) {
                var success = false, $el = $(el);
                ttEach(this.first(), function (t) {
                    success = t.autocomplete($el);
                });
                return success;
            },
            moveCursor: function moveCursoe(delta) {
                var success = false;
                ttEach(this.first(), function (t) {
                    success = t.moveCursor(delta);
                });
                return success;
            },
            val: function val(newVal) {
                var query;
                if (!arguments.length) {
                    ttEach(this.first(), function (t) {
                        query = t.getVal();
                    });
                    return query;
                } else {
                    ttEach(this, function (t) {
                        t.setVal(newVal);
                    });
                    return this;
                }
            },
            destroy: function destroy() {
                ttEach(this, function (typeahead, $input) {
                    revert($input);
                    typeahead.destroy();
                });
                return this;
            }
        };
        $.fn.typeahead = function (method) {
            if (methods[method]) {
                return methods[method].apply(this, [].slice.call(arguments, 1));
            } else {
                return methods.initialize.apply(this, arguments);
            }
        };
        $.fn.typeahead.noConflict = function noConflict() {
            $.fn.typeahead = old;
            return this;
        };
        function ttEach($els, fn) {
            $els.each(function () {
                var $input = $(this), typeahead;
                (typeahead = $input.data(keys.typeahead)) && fn(typeahead, $input);
            });
        }
        function buildHintFromInput($input, www) {
            return $input.clone().addClass(www.classes.hint).removeData().css(www.css.hint).css(getBackgroundStyles($input)).prop("readonly", true).removeAttr("id name placeholder required").attr({
                autocomplete: "off",
                spellcheck: "false",
                tabindex: -1
            });
        }
        function prepInput($input, www) {
            $input.data(keys.attrs, {
                dir: $input.attr("dir"),
                autocomplete: $input.attr("autocomplete"),
                spellcheck: $input.attr("spellcheck"),
                style: $input.attr("style")
            });
            $input.addClass(www.classes.input).attr({
                autocomplete: "off",
                spellcheck: false
            });
            try {
                !$input.attr("dir") && $input.attr("dir", "auto");
            } catch (e) { }
            return $input;
        }
        function getBackgroundStyles($el) {
            return {
                backgroundAttachment: $el.css("background-attachment"),
                backgroundClip: $el.css("background-clip"),
                backgroundColor: $el.css("background-color"),
                backgroundImage: $el.css("background-image"),
                backgroundOrigin: $el.css("background-origin"),
                backgroundPosition: $el.css("background-position"),
                backgroundRepeat: $el.css("background-repeat"),
                backgroundSize: $el.css("background-size")
            };
        }
        function revert($input) {
            var www, $wrapper;
            www = $input.data(keys.www);
            $wrapper = $input.parent().filter(www.selectors.wrapper);
            _.each($input.data(keys.attrs), function (val, key) {
                _.isUndefined(val) ? $input.removeAttr(key) : $input.attr(key, val);
            });
            $input.removeData(keys.typeahead).removeData(keys.www).removeData(keys.attr).removeClass(www.classes.input);
            if ($wrapper.length) {
                $input.detach().insertAfter($wrapper);
                $wrapper.remove();
            }
        }
        function $elOrNull(obj) {
            var isValid, $el;
            isValid = _.isJQuery(obj) || _.isElement(obj);
            $el = isValid ? $(obj).first() : [];
            return $el.length ? $el : null;
        }
    })();
});