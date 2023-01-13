window.onload = function () {    
    var pss = document.querySelectorAll("p");
    pss.forEach(function (node, index) {
        if (node.style.marginLeft < '0px')
            node.style.marginLeft = '0px';
    });

};