package com.kendoui.spring.controllers.dialog;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("dialog-events-controller")
@RequestMapping(value="/dialog/")
public class EventsController {    
    
    @RequestMapping(value = "/events", method = RequestMethod.GET)
    public String index() {
        return "dialog/events";
    }
}

