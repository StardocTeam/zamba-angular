package com.kendoui.spring.controllers.treeview;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("treeview-filter-controller")
@RequestMapping(value="/treeview/")
public class FilterController {
    
    @RequestMapping(value = "/filter-treeview-in-dialog", method = RequestMethod.GET)
    public String index() {
        return "treeview/filter";
    }
}

