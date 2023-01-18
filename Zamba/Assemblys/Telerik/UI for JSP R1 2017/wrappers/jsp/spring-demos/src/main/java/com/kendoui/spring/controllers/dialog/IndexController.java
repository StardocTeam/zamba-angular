package com.kendoui.spring.controllers.dialog;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("dialog-home-controller")
@RequestMapping(value="/dialog/")
public class IndexController {    
    
    @RequestMapping(value = {"/", "/index"}, method = RequestMethod.GET)
    public String index() {
        return "dialog/index";
    }
}

