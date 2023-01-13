package com.kendoui.spring.controllers.dialog;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("dialog-navigation-controller")
@RequestMapping(value="/dialog/")
public class KeyboardNavigationController {    
    
    @RequestMapping(value = "/keyboard-navigation", method = RequestMethod.GET)
    public String index() {
        return "dialog/keyboard-navigation";
    }
}