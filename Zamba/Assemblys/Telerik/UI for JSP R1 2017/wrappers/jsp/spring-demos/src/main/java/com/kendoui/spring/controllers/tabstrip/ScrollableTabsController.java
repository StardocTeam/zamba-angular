package com.kendoui.spring.controllers.tabstrip;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("tabstrip-scrollabletabs-controller")
@RequestMapping(value="/tabstrip/")
public class ScrollableTabsController {
    
    @RequestMapping(value = "/scrollable-tabs", method = RequestMethod.GET)
    public String index() {       
        return "tabstrip/scrollable-tabs";
    }    
}