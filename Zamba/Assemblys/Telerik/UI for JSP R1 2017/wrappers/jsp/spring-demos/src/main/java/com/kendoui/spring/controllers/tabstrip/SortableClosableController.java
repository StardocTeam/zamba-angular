package com.kendoui.spring.controllers.tabstrip;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("tabstrip-sortable-closable-controller")
@RequestMapping(value="/tabstrip/")
public class SortableClosableController {
    
    @RequestMapping(value = "/sortable-closable", method = RequestMethod.GET)
    public String index() {       
        return "tabstrip/sortable-closable";
    }    
}