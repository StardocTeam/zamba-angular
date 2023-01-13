package com.kendoui.spring.controllers.dialog;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("dialog-rtl-controller")
@RequestMapping(value="/dialog/")
public class RightToLeftSupportController {    
    
    @RequestMapping(value = "/right-to-left-support", method = RequestMethod.GET)
    public String index() {
        return "dialog/right-to-left-support";
    }
}

