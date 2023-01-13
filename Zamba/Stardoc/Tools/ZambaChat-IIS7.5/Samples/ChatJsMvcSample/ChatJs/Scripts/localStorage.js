var chatGroups = {
    n: "groups",
    getLS: function () {
        var ls = localStorage.getItem(this.n);
        return (validateLS(ls)) ? JSON.parse(ls) : new Array();
    },
    UpdateLastMessage: function (groupId) {
        var gs = this.getLS();
        for (var i = 0; i <= gs.length - 1; i++) {
            if (gs[i].ChatId == groupId) {
                gs[i].LastMessage = new Date().toString();
                this.Remove(groupId);
                this.Add(gs[i]);
                break;
            }
        }
    },
    Add: function (group) {
        var gs = this.getLS();
        gs.push(group);
        localStorage.setItem(this.n, JSON.stringify(gs));
    },
    Set: function (groups) {
        var gs = JSON.stringify(groups);
        localStorage.setItem(this.n, gs);
    },
    Get: function () {
        return this.getLS();
    },
    Remove: function (id) {
        var gs = this.getLS();
        for (var i = 0; i <= gs.length - 1; i++) {
            if (gs[i].ChatId == id) {
                gs.splice(i, 1);
                localStorage.setItem(this.n, JSON.stringify(gs));
                break;
            }
        }
    },
    Clear: function () {
        localStorage.setItem(this.n, new Array());
    },
};

var myUserInfoZamba = {
    n: "myUserZambaInfo",
    getLS: function () {
        var ls = localStorage.getItem(this.n);
        if (validateLS(ls) && JSON.parse(ls).ID == thisUserId) return JSON.parse(ls);
        else {
            var user = UserFromWS(thisUserId)[0];
            localStorage.setItem(this.n, JSON.stringify(user));
            return user;
        };
    },
    Set: function (info) {
        var gs = JSON.stringify(info);
        localStorage.setItem(this.n, gs);
    },
    Get: function () {
        return this.getLS();
    },
    Clear: function () {
        localStorage.setItem(this.n, new Array());
    },
    Update: function (key, value) {
        var ls = this.Get();
        for (var k in ls) {
            if (ls.hasOwnProperty(k) && k.toString() == key) {
                ls[k] = value;
                break;
            }
        }
        localStorage.setItem(this.n, JSON.stringify(ls));
    },
};

var myUserInfoChat = {
    n: "myUserInfo",
    getLS: function () {
        var ls = localStorage.getItem(this.n);
        if (validateLS(ls) && JSON.parse(ls).Id == thisUserId) return JSON.parse(ls);
        else {
            var user = (GetChatUser(thisUserId));
            localStorage.setItem(this.n, JSON.stringify(user));
            localStorage.setItem("userId", user.Id == thisUserId ? user.Id : "");
            return user;
        };
    },
    Set: function (info) {
        var gs = JSON.stringify(info);
        localStorage.setItem(this.n, gs);
    },
    Get: function () {

        return this.getLS();
    },
    Update: function (key, value) {
        var ls = this.Get();
        for (var k in ls) {
            if (ls.hasOwnProperty(k) && k.toString() == key) {
                ls[k] = value;
                break;
            }
        }
        localStorage.setItem(this.n, JSON.stringify(ls));
    },
    Clear: function () {
        localStorage.setItem(this.n, new Array());
    },
};

var myUserInfoExt = {
    n: "myUserInfoExt",
    getLS: function () {
        var ls = localStorage.getItem(this.n);
        if (validateLS(ls)) return JSON.parse(ls);
        else {
            var user = GetChatUserCollaboration(myUserInfo.CORREO);
            localStorage.setItem(this.n, JSON.stringify(user));
            return user;
        };
    },
    Set: function (info) {
        var gs = JSON.stringify(info);
        localStorage.setItem(this.n, gs);
    },
    Get: function () {
        return this.getLS();
    },
    Update: function (key, value) {
        var ls = this.Get();
        for (var k in ls) {
            if (ls.hasOwnProperty(k) && k.toString() == key) {
                ls[k] = value;
                break;
            }
        }
        localStorage.setItem(this.n, JSON.stringify(ls));
    },
    Clear: function () {
        localStorage.setItem(this.n, new Array());
    },
};

var extUsers = {
    n: "extUsers",
    getLS: function () {
        var ls = localStorage.getItem(this.n);
        if (validateLS(ls)) return JSON.parse(ls);
        else {
            var users = GetExtChat();
            localStorage.setItem(this.n, JSON.stringify(users));
            return users;
        };
    },
    Add: function (group) {
        var gs = getLS();
        gs.push(group);
        localStorage.setItem(this.n, JSON.stringify(gs));
    },
    Set: function (groups) {
        var gs = JSON.stringify(groups);
        localStorage.setItem(this.n, gs);
    },
    Get: function () {
        return this.getLS();
    },
    Remove: function (id) {
        var gs = getLS();
        for (var i = 0; i <= gs.length - 1; i++) {
            if (gs[i].ChatId == id) {
                gs.splice(i, 1);
                localStorage.setItem(this.n, JSON.stringify(gs));
                break;
            }
        }
    },
    Clear: function () {
        localStorage.setItem(this.n, new Array());
    },
};



function validateLS(ls) {
    return ls != undefined && ls != "" && ls != "[]" && ls != "null" && ls != "undefined";
}




var chatGroupsForum = {
    n: "groupsForum",
    getLS: function () {
        var ls = localStorage.getItem(this.n);
        return (validateLS(ls)) ? JSON.parse(ls) : new Array();
    },

    Add: function (group) {
        var gs = this.getLS();
        gs.push(group);
        localStorage.setItem(this.n, JSON.stringify(gs));
    },
    Set: function (groups) {
        var gs = JSON.stringify(groups);
        localStorage.setItem(this.n, gs);
    },
    Get: function () {
        return this.getLS();
    },
    Remove: function (id) {
        var gs = this.getLS();
        for (var i = 0; i <= gs.length - 1; i++) {
            if (gs[i].ChatId == id) {
                gs.splice(i, 1);
                localStorage.setItem(n, JSON.stringify(gs));
                break;
            }
        }
    },
    Clear: function () {
        localStorage.setItem(this.n, new Array());
    },
};