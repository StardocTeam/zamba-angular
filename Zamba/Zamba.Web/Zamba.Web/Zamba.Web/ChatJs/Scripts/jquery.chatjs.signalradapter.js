﻿function SignalRAdapter() {
    /// <summary>
    /// SignalR adapter for ChatJS. In order to use this adapter.. Pass an instance of this 
    /// function to $.chat()
    /// </summary>
}

var errorSignalRConnection = false;
var errorSignalRConnectionExt = false;
 
SignalRAdapter.prototype = {
    init: function (chat, done) {
        /// <summary>This function will be called by ChatJs to initialize the adapter</summary>
        /// <param FullName="chat" type="Object"></param>
        var _this = this;
          
        //jQuery.extend(true, {}, $.connection.hub); //
        var conL = $.connection.hub;
        conL.url = URLServer + "signalr";
        conL.qs = "UserId=" + thisUserId;
        conL.proxies.chathub = $.connection.inCH; // $.connection.chatHub;
        conL.stateChanged(function (change) {
            if (change.BCHistorytate === $.signalR.connectionState.reconnecting) {
                console.error("Error de conexion local, reconectando: " + (new Date()).toString());

                if (!errorSignalRConnection) {
                    errorSignalRConnection = true;
                    bootbox.alert("Reconectando... Error al conectarse a servidor local").css("z-index", 99999999999999);
                }
                $(".chat-window-content").fadeOut();
                setTimeout(function () {
                    conL.start();
                }, 1000);
            }
            else if (change.BCHistorytate === $.signalR.connectionState.connected) {
                bootbox.hideAll();
                console.warn("Conexion interna exitosa: " + (new Date()).toString());
                $(".chat-window-content").fadeIn();
            }
        });

        if ($.connection.exCH == undefined || colServer == "") {
            console.warn("Verificar conexion con hub externo, server: " + colServer);
        }
        else {
            var conE = $.connection.exCH.connection;
            conE.url = zCollServer + "signalr";
            conE.qs = "UserId=" + (myUserChatInfoCol == null ? 0 : myUserChatInfoCol.Id);//Nunca ingreso
            conE.proxies.chathub = $.connection.exCH;
            conE.stateChanged(function (change) {
                if (change.BCHistorytate === $.signalR.connectionState.reconnecting) {
                    console.warn("Error de conexion externa, reconectando: " + (new Date()).toString());
                    if (!errorSignalRConnectionExt) {
                        errorSignalRConnectionExt = true;
                        bootbox.alert("Reconectando... Error al conectarse a servidor externo").css("z-index", 99999999999999);
                    }
                    $(".chat-window-content").fadeOut();
                    setTimeout(function () {
                        conE.start();
                    }, 1000);
                }
                else if (change.BCHistorytate === $.signalR.connectionState.connected) {
                    bootbox.hideAll();
                    console.info("Conexion externa exitosa " + (new Date()).toString());
                    $(".chat-window-content").fadeIn();
                }
            });

            conE.disconnected(function () {
                setTimeout(function () {
                    conE.url = zCollServer + "signalr";
                    conE.qs = "UserId=" + (myUserChatInfoCol == null ? 0 : myUserChatInfoCol.Id);
                    conE.start();
                }, 5000); // Restart connection after 5 seconds.
            });

            var conExt = $.connection.exCH;// conE.createHubProxy('externalchathub');
            if (!window.hubExternalReady) {
                conE.url = zCollServer + "signalr";
                conE.qs = "UserId=" + (myUserChatInfoCol == null ? 0 : myUserChatInfoCol.Id);//Nunca ingreso
                window.hubExternalReady = conE.start({
                    transport: ['websockets', 'longPolling'], jsonp: true, xdomain: true
                });
            }
            window.hubExternalReady.done(function () {
                // done();
            });

            _this.hubExternal = conExt;
            _this.serverExternal = new Object();
            _this.serverExternal.getUsersList = function (done) {
                _this.hubExternal.server.getUsersList().done(function (result) {
                    if (done)
                        done(result);
                });
            };
            _this.serverExternal.sendMsgToUsers = function (usersIds, chatId, messageText, isGroup, clientGuid, done) {
                _this.hubExternal.server.sendMsgToUsers(usersIds, chatId, messageText, isGroup, clientGuid).done(function (result) {
                    if (done)
                        done(result);
                });
            };
            var lstExtMsg = new Object();
            _this.hubExternal.client.sendMsgToUsers = function (message) {
                message.isExternal = true;
                if (lstExtMsg.LM != message.LastMessage || (lstExtMsg.LM == message.LastMessage && lstExtMsg.Id != message.Id))
                    chat.client.sendMsgToUsers(message);
                lstExtMsg.Id = message.Id;
                lstExtMsg.LM = message.LastMessage;
            };
        }

        _this.hub = $.connection.inCH;
        if (_this.hub == undefined) _this.hub = $.connection.exCH;
        _this.hub.client.sendMessage = function (message) {
            chat.client.sendMessage(message);
        };
        _this.hub.client.changeStatus = function (response) {
            chat.client.changeStatus(response);
        };
        _this.hub.client.changeName = function (response) {
            chat.client.changeName(response);
        };
        _this.hub.client.changeGroupName = function (response) {
            chat.client.changeGroupName(response);
        };
        _this.hub.client.changeAvatar = function (response) {
            chat.client.changeAvatar(response);
        };
        var lstMsg = new Object();
        _this.hub.client.sendMsgToUsers = function (message) {
            //Validacion que el mensaje no sea el mismo, se enviaba dos veces.
            if (lstMsg.LM != message.LastMessage || (lstMsg.LM == message.LastMessage && lstMsg.Id != message.Id))
                chat.client.sendMsgToUsers(message);
            lstMsg.Id = message.Id;
            lstMsg.LM = message.LastMessage;
        };
        _this.hub.client.sendTypingSignal = function (otherUserId) {
            chat.client.sendTypingSignal(otherUserId);
        };
        _this.hub.client.usersListChanged = function (usersList) {
            chat.client.usersListChanged(usersList);
        };
        conL.disconnected(function () {
            setTimeout(function () {
                conL.start();
            }, 5000); // Restart connection after 5 seconds.
        });

        var isChrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
        if (!window.hubReady) {
            window.hubReady = conL.start({
                transport: ['websockets', 'longPolling'], jsonp: true, xdomain: true
            });
        }
        window.hubReady.done(function () {
            // function passed by ChatJS to the adapter to be called when the adapter initialization is completed
            done();
        });

        // These are the methods that ARE CALLED BY THE CLIENT
        // Client functions should call these functions
        _this.server = new Object();

        _this.server.sendMessage = function (otherUserId, messageText, clientGuid, done) {
            /// <summary>Sends a message to server</summary>
            /// <param FullName="otherUserId" type="Number">The id of the user to which the message is being sent</param>
            /// <param FullName="messageText" type="String">Message text</param>
            /// <param FullName="clientGuid" type="String">Message client guid. Each message must have a client id in order for it to be recognized when it comes back from the server</param>
            /// <param FullName="done" type="Function">Function to be called when this method completes</param>
            _this.hub.server.sendMessage(otherUserId, messageText, clientGuid).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.changeStatus = function (userId, status, done) {
            _this.hub.server.changeStatus(userId, status).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.changeAvatar = function (userId, avatar, done) {
            _this.hub.server.changeAvatar(userId, avatar).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.changeName = function (userId, name, done) {
            _this.hub.server.changeName(userId, name).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.changeGroupName = function (userId, chatId, name, done) {
            _this.hub.server.changeGroupName(userId, chatId, name).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.sendMsgToUsers = function (usersIds, chatId, messageText, isGroup, clientGuid, done) {
            _this.hub.server.sendMsgToUsers(usersIds, chatId, messageText, isGroup, clientGuid).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.sendTypingSignal = function (otherUserId, done) {
            /// <summary>Sends a typing signal to the server</summary>
            /// <param FullName="otherUserId" type="Number">The id of the user to which the typing signal is being sent</param>
            /// <param FullName="done" type="Function">Function to be called when this method completes</param>
            _this.hub.server.sendTypingSignal(otherUserId).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.getMessageHistory = function (otherUserId, chatType, done) {
            _this.hub.server.getMessageHistory(otherUserId, chatType).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.getMsgHistoryGroup = function (usersIds, done) {
            _this.hub.server.getMsgHistoryGroup(usersIds).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.getUserInfo = function (otherUserId, done) {
            /// <summary>Gets information about the user</summary>
            /// <param FullName="otherUserId" type="Number">The id of the user from which you want the information</param>
            /// <param FullName="done" type="Function">FUnction to be called when this method completes</param>
            _this.hub.server.getUserInfo(otherUserId).done(function (result) {
                if (done)
                    done(result);
            });
        };
        _this.server.getUsersList = function (done) {
            /// <summary>Gets the list of the users in the current room</summary>
            /// <param FullName="otherUserId" type="Number">The id of the user from which you want the information</param>
            /// <param FullName="done" type="Function">FUnction to be called when this method completes</param>
            _this.hub.server.getUsersList().done(function (result) {
                if (done)
                    done(result);
            });
        };
    }
}