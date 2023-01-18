package com.kendoui.spring.controllers.mediaplayer;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("mediaplayer-navigation-controller")
@RequestMapping(value="/mediaplayer/")
public class KeyboardNavigationController {
    
    @RequestMapping(value = {"/keyboard-navigation"}, method = RequestMethod.GET)
    public String index() {       
        return "mediaplayer/keyboard-navigation";
    }    
}