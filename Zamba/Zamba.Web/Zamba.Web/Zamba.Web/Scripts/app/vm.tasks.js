define('vm.tasks',
    ['jquery', 'underscore', 'ko', 'datacontext', 'router', 'filter.tasks', 'sort', 'event.delegates', 'utils', 'messenger', 'config', 'store'],
    function ($, _, ko, datacontext, router, taskFilter, sort, eventDelegates, utils, messenger, config, store) {
    	var
            filterTemplate = 'tasks.filterbox',
            isBusy = false,
            isRefreshing = false,
            taskFilter = new taskFilter(),
            taskTemplate = 'tasks.view',
            tasks = ko.observableArray(),
            tasksids = ko.observableArray(),
            speakers = ko.observableArray(),
            stateKey = { filter: 'vm.tasks.filter' },
            timeslots = ko.observableArray(),
            tracks = ko.observableArray(),

            activate = function (routeData, callback) {
            	messenger.publish.viewModelActivated({ canleaveCallback: canLeave });
            	getSpeakers();
            	getTimeslots();
            	getTracks();
            	gettasks(callback);
            },

            addFilterSubscriptions = function () {
            	taskFilter.searchText.subscribe(onFilterChange);
            	taskFilter.speaker.subscribe(onFilterChange);
            	taskFilter.timeslot.subscribe(onFilterChange);
            	taskFilter.track.subscribe(onFilterChange);
            	taskFilter.favoriteOnly.subscribe(onFilterChange);
            },

            canLeave = function () {
            	return true;
            },

            clearAllFilters = function () {
            	taskFilter.favoriteOnly(false).speaker(null)
                    .timeslot(null).track(null).searchText('');
            	gettasks();
            },

            clearFilter = function () {
            	taskFilter.searchText('');
            },

            dataOptions = function (force) {
            	return {
            		results: tasks,
            		filter: taskFilter,
            		sortFunction: sort.taskSort,
            		forceRefresh: force
            	};
            },

            forceRefreshCmd = ko.asyncCommand({
            	execute: function (complete) {
            		$.when(datacontext.tasks.gettasksAndAttendance(dataOptions(true)))
                        .always(complete);
            	}
            }),

            gettasks = function (callback) {
            	if (!isRefreshing) {
            		isRefreshing = true;
            		restoreFilter();
            		$.when(datacontext.tasks.getData(dataOptions(false)))
                        .always(utils.invokeFunctionIfExists(callback));
            		isRefreshing = false;
            	}

            },

            getSpeakers = function () {
            	if (!speakers().length) {
            		datacontext.speakertasks.getLocalSpeakers(speakers, {
            			sortFunction: sort.speakerSort
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

            gotoDetails = function (selectedtask) {
            	if (selectedtask && selectedtask.id()) {
            		router.navigateTo(config.hashes.tasks + '/' + selectedtask.id());
            	}
            },

            onFilterChange = function () {
            	if (!isRefreshing) {
            		store.save(stateKey.filter, ko.toJS(taskFilter));
            		gettasks();
            	}
            },

            restoreFilter = function () {
            	var stored = store.fetch(stateKey.filter);
            	if (!stored) { return; }
            	utils.restoreFilter({
            		stored: stored,
            		filter: taskFilter,
            		datacontext: datacontext
            	});
            },

            saveFavorite = function (selectedtask) {
            	if (isBusy) { return; }
            	isBusy = true;
            	var cudMethod = selectedtask.isFavorite()
                    ? datacontext.attendance.deleteData
                    : datacontext.attendance.addData;
            	cudMethod(selectedtask,
                        {
                        	success: function () { isBusy = false; },
                        	error: function () { isBusy = false; }
                        }
                    );
            },

            init = function () {
            	// Bind jQuery delegated events
            	eventDelegates.tasksListItem(gotoDetails);
            	eventDelegates.tasksFavorite(saveFavorite);

            	// Subscribe to specific changes of observables
            	addFilterSubscriptions();
            };

    	init();

    	return {
    		activate: activate,
    		canLeave: canLeave,
    		clearFilter: clearFilter,
    		clearAllFilters: clearAllFilters,
    		filterTemplate: filterTemplate,
    		forceRefreshCmd: forceRefreshCmd,
    		taskFilter: taskFilter,
    		tasks: tasks,
    		speakers: speakers,
    		taskTemplate: taskTemplate,
    		timeslots: timeslots,
    		tracks: tracks
    	};
    });