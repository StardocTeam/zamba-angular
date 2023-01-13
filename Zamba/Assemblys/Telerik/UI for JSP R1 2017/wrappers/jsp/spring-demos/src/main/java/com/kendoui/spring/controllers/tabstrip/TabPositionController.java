package com.kendoui.spring.controllers.tabstrip;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("tabstrip-tabposition-controller")
@RequestMapping(value="/tabstrip/")
public class TabPositionController {
    
    @RequestMapping(value = "/tab-position", method = RequestMethod.GET)
    public String index() {       
        return "tabstrip/tab-position";
    }    
}