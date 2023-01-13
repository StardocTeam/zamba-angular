define('vm.task',
    ['ko', 'datacontext', 'config', 'router', 'messenger', 'sort'],
    function (ko, datacontext, config, router, messenger, sort) {

        var
            currenttaskId = ko.observable(),
            logger = config.logger,
            rooms = ko.observableArray(),
            task = ko.observable(),
            timeslots = ko.observableArray(),
            tracks = ko.observableArray(),
            //validationErrors = ko.observableArray([]),

            validationErrors = ko.computed(function () {
                // We don;t have a task early on. So we return an empty [].
                // Once we get a task, we want to point to its validation errors.
                var valArray = task() ? ko.validation.group(task())() : [];
                return valArray;
            }),

            canEdittask = ko.computed(function () {
                return task() && config.currentUser() && config.currentUser().id() === task().speakerId();
            }),

            canEditEval = ko.computed(function () {
                return task() && config.currentUser() && config.currentUser().id() !== task().speakerId();
            }),

            isDirty = ko.computed(function () {
                if (canEdittask()) {
                    return task().dirtyFlag().isDirty();
                }
                if (canEditEval()) {
                    if (task() && task().attendance && task().attendance()) {
                        return task().attendance().dirtyFlag().isDirty();
                    }
                }
                return false;
            }),

            isValid = ko.computed(function () {
                return (canEditEval() || canEdittask()) ? validationErrors().length === 0 : true;
            }),

            activate = function (routeData, callback) {
                messenger.publish.viewModelActivated({ canleaveCallback: canLeave });
                currenttaskId(routeData.id);
                getRooms();
                getTimeslots();
                getTracks();
                gettask(callback);
            },

            cancelCmd = ko.asyncCommand({
                execute: function (complete) {
                    var callback = function () {
                        complete();
                        logger.success(config.toasts.retreivedData);
                    };
                    canEdittask() ? gettask(callback, true) : getAttendance(callback, true);
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && isDirty();
                }
            }),

            goBackCmd = ko.asyncCommand({
                execute: function (complete) {
                    router.navigateBack();
                    complete();
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && !isDirty();
                }
            }),

            canLeave = function () {
                return !isDirty() && isValid();
            },

            gettask = function (completeCallback, forceRefresh) {
                var callback = function () {
                    if (completeCallback) { completeCallback(); }
                };

                datacontext.tasks.getFulltaskById(
                    currenttaskId(), {
                        success: function (s) {
                            task(s);
                            callback();
                        },
                        error: callback
                    },
                    forceRefresh
                );
            },

            getAttendance = function (completeCallback, forceRefresh) {
                // Refresh the attendance in the datacontext
                var callback = completeCallback || function () {
                };

                datacontext.attendance.gettaskFavorite(
                    task().attendance().taskId(),
                    {
                        success: function () { callback(); },
                        error: function () { callback(); }
                    },
                    forceRefresh
                );
            },

            getRooms = function () {
                if (!rooms().length) {
                    datacontext.rooms.getData({
                        results: rooms,
                        sortFunction: sort.roomSort
                    });
                }
            },

            getTimeslots = function () {
                if (!timeslots().length) {
                    datacontext.timeslots.getData({
                        results: timeslots,
                        sortFunction: sort.timeslotSort
                    });
                }
            },

            getTracks = function () {
                if (!tracks().length) {
                    datacontext.tracks.getData({
                        results: tracks,
                        sortFunction: sort.trackSort
                    });
                }
            },

            saveCmd = ko.asyncCommand({
                execute: function (complete) {
                    if (canEdittask()) {
                        $.when(datacontext.tasks.updateData(task()))
                            .always(complete);
                        return;
                    }
                    if (canEditEval()) {
                        $.when(datacontext.attendance.updateData(task()))
                            .always(complete);
                        return;
                    }
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && isDirty() && isValid();
                }
            }),

            saveFavoriteCmd = ko.asyncCommand({
                execute: function (complete) {
                    var
                        wrapper = function () { saveFavoriteDone(complete); },

                        cudMethod = task().isFavorite()
                            ? datacontext.attendance.deleteData
                            : datacontext.attendance.addData;

                    cudMethod(task(),
                        {
                            success: wrapper,
                            error: wrapper
                        });
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && task() && task().isUnlocked();
                }
            }),

            saveFavoriteDone = function (complete) {
                task.valueHasMutated(); // Trigger re-evaluation of isDirty
                complete();
            },

            tmplName = function () {
                return canEdittask() ? 'task.edit' : 'task.view';
            };

        return {
            activate: activate,
            cancelCmd: cancelCmd,
            canEdittask: canEdittask,
            canEditEval: canEditEval,
            canLeave: canLeave,
            goBackCmd: goBackCmd,
            isDirty: isDirty,
            isValid: isValid,
            rooms: rooms,
            task: task,
            saveCmd: saveCmd,
            saveFavoriteCmd: saveFavoriteCmd,
            timeslots: timeslots,
            tmplName: tmplName,
            tracks: tracks
        };
    });