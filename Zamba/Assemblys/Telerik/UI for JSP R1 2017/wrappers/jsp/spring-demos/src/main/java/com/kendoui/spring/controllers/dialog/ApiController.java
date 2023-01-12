package com.kendoui.spring.controllers.dialog;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("dialog-api-controller")
@RequestMapping(value="/dialog/")
public class ApiController {    
    
    @RequestMapping(value = "/api", method = RequestMethod.GET)
    public String index() {
        return "dialog/api";
    }
}

