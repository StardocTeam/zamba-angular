app.controller('TreeViewController', function ($scope, $filter, $http, treeViewServices,$rootScope) {
    $scope.ChildsEntities = [];
    $scope.lastSelectedNode = {};
    $scope.ResultsNodeCounts = {};
    $scope.viewSelected;
    $scope.UserActionsForTaskTree = {};


    $scope.GetButtonsTasksTree = function () {
        $scope.UserActionsForTaskTree = treeViewServices.GetButtonsTasksTree();
        var arrOptions = [];
        $scope.UserActionsForTaskTree.forEach(function (e) {
            arrOptions.push(e);
        });
        let firstOption = {
            ButtonId: "zamba_rule_0",
            ButtonOrder: "0",
            Caption: "Acciones...",
            GroupClass: "",
            GroupName: "FirstOption",
            Idicon: 0,
            NeedRights: true,
            Params: "",
            PlaceId: 3,
            RuleId: 0,
            StepId: 0,
            TypeId: 0,
            ViewClass: ""
        };
        if (arrOptions.length>1)
            arrOptions.unshift(firstOption);
        $scope.UserActionsForTaskTree = arrOptions;
        $scope.item = arrOptions[0];

    }

    $scope.LoadWFTreeResults = function (nodeId, useCache) {
        $scope.ChildsEntities = [];
        $scope.lastSelectedNode = {};

        let childsEntities = treeViewServices.getResults();

        childsEntities.forEach($scope.mapChildEntities)

        $scope.ChildsEntities = childsEntities;

        if (nodeId != undefined)
            $scope.findNodeByLastSelectedNodeId(nodeId, $scope.ChildsEntities);

        setTimeout($scope.LoadCountersInNodes, 100, useCache);
    };

    $scope.mapChildEntities = function (node) {
        var nodePlusProperties = node;
        nodePlusProperties.selected = false;
        nodePlusProperties.opened = false;

        if (node.ChildsEntities != undefined && node.ChildsEntities.length > 0) {

            node.ChildsEntities.forEach(function (childNode) {
                childNode.parentNode = node;
            });

            node.ChildsEntities.forEach($scope.mapChildEntities);
        }

        return nodePlusProperties;
    };

    $scope.LoadCountersInNodes = function () {

        treeViewServices.getTaskCount().then(function (result) {
            $scope.ResultsNodeCounts = result.data;
            $scope.ChildsEntities.forEach($scope.fillCounterInTreeviewNode);
            $scope.$applyAsync();
        });

    };

    $scope.fillCounterInTreeviewNode = function (node) {

        $scope.ResultsNodeCounts.forEach(function (nodeCountDTO) {

            if (node.ID == nodeCountDTO.ID &&
                nodeCountDTO.ObjecttypeId == node.ObjecttypeId) {

                node.TasksCount = nodeCountDTO.TotalCount

                if (node.ChildsEntities && node.ChildsEntities.length > 0) {
                    node.ChildsEntities.forEach($scope.fillCounterInTreeviewNode);
                }
                return;
            }
        });

    };

    $scope.LoadResultsNodesPanel = function () {
        //TODO: Que traiga el ultimo nodo seleccionado o si el stepid o stateid en el search estan cargados que busque esos. ML.

        if ($scope.Search.lastNodeSelected == undefined || $scope.Search.lastNodeSelected == null || $scope.Search.lastNodeSelected == '')
            $scope.Search.lastNodeSelected = treeViewServices.GetLastWFSelected("MyProcess");
        if ($scope.Search.lastNodeSelected == null || $scope.Search.lastNodeSelected == undefined || $scope.Search.lastNodeSelected == '') {
            $scope.Search.lastNodeSelected = $scope.ChildsEntities[0].ID + "-" + $scope.ChildsEntities[0].ChildsEntities[0].ID;
        }
        var nodeIDs = $scope.Search.lastNodeSelected.split('-');


        var nodeId = parseInt(nodeIDs[nodeIDs.length - 1]);

        $scope.findNodeByLastSelectedNodeId(nodeId, $scope.ChildsEntities);


        var nodeFounded = $scope.getNodeById(nodeId);

        if (nodeFounded.length > 0) {
            nodeFounded = nodeFounded[0];
            if (nodeFounded.parentNode != undefined) {
                var StateId = 0;
                var StepId = 0;

                if (nodeFounded.parentNode.parentNode != undefined) {
                    StateId = nodeFounded.ID;
                    StepId = nodeFounded.parentNode.ID;
                }
                else {
                    StepId = nodeFounded.ID;
                }

                $scope.callDoSearch(StepId, StateId, nodeFounded, nodeFounded.parentNode);
            }
        }

    };

    $scope.LoadResultsNodesPanelWithCache = function () {
        //TODO: Que traiga el ultimo nodo seleccionado o si el stepid o stateid en el search estan cargados que busque esos. ML.
        if ($scope.Search.lastNodeSelected == undefined && $scope.Search.lastNodeSelected == null)
            $scope.Search.lastNodeSelected = treeViewServices.GetLastWFSelected("MyProcess");

        var nodeIDs = $scope.Search.lastNodeSelected.split('-');
        var nodeId = parseInt(nodeIDs[nodeIDs.length - 1]);

        $scope.findNodeByLastSelectedNodeId(nodeId, $scope.ChildsEntities);

        var nodeFounded = $scope.getNodeById(nodeId);
        if (nodeFounded.length > 0) {
            nodeFounded = nodeFounded[0];
            if (nodeFounded.parentNode != undefined) {
                var StateId = 0;
                var StepId = 0;

                if (nodeFounded.parentNode.parentNode != undefined) {
                    StateId = nodeFounded.ID;
                    StepId = nodeFounded.parentNode.ID;
                }
                else {
                    StepId = nodeFounded.ID;
                }

                $scope.NocallDoSearch(StepId, StateId, nodeFounded, nodeFounded.parentNode);
            }
        }
    };

    $scope.findNodeIntoChildsEntitiesByNodeId = function (nodeID) {

        $scope.ChildsEntities.forEach(function (childNode) {
            $scope.setSelectedNodeStylesByNodeID(childNode, nodeID);
        });
    };

    $scope.setSelectedNodeStylesByNodeID = function (childNode, nodeID) {
        if (childNode.ID == nodeID) {
            childNode.selected = true;
            $scope.$applyAsync();

        } else {
            if (childNode.ChildsEntities != undefined) {
                childNode.ChildsEntities.forEach(function (innerChildNode) {
                    $scope.setSelectedNodeStylesByNodeID(innerChildNode, nodeID)
                })
            }
        }
    }

    $scope.saveLastSelectedTreeviewNode = function (node) {
        var compositeKeyArray = [];
        var compositeKeyString = "";
        compositeKeyArray = $scope.generateCompositeKeyArray(node, compositeKeyArray);
        compositeKeyString = $scope.generateCompositeKeyString(compositeKeyArray);
        treeViewServices.SetLastWFSelected(compositeKeyString, null, "MyProcess");
    };

    $scope.generateCompositeKeyArray = function (node, compositeKeyArray) {

        if (node.parentNode != undefined)
            $scope.generateCompositeKeyArray(node.parentNode, compositeKeyArray)

        compositeKeyArray.push(node.ID)

        return compositeKeyArray;
    };

    $scope.generateCompositeKeyString = function (compositeKeyArray) {

        var compositeKeyString = "";
        compositeKeyArray.forEach(function (ID) {
            compositeKeyString += ID + "-";
        });
        if (compositeKeyArray.length > 0)
            compositeKeyString = compositeKeyString.substring(0, compositeKeyString.length - 1)

        return compositeKeyString;

    };

    //Refresh WFTree
    $scope.RefreshZambaTreeview = function () {
        if ($scope.Search.lastNodeSelected == undefined || $scope.Search.lastNodeSelected == null || $scope.Search.lastNodeSelected == '')
            $scope.Search.lastNodeSelected = treeViewServices.GetLastWFSelected("MyProcess");

        var nodeIDs = $scope.Search.lastNodeSelected.split('-');
        var nodeId = parseInt(nodeIDs[nodeIDs.length - 1]);

        $scope.LoadWFTreeResults(nodeId, false);

    };

    $scope.getNodeById = function (nodeID) {
        var nodeFounded = [];
        $scope.ChildsEntities.filter(function (node) {
            if (node.ID == nodeID)
                nodeFounded.push(node);
            if (node.ChildsEntities != undefined) {
                node.ChildsEntities.filter(function (childNode) {
                    if (childNode.ID == nodeID)
                        nodeFounded.push(childNode);

                    childNode.ChildsEntities.filter(function (innerChildNode) {
                        if (innerChildNode.ID == nodeID)
                            nodeFounded.push(innerChildNode);
                    });
                });
            }
        });

        return nodeFounded
    };

    $scope.findNodeByLastSelectedNodeId = function (nodeId, childsEntities) {

        childsEntities.forEach(function (childNode) {
            if (childNode.ID == nodeId) {

                let lastNodeSelected = childNode;

                childNode.selected = true;
                childNode.opened = true;

                if (childNode.parentNode != undefined) {
                    childNode.parentNode.opened = true;
                    if (childNode.parentNode.parentNode != undefined)
                        childNode.parentNode.parentNode.opened = true;
                }
            }
            if (childNode.ChildsEntities != undefined)
                $scope.findNodeByLastSelectedNodeId(nodeId, childNode.ChildsEntities);

        });

    };

    $scope.findNodeById = function (nodeId, childsEntities, nodesFounded) {
        childsEntities.forEach(function (childNode) {
            if (childNode.ID == nodeId) {
                nodesFounded.push(childNode);
            }
            if (nodesFounded.length == 0) {
                if (childNode.ChildsEntities != undefined)
                    $scope.findNodeById(nodeId, childNode.ChildsEntities, nodesFounded);
            }
        });
    };
    $scope.executeButtonAction = function (RuleId) {
        var currentItem = $scope.getActionTaskTreeByRuleId(RuleId);
        var scope_taskController = angular.element($("#taskController")).scope();
        scope_taskController.executeAcctionGrid(currentItem);
    }

    $scope.executeActionTaskTree = function () {
        debugger;
        let currentItem = this.item;
        if (currentItem.RuleId == 0)
            return;
        var scope_taskController = angular.element($("#taskController")).scope();
        scope_taskController.executeAcctionGrid(currentItem);
    }
    $scope.getActionTaskTreeByRuleId = function (RuleId) {
        for (var value of $scope.UserActionsForTaskTree) {
            if (value.RuleId == RuleId)
                return value;
        }

    }
    $scope.clearSelectedNode = function () {
        $scope.lastSelectedNode = {};
        //Quita los seleccionados
        $('.z-selectable-item').removeClass('selected-node-button-backcolor');
        $('.z-selectable-item-inner-content').removeClass('selected-node-text-color');

        $('.z-treeview-innerPanel').css('display', 'none');
        $('.z-tree-icon').text('arrow_right');

        // var scope_searchController = angular.element($("#ResultsCtrl")).scope();
        $scope.callDoSearchFromTreeviewSelectedNode(0, 0);
        $scope.selectedStepStateName = '';
        // $scope.selectedStepStateName = '';


    };

    $scope.showInnerNodesPanel = function (ID, iconClicked, node) {
        if (iconClicked == undefined) {
        } else {
            node.opened = !node.opened;
        }
        setTimeout(function () {
            $scope.setSelectedNodeStyles(node);
            $scope.saveLastSelectedTreeviewNode(node);
        }, 500);

    };


    $scope.setSelectedNodeStyles = function (node) {

        $scope.deselectNodes($scope.ChildsEntities);
        //aplica css del ng-class
        if (node != undefined) {
            node.selected = true;
        }
    };

    $scope.deselectNodes = function (ChildsEntities) {
        ChildsEntities = ChildsEntities.map(function (node) {
            node.selected = false;
            if (node.ChildsEntities != undefined &&
                node.ChildsEntities.length > 0) {
                $scope.deselectNodes(node.ChildsEntities);
            }
            return node;
        });
    };


    $scope.callDoSearch = function (StepId, StateId, node, parentNode) {
        try {

            $scope.Search.lastNodeSelected = treeViewServices.GetLastWFSelected("MyProcess");
            $scope.callDoSearchFromTreeviewSelectedNode(StepId, StateId);

            if (StateId == 0)
                $scope.selectedStepStateName = 'Etapa: ' + node.Nombre;
            else
                $scope.selectedStepStateName = 'Etapa: ' + parentNode.Nombre + ' >  Estado: ' + node.Nombre;

            $scope.handleLoadStepState();

        } catch (e) {
            console.error(e);
        }
    }

    $scope.NocallDoSearch = function (StepId, StateId, node, parentNode) {
        if ($scope.lastSelectedNode != node) {
            $scope.lastSelectedNode = node;

            if (StateId == 0)
                $scope.selectedStepStateName = 'Etapa: ' + node.Nombre;
            else
                $scope.selectedStepStateName = 'Etapa: ' + parentNode.Nombre + ' >  Estado: ' + node.Nombre;
        }

        $scope.handleLoadStepState();
    }

    $scope.handleLoadStepState = function () {
        $scope.$emit('LoadStep', { StepStateName: $scope.selectedStepStateName });
    };
});

app.directive('zambaTreeview', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.OpenWFSideBar();
            $scope.LoadWFTreeResults(undefined, true);
            $scope.GetButtonsTasksTree();
            if ($scope.ExecuteDoSearchFromLocal != undefined) {
                $scope.LoadResultsNodesPanelWithCache();
            } else {
                $scope.LoadResultsNodesPanel();
            }

        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/TreeView/treeView.html?v=248'),

    }
});
