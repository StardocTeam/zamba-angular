'use strict';
var serviceBase = ZambaWebRestApiURL;

app.factory('ZambaAssociatedService', ['$http', '$q', function ($http, $q) {
    var zambaAssociatedFactory = {};

    //ver de poner async con IE
     function _getResults(parentResultId, parentEntityId, associatedIds, parentTaskId) {

        if (parentEntityId == null) {
            parentEntityId = 0;
        }

        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "parentResultId": String(parentResultId),
                "parentEntityId": String(parentEntityId),
                "parentTaskId": String(parentTaskId),
                "AsociatedIds": String(associatedIds)
            }
        };

        return $http.post(serviceBase + '/search/getAssociatedResults', JSON.stringify(genericRequest));

    };
    

    //function asyncGeneratorStep(gen, resolve, reject, _next, _throw, key, arg) { try { var info = gen[key](arg); var value = info.value; } catch (error) { reject(error); return; } if (info.done) { resolve(value); } else { Promise.resolve(value).then(_next, _throw); } }

    //function _asyncToGenerator(fn) { return function () { var self = this, args = arguments; return new Promise(function (resolve, reject) { var gen = fn.apply(self, args); function _next(value) { asyncGeneratorStep(gen, resolve, reject, _next, _throw, "next", value); } function _throw(err) { asyncGeneratorStep(gen, resolve, reject, _next, _throw, "throw", err); } _next(undefined); }); }; }

    //function _getResults(_x, _x2, _x3, _x4) {
    //    return _getResults2.apply(this, arguments);
    //}

    //function _getResults2() {
    //    _getResults2 = _asyncToGenerator(
    //        /*#__PURE__*/
    //        regeneratorRuntime.mark(function _callee(parentResultId, parentEntiyId, associatedIds, parentTaskId) {
    //            var results, genericRequest;
    //            return regeneratorRuntime.wrap(function _callee$(_context) {
    //                while (1) {
    //                    switch (_context.prev = _context.next) {
    //                        case 0:
    //                            results = null;

    //                            if (parentEntiyId == null) {
    //                                parentEntiyId = 0;
    //                            }

    //                            genericRequest = {
    //                                UserId: GetUID(),
    //                                Params: {
    //                                    "parentResultId": String(parentResultId),
    //                                    "parentEntityId": String(parentEntiyId),
    //                                    "parentTaskId": String(parentTaskId),
    //                                    "AsociatedIds": String(associatedIds)
    //                                }
    //                            };
    //                            return _context.abrupt("return", $http.post(serviceBase + '/search/getAssociatedResults', JSON.stringify(genericRequest)));

    //                        case 4:
    //                        case "end":
    //                            return _context.stop();
    //                    }
    //                }
    //            }, _callee);
    //        }));
    //    return _getResults2.apply(this, arguments);
    //};

    zambaAssociatedFactory.getResults = _getResults;

    return zambaAssociatedFactory;
}]);

function RestApiGrid() {
    return angular.element(document.body).injector().get("ruleExecutionService");
};